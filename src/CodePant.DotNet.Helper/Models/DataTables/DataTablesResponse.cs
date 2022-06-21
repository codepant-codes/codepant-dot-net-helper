using System.Collections.Generic;
using Newtonsoft.Json;

namespace CodePant.DotNet.Helper.Models.DataTables
{
    public class DataTablesResponse<T> where T : class
    {
        [JsonProperty(PropertyName = "data")] public List<T> Data { get; set; }

        [JsonProperty(PropertyName = "draw")] public int Draw { get; set; }

        [JsonProperty(PropertyName = "records_filtered")]
        public int RecordsFiltered { get; set; }

        [JsonProperty(PropertyName = "records_total")]
        public int RecordsTotal { get; set; }
    }
}