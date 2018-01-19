using System.Windows;
using CHW2.Assets;
using CHW2.Models;

namespace UserInterface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var helper = new Helper();
            helper.Init();

            DataGrid.ItemsSource = helper.GetTerminals();
           
        }
        private void ButtonStatistics_Click(object sender, RoutedEventArgs e)
        {
            var ds = new DailyStatistics();
            ds.Show();
        }

        private void ButtonEmptyTerminals_Click(object sender, RoutedEventArgs e)
        {
            var et = new EmptyTerminals();
            et.Show();
        }

        private void ButtonBadSelling_Click(object sender, RoutedEventArgs e)
        {
            var bsp = new BadSellingProducts();
            bsp.Show();
        }

        private void DataGrid_OnVM(object sender, System.Windows.Controls.DataGridCellEditEndingEventArgs e)
        {
            var terminal = (Terminal)((System.Windows.Controls.DataGrid)sender).CurrentCell.Item;
            var newValue = ((System.Windows.Controls.TextBox)e.EditingElement).Text;

            if (terminal != null)
                new Helper().UpdateTerminalAdress(terminal.Id, newValue);
        }
    }
}
