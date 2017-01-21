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
using CrystalDecisions.CrystalReports.Engine;
using CHART_ACC_REPORT.UTILITY;

namespace CHART_ACC_REPORT.UI
{
    /// <summary>
    /// Interaction logic for IncomeStatement.xaml
    /// </summary>
    public partial class IncomeStatement : Window
    {
        BIncomeStatement objBIncomeStatement = new BIncomeStatement();
        public IncomeStatement()
        {
            InitializeComponent();
            GetFisDate();
        }

        private void GetFisDate()
        {
            try
            {
                foreach (ETrialBalanceReport date in objBIncomeStatement.GetFisDate())
                {
                    txtFiscalStartDate.Text = date.FiscalStartDate.ToShortDateString();
                    txtFiscalEndDate.Text = date.FisCalEndDate.ToShortDateString();
                    dtpFiscalStartDate.SelectedDate = date.FiscalStartDate;
                    dtpPeriod.SelectedDate = date.FisCalEndDate;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: GetFisDate()\n" + ex.Message, "Income Statement", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void SetInventory(ReportClass rpt, decimal depriciation, decimal tax,decimal incomeLoss)
        {
            rpt.DataDefinition.FormulaFields["Tax"].Text = "'" + tax + "'";
            rpt.DataDefinition.FormulaFields["Depriciation"].Text = "'" + depriciation + "'";
            rpt.DataDefinition.FormulaFields["IncomeLoss"].Text = "'" + incomeLoss + "'";
        }
        public void VerifyField()
        {
            if (txtTAX.Text == "")
            {
                txtTAX.Text = Convert.ToString(0);
            }
        }

        private bool CheckField()
        {
            if (dtpFiscalStartDate.SelectedDate == null)
            {
                MessageBox.Show("Please select start date between the current fiscal year.", "Income Statement", MessageBoxButton.OK, MessageBoxImage.Information);
                dtpFiscalStartDate.Focus();
                return false;
            }
            if (dtpPeriod.SelectedDate == null)
            {
                MessageBox.Show("Please select end date between the current fiscal year.", "Income Statement", MessageBoxButton.OK, MessageBoxImage.Information);
                dtpPeriod.Focus();
                return false;
            }
            if (dtpPeriod.SelectedDate < dtpFiscalStartDate.SelectedDate)
            {
                MessageBox.Show("Please verify your date selection.", "Income Statement", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }

            if (objBIncomeStatement.GetDepriciationForCurrentYear() <= 0)
            {
                if (MessageBox.Show("You hav'nt generate depriciation for this year.\nAre you sure want to get Income Statement without depriciation ?", "Income Statement", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        private void btnPreview_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CheckField() == true)
                {
                    List<EIncomeStatement> IncomeList = objBIncomeStatement.GetIncomeStatement(Convert.ToDateTime(dtpFiscalStartDate.SelectedDate), Convert.ToDateTime(dtpPeriod.SelectedDate));

                    IncomeList = OnlyTransaction(IncomeList);

                    if (IncomeList.Count > 1)
                    {
                        CrptIncomeStatement objCrpt = new CrptIncomeStatement();
                        AccountingRptUtility.SetDate(objCrpt, txtFiscalStartDate.Text, dtpPeriod.SelectedDate.Value.ToShortDateString());
                        AccountingRptUtility.setCompanyDeifinition(objCrpt, "INCOME STATEMENT");
                        VerifyField();
                        SetInventory(objCrpt, objBIncomeStatement.GetDepriciationForCurrentYear(), Convert.ToDecimal(txtTAX.Text), objBIncomeStatement.GetNetIncomeLoss(Convert.ToDateTime(dtpFiscalStartDate.SelectedDate), Convert.ToDateTime(dtpPeriod.SelectedDate)));
                        AccountingRptUtility.Display_report(objCrpt, IncomeList, this);
                    }
                    else if (IncomeList.Count == 1)
                    {
                        MessageBox.Show("Please Configure Income Statement Sheet.", "Income Statement", MessageBoxButton.OK, MessageBoxImage.Information);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error :\n" + ex.Message, "Income Statement",MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }

        private List<EIncomeStatement> OnlyTransaction(List<EIncomeStatement> listIncome)
        {
            List<EIncomeStatement> list = new List<EIncomeStatement>();
            foreach (var objTrb in listIncome)
            {
                if (objTrb.Debit - objTrb.Credit != 0)
                {
                    list.Add(objTrb);
                }
            }
            return list;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.ShowInTaskbar = true;
            this.Owner = null;
        }
        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        #region TEXT -INPUT
        private void txtTAX_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !AreAllValidNumericChars(e.Text);
            base.OnPreviewTextInput(e);
        }

        bool AreAllValidNumericChars(string str)
        {
            bool ret = true;
            if (str == System.Globalization.NumberFormatInfo.CurrentInfo.CurrencyDecimalSeparator |
                str == System.Globalization.NumberFormatInfo.CurrentInfo.CurrencyGroupSeparator |
                str == System.Globalization.NumberFormatInfo.CurrentInfo.CurrencySymbol |
                str == System.Globalization.NumberFormatInfo.CurrentInfo.NegativeSign |
                str == System.Globalization.NumberFormatInfo.CurrentInfo.NegativeInfinitySymbol |
                str == System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator |
                str == System.Globalization.NumberFormatInfo.CurrentInfo.NumberGroupSeparator |
                str == System.Globalization.NumberFormatInfo.CurrentInfo.PercentDecimalSeparator |
                str == System.Globalization.NumberFormatInfo.CurrentInfo.PercentGroupSeparator |
                str == System.Globalization.NumberFormatInfo.CurrentInfo.PercentSymbol |
                str == System.Globalization.NumberFormatInfo.CurrentInfo.PerMilleSymbol |
                str == System.Globalization.NumberFormatInfo.CurrentInfo.PositiveInfinitySymbol |
                str == System.Globalization.NumberFormatInfo.CurrentInfo.PositiveSign)
                return ret;

            int l = str.Length;
            for (int i = 0; i < l; i++)
            {
                char ch = str[i];
                ret &= Char.IsDigit(ch);
            }

            return ret;

        }
        #endregion
    }
}
