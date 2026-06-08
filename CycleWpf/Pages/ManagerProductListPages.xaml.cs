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
    /// Логика взаимодействия для ManagerProductListPages.xaml
    /// </summary>
    public partial class ManagerProductListPages : Page
    {
        public static List<Product> products { get; set; }
        public static List<Supplier> suppliers { get; set; }
        public static Supplier currentSupplier = null;
        public static string currentSort = "Все поставщики";
        public static string searchText = "";
        public ManagerProductListPages(User user)
        {
            InitializeComponent();
            products = new List<Product>(Connection.cycle.Product.ToList());
            suppliers = new List<Supplier>(Connection.cycle.Supplier.ToList());
            suppliers.Insert(0, new Supplier { Id = -1, Name = "Все поставщики" });
            FIOTb.Text = $"Менеджер:{user.FIO}";
            this.DataContext = this;
        }
        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void SortCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selecteItem = SortCmb.SelectedItem as ComboBoxItem;
            if (selecteItem != null)
            {
                currentSort = selecteItem.Content.ToString();
                ApplyFilter();
            }

        }

        private void SupplierCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            currentSupplier = SupplierCmb.SelectedItem as Supplier;
            ApplyFilter();
        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            searchText = SearchTb.Text;
            ApplyFilter();

        }

        private void ApplyFilter()
        {
            List<Product> filterprod = products;
            if (currentSupplier != null && currentSupplier.Id != -1)
            {
                filterprod = filterprod.Where(x => x.IdSupplier == currentSupplier.Id).ToList();
            }
            else filterprod = products;

            if (currentSort == "По возрастанию")
            {
                filterprod = filterprod.OrderBy(x => x.WorkShopCount).ToList();
            }
            else if (currentSort == "По убыванию")
            {
                filterprod = filterprod.OrderByDescending(x => x.WorkShopCount).ToList();
            }
            else filterprod = filterprod;


            filterprod = filterprod.Where(x => x.Name.ToLower().Contains(SearchTb.Text.ToLower()) ||
                x.Manufacturer.Name.ToLower().Contains(SearchTb.Text.ToLower()) ||
                x.Supplier.Name.ToLower().Contains(SearchTb.Text.ToLower()) ||
                x.ProductCategory.Name.ToLower().Contains(SearchTb.Text.ToLower()) ||
                x.Description.ToLower().Contains(SearchTb.Text.ToLower())).ToList();


            productLv.ItemsSource = filterprod;




        }
    }
}

