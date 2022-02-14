using System.Collections.Generic;

namespace EFTHelper.Models.TarkovTools
{
    public class EFTTaskRequirement
    {
        public int Level { get; set; }

        public List<int> Quests { get; set; }

        public List<EFTTaskBase> PrerequisiteQuests { get; set; }
    }
}