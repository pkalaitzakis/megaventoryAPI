# megaventoryAPI
A small test project from Megaventory, where we play around with their API.

This application is a ASP.NET API WebApp written in C#, and it doesn't have a front end. It just integrates with megaventory's existing API endpoints in order to illustrate how a developer might use it to push data to a user's megaventory account, using their API Key.

There's support for swagger, so once you compile and run the app you'll be able to see which of the endpoints and how they are implemented, as well as run your own REST requests to see everything in action.

https://api.megaventory.com/v2017a/documentation/index.html


| Megaventory API Endpoint | WebApp API Endpoint | HTTP Method |
| --- | --- | --- |
| `/Product/ProductGet` | `/Product/ProductGetBySKU` | POST |
| `/Product/ProductUpdate` | `/Product/ProductUpdate` | POST |
| `/SuppierClient/SupplierClientGet` | `/SuppierClient/SupplierClientGetByPhone1` | POST |
| `/SuppierClient/SupplierClientUpdate` | `/SuppierClient/SupplierClientUpdate` | POST |
| `/LocationInventory/LocationInventoryGet` | `/LocationInventory/LocationInventoryGet` | POST |
|`/LocationInventory/LocationInventoryUpdate` | `/LocationInventory/LocationInventoryUpdate` | POST |
|`/ProductClient/ProductClientUpdate` | `/SupplierClient/SupplierClientRelationUpdate` | POST |
|`/ProductSupplier/ProductSupplierUpdate` | `/SupplierClient/SupplierClientRelationUpdate` | POST |
|`/LocationInventoryStock/LocationInventoryStockUpdate` | `/LocationInventory/LocationInventoryProductUpdate` | POST |
