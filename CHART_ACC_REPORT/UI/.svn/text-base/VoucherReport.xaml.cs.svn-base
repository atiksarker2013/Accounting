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
using CrystalDecisions.CrystalReports.Engine;

namespace CHART_ACC_REPORT.UI
{
    /// <summary>
    /// Interaction logic for VoucherReport.xaml
    /// </summary>
    public partial class VoucherReport : Window
    {
        BDailyBook objBDailyBook = new BDailyBook();
        BTrialBalanceReport objBTrialBalanceReport = new BTrialBalanceReport();

        public VoucherReport()
        {
            InitializeComponent();
            GetFisDate();
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

        private void GetFisDate()
        {
            try
            {
                foreach (ETrialBalanceReport date in objBTrialBalanceReport.GetFisDate())
                {
                    txtFiscalStartDate.Text = date.FiscalStartDate.ToShortDateString();
                    txtFiscalEndDate.Text = date.FisCalEndDate.ToShortDateString();
                    dtpFromDate.SelectedDate = DateTime.Now;
                    dtpToDate.SelectedDate = DateTime.Now;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: GetFisDate()\n" + ex.Message, "Voucher Report", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void TotalDeposit(ReportClass rpt, int Total_Debit)
        {
            rpt.DataDefinition.FormulaFields["Total_Debit"].Text = "'" + Total_Debit + "'";
        }

        public void TotalPayment(ReportClass rpt, int Total_Credit)
        {
            rpt.DataDefinition.FormulaFields["Total_Credit"].Text = "'" + Total_Credit + "'";
        }

        public void TotalContra(ReportClass rpt, int Total_Contra)
        {
            rpt.DataDefinition.FormulaFields["Total_Contra"].Text = "'" + Total_Contra + "'";
        }

        public void TotalJournal(ReportClass rpt, int Total_Journal)
        {
            rpt.DataDefinition.FormulaFields["Total_Journal"].Text = "'" + Total_Journal + "'";
        }

        private bool CheckField()
        {
            if (dtpFromDate.SelectedDate > dtpToDate.SelectedDate)
            {
                MessageBox.Show("Please verify your date selection", "Voucher Report", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            if (rdReceive.IsChecked == false && rdPayment.IsChecked == false && rdJournal.IsChecked == false && rdContra.IsChecked == false && rdAllTransaction.IsChecked == false)
            {
                MessageBox.Show("Please choose which voucher you want to see.", "Voucher Report", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            return true;
        }

        private void btnPreview_Click(object sender, RoutedEventArgs e)
        {
            if (CheckField() == true)
            {
                if (rdReceive.IsChecked == true)
                {
                    GetAllDateWise_TransactionReceive();
                }
                else if (rdPayment.IsChecked == true)
                {
                    GetAllDateWise_TransactionPayment();
                }
                else if (rdJournal.IsChecked == true)
                {
                    GetAllDateWise_TransactionJournal();
                }
                else if (rdContra.IsChecked == true)
                {
                    GetAllDateWise_TransactionContra();
                }
                else
                {
                    GetAllDateWise_Transaction();
                }

            }
        }

        private void GetAllDateWise_Transaction()
        {
            string FromDate = dtpFromDate.SelectedDate.ToString();
            string ToDate = dtpToDate.SelectedDate.ToString();
            DateTime from_Date = Convert.ToDateTime(dtpFromDate.SelectedDate);
            DateTime to_Date = Convert.ToDateTime(dtpToDate.SelectedDate);
            try
            {

                List<EDailyBook> DayTransactionList = objBDailyBook.GetTransaction_DateWise(FromDate, ToDate);
                CrptDayBook objCrpt = new CrptDayBook();
                AccountingRptUtility.setCompanyDeifinition(objCrpt, "VOUCHER REPORT");
                AccountingRptUtility.SetDate(objCrpt, from_Date.ToShortDateString(), to_Date.ToShortDateString());
                TotalContra(objCrpt,objBDailyBook.GetTotalContraVoucher_DateWise(FromDate,ToDate));
                TotalDeposit(objCrpt,objBDailyBook.GetTotalDepositVoucher_DateWise(FromDate,ToDate));
                TotalJournal(objCrpt, objBDailyBook.GetTotalJournalVoucher_DateWise(FromDate, ToDate));
                TotalPayment(objCrpt, objBDailyBook.GetTotalPaymentVoucher_DateWise(FromDate, ToDate));
                AccountingRptUtility.Display_report(objCrpt, DayTransactionList, this);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error :GetAllDateWise_Transaction()." + ex.Message, "Voucher Report", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void GetAllDateWise_TransactionReceive()
        {
            string FromDate = dtpFromDate.SelectedDate.ToString();
            string ToDate = dtpToDate.SelectedDate.ToString();
            DateTime from_Date = Convert.ToDateTime(dtpFromDate.SelectedDate);
            DateTime to_Date = Convert.ToDateTime(dtpToDate.SelectedDate);
            try
            {

                List<EDailyBook> DayTransactionList = objBDailyBook.GetTransaction_DateWise_Receive(FromDate, ToDate);
                CrptDayBookSingle objCrpt = new CrptDayBookSingle();
                AccountingRptUtility.setCompanyDeifinition(objCrpt, "VOUCHER REPORT -(RECEIVE)");
                AccountingRptUtility.SetDate(objCrpt, from_Date.ToShortDateString(), to_Date.ToShortDateString());
                objCrpt.DataDefinition.FormulaFields["Total_Voucher"].Text = "'" + "Receive Voucher : " + objBDailyBook.GetTotalDepositVoucher_DateWise(FromDate,ToDate) + "'";
                AccountingRptUtility.Display_report(objCrpt, DayTransactionList, this);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error :GetAllDateWise_TransactionReceive()." + ex.Message, "Voucher Report", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void GetAllDateWise_TransactionPayment()
        {
            string FromDate = dtpFromDate.SelectedDate.ToString();
            string ToDate = dtpToDate.SelectedDate.ToString();
            DateTime from_Date = Convert.ToDateTime(dtpFromDate.SelectedDate);
            DateTime to_Date = Convert.ToDateTime(dtpToDate.SelectedDate);
            try
            {

                List<EDailyBook> DayTransactionList = objBDailyBook.GetTransaction_DateWise_Payment(FromDate, ToDate);
                CrptDayBookSingle objCrpt = new CrptDayBookSingle();
                AccountingRptUtility.setCompanyDeifinition(objCrpt, "VOUCHER REPORT -(PAYMENT)");
                AccountingRptUtility.SetDate(objCrpt, from_Date.ToShortDateString(), to_Date.ToShortDateString());
                objCrpt.DataDefinition.FormulaFields["Total_Voucher"].Text = "'" + "Payment Voucher : " + objBDailyBook.GetTotalPaymentVoucher_DateWise(FromDate, ToDate) + "'";
                AccountingRptUtility.Display_report(objCrpt, DayTransactionList, this);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error :GetAllDateWise_TransactionPayment()." + ex.Message, "Voucher Report", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void GetAllDateWise_TransactionJournal()
        {
            string FromDate = dtpFromDate.SelectedDate.ToString();
            string ToDate = dtpToDate.SelectedDate.ToString();
            DateTime from_Date = Convert.ToDateTime(dtpFromDate.SelectedDate);
            DateTime to_Date = Convert.ToDateTime(dtpToDate.SelectedDate);
            try
            {

                List<EDailyBook> DayTransactionList = objBDailyBook.GetTransaction_DateWise_Journal(FromDate, ToDate);
                CrptDayBookSingle objCrpt = new CrptDayBookSingle();
                AccountingRptUtility.setCompanyDeifinition(objCrpt, "VOUCHER REPORT -(JOURNAL)");
                AccountingRptUtility.SetDate(objCrpt, from_Date.ToShortDateString(), to_Date.ToShortDateString());
                objCrpt.DataDefinition.FormulaFields["Total_Voucher"].Text = "'" + "Journal Voucher : " + objBDailyBook.GetTotalJournalVoucher_DateWise(FromDate, ToDate) + "'";
                AccountingRptUtility.Display_report(objCrpt, DayTransactionList, this);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : GetAllDateWise_TransactionJournal()." + ex.Message, "Voucher Report", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void GetAllDateWise_TransactionContra()
        {
            string FromDate = dtpFromDate.SelectedDate.ToString();
            string ToDate = dtpToDate.SelectedDate.ToString();
            DateTime from_Date = Convert.ToDateTime(dtpFromDate.SelectedDate);
            DateTime to_Date = Convert.ToDateTime(dtpToDate.SelectedDate);
            try
            {

                List<EDailyBook> DayTransactionList = objBDailyBook.GetTransaction_DateWise_Contra(FromDate, ToDate);
                CrptDayBookSingle objCrpt = new CrptDayBookSingle();
                AccountingRptUtility.setCompanyDeifinition(objCrpt, "VOUCHER REPORT -(CONTRA)");
                AccountingRptUtility.SetDate(objCrpt, from_Date.ToShortDateString(), to_Date.ToShortDateString());
                objCrpt.DataDefinition.FormulaFields["Total_Voucher"].Text = "'" + "Contra Voucher : " + objBDailyBook.GetTotalContraVoucher_DateWise(FromDate, ToDate) + "'";
                AccountingRptUtility.Display_report(objCrpt, DayTransactionList, this);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : GetAllDateWise_TransactionContra()." + ex.Message, "Voucher Report", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
