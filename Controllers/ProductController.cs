using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using Newtonsoft.Json;

[Route("[controller]")]
[ApiController]
public class ProductController : ControllerBase
{


    [HttpPost("ProductGetBySKU")]
    public async Task<IActionResult> PostAsync(string productSKU)
    {
        var request = new Dictionary<string, object>() {
            {"APIKEY", Globals.APIKEY}
        };
        var filters = new Dictionary<string, string> {
            { "FieldName", "ProductSKU" },
            { "SearchOperator", "Equals" },
            { "SearchValue", productSKU }
        };

        request.Add("Filters", filters);

        // now retrieve the product from megaventory 

        HttpResponseMessage productResponse = await Globals.megaventoryClient.PostAsync("Product/ProductGet", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json"));
        // check if productget was successful 
        if (productResponse.IsSuccessStatusCode)
        {
            var content = await productResponse.Content.ReadAsStringAsync();
            int startIndex = content.IndexOf("{\"mvProducts\":");
            int endIndex = content.LastIndexOf("{\"ErrorCode\":\"0\"}}");
            var json = content.Substring(startIndex, endIndex - startIndex + 18);

            mvProducts mvProducts = JsonConvert.DeserializeObject<mvProducts>(json);

            if (mvProducts.Products == null || mvProducts.Products.Count <= 0)
                return StatusCode(500, "Product deserialization failed!");
            else
                return Ok(JsonConvert.SerializeObject(mvProducts.Products[0]));
        }

        else 
            return StatusCode(500, "GetByProductSKU request failed.");
    }

        [HttpPost("ProductUpdate")]
        public async Task<IActionResult> PostAsync([FromBody] Product product)
        {
            var jsonContent = new StringContent(
                System.Text.Json.JsonSerializer.Serialize(new
                {
                    APIKEY = Globals.APIKEY,
                    mvProduct = product,
                    mvRecordAction = "Insert",
                }), Encoding.UTF8, "application/json");

            HttpResponseMessage response = await Globals.megaventoryClient.PostAsync("Product/ProductUpdate", jsonContent);
            Console.WriteLine(response.EnsureSuccessStatusCode().ToString());

            var jsonResponse = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"{jsonResponse}\n");

            return Ok(jsonResponse);
        }
    }
