using System.Diagnostics;
using Megaplan.API.Attributes;

namespace Megaplan.API
{
    using System.Collections;
    using System.Reflection;
    using System.Text;

    using Newtonsoft.Json;

    public class QueryBuider
    {
        private readonly object queryParams;

        private StringBuilder sb;

        private bool first;

        public QueryBuider(object queryParams)
        {
            this.queryParams = queryParams;
        }

        public byte[] BuildPostData()
        {
            var build = BuildQuery();

            if (string.IsNullOrWhiteSpace(build))
                return null;

#if DEBUG
            Debug.WriteLine("Post data: " + build);
#endif

            return Encoding.UTF8.GetBytes(build);
        }

        public string Build()
        {
            var build = BuildQuery();
           
            if (string.IsNullOrWhiteSpace(build))
                return "";

            return "?" + build;
        }

        private string BuildQuery()
        {
            if (queryParams == null)
                return null;

            sb = new StringBuilder();

            first = true;
            ProcessObject(queryParams);

            return sb.ToString();
        }

        private void ProcessObject(object obj, string prefix = "")
        {
            foreach (var propertyInfo in obj.GetType().GetRuntimeProperties())
            {
                var propertyName = GetPropertyName(propertyInfo);

                var propertyValue = GetPropertyValue(propertyInfo, obj);
         

                var arrayAttribute = propertyInfo.GetCustomAttribute<ArrayAttribute>();
                if (arrayAttribute != null)
                {
                

                    var array = (IEnumerable)propertyValue;
                    int i = 0;
                    foreach (var value in array)
                    {
                        var subobjectPrefix = arrayAttribute.Mask.Replace("%", i++.ToString());
                        ProcessObject(value, subobjectPrefix);
                    }
                    return;
                }

                AddProperty(prefix + propertyName, propertyValue);
            }
        }

        private static string GetPropertyName(PropertyInfo propertyInfo)
        {
            var jsonPropertyAttribute = propertyInfo.GetCustomAttribute<JsonPropertyAttribute>();

            var propertyName = jsonPropertyAttribute != null ? jsonPropertyAttribute.PropertyName : propertyInfo.Name;
            return propertyName;
        }

        private object GetPropertyValue(PropertyInfo propertyInfo, object obj)
        {
            object propertyValue = propertyInfo.GetValue(obj);
            if (propertyValue == null)
                return null;
            if (propertyInfo.GetCustomAttribute<BuildBoolAsIntAttribute>() != null)
            {
                var boolValue = (bool)propertyValue;
                propertyValue = boolValue ? 1 : 0;
            }
            else if (propertyValue is IEnumerable)
            {
                
            }
            else if (propertyInfo.GetCustomAttribute<BuildWithoutToLowerAttribute>() == null)
            {
                propertyValue = propertyValue.ToString().ToLower();
            }
            return propertyValue;
        }

        private void AddProperty(string propertyName, object propertyValue)
        {
            if (propertyValue == null)
            {
                return;
            }
            if (first)
            {
                first = false;
            }
            else
            {
                sb.Append("&");
            }
            sb.Append(propertyName);
            sb.Append("=");
            sb.Append(propertyValue);
        }
    }
}