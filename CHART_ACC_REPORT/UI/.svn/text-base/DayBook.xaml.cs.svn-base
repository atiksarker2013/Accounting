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
    /// Interaction logic for DayBook.xaml
    /// </summary>
    public partial class DayBook : Window
    {
        BDailyBook objBDailyBook = new BDailyBook();
        BTrialBalanceReport objBTrialBalanceReport = new BTrialBalanceReport();
        public DayBook()
        {
            InitializeComponent();
            dtpJournalDate.SelectedDate = DateTime.Now;
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
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: GetFisDate()\n" + ex.Message, "Day Book", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
        }

        public void TotalDeposit(ReportClass rpt, int Total_Deposit)
        {
            rpt.DataDefinition.FormulaFields["Total_Debit"].Text = "'" + Total_Deposit + "'";
        }
        public void TotalPayment(ReportClass rpt, int Total_Payment)
        {
            rpt.DataDefinition.FormulaFields["Total_Credit"].Text = "'" + Total_Payment + "'";
        }
        public void TotalJournal(ReportClass rpt, int Total_Journal)
        {
            rpt.DataDefinition.FormulaFields["Total_Journal"].Text = "'" + Total_Journal + "'";
        }
        public void TotalContra(ReportClass rpt, int Total_Contra)
        {
            rpt.DataDefinition.FormulaFields["Total_Contra"].Text = "'" + Total_Contra + "'";
        }

        private void btnPreview_Click(object sender, RoutedEventArgs e)
        {
            if (CheckField() == true)
            {
                if (rdReceive.IsChecked == true)
                {
                    DailyTransactionReceive();
                }
                else if (rdPayment.IsChecked == true)
                {
                    DailyTransactionPayment();
                }
                else if (rdJournal.IsChecked == true)
                {
                    DailyTransactionJournal();
                }
                else if (rdContra.IsChecked == true)
                {
                    DailyTransactionContra();
                }
                else
                {
                    DailyTransactionAll();
                }
            }
        }

        private void DailyTransactionAll()
        {
            string journalDate = dtpJournalDate.SelectedDate.ToString();
            DateTime day_date =Convert.ToDateTime(dtpJournalDate.SelectedDate);
            try
            {

                List<EDailyBook> DayTransactionList = objBDailyBook.GetAllDailyTransaction(journalDate);
                CrptDayBook objCrpt = new CrptDayBook();
                AccountingRptUtility.setCompanyDeifinition(objCrpt, "DAY-BOOK REPORT");
                AccountingRptUtility.SetDate(objCrpt, day_date.ToShortDateString(), day_date.ToShortDateString());
                TotalDeposit(objCrpt, objBDailyBook.GetTotalDepositVoucher(journalDate));
                TotalPayment(objCrpt, objBDailyBook.GetTotalPaymentVoucher(journalDate));
                TotalJournal(objCrpt, objBDailyBook.GetTotalJournalVoucher(journalDate));
                TotalContra(objCrpt, objBDailyBook.GetTotalContraVoucher(journalDate));
                AccountingRptUtility.Display_report(objCrpt, DayTransactionList, this);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error :DailyTransactionAll().\n" + ex.Message, "Day Book", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DailyTransactionReceive()
        { 
          string journalDate = dtpJournalDate.SelectedDate.ToString();
          DateTime day_date = Convert.ToDateTime(dtpJournalDate.SelectedDate);
          try
          {

              List<EDailyBook> DayTransactionList = objBDailyBook.GetAllDailyTransactionReceive(journalDate);
              CrptDayBookSingle objCrpt = new CrptDayBookSingle();
              AccountingRptUtility.setCompanyDeifinition(objCrpt, "DAY-BOOK REPORT -(RECEIVE)");
              AccountingRptUtility.SetDate(objCrpt, day_date.ToShortDateString(), day_date.ToShortDateString());
              objCrpt.DataDefinition.FormulaFields["Total_Voucher"].Text = "'" + "Receive Voucher : " + objBDailyBook.GetTotalDepositVoucher(journalDate) + "'";
              AccountingRptUtility.Display_report(objCrpt, DayTransactionList, this);
          }
          catch (Exception ex)
          {
              MessageBox.Show("Error :DailyTransactionReceive().\n" + ex.Message, "Day Book", MessageBoxButton.OK, MessageBoxImage.Error);
          }
        }

        private void DailyTransactionPayment()
        {
            string journalDate = dtpJournalDate.SelectedDate.ToString();
            DateTime day_date = Convert.ToDateTime(dtpJournalDate.SelectedDate);
            try
            {

                List<EDailyBook> DayTransactionList = objBDailyBook.GetAllDailyTransactionPayment(journalDate);
                CrptDayBookSingle objCrpt = new CrptDayBookSingle();
                AccountingRptUtility.setCompanyDeifinition(objCrpt, "DAY-BOOK REPORT -(PAYMENT)");
                AccountingRptUtility.SetDate(objCrpt, day_date.ToShortDateString(), day_date.ToShortDateString());
                objCrpt.DataDefinition.FormulaFields["Total_Voucher"].Text = "'" + "Payment Voucher : " + objBDailyBook.GetTotalPaymentVoucher(journalDate) + "'";
                AccountingRptUtility.Display_report(objCrpt, DayTransactionList, this);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error :DailyTransactionPayment().\n" + ex.Message, "Day Book", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DailyTransactionJournal()
        {
            string journalDate = dtpJournalDate.SelectedDate.ToString();
            DateTime day_date = Convert.ToDateTime(dtpJournalDate.SelectedDate);
            try
            {
                List<EDailyBook> DayTransactionList = objBDailyBook.GetAllDailyTransactionJournal(journalDate);
                CrptDayBookSingle objCrpt = new CrptDayBookSingle();
                AccountingRptUtility.setCompanyDeifinition(objCrpt, "DAY-BOOK REPORT -(JOURNAL)");
                AccountingRptUtility.SetDate(objCrpt, day_date.ToShortDateString(), day_date.ToShortDateString());
                objCrpt.DataDefinition.FormulaFields["Total_Voucher"].Text = "'" + "Journal Voucher : " + objBDailyBook.GetTotalJournalVoucher(journalDate) + "'";
                AccountingRptUtility.Display_report(objCrpt, DayTransactionList, this);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error :DailyTransactionJournal().\n" + ex.Message, "Day Book", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DailyTransactionContra()
        {
            string journalDate = dtpJournalDate.SelectedDate.ToString();
            DateTime day_date = Convert.ToDateTime(dtpJournalDate.SelectedDate);
            try
            {

                List<EDailyBook> DayTransactionList = objBDailyBook.GetAllDailyTransactionContra(journalDate);
                CrptDayBookSingle objCrpt = new CrptDayBookSingle();
                AccountingRptUtility.setCompanyDeifinition(objCrpt, "DAY-BOOK REPORT -(CONTRA)");
                AccountingRptUtility.SetDate(objCrpt, day_date.ToShortDateString(), day_date.ToShortDateString());
                objCrpt.DataDefinition.FormulaFields["Total_Voucher"].Text = "'" + "Contra Voucher : " + objBDailyBook.GetTotalContraVoucher(journalDate) + "'";
                AccountingRptUtility.Display_report(objCrpt, DayTransactionList, this);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error :DailyTransactionContra().\n" + ex.Message, "Day Book", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool CheckField()
        {
            if (dtpJournalDate.SelectedDate == null)
            {
                MessageBox.Show("Please select date.", "Day Book", MessageBoxButton.OK, MessageBoxImage.Information);
                dtpJournalDate.Focus();
                return false;
            }
            if (rdReceive.IsChecked == false && rdPayment.IsChecked == false && rdJournal.IsChecked == false && rdContra.IsChecked == false && rdAllTransaction.IsChecked == false)
            {
                MessageBox.Show("Please choose which voucher you want to see.","Day Book",MessageBoxButton.OK,MessageBoxImage.Information);
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
