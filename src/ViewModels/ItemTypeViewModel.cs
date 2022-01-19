using Caliburn.Micro;
using EFTHelper.Enums;
using EFTHelper.Extensions;
using EFTHelper.Models;

namespace EFTHelper.ViewModels
{
    public class ItemTypeViewModel : PropertyChangedBase, IMenuItem
    {
        #region Fields

        private ItemTypes _itemType;

        #endregion

        #region Constructors

        public ItemTypeViewModel(ItemTypes itemType)
        {
            _itemType = itemType;
        }

        #endregion

        #region Properties

        public string Label => _itemType.ToString().ToSentence();

        public string Title => _itemType.ToString().ToSentence();

        public string Icon { get; }

        public ItemTypes ItemType => _itemType;

        #endregion
    }
}
