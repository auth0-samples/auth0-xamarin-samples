# Xamarin Forms Custom Login with Auth0

This project demonstrates how to integrate Auth0 with a Xamarin Forms application. Using the Auth0 SDK, the implementation presented allows the developer to implement their own UI for both traditional username and password authentication as well as social login.

## Prerequisites

1. This solution was built with Xamarin Studio 6.1. It will likely work on earlier versions, as well as Visual Studio 2015.
2. An [Auth0](https://auth0.com) is required. If you don't already have one, [sign up](https://auth0.com/signup) for a free account before you get started.
3. In your [Auth0 Dashboard](https://dashboard.auth0.com), make sure to set `https://YOUR-AUTH0-DOMAIN.auth0.com/mobile` as an allowed callback URL.

## Setup

1. Open the solution and open the `LoginPage.cs` file.
2. Change every instance of `YOUR-AUTH0-DOMAIN` with your Auth0 domain.
3. Change every instance of `YOUR-AUTH0-CLIENT-ID` with your Auth0 Client ID.
4. Open the `HomePage.cs` file.
5. Change every instance of `YOUR-AUTH0-DOMAIN` with your Auth0 domain.
6. Change every instance of `YOUR-AUTH0-CLIENT-ID` with your Auth0 Client ID.

## Test the application

To test the application, run the application in the debugger for either iOS or Android. Login with an existing account or with your Facebook account.

## What is Auth0?

Auth0 helps you to:

* Add authentication with [multiple authentication sources](https://docs.auth0.com/identityproviders), either social like **Google, Facebook, Microsoft Account, LinkedIn, GitHub, Twitter, Box, Salesforce, among others**, or enterprise identity systems like **Windows Azure AD, Google Apps, Active Directory, ADFS or any SAML Identity Provider**.
* Add authentication through more traditional **[username/password databases](https://docs.auth0.com/mysql-connection-tutorial)**.
* Add support for **[linking different user accounts](https://docs.auth0.com/link-accounts)** with the same user.
* Support for generating signed [JSON Web Tokens](https://docs.auth0.com/jwt) to call your APIs and **flow the user identity** securely.
* Analytics of how, when and where users are logging in.
* Pull data from other sources and add it to the user profile, through [JavaScript rules](https://docs.auth0.com/rules).

## Create a free account in Auth0

1. Go to [Auth0](https://auth0.com) and click Sign Up.
2. Use Google, GitHub or Microsoft Account to login.

## Issue Reporting

If you have found a bug or if you have a feature request, please report them at this repository issues section. Please do not report security vulnerabilities on the public GitHub issue tracker. The [Responsible Disclosure Program](https://auth0.com/whitehat) details the procedure for disclosing security issues.

## Author

[Auth0](auth0.com)

## License

This project is licensed under the MIT license. See the [LICENSE](LICENSE) file for more info.