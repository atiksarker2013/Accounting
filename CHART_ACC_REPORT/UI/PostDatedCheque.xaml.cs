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
using CHART_ACC_REPORT.BLL;
using CHART_ACC_REPORT.ENTITY;
using CHART_ACC_REPORT.RPT;
using CHART_ACC_REPORT.UTILITY;
//Author:REFAT
namespace CHART_ACC_REPORT.UI
{
    /// <summary>
    /// Interaction logic for PostDatedCheque.xaml
    /// </summary>
    public partial class PostDatedCheque : Window
    {
        string caption = "Post Dated Cheque";
        BPostDatedCheque objBPostDatedCheque = new BPostDatedCheque();
        public PostDatedCheque()
        {
            InitializeComponent();
            InitiTask();
        }

        private void InitiTask()
        {
            dtpFrom.SelectedDate = DateTime.Now;
            dtpTo.SelectedDate = DateTime.Now;
        }

        private void btnPreview_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (AreAllFieldCompleted())
                {
                    List<EPostDatedCheque> listPostDatedCheque = objBPostDatedCheque.GetAllPostDatedCheque(Convert.ToDateTime(dtpFrom.SelectedDate), Convert.ToDateTime(dtpTo.SelectedDate));
                    if (listPostDatedCheque.Count > 0)
                    {
                        CrptPostDatedCheque objCrptPostDatedCheque = new CrptPostDatedCheque();
                        AccountingRptUtility.SetDate(objCrptPostDatedCheque, Convert.ToDateTime(dtpFrom.SelectedDate).ToShortDateString(), Convert.ToDateTime(dtpTo.SelectedDate).ToShortDateString());
                        AccountingRptUtility.setCompanyDeifinition(objCrptPostDatedCheque, caption);
                        AccountingRptUtility.Display_report(objCrptPostDatedCheque, listPostDatedCheque, this);
                    }
                    else
                    {
                        MessageBox.Show("No record found", caption, MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Following Problem Occur.\n"+ex.Message,caption,MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }

        private bool AreAllFieldCompleted()
        {
            if (dtpFrom.Text == string.Empty)
            {
                dtpFrom.Focus();
                MessageBox.Show("Please select from date.", caption, MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            if (dtpTo.Text == string.Empty)
            {
                dtpTo.Focus();
                MessageBox.Show("Please select to date.", caption, MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            if (dtpTo.SelectedDate < dtpFrom.SelectedDate)
            {
                MessageBox.Show("Please verify your date selection.", caption, MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            return true;
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
