using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StolotoParser_v2.Services
{
    public interface IJsonService
    {
        T JsonConvertDeserializeObject<T>(string json);
    }
}
