using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ZadaniePM01
{
    /// <summary>
    /// Логика взаимодействия для ProductsSales.xaml
    /// </summary>
    public partial class ProductsSales : Page
    {
        private ProductSale _prodSale = new ProductSale();
        private Product selProd = new Product();
        public ProductsSales(Product selProd)
        {
            InitializeComponent();
            var currentSale = Manager.GetContext().ProductSale.ToList();
            var comboSales = Manager.GetContext().Product.ToList();
            comboSales.Insert(0, new Product
            {
                Title = "Все товары"
            });
            ComboSale.ItemsSource = comboSales;
            if (selProd != null)
            {
                currentSale = currentSale.Where(p => p.ProductID == selProd.ID).ToList();
                ComboSale.SelectedIndex = selProd.ID;
            }
            else
            {
                ComboSale.SelectedIndex = 0;
            }
            DGridSales.ItemsSource = currentSale.OrderByDescending(p => p.SaleDate).ToList();
        }
        private void Update()
        {

        }
        private void ComboSale_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboSale.SelectedIndex > 0)
                comboLabel.Content = ComboSale.SelectedIndex;
            DGridSales.ItemsSource = Manager.GetContext().ProductSale.ToList().Where(p => p.Product.Title == (ComboSale.SelectedItem as Product).Title).ToList();

        }
    }
}
