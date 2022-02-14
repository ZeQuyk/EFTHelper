using System.Collections.Generic;

namespace EFTHelper.Models.TarkovTools
{
    public class EFTTaskObjective
    {
        #region Constructors

        public EFTTaskObjective()
        {
            Target = new List<string>();
        }

        #endregion

        #region Properties

        public string Id { get; set; }

        public string Type { get; set; }

        public List<string> Target { get; set; }

        public ItemBase TargetItem { get; set; }

        public int Number { get; set; }

        public string Location { get; set; }

        #endregion
    }
}