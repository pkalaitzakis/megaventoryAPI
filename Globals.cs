public static class Globals
{
       public static HttpClient megaventoryClient = new HttpClient() {
              BaseAddress = new Uri("https://api.megaventory.com/v2017a/")
       };
       public static HttpClient webApp = new HttpClient() {
              BaseAddress = new Uri("http://localhost:5074/")
       };
       public static string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
       public static string APIKEY = "706df866141d40a4@m147572";
       public static List<string> AddressTypes = new List<string> {"General", "Billing", "Shipping1", "Shipping2"};
       public static List<string> MVRecordAction = new List<string>{"Insert", "Update", "InsertOrUpdateNonEmptyFields"};
}