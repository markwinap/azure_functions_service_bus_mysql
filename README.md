# Azure Functions Service Bus Queue Trigger DB

## Documentation
https://docs.microsoft.com/en-us/azure/azure-functions/functions-bindings-service-bus-trigger?tabs=in-process%2Cextensionv5&pivots=programming-language-csharp

## Requirements
- Storage Emulator - https://docs.microsoft.com/en-us/azure/storage/common/storage-use-emulator#get-the-storage-emulator

## Packages
dotnet add package MySql.Data

## Start Local Storage Emulator

Azure Storage Emulator
https://docs.microsoft.com/en-us/azure/storage/common/storage-use-emulator#start-and-initialize-the-storage-emulator

"C:\Program Files (x86)\Microsoft SDKs\Azure\Storage Emulator\"

### Commands
```sh
AzureStorageEmulator.exe init            : Initialize the emulator database and configuration.
AzureStorageEmulator.exe start           : Start the emulator.
AzureStorageEmulator.exe stop            : Stop the emulator.
AzureStorageEmulator.exe status          : Get current emulator status.
AzureStorageEmulator.exe clear           : Delete all data in the emulator.
AzureStorageEmulator.exe help [command]  : Show general or command-specific help.
```

## MySQl

### Docker Image
```sh
docker run --name some-mysql -p 3306:3306 -v ./db:/var/lib/mysql -e MYSQL_ROOT_PASSWORD=test -d mysql:latest
```

### Create Database and tables
```sql
CREATE DATABASE demo;
```
### Create Users Table
```sql
USE demo;
CREATE TABLE IF NOT EXISTS Users (
    UserId BINARY(16) PRIMARY KEY,
    FullName VARCHAR(150),
    Age INT,
    Created DATETIME
);
```

### Create Store Procedure
```sql
USE demo;
delimiter //
CREATE PROCEDURE postUser (
    IN _UserId VARCHAR(40),
    IN _FullName VARCHAR(150),
    IN _Age INT
) BEGIN
INSERT INTO Users (UserId, FullName, Age, Created) VALUES(UUID_TO_BIN(_UserId), _FullName, _Age, now());
SELECT BIN_TO_UUID(UserId) AS UserId, FullName, Age, Created FROM Users WHERE UserId = UUID_TO_BIN(_UserId);
END//
```

### Insert Data Example
```sql
CALL postUser("0f898832-f191-428c-b24b-cb56f88208b1", "Marco", 35);
```