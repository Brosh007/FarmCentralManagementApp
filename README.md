Farmer Central Management App

Farmer Central Management App is an application that allows employees to register farmers and manage farmer-related information. Farmers can log in to create, edit, and delete products, as well as view product details. The application also provides features for employees to search and view products created by farmers.
Application Features

    Admin Portal:
        Register employees:
            The admin can register employees by providing their details such as name, contact information, role, etc. Once registered, employees can log in to the system.

    Employee Portal:
        Register farmers:
            Employees can access a registration form to enter details of a farmer such as name, contact information, address, etc. Once submitted, the farmer will be added to the system.
        Create, edit, and delete farmers:
            Employees can perform CRUD (Create, Read, Update, Delete) operations on farmers' information. They can add, modify, or remove farmers as needed.
        View products created by farmers:
            Employees can view a list of products created by farmers. They can search and filter the products by name or farmer ID.

    Farmer Portal:
        Create, edit, and delete products:
            Farmers can create new products by providing details such as product name, description, price, etc. They can also modify or remove existing products.
        View product details:
            Farmers can access detailed information about each product they have created, including its name, description, price, etc.

Website URL

The application is hosted on Azure and can be accessed at the following URL: https://farmercentralmanagamentapp20230525105752.azurewebsites.net/
Installation

To install and run the application locally, follow these steps:

    Clone the Git repository:

    bash

git clone https://github.com/VCSTDN/prog7311-part2-St10090453.git

Install the necessary dependencies:

bash

    cd prog7311-part2-St10090453
    npm install

    Set up the SQL database:
        Open SQL Server Management Studio (SSMS).
        Connect to your local SQL Server instance.
        Open the file Farmer_Central_Management_App.sql in SSMS.
        Execute the SQL queries in the file to create the required tables.

    Configure the application:
        Update the database connection settings in the configuration file (config.js) to match your local environment.

    Run the application:
        Open the project in Visual Studio.
        Build and run the application using the green play button or the "Start" or "Debug" option.

    Access the application in your web browser:
        Open http://localhost:3000 in your preferred browser.

    Login credentials:
        For employee login:
            Email: Cr@gmail.com
            Password: 12345
        For farmer login:
            Email: john@gmail.com
            Password: 123456

Contributing

Contributions to this project are welcome. If you find any issues or have suggestions for improvement, please open an issue or submit a pull request.
License

This project is open source and does not have a specific license. You are free to use, modify, and distribute the code as per your requirements.
