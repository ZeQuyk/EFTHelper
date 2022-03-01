using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;
using EFTHelper.Enums;
using EFTHelper.Extensions;
using EFTHelper.Helpers;
using EFTHelper.Models.TarkovTools;

namespace EFTHelper.ViewModels
{
    public class ItemDetailViewModel: PropertyChangedBase
    {
        #region Constructors

        public ItemDetailViewModel(Item item)
        {
            Id = item.Id;
            Name = item.Name;
            ShortName = item.ShortName;
            IconLink = item.IconLink;
            BasePrice = $"{item.BasePrice.ToString("N0")}{CurrencyHelper.GetCurrencySymbol("RUB")}";
            Types = item.Types.ToItemTypes();
            GridImageLink = item.GridImageLink;
            ImageLink = item.ImageLink;
            WikiLink = item.WikiLink;
            Link = item.Link;
            NormalizedName = item.NormalizedName;
            AccuracyModifier = item.AccuracyModifier;
            RecoilModifier = item.RecoilModifier;
            ErgonomicsModifier = item.ErgonomicsModifier;
            HasGrid = item.HasGrid;
            BlocksHeadphones = item.BlocksHeadphones;
            BuyFor = item.BuyFor.Select(x => new TransactionInformationViewModel(x)).OrderBy(x => x.GetPriceInRoubles()).ToList();
            SellFor = item.SellFor.Select(x => new TransactionInformationViewModel(x)).OrderByDescending(x => x.GetPriceInRoubles()).ToList();
            BuyFor.RemoveAll(x => x.Price == 0);
            SellFor.RemoveAll(x => x.Price == 0);
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

        public string GridImageLink { get; set; }

        public string ImageLink { get; set; }

        public string WikiLink { get; set; }

        public string Link { get; set; }

        public string NormalizedName { get; set; }

        public List<TransactionInformationViewModel> BuyFor { get; set; }

        public bool CanBuy => BuyFor != null && BuyFor.Any();

        public bool CanNotBuy => !CanBuy;

        public List<TransactionInformationViewModel> SellFor { get; set; }

        public bool CanSell => SellFor != null && SellFor.Any();

        public bool CanNotSell => !CanSell;

        public int AccuracyModifier { get; set; }

        public int RecoilModifier { get; set; }

        public int ErgonomicsModifier { get; set; }

        public bool HasGrid { get; set; }

        public bool BlocksHeadphones { get; set; }

        public bool HasWikiLink => !string.IsNullOrEmpty(WikiLink);

        #endregion

        #region Methods

        public void OpenWiki()
        {
            ProcessHelper.StartProcess(WikiLink);
        }

        #endregion
    }
}
