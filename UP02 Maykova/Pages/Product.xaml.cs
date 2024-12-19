using System;
using System.Collections.Generic;
using System.Data;
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

namespace UP02_Maykova.Pages
{
    /// <summary>
    /// Логика взаимодействия для Product.xaml
    /// </summary>
    public partial class Product : Page
    {
        public Product(Partners p)
        {
            InitializeComponent();
            DataGridRealise.ItemsSource = Entities.GetContext().Partner_products.Where(x => x.ID_Partners == p.ID_partner).ToList();
            Partner.Text = p.Name;
        }
    }
}
