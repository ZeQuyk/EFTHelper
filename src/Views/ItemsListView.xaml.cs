using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace EFTHelper.Views;

/// <summary>
/// Interaction logic for ItemsListView.xaml
/// </summary>
public partial class ItemsListView : UserControl
{
    public ItemsListView()
    {
        InitializeComponent();
    }

    private void Query_Loaded(object sender, System.Windows.RoutedEventArgs e)
    {
        Dispatcher.BeginInvoke(DispatcherPriority.Input, new Action(delegate ()
        {
            Keyboard.Focus(Query);
        }));
    }
}
