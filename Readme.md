## Welcome to the ASP.NET Core WebApi assessment for Experienced .NET Software Engineers
### The Transaction Web API
The Transaction API is a monolithic application consists of the following entities:

1.  Person
2.  Account
3.  Transaction

Each of these entities has their separate controllers with their respective GET, POST, and PUT methods.

You are required to restructure the solution to the following:

1.  **N-Tier (Multi-layered) Architecture:** Separate the necessary application codes into their respective layers as separate projects.
2.  **Repository Pattern:** Create _Interfaces_ for Person, Account and Transaction as well as the _Repositories_ that implement the interfaces.
3.  **N-Tier (Multi-layered) Architecture:** Separate the necessary application codes into their respective layers as separate projects.
4.  **Additional Endpoint:** Create an endpoint in the Transaction Controller API called "GetTransactionsByPersonID" that receives a PersonID and returns all transactions that have been made to the person's account. The details of the transaction should be a list containing the following view model:
    *   Surname, Firstname, AccountName, AccountNumber, TransactionType, TransactionAmount, AccountBalance, OffsetAccount and TransactionDateTime (in AM/PM). Offset Account is the account Name and Number of the DrAccountID or CrAccountID depending on the TransactionType.
5.  **Fix bugs:** The application has not been tested. Kindly ensure that all endpoints are working properly.
6. **API Documentation:** Set up API documentation for the application using Swagger UI or any API documentation library you are comfortable with. 
7.  **Branching:** Create a branch from "master" and name it "yoursurname-yourfirstname" and push your solution to your created branch. Please do not push to master.

**NB:** See the /Script folder for the Database Table Scripts