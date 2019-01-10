using System;
using System.Collections.Generic;
using System.Text;
using GPMDP_Api.Enums;
using Newtonsoft.Json;

namespace GPMDP_Api.Converters
{
    public class RepeatTypeConverter : JsonConverter
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
                case "LIST_REPEAT":
                    return RepeatType.List;
                case "SINGLE_REPEAT":
                    return RepeatType.Single;
                case "NO_REPEAT":
                    return RepeatType.None;
                default:
                    return RepeatType.Unknown;
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var val = (RepeatType)value;
            switch (val)
            {
                case RepeatType.List:
                    writer.WriteValue("LIST_REPEAT");
                    break;
                case RepeatType.Single:
                    writer.WriteValue("SINGLE_REPEAT");
                    break;
                case RepeatType.None:
                    writer.WriteValue("NO_REPEAT");
                    break;
                case RepeatType.Unknown:
                    writer.WriteValue("unknown");
                    break;
            }
        }
    }
}
