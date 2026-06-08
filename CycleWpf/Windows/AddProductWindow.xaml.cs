using CycleWpf.DBCon;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace CycleWpf.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddProductWindow.xaml
    /// </summary>
    public partial class AddProductWindow : Window
    {
        private string selectedPhotoPath = null;
        public AddProductWindow()
        {
            InitializeComponent();
            LoadComboBoxes();


        }

        private void LoadComboBoxes()
        {
            CategoryCmb.ItemsSource = Connection.cycle.ProductCategory.ToList();
            ManufacturerCmb.ItemsSource = Connection.cycle.Manufacturer.ToList();
            SupplierCmb.ItemsSource = Connection.cycle.Supplier.ToList();
            UnitCmb.ItemsSource = Connection.cycle.Unit.ToList();
        }

        private void LoadPhotoBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog=new OpenFileDialog();
            openFileDialog.Title = "Выберите фото";
            if (openFileDialog.ShowDialog() == true)
            {
                selectedPhotoPath = openFileDialog.FileName;
                PhotoImage.Source=new BitmapImage(new Uri(selectedPhotoPath));
            }
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string photoName = null;

                if (!string.IsNullOrWhiteSpace(selectedPhotoPath))
                {
                    // ПРОСТО КОПИРУЕМ В ПАПКУ С ПРОГРАММОЙ
                    string fileName = System.IO.Path.GetFileName(selectedPhotoPath);
                    string destPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
                    File.Copy(selectedPhotoPath, destPath, true);
                    photoName = fileName;  // Сохраняем только имя файла
                }

                Product newProduct = new Product()
                {
                    Name = NameTxb.Text,
                    Description = DescriptionTxb.Text,
                    Price = Convert.ToDouble(PriceTxb.Text),
                    WorkShopCount = Convert.ToInt32(CountTxb.Text),
                    ActiveDiscount = Convert.ToInt32(DiscountTxb.Text),
                    IdProductCategory = (CategoryCmb.SelectedItem as ProductCategory).Id,
                    IdManufacturer = (ManufacturerCmb.SelectedItem as Manufacturer).Id,
                    IdSupplier = (SupplierCmb.SelectedItem as Supplier).Id,
                    IdUnit = (UnitCmb.SelectedItem as Unit).Id,
                    Photo = photoName
                };

                Connection.cycle.Product.Add(newProduct);
                Connection.cycle.SaveChanges();
                MessageBox.Show("Товар добавлен!", "Успех");
                this.DialogResult = true;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
