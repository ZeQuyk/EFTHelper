using EFTHelper.Extensions;
using EFTHelper.Models.TarkovTools;

namespace EFTHelper.ViewModels
{
    public class RequirementViewModel
    {
        #region Constructors

        public RequirementViewModel(Requirement requirement)
        {
            Type = requirement.Type.ToSentence();
            Value = requirement.Value;
        }

        #endregion

        #region Properties

        public string Type { get; set; }

        public int Value { get; set; }

        #endregion
    }
}
