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
using CHART_ACC_REPORT.BLL;
using CHART_ACC_REPORT.RPT;
using CHART_ACC_REPORT.UTILITY;

namespace CHART_ACC_REPORT.UI
{
    /// <summary>
    /// Interaction logic for TrialBalance.xaml
    /// </summary>
    public partial class TrialBalance : Window
    {
        BTrialBalanceReport objBTrialBalanceReport = new BTrialBalanceReport();
        BChartAccount objBChartAccount = new BChartAccount();

        public TrialBalance()
        {
            InitializeComponent();
            GetFisDate();
        }

        private void GetFisDate()
        {
            try
            {
                foreach (ETrialBalanceReport date in objBTrialBalanceReport.GetFisDate())
                {
                    txtFiscalStartDate.Text = date.FiscalStartDate.ToShortDateString();
                    txtFiscalEndDate.Text = date.FisCalEndDate.ToShortDateString();
                    dtpFiscalStartDate.SelectedDate = date.FiscalStartDate;
                    dtpPeriod.SelectedDate = date.FisCalEndDate;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : GetFisDate()" + ex.Message, "Trial Balance", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
        }

        private void btnPreview_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CheckField() == true)
                {
                    if (dtpPeriod.SelectedDate != null && dtpFiscalStartDate.SelectedDate != null)
                    {
                        List<ETrialBalanceReport> liTrailBalance = objBTrialBalanceReport.GetFinalAccountBalance(Convert.ToDateTime(dtpFiscalStartDate.SelectedDate),Convert.ToDateTime(dtpPeriod.SelectedDate));

                        liTrailBalance.Add(objBTrialBalanceReport.ProcessIncomeLoss(Convert.ToDateTime((objBChartAccount.GetCompanyFirstFisDate()).FiscalStartDate), Convert.ToDateTime(dtpFiscalStartDate.SelectedDate).AddDays(-1)));
                        if (chkTransaction.IsChecked == true)
                        {
                            liTrailBalance = OnlyTransaction(liTrailBalance);
                        }
                        if (liTrailBalance.Count > 0)
                        {
                            CrptTrialBalance objCrpt = new CrptTrialBalance();
                            AccountingRptUtility.SetDate(objCrpt, txtFiscalStartDate.Text, dtpPeriod.SelectedDate.Value.ToShortDateString());
                            AccountingRptUtility.setCompanyDeifinition(objCrpt, "TRIAL BALANCE");
                            AccountingRptUtility.Display_report(objCrpt, liTrailBalance, this);
                        }
                        else
                        {
                            MessageBox.Show("No information available for creating Trial Balance.", "Trial Balance", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please verify your date selection.", "Trial Balance", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error :\n" + ex.Message, "Trial Balance", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private List<ETrialBalanceReport> OnlyTransaction(List<ETrialBalanceReport> listTrialBalance)
        {
            List<ETrialBalanceReport> list = new List<ETrialBalanceReport>();
            foreach (var objTrb in listTrialBalance)
            {
                if (objTrb.Debit - objTrb.Credit != 0)
                {
                    list.Add(objTrb);
                }
            }
            return list;
        }

        private bool CheckField()
        {
            if (dtpFiscalStartDate.SelectedDate == null)
            {
                MessageBox.Show("Please select start date.", "Trial Balance", MessageBoxButton.OK, MessageBoxImage.Information);
                dtpFiscalStartDate.Focus();
                return false;
            }
            if (dtpPeriod.SelectedDate == null)
            {
                MessageBox.Show("Please select end date.", "Trial Balance", MessageBoxButton.OK, MessageBoxImage.Information);
                dtpPeriod.Focus();
                return false;
            }
            if (dtpPeriod.SelectedDate < dtpFiscalStartDate.SelectedDate)
            {
                MessageBox.Show("Please verify your date selection.", "Trial Balance", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
           
            return true;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.ShowInTaskbar = true;
            this.Owner = null;
        }
    }
}
