using System.Collections.Generic;
using Newtonsoft.Json;

namespace CodePant.DotNet.Helper.Models.DataTables
{
    public class DataTablesRequest
    {
        [JsonProperty(PropertyName = "columns")]
        public List<Columns> Columns { get; set; }

        [JsonProperty(PropertyName = "draw")] public int Draw { get; set; }

        [JsonProperty(PropertyName = "length")]
        public int Length { get; set; }

        [JsonProperty(PropertyName = "order")] public List<Order> Order { get; set; }

        [JsonProperty(PropertyName = "search")]
        public Search Search { get; set; }

        [JsonProperty(PropertyName = "start")] public int Start { get; set; }
    }
}