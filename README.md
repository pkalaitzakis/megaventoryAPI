# megaventoryAPI
A small test project from Megaventory, where we play around with their API.


This application is a ASP.NET API WebApp written in C#, and it doesn't have a front end. It just integrates with megaventory's existing API endpoints in order to illustrate how a developer might use it to push data to a user's megaventory account, using their API Key.

Once you run the app, there's support for swagger, so you'll be able to see which of the endpoints are implemented, as well as run the REST requests to see everything in action.

Entities-api endpoints used from the megaventory api:

/LocationInventory/
      /LocationInventoryUpdate 
      /LocationInventoryGet    

/SuppierClient/
      /SupplierClientGet       
      /SupplierClientUpdate    

/Product/
      /ProductGet              
      /ProductUpdate           

/LocationInventoryStock/
      /LocationInventortStockUpdate

/ProductClient/
      /ProductClientUpdate

/ProductSupplier/
      /ProductSupplierUpdate
