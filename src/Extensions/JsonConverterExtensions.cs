using System.Collections.Generic;
using System.Text.Json.Serialization;
using EFTHelper.Converters;

namespace EFTHelper.Extensions
{
    public static class JsonConverterExtensions
    {
        #region Methods

        public static void AddCustomConverters(this IList<JsonConverter> jsonConverters)
        {
            jsonConverters.Add(new JsonNullToIntConverter());
            jsonConverters.Add(new JsonNullToBoolConverter());
        }

        #endregion
    }
}
