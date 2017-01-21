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
    /// Interaction logic for PartyStatement.xaml
    /// </summary>
    public partial class PartyStatement : Window
    {
        BChartAccount objBChartOfAccount = new BChartAccount();
        string caption = "Party Statement";
        ECustomer_Employee objECustEmp = null;
        public PartyStatement()
        {
            InitializeComponent();
            InitialTask();
        }
        private void InitialTask()
        {
            SetFiscalDate();
            dtpTo.SelectedDate = DateTime.Now;
            dtpFrom.SelectedDate = DateTime.Now;
        }

        private void SetFiscalDate()
        {
            try
            {
                EChartOfAccount obj = objBChartOfAccount.GetFisDate();
                txtFiscalStartDate.Text = obj.FiscalStartDate.ToShortDateString();
                txtFiscalEndDate.Text = obj.FisCalEndDate.ToShortDateString();
            }
            catch (Exception)
            {
                MessageBox.Show("Problem Occur While Loading Fiscal Information.", caption, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void btnPartySearch_Click(object sender, RoutedEventArgs e)
        {
            PopupCustomerList objPopupCustomerList = new PopupCustomerList();
            if (objPopupCustomerList.ShowDialog() == true)
            {                
                objECustEmp = objPopupCustomerList.lvCustomerEmployee.SelectedItem as ECustomer_Employee;
                txtAccountCode.Text = objECustEmp.AccountCode;
                txtPartyName.Text = objECustEmp.EployeeName;
            }
        }
        private bool AreAllFieldsCompleted()
        {
            if (objECustEmp == null)
            {
                btnPartySearch.Focus();
                MessageBox.Show("Please select Party.", caption, MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
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
        private void btnPreview_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (AreAllFieldsCompleted())
                {
                    List<EAccountStatement> listECreateAccountStatement = new BAccountStatement().GetAccStatement(Convert.ToDateTime(dtpFrom.SelectedDate), Convert.ToDateTime(dtpTo.SelectedDate), objECustEmp.AccountName, objECustEmp.AccountId);

                    if (listECreateAccountStatement.Count > 0)
                    {
                        CrptAccountStatement objCrptAccountStatement = new CrptAccountStatement();
                        AccountingRptUtility.SetDate(objCrptAccountStatement, Convert.ToDateTime(dtpFrom.SelectedDate).ToShortDateString(), Convert.ToDateTime(dtpTo.SelectedDate).ToShortDateString());
                        AccountingRptUtility.setCompanyDeifinition(objCrptAccountStatement, caption);
                        AccountingRptUtility.Display_report(objCrptAccountStatement, listECreateAccountStatement, this);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unknown problem occured while retriving information.\n" + ex.Message, caption, MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void txtAccountCode_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtAccountCode.Text) == false)
                {
                    objECustEmp = null;
                    objECustEmp = new BCustomer_Employee().GetSingleCustomerEmployee(txtAccountCode.Text);
                    if (objECustEmp != null)
                    {
                        txtAccountCode.Text = objECustEmp.AccountCode;
                        txtPartyName.Text = objECustEmp.EployeeName;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unknown problem occured while Searching.\n" + ex.Message, caption, MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            InitialTask();
            txtAccountCode.Text = string.Empty;
            txtPartyName.Text = string.Empty;
            objECustEmp = null;
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
