using System;
using System.Collections.Generic;
using System.Linq;

namespace EFTHelper.Helpers;

public static class EnumHelper
{
    #region Methods

    public static IEnumerable<TEnum> GetEnumValues<TEnum>()
    {
        if (typeof(TEnum).IsEnum)
        {
            return Enum.GetValues(typeof(TEnum)).Cast<TEnum>();
        }

        return Enumerable.Empty<TEnum>();
    }

    #endregion
}