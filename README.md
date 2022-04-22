# Azure Functions Service Bus Queue Trigger DB

## Documentation
https://docs.microsoft.com/en-us/azure/azure-functions/functions-bindings-service-bus-trigger?tabs=in-process%2Cextensionv5&pivots=programming-language-csharp

## Requirements
- Storage Emulator https://azure.microsoft.com/en-us
- 
- 
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