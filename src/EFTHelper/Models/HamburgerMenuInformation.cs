using System.Collections.Generic;

namespace EFTHelper.Models;

public class HamburgerMenuInformation
{
    public IEnumerable<IMenuItem> Items { get; set; }

    public string Header { get; set; }

    public IMenuItem SelectedItem { get; set; }
}
