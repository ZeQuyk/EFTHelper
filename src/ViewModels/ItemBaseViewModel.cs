using System;
using System.Collections.Generic;
using System.Linq;
using Caliburn.Micro;
using EFTHelper.Enums;
using EFTHelper.Extensions;
using EFTHelper.Models.TarkovTools;

namespace EFTHelper.ViewModels
{
    public class ItemBaseViewModel: Screen
    {
        public ItemBaseViewModel(ItemBase item)
        {
            Id= item.Id;
            Name= item.Name;
            ShortName= item.ShortName;
            IconLink= item.IconLink;
            BasePrice= item.BasePrice;
            Types = item.Types.Select(x => Enum.Parse<ItemTypes>(x, ignoreCase: true)).ToList();
        }

        #region Properties

        public string Id { get; set; }

        public string Name { get; set; }

        public string ShortName { get; set; }

        public string IconLink { get; set; }

        public long BasePrice { get; set; }

        public List<ItemTypes> Types { get; set; }

        public string TypesValue => string.Join(", ", Types.Select(x => x.ToString().ToSentence()));

        #endregion
    }
}
