using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsoftyBackend.Infrastructure.Utils
{
    public static class Utils
    {
        public static Dictionary<string, object> ObjectToDictionary(object obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            return JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
        }
    }
}
