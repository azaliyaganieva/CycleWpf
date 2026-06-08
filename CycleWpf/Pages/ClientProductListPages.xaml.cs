using CycleWpf.DBCon;
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

namespace CycleWpf.Pages
{
    /// <summary>
    /// Логика взаимодействия для ClientProductListPages.xaml
    /// </summary>
    public partial class ClientProductListPages : Page
    {
        public static List<Product> products { get; set; }
        public static List<Supplier> suppliers { get; set; }
        public ClientProductListPages(User user)
        {
            InitializeComponent();
            products = new List<Product>(Connection.cycle.Product.ToList());
            suppliers = new List<Supplier>(Connection.cycle.Supplier.ToList());
           
            FIOTb.Text = $"Пользователь:{user.FIO}";
            this.DataContext = this;
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
