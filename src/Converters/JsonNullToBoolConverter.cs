using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EFTHelper.Converters
{
    class JsonNullToBoolConverter : JsonConverter<bool>
    {
        #region Methods

        public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            try
            {
                var result = reader.GetBoolean();
                return result;
            }
            catch
            {
                return default(bool);
            }
        }

        public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options)
        {
            writer.WriteBooleanValue(value);
        }

        #endregion
    }
}
