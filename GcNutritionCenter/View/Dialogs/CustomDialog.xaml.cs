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
    /// Interaktionslogik für CustomDialog.xaml
    /// </summary>
    public partial class CustomDialog : Window
    {
        public enum InputType
        {
            OkCancel = 0,
            YesNo = 1,
            Ok = 2,
        }

        public CustomDialog(string question, InputType inputType = InputType.OkCancel)
        {
            InitializeComponent();

            switch(inputType)
            {
                case InputType.OkCancel:
                    btnDialogCancelNo.Visibility = Visibility.Visible;
                    btnDialogOkYes.Visibility = Visibility.Visible;
                    btnDialogCancelNo.Content = "Abbrechen";
                    btnDialogOkYes.Content = "OK";
                    break;
                case InputType.YesNo:
                    btnDialogCancelNo.Visibility = Visibility.Visible;
                    btnDialogOkYes.Visibility = Visibility.Visible;
                    btnDialogCancelNo.Content = "Nein";
                    btnDialogOkYes.Content = "Ja";
                    break;
                case InputType.Ok:
                    btnDialogCancelNo.Visibility = Visibility.Collapsed;
                    btnDialogOkYes.Visibility = Visibility.Visible;
                    btnDialogOkYes.Content = "OK";
                    break;
                default:
                    btnDialogCancelNo.Visibility = Visibility.Visible;
                    btnDialogOkYes.Visibility = Visibility.Visible;
                    btnDialogCancelNo.Content = "Abbrechen";
                    btnDialogOkYes.Content = "OK";
                    break;
            }
            lblQuestion.Text = question;
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            //
        }

        private void btnDialogOkYes_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
