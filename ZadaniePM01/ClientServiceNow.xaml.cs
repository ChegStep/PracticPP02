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
using System.Windows.Threading;

namespace ZadaniePM01
{
    /// <summary>
    /// Логика взаимодействия для ClientServiceNow.xaml
    /// </summary>
    public partial class ClientServiceNow : Window
    {
        public ClientServiceNow()
        {
            InitializeComponent();
            Update();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }
        private int inc = 30;
        private void timer_Tick(object sender, EventArgs e)
        {
            inc--;
            if (inc == 0)
            {
                Manager.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                inc = 30;
                Update();
            }
            txtReboot.Text = inc.ToString();
        }
        public void Update()
        {
            var currentProd = Manager.GetContext().ClientService.ToList();
            currentProd = currentProd.Where(p => p.StartTime >= DateTime.Now && p.StartTime < DateTime.Now.AddDays(2)).ToList();
            LViewProd.ItemsSource  = currentProd.OrderBy(p => p.StartTime).ToList();
        }
    }
}
