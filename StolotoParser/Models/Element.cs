using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace StolotoParser_v2.Models
{
    public class Element
    {
        [JsonProperty("btnName")]
        public string BtnName { get; set; }

        [JsonProperty("pathName")]
        public string PathName { get; set; }

        [JsonProperty("fileName")]
        public string FileName { get; set; }

        [JsonProperty("fileAllName")]
        public string FileAllName { get; set; }

        [JsonProperty("totalCount")]
        public int? TotalCount { get; set; }
    }
}
