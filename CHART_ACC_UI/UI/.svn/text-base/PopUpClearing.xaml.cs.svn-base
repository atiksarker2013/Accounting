using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CHART_ACC_UI.UI
{
    /// <summary>
    /// Interaction logic for PopUpClearing.xaml
    /// </summary>
    public partial class PopUpClearing : Window
    {
        public PopUpClearing()
        {
            InitializeComponent();
            cmbClearingMethod.Items.Add("Inward Clearing");
            cmbClearingMethod.Items.Add("Outward Clearing");
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (cmbClearingMethod.Text != string.Empty)
            {
                Clearing objClearing = new Clearing(cmbClearingMethod.Text);
                OwnerSet(objClearing);
                objClearing.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Please Select a Clearing Method.","Inward/Outward Clearing",MessageBoxButton.OK,MessageBoxImage.Information);
                cmbClearingMethod.Focus();
            }
        }
        private static void OwnerSet(Window objWND)
        {
            foreach (Window wnd in Application.Current.Windows)
            {
                string[] splitedNamespace = (wnd.ToString()).Split('.');
                string aClassNameFromCollection = splitedNamespace[splitedNamespace.Length - 1];
                if (aClassNameFromCollection == "MainWindow")
                {
                    objWND.Owner = wnd;
                    break;
                }
            }
        }
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.ShowInTaskbar = true;
            this.Owner = null;
        }
    }
}
