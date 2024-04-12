using Newtonsoft.Json;

public class mvSupplierClients
{
    private List<SupplierClient> supplierClients = new List<SupplierClient>();
    [JsonProperty("mvSupplierClients")]
    public List<SupplierClient> SupplierClients { get; set; }
}

public class SupplierClient
{
    private int supplierClientID;
    private string supplierClientName;
    private string supplierClientType;
    private string supplierClientShippingAddress1;
    private string supplierClientEmail;
    private string supplierClientPhone1;

    [JsonProperty("SupplierClientID")]
    public int SupplierClientID{
        get {return supplierClientID;}
        set {supplierClientID = value;}
    }

    [JsonProperty("SupplierClientName")]
    public string SupplierClientName {
        get {return supplierClientName;}
        set {supplierClientName = value;}
    }
    [JsonProperty("SupplierClientType")]
    public string SupplierClientType {
        get {return supplierClientType;}
        set {supplierClientType = value;}
    }
    [JsonProperty("SupplierClientShippingAddress1")]
    public string SupplierClientShippingAddress1 {
        get {return supplierClientShippingAddress1;}
        set {supplierClientShippingAddress1 = value;}
    }
    [JsonProperty("SupplierClientEmail")]
    public string SupplierClientEmail {
        get {return supplierClientEmail;}
        set {supplierClientEmail = value;}
    }
    [JsonProperty("SupplierClientPhone1")]
    public string SupplierClientPhone1 {
        get {return supplierClientPhone1;}
        set {supplierClientPhone1 = value;}
    }
}