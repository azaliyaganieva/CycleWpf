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
    /// Логика взаимодействия для AuthPages.xaml
    /// </summary>
    public partial class AuthPages : Page
    {
        public static List<User> users {  get; set; }
        public AuthPages()
        {
            InitializeComponent();
        }

        private void AuthBtn_Click(object sender, RoutedEventArgs e)
        {
            string login=LoginTb.Text.Trim();
            string password=PasswordTb.Password.Trim();
            users=new List<User>(Connection.cycle.User.ToList());
            User currenttUser = users.FirstOrDefault(x => x.Login.Trim() == login && x.Password.Trim() == password);
            if (currenttUser != null)
            {
                MessageBox.Show("Авторизация прошла успешно");
                if (currenttUser.IdRoleEmployee == 1)
                {
                    NavigationService.Navigate(new AdminProductListPages(currenttUser));
                }
                else if (currenttUser.IdRoleEmployee== 2)
                {
                    NavigationService.Navigate(new ManagerProductListPages(currenttUser));
                }
               else if(currenttUser.IdRoleEmployee == 3)
                {
                    NavigationService.Navigate(new ClientProductListPages(currenttUser));
                }
                else
                {
                    MessageBox.Show("авторизация прошла неуспешно");
                }
            }

        }

        private void GuestBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new GuestProductListPages());
        }
    }
}
