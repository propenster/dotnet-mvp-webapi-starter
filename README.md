# dotnet-mvp-webapi-starter
A .NET MVP Microservices Suite/Web API to get up and running building your prototype fast.

Table of Contents
-----------------

- [Features](#features)
- [Prerequisites](#prerequisites)
- [Getting Started](#getting-started)
- [Obtaining API Keys for Stripe, Paypal and SendGrid API's](#obtaining-api-keys)
- [Project Structure](#project-structure)
- [Dependencies](#list-of-packages)
- [FAQ](#faq)
- [How It Works](#how-it-works-mini-guides)
- [Changelog](#changelog)
- [Contributing](#contributing)
- [License](#license)

Features
--------

- **Local Authentication** using Email and Password
- **OAuth 1.0a Authentication** via Twitter
- **OAuth 2.0 Authentication** via Facebook, Google, GitHub, LinkedIn, Instagram
- Flash notifications
- MVC Project Structure
- Bootstrap 3
- Contact Form (powered by Mailgun, Sendgrid or Mandrill)
- **Account Management**
 - Gravatar
 - Profile Details
 - Change Password
 - Forgot Password
 - Reset Password
 - Delete Account
 - CSRF protection
 - **API Examples**: Facebook, Foursquare, Last.fm, Tumblr, Twitter, Stripe, LinkedIn and more.
- **Automatic Documentation**

Prerequisites
-------------

- [Mysql](https://www.mysql.com/) or [Postgresql](http://www.postgresql.org/)
- [PHP 5.4+](http://php.net/)
- Command Line Tools
 - <img src="http://deluge-torrent.org/images/apple-logo.gif" height="17">&nbsp;**Mac OS X:** [Xcode](https://itunes.apple.com/us/app/xcode/id497799835?mt=12) (or **OS X 10.9+**: `xcode-select --install`)
 - <img src="http://dc942d419843af05523b-ff74ae13537a01be6cfec5927837dcfe.r14.cf1.rackcdn.com/wp-content/uploads/windows-8-50x50.jpg" height="17">&nbsp;**Windows:** [Visual Studio](https://www.visualstudio.com/products/visual-studio-community-vs)
 - <img src="https://lh5.googleusercontent.com/-2YS1ceHWyys/AAAAAAAAAAI/AAAAAAAAAAc/0LCb_tsTvmU/s46-c-k/photo.jpg" height="17">&nbsp;**Ubuntu** / <img src="https://upload.wikimedia.org/wikipedia/commons/3/3f/Logo_Linux_Mint.png" height="17">&nbsp;**Linux Mint:** `sudo apt-get install build-essential`
 - <img src="http://i1-news.softpedia-static.com/images/extra/LINUX/small/slw218news1.png" height="17">&nbsp;**Fedora**: `sudo dnf groupinstall "Development Tools"`
 - <img src="https://en.opensuse.org/images/b/be/Logo-geeko_head.png" height="17">&nbsp;**OpenSUSE:** `sudo zypper install --type pattern devel_basis`
 - <img src="https://global-uploads.webflow.com/5ea1b599e88dc9edc465e8f5/5ea8b30dd43a0b44bbc91bd8_favicon-32x32.png" height="17">&nbsp;**Optic:** `npm install -g @useoptic/cli` (needed for automatic documentation)

**Note:** If you are new to Laravel, I recommend to watch
[Laravel From Scratch](https://laracasts.com/series/laravel-5-from-scratch) screencast by Jeffery Way that teaches Laravel 5 from scratch. Alternatively,
here is another great tutorial for building a project management app for beginners/intermediate developers - [How to build a project management app in Laravel 5](http://goodheads.io/2015/09/16/how-to-build-a-project-management-app-in-laravel-5-part-1/).

Getting Started
---------------

#### Via Cloning The Repository:

```bash
# Get the project
git clone https://github.com/propenster/dotnet-mvp-webapi-starter.git
# Change directory
cd dotnet-mvp-webapi-starter/DotNetWebAPIMVPStarter

# Update your config values e.g connection strings,apiKeys etc in appsettings.json
# Create a database (SQLServer)
# And update apappsettings.json with its connection string.


# Run Migrations at the Package Manager Console 
Add-Migration initialMigration 

# Update Database
Update-Database
# Build and Run the API for the first time. 

dotnet run
```

This starter pack includes the following APIs. You will need to obtain appropriate credentials like Client ID, zClient secret, API key, or Username & Password by going through each provider and generate new credentials.

* Cloudder
* Twitter
* Twillo
* Github
* Slack
* Socialite Providers
* Socialite LinkedIn

Obtaining API Keys
------------------


<img src="https://stripe.com/img/about/logos/logos/black@2x.png" width="200">

- [Sign up](https://stripe.com/) or log into your [dashboard](https://manage.stripe.com)
- Click on your profile and click on Account Settings
- Then click on [API Keys](https://manage.stripe.com/account/apikeys)
- Copy the **Secret Key**. and add this into `.env` file
<hr>

<img src="https://cdn.pixabay.com/photo/2015/05/26/09/37/paypal-784404_960_720.png" width="200">

- Visit [PayPal Developer](https://developer.paypal.com/)
- Log in to your PayPal account
- Click **Applications > Create App** in the navigation bar
- Enter *Application Name*, then click **Create app**
- Copy and paste *Client ID* and *Secret* keys into `.env` file
- *App ID* is **client_id**, *App Secret* is **client_secret**
- Change **host** to api.paypal.com if you want to test against production and use the live credentials

<hr>


<img src="http://iandouglas.com/presentations/pyconca2012/logos/sendgrid_logo.png" width="200">

- Go to https://sendgrid.com/user/signup
- Sign up and **confirm** your account via the *activation email*
- Then enter your SendGrid *Username* and *Password* into `.env` file


<img src="https://s3.amazonaws.com/ahoy-assets.twilio.com/global/images/wordmark.svg" width="200">

- Go to https://www.twilio.com/try-twilio
- Sign up for an account.
- Once logged into the dashboard, expand the link 'show api credentials'
- Copy your Account Sid and Auth Token

Project Structure
-----------------

| Name                                     | Description                                                  |
| ----------------------------------       | ------------------------------------------------------------ |
| **config**/app.php                       | Configuration for service providers and facades              |
| **config**/auth.php                      | Configuration for password resets                            |
| **config**/broadcasting.php              | Configuration for broadcasting                               |
| **config**/cache.php                     | Configuration for cache generation and storage               |
| **config**/cloudder.php                  | Configuration for cloudinary                                 |
| **config**/compile.php                   | Configuration for compilation                                |
| **config**/database.php                  | Configuration for database drivers                           |
| **config**/filesystems.php               | Configuration for different file systems                     |
| **config**/github.php                    | Configuration for github API                                 |
| **config**/mail.php                      | Configuration for mails                                      |
| **config**/queue.php                     | Configuration for queue                                      |
| **config**/services.php                  | Configuration for several services like mailgun etc.         |
| **config**/session.php                   | Configuration for sessions                                   |
| **config**/ttwitter.php                  | Twitter API config file                                      |
| **config**/twilio.php                    | Twilio API config file                                       |
| **config**/view.php                      | Configuration for location of views and view cache           |
| **controllers**/AccountController.php    | Controller for Account management                            |
| **controllers**/AviaryController.php     | Controller for Aviary API functionality                      |
| **controllers**/ClockworkController.php  | Controller for Clockwork API functionality                   |
| **controllers**/ContactController.php    | Controller for Contact page                                  |
| **controllers**/Controller.php           | BaseController                                               |
| **controllers**/GithubController.php     | Controller for Github API functionality                      |
| **controllers**/LastFmController.php     | Controller for LastFM API functionality                      |
| **controllers**/LobController.php        | Controller for Lob API functionality.                        |
| **controllers**/NytController.php        | Controller for New York Times API functionality              |
| **controllers**/OauthController.php      | Controller for Oauthentication                               |
| **controllers**/PaypalController.php     | Controller for Paypal API functionality                      |
| **controllers**/SteamController.php      | Controller for Stream API functionality                      |
| **controllers**/StripeController.php     | Controller for Stripe API functionality                      |
| **controllers**/TwilioController.php     | Controller for Twilio API functionality                      |
| **controllers**/TwitterController.php    | Controller for Twitter API functionality                     |
| **controllers**/WebScrapingController.php| Controller for Web Scraping.                                 |
| **controllers**/YahooController.php      | Controller for Yahoo API functionality                       |
| **controllers**/user.js                  | Controller for user account management.                      |
| **models**/User.php                      | Model for User.                                             |
| **public**/                              | Static assets (fonts, css, js, img).                         |
| **public**/**css**/main.css              | Main stylesheet for your app.                                |
| **resources/views/account**/             | Templates for *login, password reset, signup, profile*.      |
| **views/api**/                           | Templates for API Examples.                                  |
| **views/partials**/alerts.blade.php      | Error, info and success flash notifications.                 |
| **views/partials**/navbar.blade.php      | Navbar partial template.                                     |
| **views**/layouts**/master.blade.php     | Base template.                                               |
| **views**/apidashboard.blade.php         | API dashboard template.                                      |
| **views**/contact.blade.php              | Contact page template.                                       |
| **views**/welcome.blade.php              | Home page template.                                          |
| .travis.yml                              | [Travis CI](https://travis-ci.org/) integration.             |
| .env.example                             | Your API keys, tokens, passwords and database URI.           |
| composer.json                            | File for loading all php packages.                           |
| package.json                             | File for loading all necessary node modules.                 |
| artisan                                  | File for enabling commands to run                            |


Dependencies
----------------

| Package                         | Description                                                           |
| ------------------------------- | --------------------------------------------------------------------- |
| socialite                       | Sign-in with Facebook, Twitter and Github                             |
| socialite providers             | Sign-in with LinkedIn, Instagram                                      |
| cloudder                        | Upload images to Cloudinary                                           |
| laravel github                  | Github API library                                                    |
| clockwork                       | Clockwork SMS API library.                                            |
| goutte                          | Scrape web pages using jQuery-style syntax.                           |
| laravel framework               | PHP web framework                                                     |
| twitter                         | Twitter API library                                                   |
| twilio                          | Twilio API library                                                    |
| lob-php                         | Lob API library                                                       |
| lastfm-api-wrapper              | Lastfm API library                                                    |
| phpunit                         | PHP testing library                                                   |
| guzzlehttp                      | Simplified HTTP Request library                                 

 
<img src="https://upload.wikimedia.org/wikipedia/commons/f/ff/Windows_Azure_logo.png" width="200">

- Login to [Windows Azure Management Portal](https://manage.windowsazure.com/)
- Click the **+ NEW** button on the bottom left of the portal
- Click **COMPUTE**, then **WEB APP**, then **QUICK CREATE**
- Enter a name for **URL** and select the datacenter **REGION** for your web site
- Click on **CREATE WEB APP** button
- Once the web site status changes to *Running*, click on the name of the web site to access the Dashboard
- At the bottom right of the Quickstart page, select **Set up a deployment from source control**
- Select **Local Git repository** from the list, and then click the arrow
- To enable Git publishing, Azure will ask you to create a user name and password
- Once the Git repository is ready, you will be presented with a **GIT URL**
- Inside your *Hackathon Starter* directory, run `git remote add azure [Azure Git URL]`
- To push your changes simply run `git push azure master`
 - **Note:** *You will be prompted for the password you created earlier*
- On **Deployments** tab of your Windows Azure Web App, you will see the deployment history


**Note:** Alternative directions, including how to setup the project with a DevOps pipeline are available at [http://ibm.biz/hackstart](http://ibm.biz/hackstart).
A longer version of these instructions with screenshots is available at [http://ibm.biz/hackstart2](http://ibm.biz/hackstart2).
Also, be sure to check out the [Jump-start your hackathon efforts with DevOps Services and Bluemix](https://www.youtube.com/watch?v=twvyqRnutss) video.

## Contributing

Thank you for considering contributing to the project. Kindly create an issue and a new branch like so features/name-of-my-branch for a new feature and bugfixes/name-of-my-branch and also include a test with your code to ensure all is still good. The contribution guide can be found in the [Contribution File](CONTRIBUTING.md)

## Security Vulnerabilities

If you discover a security vulnerability within Laravel Hackathon Starter, please send an e-mail to Prosper Otemuyiwa at prosperotemuyiwa@gmail.com. All security vulnerabilities will be promptly addressed.

## Credits
* [Sahat Yalkabov](https://github.com/sahat/hackathon-starter) - Awesome

## How can I thank you?

Why not star the github repo? I'd love the attention! Why not share the link for this repository on Twitter or HackerNews? Spread the word!

Don't forget to [follow me on twitter](https://twitter.com/propenster_dev)!

Thanks!
Faith Olusegun - propenster.

## License

The MIT License (MIT). Please see [License File](LICENSE.md) for more information.
