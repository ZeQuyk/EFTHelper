using System.Collections.Generic;

namespace EFTHelper.Models.TarkovTools.Responses
{
    public class ItemsByNameResponse
    {
        #region Constructors

        public ItemsByNameResponse()
        {
            ItemsByName = new List<ItemBase>();
        }

        #endregion

        #region Properties

        public List<ItemBase> ItemsByName { get; set; }

        #endregion
    }
}
