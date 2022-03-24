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
    /// Логика взаимодействия для ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Page
    {
        public ProductPage(int flag)
        {
            InitializeComponent();
            ComboDisc.SelectedIndex = 0;
            ComboSort.SelectedIndex = 0;
            Update();
        }
        public void Update()
        {
            var allProd = Manager.GetContext().Service.Count();
            var currentProd = Manager.GetContext().Service.ToList();
            switch (ComboDisc.SelectedIndex)
            {
                case 0:
                    currentProd = Manager.GetContext().Service.ToList();
                    break;
                case 1:
                    currentProd = currentProd.Where(p => p.Discount < 5 && p.Discount >= 0).ToList();
                    break;
                case 2:
                    currentProd = currentProd.Where(p => p.Discount < 15 && p.Discount >= 5).ToList();
                    break;
                case 3:
                    currentProd = currentProd.Where(p => p.Discount < 30 && p.Discount >= 15).ToList();
                    break;
                case 4:
                    currentProd = currentProd.Where(p => p.Discount < 70 && p.Discount >= 30).ToList();
                    break;
                case 5:
                    currentProd = currentProd.Where(p => p.Discount < 100 && p.Discount >= 70).ToList();
                    break;
            }
            currentProd = currentProd.Where(p => p.Title.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();
            switch (ComboSort.SelectedIndex)
            {
                case 0:
                    LViewProd.ItemsSource = currentProd;
                    break;
                case 1:
                    LViewProd.ItemsSource = currentProd.OrderBy(p => p.CostSale).ToList();
                    break;
                case 2:
                    LViewProd.ItemsSource = currentProd.OrderByDescending(p => p.CostSale).ToList();
                    break;
            }
            LabCount.Content = currentProd.Count() + " из " + allProd.ToString();
        }
        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Update();
        }

        private void ComboManuf_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void ComboSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void LViewProd_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage(LViewProd.SelectedItem as Service));
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(Visibility == Visibility.Visible)
            {
                Manager.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                LViewProd.ItemsSource = Manager.GetContext().Service.ToList();
                Update();
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage(null));
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var removeProd = LViewProd.SelectedItems.Cast<Service>().ToList();
            if(MessageBox.Show($"Точно хотите удалить?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    Manager.GetContext().Service.RemoveRange(removeProd);
                    Manager.GetContext().SaveChanges();
                    MessageBox.Show("Удалено");
                    Update();
                }
                catch
                {
                    MessageBox.Show("Ошибка");
                }
            }
            if (MessageBox.Show($"Точно хотите удалить?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
            {
                try
                {
                    MessageBox.Show("Удаление отменено");
                }
                catch
                {
                    MessageBox.Show("Ошибка");
                }
            }
        }

        private void BtnSale_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddServiceClient(LViewProd.SelectedItem as Service));
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage((sender as Button).DataContext as Service));
        }

        private void BtnServiceNow_Click(object sender, RoutedEventArgs e)
        {
            ClientServiceNow clNow = new ClientServiceNow();
            clNow.Show();
        }
    }
}
