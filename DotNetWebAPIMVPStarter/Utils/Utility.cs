using DotNetWebAPIMVPStarter.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Slugify;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DotNetWebAPIMVPStarter.Utils
{
    public static class Utility
    {
        static IOptions<JwtConfigSettings> JwtSettings;
        private static JwtConfigSettings _jwtConfig = JwtSettings.Value;
        public static string GenerateJwtToken(User user)
        {
            JwtSecurityTokenHandler TokenHandler = new JwtSecurityTokenHandler();
            byte[] SigningKey = Encoding.ASCII.GetBytes(_jwtConfig.IssuerSigningKey); //why ascii

            SecurityTokenDescriptor TokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                     //new Claim("id", user.Id.ToString()),
                     new Claim(ClaimTypes.Name, user.Id.ToString()),
                     new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddMinutes(300),
                //Issuer = _jwtConfig.Issuer,
                //Audience = _jwtConfig.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(SigningKey), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken Token = TokenHandler.CreateToken(TokenDescriptor);
            string TokenOutput = TokenHandler.WriteToken(Token);

            return TokenOutput;


        }

        public static string Slugify(this string TextToSlugify)
        {
            if (string.IsNullOrWhiteSpace(TextToSlugify)) return null;
            try
            {
                SlugHelper Helper = new SlugHelper();
                string ResultString = Helper.GenerateSlug(TextToSlugify);
                    //TextToSlugify.RemoveAccents().ToLower();
                ////invalid Chars
                //ResultString = Regex.Replace(ResultString, @"[^a-z0-9\s-]", "");
                ////shrink multiple spaces into one space
                //ResultString = Regex.Replace(ResultString, @"\s+", " ").Trim();
                ////cut and Trim
                //ResultString = ResultString.Substring(0, ResultString.Length <= 45 ? ResultString.Length : 45).Trim();
                ////replace spaces with hypens
                //ResultString = Regex.Replace(ResultString, @"\s", "-");
                ////
                return ResultString;

            }
            catch (Exception ex)
            {
                return null;    
                Console.Write(ex.Message);
            }




        }

        public static string RemoveAccents(this string Text)
        {
            if (string.IsNullOrWhiteSpace(Text)) return null;

            byte[] Bytes = Encoding.GetEncoding("Cyrillic").GetBytes(Text);
            return Encoding.ASCII.GetString(Bytes);
        }
    }
}
