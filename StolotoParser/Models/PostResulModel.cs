using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace StolotoParser_v2.Models
{
    public class PostResulModel
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("data")]
        public string Data { get; set; }

        [JsonProperty("stop")]
        public bool Stop { get; set; }
    }
}
