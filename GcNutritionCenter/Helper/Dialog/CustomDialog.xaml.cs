using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
            YesNo = 3
        }

        public CustomDialog(string question, InputType inputType = InputType.OkCancel)
        {
            InitializeComponent();

            txtInput.Text = String.Empty;
            switch (inputType)
            {
                case InputType.Ok:
                    txtInput.Visibility = Visibility.Collapsed;
                    btnDialogOk.Visibility = Visibility.Visible;
                    btnDialogYes.Visibility = Visibility.Collapsed;
                    btnDialogCancel.Visibility = Visibility.Collapsed;
                    btnDialogNo.Visibility = Visibility.Collapsed;
                    break;
                case InputType.OkCancel:
                    txtInput.Visibility = Visibility.Collapsed;
                    btnDialogOk.Visibility = Visibility.Visible;
                    btnDialogYes.Visibility = Visibility.Collapsed;
                    btnDialogCancel.Visibility = Visibility.Visible;
                    btnDialogNo.Visibility = Visibility.Collapsed;
                    break;
                case InputType.InputOkCancel:
                    txtInput.Visibility = Visibility.Visible;
                    btnDialogOk.Visibility = Visibility.Visible;
                    btnDialogYes.Visibility = Visibility.Collapsed;
                    btnDialogCancel.Visibility = Visibility.Visible;
                    btnDialogNo.Visibility = Visibility.Collapsed;
                    break;
                case InputType.YesNo:
                    txtInput.Visibility = Visibility.Collapsed;
                    btnDialogOk.Visibility = Visibility.Collapsed;
                    btnDialogYes.Visibility = Visibility.Visible;
                    btnDialogCancel.Visibility = Visibility.Collapsed;
                    btnDialogNo.Visibility = Visibility.Visible;
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
            txtInput.SelectAll();
            txtInput.Focus();
        }

        public string Answer
        {
            get { return txtInput.Text; }
        }
    }
}
