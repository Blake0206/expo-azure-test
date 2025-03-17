# Test repository for the mobile application

Using Expo, Azure Functions, and Azure Table Storage

## To recreate this project from scratch

### 1. Create a frontend and a backend folder

`mkdir frontend backend`

### 2. Move to the frontend folder

`cd frontend`

### 3. Create a new expo project

`npx create-expo-app --template` \
_Choose 'Navigation (Typescript)' and name it 'my-app'_

### 4. Create a new Azure Functions project using the Azure Functions extension

In the command palette, select 'Azure Functions: Create New Project...' and select the backend folder. Choose C# for the language and .NET 9.0 Isolated for the runtime.

**Note:** It may prompt you to install .NET 9.0 SDK. You can download it [here](https://dotnet.microsoft.com/en-us/download)

Choose HTTP trigger for the function template and keep the default name. Keep the default namespace as well. Choose Function for AccessRights.

### 5. Change the `local.settings.json` file

    {
      "IsEncrypted": false,
      "Values": {
        "AzureWebJobsStorage": "UseDevelopmentStorage=true",
        "TableStorageConnectionString": "UseDevelopmentStorage=true",
        "FUNCTIONS_WORKER_RUNTIME": "dotnet-isolated"
      },
      "ConnectionStrings": {}
    }

### 6. Replace `HTTPTrigger1.cs` with the code from the repository

### 7. Include `Azure.Data.Tables` in the `backend.csproj` file

`<PackageReference Include="Azure.Data.Tables" Version="12.2.0" />`

### 8. Replace `index.tsx` with the code from the repository

### 9. Add the `.azurite` folder to the `backend/.gitignore`

`.azurite/`

## To run the Application

### 1. Open a new terminal (1) and move to the `backend` folder

`cd backend`

### 2. Launch Azurite

`azurite --silent --location .\azurite --debug .\azurite\debug.log`

_**Note:** If you don't have Azurite installed, run the following command first:_ `npm install -g azurite`

### 3. Open another new terminal (2) and move to the `backend` folder

`cd backend`

### 4. Start the functions

`func start`

### 5. Open another new terminal (3) and move to the `my-app` folder

`cd frontend/my-app`

### 6. Run the expo application using one of the following commands

* `npm run web`
* `npm run ios`
* `npm run android`

---

_The application will be available at [http://localhost:8081](http://localhost:8081)_ \
_To view the table storage, download **Microsoft Azure Storage Explorer**_
