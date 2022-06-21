using Newtonsoft.Json;

namespace CodePant.DotNet.Helper.Models.DataTables
{
    public class Search
    {
        [JsonProperty(PropertyName = "regex")] public bool Regex { get; set; }

        [JsonProperty(PropertyName = "value")] public object Value { get; set; }
    }
}