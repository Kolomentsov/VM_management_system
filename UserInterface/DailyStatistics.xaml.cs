using System.Windows;
using System.Windows.Controls;
using CHW2.Assets;

namespace UserInterface
{
    /// <summary>
    /// Interaction logic for DailyStatistics.xaml
    /// </summary>
    public partial class DailyStatistics : Window
    {
        public DailyStatistics()
        {
            InitializeComponent();
        }

        private void DatePicker_OnSelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var date = ((DatePicker) sender).DisplayDate.Date;
            var abs = new Helper().GetTerminalsByProfit(date);
            DataGrid.ItemsSource = abs;
        }
    }
}
