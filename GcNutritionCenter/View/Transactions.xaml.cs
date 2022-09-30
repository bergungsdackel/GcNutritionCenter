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

namespace GcNutritionCenter
{
    /// <summary>
    /// Interaktionslogik für Transactions.xaml
    /// </summary>
    public partial class Transactions : UserControl
    {
        public Transactions()
        {
            InitializeComponent();
        }

        // TODO: This in MVVM-Style
        private void OnLostFocus(object sender, RoutedEventArgs e)
        {
            if (sender is DataGrid)
            {
                DataGrid grid = (DataGrid)sender;
                grid!.UnselectAll();
            }
        }
    }
}
