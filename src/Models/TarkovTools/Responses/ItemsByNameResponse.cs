using System.Collections.Generic;

namespace EFTHelper.Models.TarkovTools.Responses
{
    public class ItemsByNameResponse<TItem>
        where TItem : ItemBase
    {
        #region Constructors

        public ItemsByNameResponse()
        {
            ItemsByName = new List<TItem>();
        }

        #endregion

        #region Properties

        public List<TItem> ItemsByName { get; set; }

        #endregion
    }
}