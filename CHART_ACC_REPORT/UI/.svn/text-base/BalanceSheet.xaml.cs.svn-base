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
using CHART_ACC_REPORT.ENTITY;
using CHART_ACC_REPORT.UTILITY;
using CHART_ACC_REPORT.BLL;
using CHART_ACC_REPORT.RPT;
namespace CHART_ACC_REPORT.UI
{
    /// <summary>
    /// Interaction logic for BalanceSheet.xaml
    /// </summary>
    public partial class BalanceSheet : Window
    {
        BChartAccount objBChartAccount = new BChartAccount();
 
        public BalanceSheet()
        {
            InitializeComponent();
            LoadFiscalInfo();
            dtpPeriod.SelectedDate = DateTime.Now;            
        }

        private void LoadFiscalInfo()
        {
            try
            {
                EChartOfAccount obj = objBChartAccount.GetFisDate();
                txtFiscalStartDate.Text = obj.FiscalStartDate.ToShortDateString();
                txtFiscalEndDate.Text = obj.FisCalEndDate.ToShortDateString();
                dtpFiscalStartDate.SelectedDate = obj.FiscalStartDate;               
            }
            catch(Exception)
            {
                MessageBox.Show("Problem occur While Showing Fiscal Information","Balance Sheet",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }

        private void btnPreview_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (AreAllFieldsCompleted())
                {
                    BBalanceSheet objBBalanceSheet = new BBalanceSheet();
                    List<EBalanceSheet> listEBalanceSheet = objBBalanceSheet.GetBalanceSheet(Convert.ToDateTime((objBChartAccount.GetCompanyFirstFisDate()).FiscalStartDate), Convert.ToDateTime(dtpPeriod.SelectedDate));
                    if (chkboxTransaction.IsChecked == true)
                    {
                        listEBalanceSheet = ZeroOutMainList(listEBalanceSheet);
                    }
                    if (listEBalanceSheet.Count > 1)
                    {
                        CrptViewBalanceSheet objCrptBalanceSheet = new CrptViewBalanceSheet();
                        AccountingRptUtility.SetDate(objCrptBalanceSheet, Convert.ToDateTime(dtpFiscalStartDate.SelectedDate).ToShortDateString(), Convert.ToDateTime(dtpPeriod.SelectedDate).ToShortDateString());
                        AccountingRptUtility.setCompanyDeifinition(objCrptBalanceSheet, "Balance Sheet");
                        AccountingRptUtility.Display_report(objCrptBalanceSheet, listEBalanceSheet, this);
                    }
                    else if (listEBalanceSheet.Count == 1)
                    {                    
                        MessageBox.Show("Please Configure Balance Sheet.", "Balance Sheet", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("Unknown problem occured while retriving journal information.\n" + exception.Message, "Balance Sheet", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private List<EBalanceSheet> ZeroOutMainList(List<EBalanceSheet> listEBalanceSheet)
        {
            List<EBalanceSheet> list = new List<EBalanceSheet>();
            foreach (var objEBS in listEBalanceSheet)
            {
                if (objEBS.AccType == "ASSET")
                {
                    if (objEBS.Debit > 0)
                    {
                        list.Add(objEBS);
                    }
                }
                else
                {
                    if (objEBS.Credit > 0)
                    {
                        list.Add(objEBS);
                    }
                }
            }
            return list;
        }
        private bool AreAllFieldsCompleted()
        {
            if (dtpFiscalStartDate.Text == string.Empty)
            {
                dtpFiscalStartDate.Focus();
                MessageBox.Show("Please select Start date.", "Balance Sheet", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            if (dtpPeriod.Text == string.Empty)
            {
                dtpPeriod.Focus();
                MessageBox.Show("Please select Period.", "Balance Sheet", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            //if (dtpPeriod.SelectedDate > Convert.ToDateTime(txtFiscalEndDate.Text))
            //{
            //    dtpPeriod.Focus();
            //    MessageBox.Show("Please select date between the current fiscal year", "Balance Sheet", MessageBoxButton.OK, MessageBoxImage.Information);
            //    return false;
            //}
            if (dtpPeriod.SelectedDate < dtpFiscalStartDate.SelectedDate)
            {
                MessageBox.Show("Please verify your date selection.", "Balance Sheet", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            return true;
        }
        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            LoadFiscalInfo();
            dtpPeriod.SelectedDate = DateTime.Now;            
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
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
