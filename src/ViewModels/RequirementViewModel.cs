using System;
using Caliburn.Micro;
using EFTHelper.Enums;
using EFTHelper.Extensions;
using EFTHelper.Helpers;
using EFTHelper.Models.TarkovTools;

namespace EFTHelper.ViewModels;

public class RequirementViewModel
{
    #region Constructors

    public RequirementViewModel(Requirement requirement)
    {
        Type = requirement.Type;
        Value = requirement.Value;
        DisplayValue = BuildDisplayValue();
    }

    #endregion

    #region Properties

    public string Type { get; set; }

    public int Value { get; set; }

    public string DisplayValue { get; set; }

    #endregion

    #region Methods

    private string BuildDisplayValue()
    {
        var isRequirementType = Enum.TryParse<RequirementTypes>(Type, true, out var type);
        if (isRequirementType && type == RequirementTypes.QuestCompleted)
        {
            var eftTask = IoC.Get<EFTTasksHelper>().GetTask(Value.ToString()).Result;
            if (eftTask != null)
            {
                return $"- Task completed: \"{eftTask.Title}\"";
            }
        }

        return $"- {Type.ToSentence()} {Value}";
    }

    #endregion
}
