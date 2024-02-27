using EFTHelper.Enums;

namespace EFTHelper.Extensions;

public static class TarkovToolsRequestTypesExtensions
{
    #region Methods

    public static string AssociatedFilterName(this TarkovToolsRequestTypes type)
    {
        return type switch
        {
            TarkovToolsRequestTypes.ItemsByName => "name",
            TarkovToolsRequestTypes.ItemsByType => "type",
            TarkovToolsRequestTypes.Item => "id",
            _ => string.Empty,
        };
    }

    #endregion
}
