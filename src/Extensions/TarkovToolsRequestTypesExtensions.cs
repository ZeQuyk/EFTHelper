using EFTHelper.Enums;

namespace EFTHelper.Extensions
{
    public static class TarkovToolsRequestTypesExtensions
    {
        #region Methods

        public static string AssociatedFilterName(this TarkovToolsRequestTypes type)
        {
            switch (type)
            {
                case TarkovToolsRequestTypes.ItemsByName:
                    return "name";
                case TarkovToolsRequestTypes.ItemsByType:
                    return "type";
                case TarkovToolsRequestTypes.Item:
                    return "id";
                default:
                    return string.Empty;
            }
        }

        #endregion
    }
}
