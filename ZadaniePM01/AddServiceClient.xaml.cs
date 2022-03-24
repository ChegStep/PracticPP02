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
    /// Логика взаимодействия для AddServiceClient.xaml
    /// </summary>
    public partial class AddServiceClient : Page
    {
        private Service _currentProd = new Service();
        private ClientService _clientServ = new ClientService();
        public AddServiceClient(Service _selProd)
        {
            InitializeComponent();
            var clientList = Manager.GetContext().Client.ToList();
            _currentProd = _selProd;
            ComboName.ItemsSource = clientList;
            DataContext = _currentProd;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string time = datePicker1.ToString();
                time = time.Replace("0:00:00", TimePicker.Text);
                _clientServ.ServiceID = _currentProd.ID;
                _clientServ.ClientID = ComboName.SelectedIndex + 1;
                _clientServ.StartTime = Convert.ToDateTime(time);
                Manager.GetContext().ClientService.Add(_clientServ);
                Manager.GetContext().SaveChanges();
                MessageBox.Show("Готово!");
                Manager.MainFrame.GoBack();
            }
            catch
            {
                MessageBox.Show("Ошибка");
            }
        }
    }
}
