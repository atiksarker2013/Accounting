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
    public partial class GLHeadWiseStatement : Window
    {
        BAccountStatement objBAccountStatement = new BAccountStatement();
        BChartAccount objBChartOfAccount = new BChartAccount();        
        long parentId = 0;
        bool childStatus = false;
        bool bankStatus = false;
        string caption = "GL Head Wise Statement";
        public GLHeadWiseStatement()
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
            listBoxSuggestion.Visibility = Visibility.Hidden;
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
            try
            {
                var trvItem = (sender as TreeView).SelectedItem as TreeViewItem;
                if (trvItem != null)
                {
                    txtBlockAccountName.Text = trvItem.Header.ToString();
                    listBoxSuggestion.Visibility = Visibility.Hidden;
                    string[] splitedId = (trvItem.Name.ToString()).Split('_');
                    parentId = Convert.ToInt64(splitedId[1]);
                    trvAccountType.Visibility = Visibility.Hidden;
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
            catch(Exception) 
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
                    if (childStatus == true)
                    {
                        List<EGL> listGL = new BGL().LoadListofGLParticaularIdwise(parentId, txtBlockAccountName.Text, Convert.ToDateTime(dtpFrom.SelectedDate), Convert.ToDateTime(dtpTo.SelectedDate),true);
                        if (listGL.Count > 0)
                        {
                            CrptGL objCrptGL = new CrptGL();
                            AccountingRptUtility.SetDate(objCrptGL, Convert.ToDateTime(dtpFrom.SelectedDate).ToShortDateString(), Convert.ToDateTime(dtpTo.SelectedDate).ToShortDateString());
                            AccountingRptUtility.setCompanyDeifinition(objCrptGL, caption);
                            AccountingRptUtility.Display_report(objCrptGL, listGL, this);
                        }
                    }
                    else
                    {
                        if (bankStatus)
                        {
                            List<EBankReconciliation> listEBankStatement = new BBankReconciliation().GetBankStatement(Convert.ToDateTime(dtpFrom.SelectedDate), Convert.ToDateTime(dtpTo.SelectedDate), txtBlockAccountName.Text,parentId);

                            if (listEBankStatement.Count > 0)
                            {
                                CrptBankStatement objCrptBankStatement = new CrptBankStatement();
                                AccountingRptUtility.SetDate(objCrptBankStatement, Convert.ToDateTime(dtpFrom.SelectedDate).ToShortDateString(), Convert.ToDateTime(dtpTo.SelectedDate).ToShortDateString());
                                AccountingRptUtility.setCompanyDeifinition(objCrptBankStatement, caption);
                                AccountingRptUtility.Display_report(objCrptBankStatement, listEBankStatement, this);
                            }
                        }
                        else
                        {
                            List<EAccountStatement> listECreateAccountStatement = objBAccountStatement.GetAccStatement(Convert.ToDateTime(dtpFrom.SelectedDate), Convert.ToDateTime(dtpTo.SelectedDate), txtBlockAccountName.Text,parentId);

                            if (listECreateAccountStatement.Count > 0)
                            {
                                CrptAccountStatement objCrptAccountStatement = new CrptAccountStatement();
                                AccountingRptUtility.SetDate(objCrptAccountStatement, Convert.ToDateTime(dtpFrom.SelectedDate).ToShortDateString(), Convert.ToDateTime(dtpTo.SelectedDate).ToShortDateString());
                                AccountingRptUtility.setCompanyDeifinition(objCrptAccountStatement, caption);
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
        private void txtBlockAccountName_TextChanged(object sender, TextChangedEventArgs e)
        {
            listBoxSuggestion.Items.Clear();
            if (txtBlockAccountName.Text != "")
            {
                List<EChartOfAccount> namelist = objBChartOfAccount.GetAccountInfoLikeNameWise(txtBlockAccountName.Text);
                if (namelist.Count > 0)
                {
                    listBoxSuggestion.Visibility = Visibility.Visible;
                    foreach (var obj in namelist)
                    {
                        listBoxSuggestion.Items.Add(obj);
                    }
                }
                else
                {
                    parentId = 0;
                    listBoxSuggestion.Visibility = Visibility.Hidden;
                }
            }
            else
            {
                listBoxSuggestion.Visibility = Visibility.Hidden;
            }
        }

        private void txtBlockAccountName_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down)
            {
                listBoxSuggestion.Focus();
            }
        }

        private void listBoxSuggestion_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (listBoxSuggestion.SelectedIndex == 0 && e.Key == Key.Up)
            {
                txtBlockAccountName.Focus();
            }
        }

        private void listBoxSuggestion_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (listBoxSuggestion.SelectedIndex > -1)
                {
                    if (e.Key == Key.Enter)
                    {
                        EChartOfAccount objSelectedCa = listBoxSuggestion.SelectedItem as EChartOfAccount;
                        txtBlockAccountName.Text = objSelectedCa.SubAccName;
                        parentId = objSelectedCa.SubAccId;
                        listBoxSuggestion.Visibility = Visibility.Hidden;
                        foreach (TreeViewItem tree in trvAccountType.Items)
                        {
                            if (tree.Header.ToString() == objSelectedCa.RootAccName)
                            {
                                tree.IsExpanded = true;
                                SetSelectedIndexCash(tree);
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }
        private void SetSelectedIndexCash(TreeViewItem tr)
        {
            foreach (TreeViewItem trItem in tr.Items)
            {
                if (trItem.Name == "_" + parentId.ToString())
                {
                    trItem.IsExpanded = true;
                    trItem.IsSelected = true;
                    ParentExpandCash(trItem);
                    break;
                }
                else
                {
                    if (trItem.HasItems)
                    {
                        SetSelectedIndexCash(trItem);
                    }
                }
            }
        }

        private void ParentExpandCash(TreeViewItem trItem)
        {
            TreeViewItem tr = trItem.Parent as TreeViewItem;
            if (tr != null)
            {
                tr.IsExpanded = true;
                ParentExpandCash(tr);
            }
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
