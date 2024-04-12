# megaventoryAPI
A small test project from Megaventory, where we play around with their API.

This application is a ASP.NET API WebApp written in C#, and it doesn't have a front end. It just integrates with megaventory's existing API endpoints in order to illustrate how a developer might use it to push data to a user's megaventory account, using their API Key.

There's support for swagger, so once you compile and run the app you'll be able to see which of the endpoints and how they are implemented, as well as run your own REST requests to see everything in action.

https://api.megaventory.com/v2017a/documentation/index.html

Entities-api endpoints used from the megaventory api:

PRODUCT:
/Product/ProductGet
/Product/ProductUpdate

SUPPLIER CLIENT:
/SuppierClient/SupplierClientGet
/SuppierClient/SupplierClientUpdate
        
LOCATION INVENTORY:
/LocationInventory/LocationInventoryGet
/LocationInventory/LocationInventoryUpdate

PRODUCT CLIENT:
/ProductClient/ProductClientUpdate

PRODUCT SUPPLIER:
/ProductSupplier/ProductSupplierUpdate

LOCATION INVENTORY STOCK:
/LocationInventoryStock/LocationInventortStockUpdate


