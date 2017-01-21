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
using CHART_ACC_ENTITY;
using CHART_ACC_BLL;

namespace CHART_ACC_UI.UI
{
    /// <summary>
    /// Interaction logic for BalanceUpdate.xaml
    /// </summary>
    public partial class BalanceUpdate : Window
    {
        public BalanceUpdate()
        {
            InitializeComponent();
            populateDebitCreditCombo();
            LoadAccountTypeTree();
            trvAccount.Visibility = Visibility.Hidden;
        }

        private void populateDebitCreditCombo()
        {
            string[] BalanceType = { "DR", "CR" };
            cmbDebitCredit.Items.Add(BalanceType[0]);
            cmbDebitCredit.Items.Add(BalanceType[1]);
        }

        private void LoadAccountTypeTree()
        {
            trvAccount.Items.Clear();
            try
            {
                foreach (EAccountType objType in new BChartOfAccountModify().GetAllAccountType())
                {
                    TreeViewItem treeItem = new TreeViewItem();
                    treeItem.Header = objType.AccTypeName;
                    treeItem.Name = "_" + objType.AccTypeId;
                    ProcessTree(objType.AccTypeId, treeItem);
                    trvAccount.Items.Add(treeItem);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error :\n" + ex.Message, "Chart of Account [Modify]", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void ProcessTree(long pId, TreeViewItem newChild)
        {
            foreach (EChartOfAccount obj in new BChartOfAccountModify().GetSubAccount(pId))
            {
                TreeViewItem newChield = new TreeViewItem();
                newChield.Header = obj.SubAccName;
                newChield.Name = "_" + obj.SubAccId;
                newChild.Items.Add(newChield);
                ProcessTree(obj.SubAccId, newChield);
            }
        }

        long accId = 0; string rootAccountName = ""; long parent_accId = 0;
        private void trvAccount_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var trvItem = trvAccount.SelectedItem as TreeViewItem;
            if (trvItem != null)
            {
                string acciuntTypeId = trvItem.Name.ToString();
                string[] splitedId = acciuntTypeId.Trim().Split('_');
                accId = Convert.ToInt64(splitedId[1]);
                List<EChartOfAccount> CodeList = new BChartOfAccountModify().GetAccountInfo(accId);
                foreach (var code in CodeList)
                {
                    if (code.AccHeader == "Yes")
                    {
                        MessageBox.Show("Mother account can not be updated.\nPlease update it's child account.", "Chart of Account [Modify]", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        if (accId >= 100 && accId < 200)
                        {
                            rootAccountName = trvItem.Header.ToString();
                        }
                        else
                        {
                            GetRootAccountName(trvItem);
                        }
                        txtParentAccName.Text = (trvItem.Parent as TreeViewItem).Header.ToString();
                        string[] splitedParentId = (trvItem.Parent as TreeViewItem).Name.ToString().Split('_');
                        parent_accId = Convert.ToInt64(splitedParentId[1]);
                        if (parent_accId >= 100 && parent_accId < 200)
                        {
                            txtParentAccountNo.Text = parent_accId.ToString();
                        }
                        else
                        {
                            txtParentAccountNo.Text = new BChartOfAccount().GetAccount_Code(parent_accId);
                        }
                        txtAccountName.Text = trvItem.Header.ToString();
                        txtAccountNo.Text = code.SubAccCode;
                    }
                    ChangeBalanceStatus();
                }
            }
        }

        private void GetRootAccountName(TreeViewItem trvItem)
        {
            TreeViewItem tree = trvItem.Parent as TreeViewItem;
            if (tree == null)
            {
                return;
            }
            else
            {
                rootAccountName = tree.Header.ToString();
                GetRootAccountName(tree);
            }
        }

        private void ChangeBalanceStatus()
        {
            if (rootAccountName == "ASSET" || rootAccountName == "EXPENSE")
            {
                cmbDebitCredit.SelectedIndex = 0;
            }
            else
            {
                cmbDebitCredit.SelectedIndex = 1;
            }
        }

        #region WINDOW
        private void btnParentAccount_Click(object sender, RoutedEventArgs e)
        {
            if (trvAccount.IsVisible == false)
            {
                trvAccount.Visibility = Visibility.Visible;
            }
            else
            {
                trvAccount.Visibility = Visibility.Hidden;
            }
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
        }

        private void Window_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            trvAccount.Visibility = Visibility.Hidden;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.ShowInTaskbar = true;
            this.Owner = null;
        }
        #endregion

        private void txtAccountNo_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                {
                    if (txtAccountNo.Text != string.Empty)
                    {
                        List<EChartOfAccount> CodeList = new BJournalVouchar().GetAccountInfoCodeWise(txtAccountNo.Text.Trim());
                        if (CodeList.Count > 0)
                        {
                            foreach (var code in CodeList)
                            {
                                if (code.AccHeader == "No")
                                {
                                    txtAccountNo.Text = code.SubAccCode;
                                    txtAccountName.Text = code.SubAccName;
                                    parent_accId = code.SubAccId;
                                    accId = code.SubAccId;
                                    rootAccountName = code.RootAccName;

                                    foreach (TreeViewItem tree in trvAccount.Items)
                                    {
                                        if (tree.Header.ToString() == rootAccountName)
                                        {
                                            tree.IsExpanded = true;
                                            SelectedIndexForTree(tree);
                                            break;
                                        }
                                    }
                                    ChangeBalanceStatus();
                                }

                                else
                                {
                                    MessageBox.Show("You can't make transaction with this account.\n Please use its child account.", "Update Balance", MessageBoxButton.OK, MessageBoxImage.Information);
                                }
                            }
                        }
                        else
                        {
                            txtParentAccName.Text = string.Empty;
                            txtParentAccountNo.Text = string.Empty;
                            txtAccountName.Text = string.Empty;
                            LoadAccountTypeTree();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem occur\n" + ex.Message, "Update Balance", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SelectedIndexForTree(TreeViewItem tr)
        {

            foreach (TreeViewItem tr1 in tr.Items)
            {
                if (tr1.Name == "_" + accId.ToString())
                {
                    tr1.IsExpanded = true;
                    tr1.IsSelected = true;
                    ParentExpand(tr1);
                    break;
                }
                else
                {
                    if (tr1.HasItems)
                    {
                        SelectedIndexForTree(tr1);
                    }
                }
            }
        }

        private void ParentExpand(TreeViewItem trItem)
        {
            TreeViewItem tr = trItem.Parent as TreeViewItem;
            if (tr != null)
            {
                tr.IsExpanded = true;
                ParentExpand(tr);
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (cmbDebitCredit.Text == "DR")
            {
                UpdateBalanceDebit();
            }
            else
            {
                UpdateBalanceCredit();
            }
        }

        private void UpdateBalanceDebit()
        {
            try
            {
                if (txtAccountName.Text != string.Empty)
                {
                    EChartOfAccount objChartAccount = new EChartOfAccount();

                    objChartAccount.SubAccId = accId;
                    objChartAccount.AccOpeningBalanceDR = Convert.ToDecimal(txtOpeningBalance.Text);

                    if (new BChartOfAccountModify().UpdateBalanceDebit(objChartAccount) == true)
                    {
                        MessageBox.Show("Balance updated successfully.", "Update Balance", MessageBoxButton.OK, MessageBoxImage.Information);
                        ResetField();
                    }
                    else
                    {
                        MessageBox.Show("Balance updatinf failed.", "Update Balance", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Please select your account.", "Update Balance", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : UpdateBalanceDebit().\n" + ex.Message, "Update Balance", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void UpdateBalanceCredit()
        {
            try
            {
                if (txtAccountName.Text != string.Empty)
                {
                    EChartOfAccount objChartAccount = new EChartOfAccount();

                    objChartAccount.SubAccId = accId;
                    objChartAccount.AccOpeningBalanceCR = Convert.ToDecimal(txtOpeningBalance.Text);

                    if (new BChartOfAccountModify().UpdateBalanceCredit(objChartAccount) == true)
                    {
                        MessageBox.Show("Balance updated successfully.", "Update Balance", MessageBoxButton.OK, MessageBoxImage.Information);
                        ResetField();
                    }
                    else
                    {
                        MessageBox.Show("Balance updatinf failed.", "Update Balance", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Please select your account.", "Update Balance", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : UpdateBalanceCredit().\n" + ex.Message, "Update Balance", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ResetField()
        {
            txtParentAccName.Text = string.Empty;
            txtParentAccountNo.Text = string.Empty;
            txtAccountName.Text = string.Empty;
            txtAccountNo.Text = string.Empty;
            txtOpeningBalance.Text = string.Empty;
            cmbDebitCredit.SelectedIndex = -1;
            LoadAccountTypeTree();
            trvAccount.Visibility = Visibility.Hidden;
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            ResetField();
        }

    }
}
