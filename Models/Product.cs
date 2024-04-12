using Newtonsoft.Json;

public class mvProducts
{
    [JsonProperty("mvProducts")]
    public List<Product> Products { get; set; }
}

public class Product {
    
    private int productID;
    private string productSKU;
    private string productDescription;
    private double productSellingPrice;
    private double productPurchasePrice;

    [JsonProperty("ProductID")]
    public int ProductID {
        get { return productID; }
        set { productID = value; }
    }
    [JsonProperty("ProductSKU")]
    public string ProductSKU {
        get { return productSKU; }
        set { productSKU = value; }
    }
    [JsonProperty("ProductDescription")]
    public string ProductDescription {
        get { return productDescription; }
        set { productDescription = value; }
    }
    [JsonProperty("ProductSellingPrice")]
    public double ProductSellingPrice {
        get { return productSellingPrice;}
        set { productSellingPrice = value;}
    }
    [JsonProperty("ProductPurchasePrice")]
    public double ProductPurchasePrice {
        get { return productPurchasePrice; }
        set { productPurchasePrice = value;}
    }

}
