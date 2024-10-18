# BookRentalCoreProjectAPI
Book Rental Service API

Create Database in  Name "BookRentalDB"

Change the app settings Connectionstring:

Run the below Migrationcommand in Package console :
   Add-Migration "InitialDBCreate"
   Update-Database

Run the webapi 
In the Swagger you can get the results for all GET methods.

For POST method use Postman 
https://localhost:7108/api/Rentals/rent/1,2

https://localhost:7108/api/Rentals/return/10

 For now "MarkOverdue" done thru Postmethod. But in realtime it will automatially run the Background jobs using "IHosService"
https://localhost:7108/api/Rentals/MarkOverDue

https://localhost:7108/api/Rentals/SendMailNotification
