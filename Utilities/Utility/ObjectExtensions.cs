using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Utility
{
    public static class ObjectExtensions
    {
        public static string ToJson(this object obj)
        {
            var js = JsonSerializer.Create(new JsonSerializerSettings());
            var jw = new StringWriter();
            js.Serialize(jw, obj);
            return jw.ToString();
        }
        public static string ToJsonCamelCase(this object obj)
        {
            var jsonSerializerSettings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            return JsonConvert.SerializeObject(obj, Formatting.Indented, jsonSerializerSettings);
        }

    }
}