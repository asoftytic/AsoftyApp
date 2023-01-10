using AsoftyBackend.Infrastructure.Data;
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


        
        public static string OperatorToSqlString(Operator op)
        {
            switch (op)
            {
                case Operator.Equals: return "=";
                case Operator.GreaterThan: return ">";
                case Operator.LessThan: return "<";
                case Operator.NotEquals: return "<>";
                default: return "=";

            }
        }
        
        public static bool NullOrEmpty(this string? str) => str == null || str == "";
        public static string AntiInjectionFormat(this string str) => str.Replace("'", "''").Replace("\"", "\"");
        public static string AntiInjectionFormat(this object str) => str?.ToString()?.Replace("'", "''").Replace("\"", "\"\"") ?? "";
    }
}
