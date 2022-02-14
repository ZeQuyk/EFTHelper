using System.Collections.Generic;

namespace EFTHelper.Models.TarkovTools.Responses
{
    public class QuestsResponse
    {
        #region Constructors

        public QuestsResponse()
        {
            Quests = new List<EFTTaskBase>();
        }

        #endregion

        #region Properties

        public List<EFTTaskBase> Quests { get; set; }

        #endregion
    }
}
