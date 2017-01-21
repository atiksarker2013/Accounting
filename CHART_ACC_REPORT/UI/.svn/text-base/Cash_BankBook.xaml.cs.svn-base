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
    /// Interaction logic for Cash_BankBook.xaml
    /// </summary>
    public partial class Cash_BankBook : Window
    {
        BAccountStatement objBAccountStatement = new BAccountStatement();
        BChartAccount objBChartOfAccount = new BChartAccount();
        long parentId = 0; string caption = "Cash and Bank Book";
        bool childStatus = false;
        bool bankStatus = false;
        public Cash_BankBook()
        {
            InitializeComponent();
            InitialTasks();
        }
        private void InitialTasks()
        {
            SetFiscalDate();
            dtpTo.SelectedDate = DateTime.Now;
            dtpFrom.SelectedDate = DateTime.Now;
            txtBlockAccountName.Text = string.Empty;
            LoadBankAccountTypeTree();
            trvCashBankAccountType.Visibility = Visibility.Hidden;            
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
        private void LoadBankAccountTypeTree()
        {
            trvCashBankAccountType.Items.Clear();
            try
            {
                long id = objBChartOfAccount.GetAccountID("Cash at Bank");
                TreeViewItem treeItem = new TreeViewItem();
                treeItem.IsExpanded = true;
                treeItem.Header = "Cash at Bank";
                treeItem.Name = "_" + id;
                ProcessTreeCash(id, treeItem);
                trvCashBankAccountType.Items.Add(treeItem);

                long idCash = objBChartOfAccount.GetAccountID("Cash in Hand");
                TreeViewItem treeItemCash = new TreeViewItem();
                treeItemCash.IsExpanded = true;
                treeItemCash.Header = "Cash in Hand";
                treeItemCash.Name = "_" + idCash;
                trvCashBankAccountType.Items.Add(treeItemCash);
            }
            catch (Exception)
            {
                MessageBox.Show("Problem Occur While Loading Cash and Bank Account", caption, MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void ProcessTreeCash(long pId, TreeViewItem newChild)
        {
            foreach (EChartOfAccount obj in objBChartOfAccount.GetSubAccount(pId))
            {
                TreeViewItem trChild = new TreeViewItem();
                trChild.Header = obj.SubAccName;
                trChild.Name = "_" + obj.SubAccId;
                newChild.Items.Add(trChild);
                if (obj.AccHeader == "Yes")
                {
                    ProcessTreeCash(obj.SubAccId, trChild);
                }
            }
        }        
        private void trvCashBankAccountType_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                var trvItem = (sender as TreeView).SelectedItem as TreeViewItem;
                if (trvItem != null)
                {
                    txtBlockAccountName.Text = trvItem.Header.ToString();                    
                    string[] splitedId = (trvItem.Name.ToString()).Split('_');
                    parentId = Convert.ToInt64(splitedId[1]);
                    trvCashBankAccountType.Visibility = Visibility.Hidden;
                    if (parentId >= 100 && parentId < 200)
                    {
                        childStatus = true;
                    }
                    else
                    {
                        if (objBChartOfAccount.GetAccountInfo(parentId).AccHeader == "Yes")
                        {
                            childStatus = true;
                        }
                        else
                        {
                            childStatus = false;
                            bankStatus = false;
                            CheckBank(trvItem);//call this method for checking this account is under of bank
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void CheckBank(TreeViewItem trvItem)
        {
            TreeViewItem tr = trvItem.Parent as TreeViewItem;
            if (tr != null)
            {
                if (tr.Header.ToString() == "Cash at Bank")
                {
                    bankStatus = true;
                }
                else
                {
                    CheckBank(tr);
                }
            }
        }
        private void btnCashBankAccount_Click(object sender, RoutedEventArgs e)
        {
            if (trvCashBankAccountType.IsVisible == false)
            {
                trvCashBankAccountType.Visibility = Visibility.Visible;
            }
            else
            {
                trvCashBankAccountType.Visibility = Visibility.Hidden;
            }
        }
        private void btnShow_Click(object sender, RoutedEventArgs e)
        {
            try
            {                
                if (AreAllFieldsCompleted())
                {
                    if (childStatus == true)
                    {
                        List<EGL> listGL = new BGL().LoadListofGLParticaularIdwise(parentId, txtBlockAccountName.Text, Convert.ToDateTime(dtpFrom.SelectedDate), Convert.ToDateTime(dtpTo.SelectedDate),true);
                        if (listGL.Count > 0)
                        {
                            CrptGL objCrptGL = new CrptGL();
                            AccountingRptUtility.SetDate(objCrptGL, Convert.ToDateTime(dtpFrom.SelectedDate).ToShortDateString(), Convert.ToDateTime(dtpTo.SelectedDate).ToShortDateString());
                            AccountingRptUtility.setCompanyDeifinition(objCrptGL, txtBlockAccountName.Text+" Book");
                            AccountingRptUtility.Display_report(objCrptGL, listGL, this);
                        }
                    }
                    else
                    {
                        if (bankStatus)
                        {
                            List<EBankReconciliation> listEBankStatement = new BBankReconciliation().GetBankStatement(Convert.ToDateTime(dtpFrom.SelectedDate), Convert.ToDateTime(dtpTo.SelectedDate), txtBlockAccountName.Text, parentId);

                            if (listEBankStatement.Count > 0)
                            {
                                CrptBankStatement objCrptBankStatement = new CrptBankStatement();
                                AccountingRptUtility.SetDate(objCrptBankStatement, Convert.ToDateTime(dtpFrom.SelectedDate).ToShortDateString(), Convert.ToDateTime(dtpTo.SelectedDate).ToShortDateString());
                                AccountingRptUtility.setCompanyDeifinition(objCrptBankStatement, txtBlockAccountName.Text + " Book");
                                AccountingRptUtility.Display_report(objCrptBankStatement, listEBankStatement, this);
                            }
                        }
                        else
                        {
                            List<EAccountStatement> listECreateAccountStatement = objBAccountStatement.GetAccStatement(Convert.ToDateTime(dtpFrom.SelectedDate), Convert.ToDateTime(dtpTo.SelectedDate), txtBlockAccountName.Text, parentId);

                            if (listECreateAccountStatement.Count > 0)
                            {
                                CrptAccountStatement objCrptAccountStatement = new CrptAccountStatement();
                                AccountingRptUtility.SetDate(objCrptAccountStatement, Convert.ToDateTime(dtpFrom.SelectedDate).ToShortDateString(), Convert.ToDateTime(dtpTo.SelectedDate).ToShortDateString());
                                AccountingRptUtility.setCompanyDeifinition(objCrptAccountStatement, txtBlockAccountName.Text+" Book");
                                AccountingRptUtility.Display_report(objCrptAccountStatement, listECreateAccountStatement, this);
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("Unknown problem occured while retriving information.\n" + exception.Message, caption, MessageBoxButton.OK, MessageBoxImage.Information);
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
            if (dtpTo.Text == string.Empty)
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
            if (txtBlockAccountName.Text == string.Empty)
            {
                btnCashBankAccount.Focus();
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
            trvCashBankAccountType.Visibility = Visibility.Hidden;
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
