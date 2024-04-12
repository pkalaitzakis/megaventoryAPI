using Microsoft.AspNetCore.Mvc;
using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Newtonsoft.Json;
using System.Security.Cryptography;
using Microsoft.VisualBasic;

[Route("[controller]")]
[ApiController]
public class SupplierClientController : ControllerBase
{

    [HttpPost("SupplierClientGetByPhone1")]
    public async Task<IActionResult> PostAsync(string supplierClientPhone1)
    {
        var request = new Dictionary<string, object>()
        {
            {"APIKEY", Globals.APIKEY}
        };
        var filters = new Dictionary<string, string>
        {
            { "FieldName", "SupplierClientPhone1" },
            { "SearchOperator", "Equals" },
            { "SearchValue", supplierClientPhone1 }
        };
        request.Add("Filters", filters);

        HttpResponseMessage response = await Globals.megaventoryClient.PostAsync("SupplierClient/SupplierClientGet", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));

        if (response.IsSuccessStatusCode)
        {
            // parse the response content into a SupplierClient Model class instance
            var content = await response.Content.ReadAsStringAsync();
            int startIndex = content.IndexOf("{\"mvSupplierClients\":[{");
            int endIndex = content.LastIndexOf("\"ErrorCode\":\"0\"}}");
            string json = content.Substring(startIndex, endIndex - startIndex + 17);

            mvSupplierClients mvSupplierClients = JsonConvert.DeserializeObject<mvSupplierClients>(json);

            if (mvSupplierClients.SupplierClients == null || mvSupplierClients.SupplierClients.Count <= 0)
                return StatusCode(404, "SupplierClient not found!");
            else
                return Ok($"{JsonConvert.SerializeObject(mvSupplierClients.SupplierClients[0])}");

        }
        else
            return StatusCode(500, "Unsuccessfull get request");
    }

    [HttpPost("SupplierClientProductRelationUpdate")]
    public async Task<IActionResult> PostAsync(string supplierClientPhone1, string productSKU)
    {
        SupplierClient cl;
        // First retrieve SupplierClient by id by doing a get to our own web app
        HttpResponseMessage response = await Globals.webApp.PostAsync($"SupplierClient/SupplierClientGetByPhone1?supplierClientPhone1={supplierClientPhone1}", null);
        if (response.IsSuccessStatusCode)
            cl = JsonConvert.DeserializeObject<SupplierClient>(await response.Content.ReadAsStringAsync());
        else
            return StatusCode(500, "Something went wrong");
        
        Product product;
        response = await Globals.webApp.PostAsync($"Product/ProductGetBySKU?productSKU={productSKU}", null);
        if (response.IsSuccessStatusCode)
            product = JsonConvert.DeserializeObject<Product>(await response.Content.ReadAsStringAsync());
        else
            return StatusCode(500, "Something went wrong");
       
        // if the client and product exist
        if (cl != null && product != null)
        {
            // construct payload appropriately
            var payload = new Dictionary<string, object>
            {
                { "APIKEY", Globals.APIKEY } 
            };
            var mvShit = new Dictionary<string, object>
            {
                { "ProductID", product.ProductID},
                { $"Product{cl.SupplierClientType}ID", cl.SupplierClientID }
                        
            };
            payload.Add($"mvProduct{cl.SupplierClientType}Update", mvShit);
            payload.Add("mvRecordAction", "Insert");

            // httpContent in json format
            var jsonContent = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
            // dynamically construct correct endpoint dependint on supplier/client type
            string endpoint = $"Product{cl.SupplierClientType}/Product{cl.SupplierClientType}Update";
            
            // proceed with the final update post to megaventory API endpoint
            HttpResponseMessage finalResponse = await Globals.megaventoryClient.PostAsync(endpoint, jsonContent);
            var jsonResponse = await finalResponse.Content.ReadAsStringAsync();
            if (finalResponse.IsSuccessStatusCode)
                return Ok($"Payload: {JsonConvert.SerializeObject(payload)}\nAPI Endpoint: {endpoint} \nResponse : {jsonResponse}");
            else
                return StatusCode(500, "Something went wrong");
        }
        else if (cl == null)
        {
            return StatusCode(404, "SupplierClient not found!");
        }
        else
            return StatusCode(404, "Product not found!");
    }

    [HttpPost("SupplierClientUpdate")]
    public async Task<IActionResult> PostAsync([FromBody] SupplierClient supplierClient)
    {
        var jsonContent = new StringContent(
            JsonConvert.SerializeObject(new
            {
                APIKEY = Globals.APIKEY,
                mvSupplierClient = supplierClient,
                mvRecordAction = "Insert",
            }), Encoding.UTF8, "application/json");

        HttpResponseMessage response = await Globals.megaventoryClient.PostAsync("SupplierClient/SupplierClientUpdate", jsonContent);
        Console.WriteLine(response.EnsureSuccessStatusCode().ToString());
        var jsonResponse = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"{jsonResponse}\n");

        return Ok(jsonResponse);
    }
}