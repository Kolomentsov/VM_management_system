using System.Windows;
using CHW2.Assets;

namespace UserInterface
{
    /// <summary>
    /// Interaction logic for BadSellingProducts.xaml
    /// </summary>
    public partial class BadSellingProducts : Window
    {
        public BadSellingProducts()
        {
            InitializeComponent();
            
            DataGrid.ItemsSource = new Helper().GetLeastSoldProducts();
        }
    }
}
