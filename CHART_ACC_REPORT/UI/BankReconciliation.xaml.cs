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
    /// Interaction logic for BankReconciliation.xaml
    /// </summary>
    public partial class BankReconciliation : Window
    {
        BBankReconciliation objBBankReconciliation = new BBankReconciliation();
        BChartAccount objBChartOfAccount = new BChartAccount();
        //BMotherAccStatement objBMotherAccStatement = new BMotherAccStatement();
        long parentId = 0;
        bool childStatus = false;
        public BankReconciliation()
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
                MessageBox.Show("Problem Occur While Loading Fiscal Information.", "Bank Reconciliation", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void LoadAccountTypeTree()
        {
            trvAccountType.Items.Clear();
            try
            {
                long id = objBChartOfAccount.GetAccountID("Cash at Bank");
                TreeViewItem treeItem = new TreeViewItem();
                treeItem.IsExpanded = true;
                treeItem.Header = "Cash at Bank";
                treeItem.Name = "_" + id;
                ProcessTree(id, treeItem);
                trvAccountType.Items.Add(treeItem);
            }
            catch (Exception)
            {
                MessageBox.Show("Problem Occur While Loading Parent Account", "Bank Reconciliation", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        private void ProcessTree(long pId, TreeViewItem newChild)
        {
            foreach (EChartOfAccount obj in objBChartOfAccount.GetSubAccount(pId))
            {
                TreeViewItem child = new TreeViewItem();
                child.Header = obj.SubAccName;
                child.Name = "_" + obj.SubAccId;
                newChild.Items.Add(child);
                if (obj.AccHeader == "Yes")
                {
                    ProcessTree(obj.SubAccId, child);
                }
            }
        }
        private void trvAccountType_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var trvItem = (sender as TreeView).SelectedItem as TreeViewItem;
            if (trvItem != null && (trvItem.HasItems == false))
            {
                txtBlockAccountName.Text = trvItem.Header.ToString();
                string[] splitedId = (trvItem.Name.ToString()).Split('_');
                parentId = Convert.ToInt64(splitedId[1]);
                trvAccountType.Visibility = Visibility.Hidden;               
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
                    decimal grandTotal = 0;
                    List<EBankReconciliation> listBankReconcilation = objBBankReconciliation.GetBankReconciliation(Convert.ToDateTime(dtpFrom.SelectedDate), Convert.ToDateTime(dtpTo.SelectedDate), txtBlockAccountName.Text,parentId,ref grandTotal);

                    if (listBankReconcilation.Count > 0)
                    {
                        CrptBankReconciliation objCrptBankReconciliation = new CrptBankReconciliation();
                        AccountingRptUtility.SetDate(objCrptBankReconciliation, Convert.ToDateTime(dtpFrom.SelectedDate).ToShortDateString(), Convert.ToDateTime(dtpTo.SelectedDate).ToShortDateString());
                        AccountingRptUtility.setCompanyDeifinition(objCrptBankReconciliation, "Bank Reconciliation");
                        objCrptBankReconciliation.DataDefinition.FormulaFields["GrandTotal"].Text = grandTotal.ToString();
                        AccountingRptUtility.Display_report(objCrptBankReconciliation, listBankReconcilation, this);
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("Unknown problem occured while retriving journal information.\n" + exception.ToString(), "Bank Reconciliation", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }

        private bool AreAllFieldsCompleted()
        {
            if (dtpFrom.Text == string.Empty)
            {
                dtpFrom.Focus();
                MessageBox.Show("Please select from date.", "Bank Reconciliation", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            if (dtpTo.Text == string.Empty)
            {
                dtpTo.Focus();
                MessageBox.Show("Please select to date.", "Bank Reconciliation", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            //if (dtpTo.SelectedDate > Convert.ToDateTime(txtFiscalEndDate.Text))
            //{
            //    dtpTo.Focus();
            //    MessageBox.Show("Please select date between the current fiscal year.", "Bank Reconciliation", MessageBoxButton.OK, MessageBoxImage.Information);
            //    return false;
            //}
            if (dtpTo.SelectedDate < dtpFrom.SelectedDate)
            {
                MessageBox.Show("Please verify your date selection.", "Bank Reconciliation", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            if (txtBlockAccountName.Text == string.Empty)
            {
                btnParentAccount.Focus();
                MessageBox.Show("Please select Bank account.", "Bank Reconciliation", MessageBoxButton.OK, MessageBoxImage.Information);
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
