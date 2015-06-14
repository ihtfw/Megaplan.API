using System.Diagnostics;
using Megaplan.API.Attributes;

namespace Megaplan.API
{
    using System.Reflection;
    using System.Text;

    using Newtonsoft.Json;

    public class QueryBuider
    {
        private readonly object queryParams;
        
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

            var sb = new StringBuilder();

            var first = true;
            foreach (var propertyInfo in queryParams.GetType().GetRuntimeProperties())
            {
                var value = propertyInfo.GetValue(queryParams);
                if (value == null)
                {
                    continue;
                }

                if (first)
                {
                    first = false;
                }
                else
                {
                    sb.Append("&");
                }

                var jsonPropertyAttribute = propertyInfo.GetCustomAttribute<JsonPropertyAttribute>();
                if (jsonPropertyAttribute != null)
                {
                    sb.Append(jsonPropertyAttribute.PropertyName);
                }
                else
                {
                    sb.Append(propertyInfo.Name);
                }
                sb.Append("=");

                if (propertyInfo.GetCustomAttribute<BuildBoolAsIntAttribute>() != null)
                {
                    var boolValue = (bool) value;
                    sb.Append(boolValue ? 1 : 0);
                }
                else if (propertyInfo.GetCustomAttribute<BuildWithoutToLowerAttribute>() != null)
                {
                    sb.Append(value);
                }
                else
                {
                    sb.Append(value.ToString().ToLower());
                }
            }

            return sb.ToString();
        }
    }
}