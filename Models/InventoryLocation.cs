using Newtonsoft.Json;

public class mvInventoryLocations{
    private List<InventoryLocation> inventoryLocations;

    [JsonProperty("mvInventoryLocations")]
    public List<InventoryLocation> InventoryLocations 
    { 
        get {return inventoryLocations;}
        set {inventoryLocations = value;}
    }
}

public class InventoryLocation {

    private int inventoryLocationID;
    private string inventoryLocationName;
    private string inventoryLocationAbbreviation;
    private string inventoryLocationAddress;

    [JsonProperty("InventoryLocationID")]
    public int InventoryLocationID 
    {
        get {return inventoryLocationID;}
        set {inventoryLocationID = value;}
    }
    [JsonProperty("InventoryLocationName")]
    public string InventoryLocationName 
    {
        get {return inventoryLocationName;}
        set {inventoryLocationName = value;}
    }
    [JsonProperty("InventoryLocationAbbreviation")]
    public string InventoryLocationAbbreviation
    {
        get {return inventoryLocationAbbreviation;}
        set {inventoryLocationAbbreviation = value;}
    }
    [JsonProperty("InventoryLocationAddress")]
    public string InventoryLocationAddress
    {
        get {return inventoryLocationAddress;}
        set {inventoryLocationAddress = value;}

    }
}