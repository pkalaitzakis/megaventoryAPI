using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;

[Route("[controller]")]
[ApiController]
public class ProductClientController : ControllerBase {

    [HttpPost("ProductClientControllerUpdate")]
    public async Task<IActionResult> PostAsync([FromBody] InventoryLocation inventoryLocation) {
        var jsonContent = new StringContent(
            JsonSerializer.Serialize(new {
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