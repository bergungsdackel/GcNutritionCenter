using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
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
    /// <summary>
    /// Interaktionslogik für CustomDialog.xaml
    /// </summary>
    public partial class CustomDialog : Window
    {
        public enum InputType
        {
            Ok = 0,
            OkCancel = 1,
            InputOkCancel = 2,
            YesNo = 3,
            Customer = 4 // special case
        }

        //private enum ButtonContent
        //{
        //    [Description("OK")]
        //    Ok = 1,
        //    [Description("Abbrechen")]
        //    Cancel = 2,
        //    [Description("Ja")]
        //    Yes = 3,
        //    [Description("Nein")]
        //    No = 4
        //}

        public CustomDialog(string question, InputType inputType = InputType.OkCancel)
        {
            InitializeComponent();

            txtInputBalance.Text = String.Empty;
            switch (inputType)
            {
                case InputType.Ok:
                    btnDialogOkYes.Content = "OK";
                    txtInputBalance.Visibility = Visibility.Collapsed;
                    txtInputFirstName.Visibility = Visibility.Collapsed;
                    txtInputLastName.Visibility = Visibility.Collapsed;
                    btnDialogOkYes.Visibility = Visibility.Visible;
                    btnDialogCancelNo.Visibility = Visibility.Collapsed;
                    break;
                case InputType.OkCancel:
                    btnDialogOkYes.Content = "OK";
                    btnDialogCancelNo.Content = "Abbrechen";
                    txtInputBalance.Visibility = Visibility.Collapsed;
                    txtInputFirstName.Visibility = Visibility.Collapsed;
                    txtInputLastName.Visibility = Visibility.Collapsed;
                    btnDialogOkYes.Visibility = Visibility.Visible;
                    btnDialogCancelNo.Visibility = Visibility.Visible;
                    break;
                case InputType.InputOkCancel:
                    btnDialogOkYes.Content = "OK";
                    btnDialogCancelNo.Content = "Abbrechen";
                    txtInputBalance.Visibility = Visibility.Visible;
                    txtInputFirstName.Visibility = Visibility.Collapsed;
                    txtInputLastName.Visibility = Visibility.Collapsed;
                    btnDialogOkYes.Visibility = Visibility.Visible;
                    btnDialogCancelNo.Visibility = Visibility.Visible;
                    break;
                case InputType.YesNo:
                    btnDialogOkYes.Content = "Ja";
                    btnDialogCancelNo.Content = "Nein";
                    txtInputBalance.Visibility = Visibility.Collapsed;
                    txtInputFirstName.Visibility = Visibility.Collapsed;
                    txtInputLastName.Visibility = Visibility.Collapsed;
                    btnDialogOkYes.Visibility = Visibility.Visible;
                    btnDialogCancelNo.Visibility = Visibility.Visible;
                    break;
                case InputType.Customer:
                    btnDialogOkYes.Content = "OK";
                    btnDialogCancelNo.Content = "Abbrechen";
                    txtInputBalance.Visibility = Visibility.Collapsed;
                    txtInputFirstName.Visibility = Visibility.Visible;
                    txtInputLastName.Visibility = Visibility.Visible;
                    btnDialogOkYes.Visibility = Visibility.Visible;
                    btnDialogCancelNo.Visibility = Visibility.Visible;
                    break;
                default:
                    break;
            }
            lblQuestion.Content = question;
        }

        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void btnDialogYes_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            txtInputBalance.Focus();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"^-?\d+([,.]\d{1,2})?$");
            e.Handled = !regex.IsMatch(e.Text);
        }

        public string Answer
        {
            get { return txtInputBalance.Text; }
        }
    }
}
