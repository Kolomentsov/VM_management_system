using System.Windows;
using CHW2.Assets;
using System.Windows.Controls;
using CHW2.Models;

namespace UserInterface
{
    /// <summary>
    /// Interaction logic for EmptyTerminals.xaml
    /// </summary>
    public partial class EmptyTerminals : Window
    {
       public EmptyTerminals()
        {

            InitializeComponent();
            DataGrid.ItemsSource = new Helper().GetTerminalsWithEmptyItems();
        }

        private void DataGrid_OnCellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            var terminal = (Terminal) ((DataGrid) sender).CurrentCell.Item;
            var newValue = ((TextBox) e.EditingElement).Text;

            if (terminal != null)
                new Helper().UpdateTerminalAdress(terminal.Id, newValue);
        }
    }
}
