using Microsoft.Extensions.Configuration;
using Q1.Entity;
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

namespace Q1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PE_PRN_Fall22B1Context context = new PE_PRN_Fall22B1Context();

        List<Director> directors = new List<Director>();

        public MainWindow()
        {
            InitializeComponent();

            rbMale.IsChecked = true;

            lvEmployees.ItemsSource = directors.ToList();
        }

        public void Reload()
        {
            lvEmployees.ItemsSource = directors.ToList();

            rbMale.IsChecked = true;
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            Director d = new Director();
            d.FullName = txtDirectorName.Text;
            d.Male = rbMale.IsChecked.Value;
            d.Dob = dpDob.SelectedDate.Value;
            string[] nation = cbNationality.SelectedItem.ToString().Split(" ");
            d.Nationality = nation[1];
            d.Description = txtDescription.Text;

            directors.Add(d);

            Reload();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            foreach(var d in directors)
            {
                d.Id = 0;
                context.Directors.Add(d);
                context.SaveChanges();
            }

            directors = new List<Director>();

            Reload();
        }
    }
}
