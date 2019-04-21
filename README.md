# holiday-management

A simple web application using `ASP.NET` Core and Angular that allow you to manage employee holiday allocation.

## Setup

Make sure you have installed the following pre-requisites:

- .NET Core 2.2 SDK
- Node.js >8.0.0
- Compiler support for C# 7.1

The project uses a SQLite database for simplicity.
You can run `scripts\setup.ps1` to initialise the database - if necessary, it will also do a full restore and build of the projects under `\src` in order to install all compile-time dependencies, notably `Microsoft.EntityFrameworkCore.Design`.

The backend `HolidayManagement.Api` is build with `ASP.NET` Core, while the `HolidayManagement.Client` frontend is designed in Angular.

## Running the app

To run it you can start both the api and client `ASP.NET` assemblies via Visual Studio, for example. If running via the default IIS profile, you can then access the client app at `https://localhost:44366/`.

Once open, you can enter `admin mode` by appending the `admin` key to you URL query string.

__The Angular frontend is incomplete and requires more work.__