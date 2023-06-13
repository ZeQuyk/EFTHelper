using System.Collections.Generic;

namespace EFTHelper.Models.TarkovTools;

/// <summary>
/// To be used when querying a product by id.
/// </summary>
public class Item : ItemBase
{
    #region Constructors

    public Item()
    {
        BuyFor = new List<TransactionInformation>();
        SellFor = new List<TransactionInformation>();
    }

    #endregion

    #region Properties

    public string GridImageLink { get; set; }

    public string ImageLink { get; set; }

    public string WikiLink { get; set; }

    public string Link { get; set; }

    public string NormalizedName { get; set; }

    public List<TransactionInformation> BuyFor { get; set; }

    public List<TransactionInformation> SellFor { get; set; }

    public int AccuracyModifier { get; set; } = 0;

    public int RecoilModifier { get; set; } = 0;

    public int ErgonomicsModifier { get; set; } = 0;

    public bool HasGrid { get; set; } = false;

    public bool BlocksHeadphones { get; set; } = false;

    #endregion
}
