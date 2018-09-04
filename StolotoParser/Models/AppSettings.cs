using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace StolotoParser_v2.Models
{
    public class AppSettings
    {
        [JsonProperty("continueFormat")]
        public string ContinueFormat { get; set; }

        [JsonProperty("format")]
        public string Format { get; set; }

        [JsonProperty("elements")]
        public Element[] Elements { get; set; }
    }
}
