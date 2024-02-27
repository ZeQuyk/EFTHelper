namespace EFTHelper.Models.TarkovTools;

public class EFTTaskBase
{
    #region Constructors

    //public EFTTaskBase()
    //{
    //    Requirements = new List<EFTTaskRequirement>();
    //    Unlocks = new List<string>();
    //    Reputation = new List<EFTTaskRewardReputation>();
    //    Objectives = new List<EFTTaskObjective>();
    //}

    #endregion

    #region Properties

    public string Id { get; set; }

    //public List<EFTTaskRequirement> Requirements { get; set; }

    //public Trader Giver { get; set; }

    //public Trader Turnin { get; set; }

    public string Title { get; set; }

    //public string WikiLink { get; set; }

    //public int Exp { get; set; }

    //public List<string> Unlocks { get; set; }

    //public List<EFTTaskRewardReputation> Reputation {get;set;}

    //public List<EFTTaskObjective> Objectives { get; set; }

    #endregion
}