using Newtonsoft.Json;

namespace CodePant.DotNet.Helper.Models.DataTables
{
    public class Columns
    {
        [JsonProperty(PropertyName = "data")] public string Data { get; set; }

        [JsonProperty(PropertyName = "name")] public string Name { get; set; }

        [JsonProperty(PropertyName = "orderable")]
        public bool Orderable { get; set; }

        [JsonProperty(PropertyName = "search")]
        public Search Search { get; set; }

        [JsonProperty(PropertyName = "searchable")]
        public bool Searchable { get; set; }
    }
}