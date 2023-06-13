using System.Collections.Generic;

namespace EFTHelper.Models.TarkovTools.Responses;

public class ItemsByTypeResponse
{
    #region Constructors

    public ItemsByTypeResponse()
    {
        ItemsByType = new List<ItemBase>();
    }

    #endregion

    #region Properties

    public List<ItemBase> ItemsByType { get; set; }

    #endregion
}
