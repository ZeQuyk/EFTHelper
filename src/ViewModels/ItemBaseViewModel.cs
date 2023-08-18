using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;
using EFTHelper.Enums;
using EFTHelper.Extensions;
using EFTHelper.Helpers;
using EFTHelper.Models.TarkovTools;

namespace EFTHelper.ViewModels;

public class ItemBaseViewModel : Screen
{
    #region Fields

    private readonly System.Action<ItemBaseViewModel> _action;

    #endregion

    #region Constructors

    public ItemBaseViewModel(ItemBase item, System.Action<ItemBaseViewModel> action)
    {
        Id = item.Id;
        Name = item.Name;
        ShortName = item.ShortName;
        IconLink = item.IconLink;
        BasePrice = $"{item.BasePrice.ToString("N0")} {CurrencyHelper.GetCurrencySymbol("RUB")}";
        Types = item.Types.ToItemTypes();
        _action = action;
    }

    #endregion

    #region Properties

    public string Id { get; set; }

    public string Name { get; set; }

    public string ShortName { get; set; }

    public string IconLink { get; set; }

    public string BasePrice { get; set; }

    public List<ItemTypes> Types { get; set; }

    public string TypesValue => string.Join(", ", Types.Select(x => x.ToString().ToSentence()));

    #endregion

    #region Methods

    public void OnItemClicked()
    {
        _action?.Invoke(this);
    }

    #endregion
}
