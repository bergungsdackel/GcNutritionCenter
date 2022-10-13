using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GcNutritionCenter
{
    // TODO: This but MVVM-Style

    /// <summary>
    /// Interaktionslogik für AddCustomerDialog.xaml
    /// </summary>
    public partial class AddCustomerDialog : Window
    {
        /// <summary>
        /// Customer first name and last name
        /// </summary>
        /// <returns>
        /// A tuple containing the following information:
        /// <list type="bullet">
        /// <item>First name.</item>
        /// <item>Last name.</item>
        /// </list>
        /// </returns>
        public (string, string) Answer
        {
            get
            {
                return (txtInputFirstName.Text.Trim(), txtInputLastName.Text.Trim());
            }            
        }

        public AddCustomerDialog()
        {
            InitializeComponent();
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            txtInputFirstName.Focus();
        }

        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
