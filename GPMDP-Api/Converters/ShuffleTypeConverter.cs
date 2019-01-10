using System;
using System.Collections.Generic;
using System.Text;
using GPMDP_Api.Enums;
using Newtonsoft.Json;

namespace GPMDP_Api.Converters
{
    public class ShuffleTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var val = (string)reader.Value;
            switch (val)
            {
                case "ALL_SHUFFLE":
                    return ShuffleType.All;
                case "NO_SHUFFLE":
                    return ShuffleType.None;
                default:
                    return ShuffleType.Unknown;
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var val = (ShuffleType)value;
            switch (val)
            {
                case ShuffleType.All:
                    writer.WriteValue("ALL_SHUFFLE");
                    break;
                case ShuffleType.None:
                    writer.WriteValue("NO_SHUFFLE");
                    break;
                case ShuffleType.Unknown:
                    writer.WriteValue("unknown");
                    break;
            }
        }
    }
}
