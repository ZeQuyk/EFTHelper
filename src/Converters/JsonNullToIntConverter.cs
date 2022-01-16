using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EFTHelper.Converters
{
    class JsonNullToIntConverter : JsonConverter<int>
    {
        #region Methods

        public override int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            try
            {
                var isRead = reader.TryGetInt32(out var value);
                return isRead ? value : default(int);
            }
            catch
            {
                return default(int);
            }
        }

        public override void Write(Utf8JsonWriter writer, int value, JsonSerializerOptions options)
        {
             writer.WriteNumberValue(value);
        }

        #endregion
    }
}
