using System.Collections.Generic;

namespace EFTHelper.Models.TarkovTools;

public class EFTTaskRequirement
{
    #region Constructors

    public EFTTaskRequirement()
    {
        Quests = new List<int>();
        PrerequisiteQuests = new List<EFTTaskBase>();
    }

    #endregion

    #region Properties

    public int Level { get; set; }

    public List<int> Quests { get; set; }

    public List<EFTTaskBase> PrerequisiteQuests { get; set; }

    #endregion
}