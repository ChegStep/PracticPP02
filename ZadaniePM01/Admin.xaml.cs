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
using System.Windows.Shapes;

namespace ZadaniePM01
{
    /// <summary>
    /// Логика взаимодействия для Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        public int flag;
        public Admin()
        {
            InitializeComponent();

        }
        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            
            if (txtLogin.Text == "0000")
            {
                MessageBox.Show("Успешно");
                MainWindow main = new MainWindow();
                main.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Пароль неверный");
                MainWindow main = new MainWindow();
                main.Show();
                this.Hide();
            }
        }
    }
}
