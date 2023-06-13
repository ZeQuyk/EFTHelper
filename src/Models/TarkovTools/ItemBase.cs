using System.Collections.Generic;

namespace EFTHelper.Models.TarkovTools;

/// <summary>
/// ItemBase is a smaller version of item. To be used when displaying lists of items.
/// </summary>
public class ItemBase
{
    #region Constructors

    public ItemBase()
    {
        Types = new List<string>();
    }

    #endregion

    #region Properties

    public string Id { get; set; }

    public string Name { get; set; }

    public string ShortName { get; set; }

    public string IconLink { get; set; }

    public long BasePrice { get; set; }

    public List<string> Types { get; set; }

    #endregion
}
