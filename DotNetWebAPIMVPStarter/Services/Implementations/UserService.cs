using DotNetWebAPIMVPStarter.DAL;
using DotNetWebAPIMVPStarter.Models;
using DotNetWebAPIMVPStarter.Services.Interfaces;
using DotNetWebAPIMVPStarter.Utils;
using DotNetWebAPIMVPStarter.Utils.Exceptions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Stripe;
using DotNetWebAPIMVPStarter.Utils.Config;
using DotNetWebAPIMVPStarter.Models.Stripe;
using DotNetWebAPIMVPStarter.Models.Role;

namespace DotNetWebAPIMVPStarter.Services.Implementations
{
    public class UserService : IUserService
    {
        private DataContext _context;
        private JwtConfigSettings _jwtConfig;
        private readonly IEmailSender _emailSender;
        private StripeConfig _stripeSettings;
        public UserService(DataContext context, IOptions<JwtConfigSettings> jwtConfig, IEmailSender emailSender, IOptions<StripeConfig> stripeSettings)
        {
            _context = context;
            _jwtConfig = jwtConfig.Value;
            _emailSender = emailSender;
            _stripeSettings = stripeSettings.Value;
            StripeConfiguration.ApiKey = _stripeSettings.ApiKey;

        }

        public User Authenticate(string Email, string Password)
        {
            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
                return null; 

            //try fetch a user with that authEmail
            User user = _context.Users.SingleOrDefault(x => x.Email == Email);
            //does email exist?
            if (user == null)
                return null;
            //Ok now we have a match...
            //let's verify password...is correct also
            if (!VerifyPasswordHash(Password, user.PasswordHash, user.PasswordSalt))
                return null;

            //if we get here then, auth is successful, just return user
            return user;
        }

        private static bool VerifyPasswordHash(string Password, byte[] StoredPasswordHash, byte[] StoredPasswordSalt)
        {
            if (Password == null) throw new ArgumentNullException("Password");
            if (string.IsNullOrWhiteSpace(Password)) throw new ArgumentException("Value cannot be empty or whitespace, only string.", "Password");
            if (StoredPasswordHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected). ", "StoredPasswordHash");
            if (StoredPasswordSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "StoredPasswordSalt");

            //all validations pass
            using(HMACSHA512 hmac = new HMACSHA512(StoredPasswordSalt))
            {
                byte[] ComputedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(Password));
                for (int i = 0; i < ComputedHash.Length; i++)
                {
                    if (ComputedHash[i] != StoredPasswordHash[i]) return false;
                }
            }

            //if we get here, hashes match
            return true;

        }

        public User CreateUser(User user, string Password, string RepeatPassword)
        {
            if (string.IsNullOrWhiteSpace(Password)) throw new AppException("Password");
            if (_context.Users.Any(x => x.Email == user.Email)) throw new AppException($"Email {user.Email} already exists");
            if (_context.Users.Any(x => x.Username == user.Username)) throw new AppException($"Username {user.Username} already exists");

            if (!Password.Equals(RepeatPassword)) throw new AppException("Passwords do not match"); //this is actually not necessary since we are susing validations on the models

            byte[] PassHash, PassSalt;
            CreatePasswordHash(Password, out PassHash, out PassSalt);

            user.PasswordHash = PassHash;
            user.PasswordSalt = PassSalt;
            string Token = GenerateJwtToken(user);
            user.VerificationToken = Token;
            user.DateCreated = DateTime.UtcNow;
            user.Role = Role.User;
            //try to create a Stripe Customer Profile for this User
            //create address object 
            //Dictionary<string, object> Address = new Dictionary<string, object>
            //    {
            //        { "city", user.City },
            //        { "country", user.Country },
            //        { "line1", user.AddressLine1 },
            //        { "line2", user.AddressLine2 },
            //        { "postal_code", user.PostalZipCode },
            //        { "state", user.StateRegion }

            //    };
            Stripe.Customer stripeCreateCustomerResponse;
            try
            {
                var options = new CustomerCreateOptions
                {
                    Address = new AddressOptions
                    {
                        City = user.City,
                        Country = user.Country,
                        Line1 = user.AddressLine1,
                        Line2 = user.AddressLine2,
                        PostalCode = user.PostalZipCode,
                        State = user.StateRegion
                    },
                    Description = "This is my very first Created Customer on Stripe in .NET Core",
                    Email = user.Email,
                    Name = $"{user.FirstName} {user.LastName}",
                    Phone = user.PhoneNumber,
                    Shipping = new ShippingOptions
                    {
                        Address = new AddressOptions
                        {
                            City = user.City,
                            Country = user.Country,
                            Line1 = user.AddressLine1,
                            Line2 = user.AddressLine2,
                            PostalCode = user.PostalZipCode,
                            State = user.StateRegion
                        },
                        Name = $"{user.FirstName} {user.LastName}",
                        Phone = user.PhoneNumber
                    }

                };
                var service = new CustomerService();
                stripeCreateCustomerResponse = service.Create(options);
                user.StripeCustomerId = stripeCreateCustomerResponse.Id;


            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                //Now create our own localDb StripeCustomer//
                //I'm still not sure why we need this class
                StripeCustomer stripeCustomer = new StripeCustomer
                {
                    StripeCustomerId = user.StripeCustomerId,
                    User = user,
                    DateCreated = DateTime.UtcNow,
                    DateUpdated = DateTime.UtcNow
                };

                _context.StripeCustomers.Add(stripeCustomer);
                _context.Users.Add(user);

                _context.SaveChanges();


            }
            return user;


        }

