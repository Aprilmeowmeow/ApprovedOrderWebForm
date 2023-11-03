using Newtonsoft.Json;

namespace Q2.Lib
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class CustEnable
    {
        [JsonProperty(PropertyName = "CustomerID")]
        public string CustomerID { get; set; }
        [JsonProperty(PropertyName = "Enable")]
        public int Enable { get; set; }

    }
}