using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
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
using CHART_ACC_REPORT.UTILITY;
using CHART_ACC_REPORT.RPT;
//Author:REFAT
namespace CHART_ACC_REPORT.UI
{
    /// <summary>
    /// Interaction logic for AccountStatementr.xaml
    /// </summary>
    public partial class LedgerReport : Window
    {
        BAccountStatement objBAccountStatement = new BAccountStatement();
        BChartAccount objBChartOfAccount = new BChartAccount();
        BLedgerRevenueExpense objBLedgerRevenueExpense = new BLedgerRevenueExpense();
        long parentId = 0; string caption = "Ledger";
        bool childStatus = false;        
        public LedgerReport()
        {
            InitializeComponent();
            InitialTasks();
        }                   
        private void InitialTasks()
        {
            SetFiscalDate();
            dtpTo.SelectedDate = DateTime.Now;
            dtpFrom.SelectedDate = DateTime.Now;
            LoadAccountTypeTree();
            txtBlockAccountName.Text = string.Empty;
            trvAccountType.Visibility = Visibility.Hidden;            
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
        private void LoadAccountTypeTree()
        {
            trvAccountType.Items.Clear();
            try
            {
                foreach (EAccountType objType in objBChartOfAccount.GetAllAccountType())
                {
                    TreeViewItem treeItem = new TreeViewItem();
                    treeItem.Header = objType.AccTypeName;
                    treeItem.Name = "_" + objType.AccTypeId;
                    ProcessTree(objType.AccTypeId, treeItem);
                    trvAccountType.Items.Add(treeItem);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Problem Occur While Loading Parent Account", caption, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        int count = 1;
        private void ProcessTree(long pId, TreeViewItem newChild)
        {
            foreach (EChartOfAccount obj in objBChartOfAccount.GetSubAccount(pId))
            {
                count += 1;
                TreeViewItem child = new TreeViewItem();
                child.Header = obj.SubAccName;
                child.Name = "_" + obj.SubAccId;
                newChild.Items.Add(child);
                if (obj.RootAccName == "ASSET" || obj.RootAccName == "LIABILITY")
                {
                    if (count < 3 && obj.AccHeader == "Yes")
                    {
                        ProcessTree(obj.SubAccId, child);
                    }
                }
                else if (count < 2 && obj.AccHeader == "Yes")
                {
                    ProcessTree(obj.SubAccId, child);
                }
                count -= 1;
            }
        }        
        private void trvAccountType_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                var trvItem = (sender as TreeView).SelectedItem as TreeViewItem;
                if (trvItem != null)
                {                                        
                    string[] splitedId = (trvItem.Name.ToString()).Split('_');
                    parentId = Convert.ToInt64(splitedId[1]);
                    
                    if (trvItem.HasItems)
                    {
                       //childStatus = true;
                    }
                    else
                    {
                        txtBlockAccountName.Text = trvItem.Header.ToString();
                        trvAccountType.Visibility = Visibility.Hidden;
                        if (objBChartOfAccount.GetAccountInfo(parentId).AccHeader == "Yes")
                        {
                            childStatus = true;
                        }
                        else
                        {
                            childStatus = false;                            
                        }
                    }
                }
            }
            catch(Exception) 
            { 
            }
        }
       
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (trvAccountType.IsVisible == false)
            {
                trvAccountType.Visibility = Visibility.Visible;
            }
            else
            {
                trvAccountType.Visibility = Visibility.Hidden;
            }
        }
        private void btnShow_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (AreAllFieldsCompleted())
                {
                    count = 1;
                    if (objBChartOfAccount.GetAccountInfo(parentId).RootAccName == "REVENUE" || objBChartOfAccount.GetAccountInfo(parentId).RootAccName == "EXPENSE")
                    {
                        ShowRevenueExpenseLedger();
                    }
                    else
                    {
                        ShowLedger();
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("Unknown problem occured while retriving information.\n" + exception.Message, caption, MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ShowRevenueExpenseLedger()
        {
            DateTime fromDate = Convert.ToDateTime(dtpFrom.SelectedDate);
            DateTime toDate = Convert.ToDateTime(dtpTo.SelectedDate);
            int startDateFisId = objBChartOfAccount.GetFiscalId(Convert.ToDateTime(dtpFrom.SelectedDate));
            int toDateFisId = objBChartOfAccount.GetFiscalId(Convert.ToDateTime(dtpTo.SelectedDate));
            if (startDateFisId != toDateFisId)
            {
                fromDate = objBChartOfAccount.GetSingleFiscalInfo(toDateFisId).FiscalStartDate;
            }
            if (childStatus == true)
            {

                List<EGL> listGL = new BGL().LoadListofGLParticaularIdwise(parentId, txtBlockAccountName.Text, fromDate, toDate, true);
                if (listGL.Count > 0)
                {
                    CrptGL objCrptGL = new CrptGL();
                    AccountingRptUtility.SetDate(objCrptGL, fromDate.ToShortDateString(), toDate.ToShortDateString());
                    AccountingRptUtility.setCompanyDeifinition(objCrptGL, caption + " of " + txtBlockAccountName.Text);
                    AccountingRptUtility.Display_report(objCrptGL, listGL, this);
                }
            }
            else
            {
                List<EAccountStatement> listECreateAccountStatement = objBLedgerRevenueExpense.GetAccStatement(fromDate, toDate, txtBlockAccountName.Text, parentId, objBChartOfAccount.GetSingleFiscalInfo(toDateFisId).FiscalStartDate);

                if (listECreateAccountStatement.Count > 0)
                {
                    CrptAccountStatement objCrptAccountStatement = new CrptAccountStatement();
                    AccountingRptUtility.SetDate(objCrptAccountStatement, fromDate.ToShortDateString(), toDate.ToShortDateString());
                    AccountingRptUtility.setCompanyDeifinition(objCrptAccountStatement, caption + " of " + txtBlockAccountName.Text);
                    AccountingRptUtility.Display_report(objCrptAccountStatement, listECreateAccountStatement, this);
                }
            }
        }

        private void ShowLedger()
        {
            if (childStatus == true)
            {
                List<EGL> listGL = new BGL().LoadListofGLParticaularIdwise(parentId, txtBlockAccountName.Text, Convert.ToDateTime(dtpFrom.SelectedDate), Convert.ToDateTime(dtpTo.SelectedDate), true);
                if (listGL.Count > 0)
                {
                    CrptGL objCrptGL = new CrptGL();
                    AccountingRptUtility.SetDate(objCrptGL, Convert.ToDateTime(dtpFrom.SelectedDate).ToShortDateString(), Convert.ToDateTime(dtpTo.SelectedDate).ToShortDateString());
                    AccountingRptUtility.setCompanyDeifinition(objCrptGL, caption + " of " + txtBlockAccountName.Text);
                    AccountingRptUtility.Display_report(objCrptGL, listGL, this);
                }
            }
            else
            {
                List<EAccountStatement> listECreateAccountStatement = objBAccountStatement.GetAccStatement(Convert.ToDateTime(dtpFrom.SelectedDate), Convert.ToDateTime(dtpTo.SelectedDate), txtBlockAccountName.Text, parentId);

                if (listECreateAccountStatement.Count > 0)
                {
                    CrptAccountStatement objCrptAccountStatement = new CrptAccountStatement();
                    AccountingRptUtility.SetDate(objCrptAccountStatement, Convert.ToDateTime(dtpFrom.SelectedDate).ToShortDateString(), Convert.ToDateTime(dtpTo.SelectedDate).ToShortDateString());
                    AccountingRptUtility.setCompanyDeifinition(objCrptAccountStatement, caption + " of " + txtBlockAccountName.Text);
                    AccountingRptUtility.Display_report(objCrptAccountStatement, listECreateAccountStatement, this);
                }
            }
        }

        private bool AreAllFieldsCompleted()
        {
            if (dtpFrom.Text == string.Empty)
            {
                dtpFrom.Focus();
                MessageBox.Show("Please select from date.", caption, MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            if(dtpTo.Text == string.Empty)
            {
                dtpTo.Focus();
                MessageBox.Show("Please select to date.", caption, MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            //if (dtpTo.SelectedDate > Convert.ToDateTime(txtFiscalEndDate.Text))
            //{
            //    dtpTo.Focus();
            //    MessageBox.Show("Please select date between the current fiscal year.", "Account Statement", MessageBoxButton.OK, MessageBoxImage.Information);
            //    return false;
            //}
            if (dtpTo.SelectedDate < dtpFrom.SelectedDate)
            {
                MessageBox.Show("Please verify your date selection.", caption, MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            if(txtBlockAccountName.Text == string.Empty)
            {
                btnParentAccount.Focus();
                MessageBox.Show("Please select account title.", caption, MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            return true;
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            InitialTasks();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Window_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            trvAccountType.Visibility = Visibility.Hidden;
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
