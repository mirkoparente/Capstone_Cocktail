# 🍸 Ecommerce CocktAIl
## 💻 Full Stack Capstone Project

Cocktail is an e-commerce platform and  management system for cocktails and mixologist products. It includes features for product management, user registration , login, access from external providers, adding items to the cart, checkout and payment. I've incorporated an artificial intelligence-powered search to engage and interact with users during navigation providing product recommendations as if they were at the real counter of a cocktail bar! 
The application was developed in Asp.Net MVC, using MVC Identity Framework for user management, external API and SQL Server to store the data.

## 📋 Functionality
* User registration and login with MVC Identity
* Login external provider Google and Facebook
* Email send to restore your password and confirm email registration
* Checkout with Paypal
* Add Edit and Delete product review
* AI-powered search page with Chat Gpt Api
* Product management and order statistics by the administrator (add, edit)
  
## Update
Visit my project 🌐

[Cocktail](https://mirkopare-001-site1.anytempurl.com/)

## 📦 Setup 
Make sure you have the following installed:

- [.NET SDK](https://dotnet.microsoft.com/download)
- [Visual Studio](https://visualstudio.microsoft.com/)
- [SQL Server](https://www.microsoft.com/sql-server/)
  
* Clone the repository.
* Open the project solution in Visual Studio.
* In the `Content` folder there is a backup of the database `Cocktail.bak` (if you want a pre-populated database), execute the restore on SQL Server.
* Alternatively, use `enable-migration`, `add-migration` and `update-database` to create a new database.
* Configure your `Connection string`, `Email provider`,  `Google`, `Facebook`, `Paypal` and `OpenAi`  APi Key in the `Web1.config` file.
* If you created a new Database create manually 'Admin' and 'User' in `AspNetRoles`, Create a user in `AspNetUsers` and insert 'UserId', 'RoleId' in `AspNetUserRole`. 
* Enjoy.

## 🖊️ Author
Mirko Parente

