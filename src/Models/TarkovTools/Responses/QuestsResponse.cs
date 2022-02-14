using System.Collections.Generic;

namespace EFTHelper.Models.TarkovTools.Responses
{
    public class QuestsResponse
    {
        public QuestsResponse()
        {
            Quests = new List<EFTTaskBase>();
        }

        public List<EFTTaskBase> Quests { get; set; }
    }
}
