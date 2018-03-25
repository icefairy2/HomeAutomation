# Home Automation

A C# ASP.NET web application with REST and Arduino communication.

## Getting Started

Unzip the project.

### Prerequisites

You need to have Microsoft Visual Studio installed, preferably the 2017 version.
You need to have Microsoft SQL Server installed, also the MS SQL Server Management Studio.
You need to have the Arduino setup built and connected, and the Arduino IDE installed.

### Running the project

Open MS SQL Server Management Studio and establish the connection, copy the server name (usually your PC name).
Create a new, empty database called "hangfire".
Open the project solution in Visual Studio.
From the Tools menu, open "Connect to Database".
In the 'Server name' field paste the server name you copied.
From the Select database dropdown menu, select the 'hangfire' database.
In the properties of the connection, you can find the field "Connection string", copy this.
Open the file CommonStrings.cs.
Replace the field ConnectionString's value with your connection string.
Open the Arduino IDE.
If you have the cable connected, you could check from here, which port the Arduino is connected to (one of the COM port).
Go back to the CommonString.cs file and replace the ComPort string value with your port on which the Arduino runs.
Run the project (F5 or Ctrl+F5 in Visual Studio).
Enjoy.



