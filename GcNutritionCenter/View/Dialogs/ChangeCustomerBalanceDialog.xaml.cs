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
    /// Interaktionslogik für ChangeCustomerBalanceDialog.xaml
    /// </summary>
    public partial class ChangeCustomerBalanceDialog : Window
    {
        public decimal? Answer
        {
            get 
            {
                if (Decimal.TryParse(txtInput.Text.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out decimal result))
                    return result;
                else
                    return null;
            }
        }

        public ChangeCustomerBalanceDialog()
        {
            InitializeComponent();
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            txtInput.Focus();
        }

        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            string oldText = txtInput.Text;
            oldText = oldText.Remove(txtInput.SelectionStart, txtInput.SelectionLength);
            string completeTxt = oldText.Insert(txtInput.CaretIndex, e.Text);

            completeTxt = completeTxt.Replace("€", "");

            string pattern = @"^(-|-?[0-9]+|-?[0-9]+[\.\,]|-?[0-9]+[\.\,][0-9][0-9]?)$";
            Regex regex = new Regex(pattern);
            if(!regex.IsMatch(completeTxt))
            {
                e.Handled = true;
            }
        }

        private void txtInput_PreviewDrop(object sender, DragEventArgs e)
        {
            e.Handled = true;
        }

        private void txtInput_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            // Prohibit space
            if (e.Key == Key.Space)
                e.Handled = true;
        }
    }
}
