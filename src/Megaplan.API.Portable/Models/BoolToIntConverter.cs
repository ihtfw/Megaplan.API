namespace Megaplan.API.Models
{
    using System;

    using Newtonsoft.Json;

    public class BoolToIntConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, System.Type objectType, object existingValue, JsonSerializer serializer)
        {
            var source = reader.Value.ToString();

            if (source == "1")
            {
                return true;
            }

            return false;
        }

        public override bool CanConvert(System.Type objectType)
        {
            return (objectType == typeof(bool));
        }
    }

    public class EnumToStringConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, System.Type objectType, object existingValue, JsonSerializer serializer)
        {
            var source = reader.Value.ToString();

            return Enum.Parse(objectType, source);
        }

        public override bool CanConvert(System.Type objectType)
        {
            return true;
        }
    }
}