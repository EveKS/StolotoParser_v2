using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace StolotoParser_v2.Services
{
    public class JsonService : IJsonService
    {
        T IJsonService.JsonConvertDeserializeObject<T>(string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch
            {
                return default(T);
            }
        }
    }
}
