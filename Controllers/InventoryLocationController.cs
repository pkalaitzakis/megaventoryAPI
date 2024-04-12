using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Newtonsoft.Json;
using System.Text;

[Route("[controller]")]
[ApiController]
public class InventoryLocationController : ControllerBase {

    [HttpPost("InventoryLocationProductUpdate")]
    public async Task<IActionResult> PostAsync(string locationName, string locationAbbreviation, string productSKU, double quantity, double cost) {
        Product product;
        var response = await Globals.webApp.PostAsync($"Product/ProductGetBySKU?productSKU={productSKU}", null);
        if (response.IsSuccessStatusCode)
            product = JsonConvert.DeserializeObject<Product>(await response.Content.ReadAsStringAsync());
        else
            return StatusCode(500, "Something went wrong");
        
        InventoryLocation loc;
        response = await Globals.webApp.PostAsync($"InventoryLocation/InventoryLocationGet?locationName={locationName}&InventoryLocationAbbreviation={locationAbbreviation}", null);
        if (response.IsSuccessStatusCode)
            loc = JsonConvert.DeserializeObject<InventoryLocation>(await response.Content.ReadAsStringAsync());
        else
            return StatusCode(500, "Something went wrong");

         // if the client and product exist
        if (loc != null && product != null)
        {
            // construct payload appropriately
            var payload = new Dictionary<string, object>
            {
                { "APIKEY", Globals.APIKEY } 
            };
            var mvShit = new Dictionary<string, object>
            {
                { "ProductSKU", product.ProductSKU},
                { "ProductQuantity", quantity },
                { "ProductUnitCost", cost},
                { "InventoryLocationID", loc.InventoryLocationID}

            };
            payload.Add("mvProductStockUpdateList", mvShit);
            payload.Add("mvRecordAction", "Insert");

            // httpContent in json format
            var jsonContent = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
            // dynamically construct correct endpoint dependint on supplier/client type
            
            // proceed with the final update post to megaventory API endpoint
            HttpResponseMessage finalResponse = await Globals.megaventoryClient.PostAsync("InventoryLocationStock/ProductStockUpdate", jsonContent);
            var jsonResponse = await finalResponse.Content.ReadAsStringAsync();
            if (finalResponse.IsSuccessStatusCode)
                return Ok($"Payload: {JsonConvert.SerializeObject(payload)}\nResponse : {jsonResponse}");
            else
                return StatusCode(500, "Something went wrong");
        }
        else if (loc == null)
        {
            return StatusCode(404, "Inventory location not found!");
        }
        else
            return StatusCode(404, "Product not found!");

    }
    [HttpPost("InventoryLocationGet")]
    public async Task<IActionResult> PostAsync(string locationName, string InventoryLocationAbbreviation){
        var requestPayload = new Dictionary<string, object>()
        {
            {"APIKEY", Globals.APIKEY}
        };
        var filter1 = new Dictionary<string, object>()
        {
            {"FieldName", "InventoryLocationName"},
            {"SearchOperator", "Equals"},
            {"SearchValue", locationName}
        };
        var filter2 = new Dictionary<string, object>()
        {
            {"AndOr", "And"},
            {"FieldName", "InventoryLocationAbbreviation"},
            {"SearchOperator", "Equals"},
            {"SearchValue", InventoryLocationAbbreviation}
        };
        var filters = new List<Dictionary<string, object>>(){filter1, filter2};

        requestPayload.Add("Filters", filters);
        requestPayload.Add("ReturnTopNRecords", 1);

        var json = JsonConvert.SerializeObject(requestPayload);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await Globals.megaventoryClient.PostAsync("InventoryLocation/InventoryLocationGet", content);
        var responseContent = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode) {
            int startIndex = responseContent.IndexOf("{\"mvInventoryLocations\":[{");
            int endIndex = responseContent.LastIndexOf("\"ErrorCode\":\"0\"}}");
            var clean = responseContent.Substring(startIndex, endIndex-startIndex + 17);

            mvInventoryLocations locs = JsonConvert.DeserializeObject<mvInventoryLocations>(clean);

            if (locs.InventoryLocations == null || locs.InventoryLocations.Count<=0)
                return StatusCode(404, $"{startIndex}{endIndex}{responseContent}Inventory Location not found");
            else
                return Ok($"{JsonConvert.SerializeObject(locs.InventoryLocations[0])}");
        } else {
            return StatusCode(500, $"{json} \n {responseContent}");
        }
    }
    [HttpPost("InventoryLocationUpdate")]
    public async Task<IActionResult> PostAsync([FromBody] InventoryLocation inventoryLocation) {
        var jsonContent = new StringContent(
            System.Text.Json.JsonSerializer.Serialize(new {
                APIKEY = Globals.APIKEY,
                mvInventoryLocation = inventoryLocation,
                mvRecordAction = "Insert",
            }), Encoding.UTF8, "application/json");
        
        HttpResponseMessage response = await Globals.megaventoryClient.PostAsync("InventoryLocation/InventoryLocationUpdate", jsonContent);
        Console.WriteLine(response.EnsureSuccessStatusCode().ToString());

        var jsonResponse = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"{jsonResponse}\n");

        return Ok(jsonResponse);
    }
}