        public string GenerateJwtToken(User user)
        {
            JwtSecurityTokenHandler TokenHandler = new JwtSecurityTokenHandler();
            byte[] SigningKey = Encoding.ASCII.GetBytes(_jwtConfig.IssuerSigningKey); //why ascii

            SecurityTokenDescriptor TokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                     new Claim("id", user.Id.ToString()),
                     new Claim(ClaimTypes.Name, user.Username)
                }),
                Expires = DateTime.UtcNow.AddMinutes(300),
                Issuer = _jwtConfig.Issuer,
                Audience = _jwtConfig.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(SigningKey), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken Token = TokenHandler.CreateToken(TokenDescriptor);
            string TokenOutput = TokenHandler.WriteToken(Token);

            return TokenOutput;


        }

        private static void CreatePasswordHash(string Password, out byte[] PasswordHash, out byte[] PasswordSalt)
        {
            if (Password == null) throw new ArgumentNullException("Password");
            if (string.IsNullOrWhiteSpace(Password)) throw new ArgumentException("Value cannot be empty or whitespace, only string.", "Password");

            using(HMACSHA512 hmac = new HMACSHA512())
            {
                PasswordSalt = hmac.Key;
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(Password));
            }
        }

        public void DeleteUser(int Id)
        {
            User user = _context.Users.Find(Id);
            if(user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }

        public void ForgotPassword(ForgotPasswordRequest model, string Origin)
        {
            User Account = _context.Users.SingleOrDefault(x => x.Email == model.Email);
            //always return Ok to prevent Email enumeration
            if (Account == null) return;

            //create reset token if a user with that email exists
            //it expires after 1 day
            Account.ResetToken = GenerateRandomTokenString();
            Account.ResetTokenExpires = DateTime.UtcNow.AddDays(1);

            _context.Users.Update(Account);
            _context.SaveChanges();

            //send email
            _emailSender.SendPasswordResetEmail(Account, Origin);
        }
        private string GenerateRandomTokenString()
        {
            RNGCryptoServiceProvider RngCrypto = new RNGCryptoServiceProvider();
            byte[] RandomBytes = new byte[40];
            RngCrypto.GetBytes(RandomBytes);
            //convert random bytes to hex string
            return BitConverter.ToString(RandomBytes).Replace("-", "");
        }


        public IEnumerable<User> GetAllUsers()
        {
            return _context.Users;
        }

        public User GetUserById(int Id)
        {
            User user = _context.Users.Where(x => x.Id == Id).FirstOrDefault();
            return user;
        }

        public void ResetPassword(ResetPasswordRequest model)
        {
            User Account = _context.Users.SingleOrDefault(x => x.ResetToken == model.Token && x.ResetTokenExpires > DateTime.UtcNow);

            if (Account == null) throw new AppException("User does not exist");

            byte[] PassHash, PassSalt;
            CreatePasswordHash(model.Password, out PassHash, out PassSalt);
            Account.PasswordHash = PassHash;
            Account.PasswordSalt = PassSalt;
            Account.PasswordReset = DateTime.UtcNow;
            Account.ResetToken = null;
            Account.ResetTokenExpires = null;

            _context.Users.Update(Account);
            _context.SaveChanges();


        }

        public void UpdateUser(User user, string Password = null)
        {
            User TargetUser = _context.Users.Find(user.Id);
            if (TargetUser == null) throw new AppException("User not found");
            //update if params are supplied in User::user
            if(!string.IsNullOrWhiteSpace(user.Email) && user.Email != user.Email)
            {
                //throw error if email is taken already...
                if (_context.Users.Any(x => x.Email == user.Email)) throw new AppException($"Email {user.Email} is already taken");
                TargetUser.Email = user.Email;
            }
            if(!string.IsNullOrWhiteSpace(user.Username) && user.Username != user.Username)
            {
                //throw error if the email is taken...
                if (_context.Users.Any(x => x.Username == user.Username)) throw new AppException($"Username {user.Username} is already taken");
                TargetUser.Username = user.Username;
            }

            //update other fields...
            if (!string.IsNullOrWhiteSpace(user.FirstName))
                TargetUser.FirstName = user.FirstName;
            if (!string.IsNullOrWhiteSpace(user.LastName))
                TargetUser.LastName = user.LastName;
            if (!string.IsNullOrWhiteSpace(user.OtherNames))
                TargetUser.OtherNames = user.OtherNames;
            if (!string.IsNullOrWhiteSpace(user.AddressLine1))
                TargetUser.AddressLine1 = user.AddressLine1;
            if (!string.IsNullOrWhiteSpace(user.AddressLine2))
                TargetUser.AddressLine2 = user.AddressLine2;
            if (!string.IsNullOrWhiteSpace(user.StateRegion))
                TargetUser.StateRegion = user.StateRegion;
            if (!string.IsNullOrWhiteSpace(user.PhoneNumber))
                TargetUser.PhoneNumber = user.PhoneNumber;
            if (!string.IsNullOrWhiteSpace(user.Country))
                TargetUser.Country = user.Country;
            if (!string.IsNullOrWhiteSpace(user.City))
                TargetUser.City = user.City;
            if (!string.IsNullOrWhiteSpace(user.PostalZipCode))
                TargetUser.PostalZipCode = user.PostalZipCode;
            //UpdatePassword if it's changed
            if (!string.IsNullOrWhiteSpace(Password))
            {
                byte[] PassHash, PassSalt;
                CreatePasswordHash(Password, out PassHash, out PassSalt);
                TargetUser.PasswordHash = PassHash;
                TargetUser.PasswordSalt = PassSalt;
            }
            TargetUser.DateLastUpdated = DateTime.UtcNow;
            //save / update changes to db
            _context.Users.Update(TargetUser);
            _context.SaveChanges();


        }

        public void ValidateResetToken(ValidateResetTokenRequest model)
        {
            User Account = _context.Users.SingleOrDefault(x => x.ResetToken == model.Token && x.ResetTokenExpires > DateTime.UtcNow);
            if (Account == null) throw new AppException("Invalid Token");
        }

        public void VerifyEmail(string Token)
        {
            User Account = _context.Users.SingleOrDefault(x => x.VerificationToken == Token);
            if (Account == null) throw new AppException("Verification Failed");
            Account.DateVerified = DateTime.UtcNow;
            Account.VerificationToken = null;

            _context.Users.Update(Account);
            _context.SaveChanges();
        }

        public User CreateAdminUser(User user, string Password, string RepeatPassword, string Role = "Admin")
        {
            if (string.IsNullOrWhiteSpace(Password)) throw new AppException("Password");
            if (_context.Users.Any(x => x.Email == user.Email)) throw new AppException($"Email {user.Email} already exists");
            if (_context.Users.Any(x => x.Username == user.Username)) throw new AppException($"Username {user.Username} already exists");

            if (!Password.Equals(RepeatPassword)) throw new AppException("Passwords do not match"); //this is actually not necessary since we are susing validations on the models

            byte[] PassHash, PassSalt;
            CreatePasswordHash(Password, out PassHash, out PassSalt);

            user.PasswordHash = PassHash;
            user.PasswordSalt = PassSalt;
            string Token = GenerateJwtToken(user);
            user.VerificationToken = Token;
            user.DateCreated = DateTime.UtcNow;
            user.Role = Role;
            
            Stripe.Customer stripeCreateCustomerResponse;
            try
            {
                var options = new CustomerCreateOptions
                {
                    Address = new AddressOptions
                    {
                        City = user.City,
                        Country = user.Country,
                        Line1 = user.AddressLine1,
                        Line2 = user.AddressLine2,
                        PostalCode = user.PostalZipCode,
                        State = user.StateRegion
                    },
                    Description = "This is my very first Created Customer on Stripe in .NET Core",
                    Email = user.Email,
                    Name = $"{user.FirstName} {user.LastName}",
                    Phone = user.PhoneNumber,
                    Shipping = new ShippingOptions
                    {
                        Address = new AddressOptions
                        {
                            City = user.City,
                            Country = user.Country,
                            Line1 = user.AddressLine1,
                            Line2 = user.AddressLine2,
                            PostalCode = user.PostalZipCode,
                            State = user.StateRegion
                        },
                        Name = $"{user.FirstName} {user.LastName}",
                        Phone = user.PhoneNumber
                    }

                };
                var service = new CustomerService();
                stripeCreateCustomerResponse = service.Create(options);
                user.StripeCustomerId = stripeCreateCustomerResponse.Id;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //Now create our own localDb StripeCustomer//
                //I'm still not sure why we need this class
                StripeCustomer stripeCustomer = new StripeCustomer
                {
                    StripeCustomerId = user.StripeCustomerId,
                    User = user,
                    DateCreated = DateTime.UtcNow,
                    DateUpdated = DateTime.UtcNow
                };
                _context.StripeCustomers.Add(stripeCustomer);
                _context.Users.Add(user);

                _context.SaveChanges();

            }
            return user;
        }
    }
}
