using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace WebHook.Services.Utilities
{
    public static class FormUrlEncodedContentUtility
    {
        private static readonly JsonSerializerOptions _options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        public static FormUrlEncodedContent ConvertFromJson(this object obj)
        {
            var jsonString = JsonSerializer.Serialize(obj, _options);

            var flatDictionary = FlattenJson(JObject.Parse(jsonString));

            return new FormUrlEncodedContent(flatDictionary);
        }

        public static string ParseObjectToQueryString(this object obj)
        {
            var jsonString = JsonSerializer.Serialize(obj, _options);
            JObject jsonObject = JObject.Parse(jsonString);

            List<string> keyValuePairs = new List<string>();
            foreach (var property in jsonObject.Properties())
            {
                string key = property.Name;

                if (property.Value.Type == JTokenType.Object)
                {
                    foreach (var item in (JObject)property.Value)
                    {
                        keyValuePairs.Add($"{key}[]={Uri.EscapeDataString(item.ToString())}");
                    }
                }
                else
                {
                    keyValuePairs.Add($"{key}={Uri.EscapeDataString(property.Value.ToString())}");
                }
            }

            return string.Join("&", keyValuePairs);
        }

        #region Private Methods

        private static Dictionary<string, string> FlattenJson(JObject json)
        {
            var result = new Dictionary<string, string>();
            FlattenJsonRecursive(json, result, "");
            return result;
        }

        private static void FlattenJsonRecursive(JObject json, Dictionary<string, string> result, string currentKey)
        {
            foreach (var property in json.Properties())
            {
                string key = currentKey + (string.IsNullOrEmpty(currentKey) ? "" : ".") + property.Name;

                if (property.Value.Type == JTokenType.Object)
                {
                    FlattenJsonRecursive((JObject)property.Value, result, key);
                }
                else
                {
                    result[key] = property.Value.ToString();
                }
            }
        }

        #endregion
    }
}
