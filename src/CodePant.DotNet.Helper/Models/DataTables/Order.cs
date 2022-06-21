using Newtonsoft.Json;

namespace CodePant.DotNet.Helper.Models.DataTables
{
    public class Order
    {
        [JsonProperty(PropertyName = "column")]
        public string Column { get; set; }

        [JsonProperty(PropertyName = "dir")] public string Dir { get; set; }
    }
}