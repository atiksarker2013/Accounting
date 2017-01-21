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
//Author:Amin
namespace CHART_ACC_UI.UI
{
    /// <summary>
    /// Interaction logic for ChartOfAccountModify.xaml
    /// </summary>
    public partial class ChartOfAccountModify : Window
    {
        BChartOfAccountModify objBChartOfAccountModify = new BChartOfAccountModify();
        BChartOfAccount objBChartOfAccount = new BChartOfAccount();
        public ChartOfAccountModify()
        {
            InitializeComponent();
            LoadAccountTypeTree();
            LoadParentAccountTree();
            trvAccount.Visibility = Visibility.Hidden;
            populateStatusCombo();
        }

        private void populateStatusCombo()
        {
            string[] Status = { "Active", "Inactive" };
            cmbStatus.Items.Add(Status[0]);
            cmbStatus.Items.Add(Status[1]);
        }

        #region TREE

        private void LoadAccountTypeTree()
        {
            trvAccountType.Items.Clear();
            try
            {
                foreach (EAccountType objType in objBChartOfAccountModify.GetAllAccountType())
                {
                    TreeViewItem treeItem = new TreeViewItem();
                    treeItem.Header = objType.AccTypeName;
                    treeItem.Name = "_" + objType.AccTypeId;
                    ProcessTree(objType.AccTypeId, treeItem);
                    trvAccountType.Items.Add(treeItem);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error :\n" + ex.Message, "Chart of Account [Modify]", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void ProcessTree(long pId, TreeViewItem newChild)
        {
            foreach (EChartOfAccount obj in objBChartOfAccountModify.GetSubAccount(pId))
            {
                TreeViewItem newChield = new TreeViewItem();
                newChield.Header = obj.SubAccName;
                newChield.Name = "_" + obj.SubAccId;
                newChild.Items.Add(newChield);
                ProcessTree(obj.SubAccId, newChield);
            }
        }

        long accId = 0; string rootAccountName = ""; long parent_accId = 0; string myAccountName = "";
        private void trvAccountType_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var trvItem = trvAccountType.SelectedItem as TreeViewItem;
            if (trvItem != null)
            {               
                string acciuntTypeId = trvItem.Name.ToString();
                string[] splitedId = acciuntTypeId.Trim().Split('_');
                accId = Convert.ToInt64(splitedId[1]);
                List<EChartOfAccount> CodeList = objBChartOfAccountModify.GetAccountInfo(accId);
                foreach (var code in CodeList)
                {
                    if (code.AccHeader == "Yes")
                    {
                        MessageBox.Show("Mother account can not be updated.\nPlease update it's child account.", "Chart of Account [Modify]",MessageBoxButton.OK,MessageBoxImage.Information);
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
                        txtParentAccountName.Text = (trvItem.Parent as TreeViewItem).Header.ToString();
                        string[] splitedParentId = (trvItem.Parent as TreeViewItem).Name.ToString().Split('_');
                        parent_accId = Convert.ToInt64(splitedParentId[1]);
                        if (parent_accId >= 100 && parent_accId < 200)
                        {
                            txtParentAccountNo.Text = parent_accId.ToString();
                        }
                        else
                        {
                            txtParentAccountNo.Text = objBChartOfAccount.GetAccount_Code(parent_accId);
                        }       

                        myAccountName = trvItem.Header.ToString();
                        txtAccountName.Text = trvItem.Header.ToString();
                        txtAccountCode.Text = code.SubAccCode;
                        cmbStatus.Text = code.AccStatus;
                    }
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

        #endregion

        #region PARENT TREE

        private void LoadParentAccountTree()
        {
            trvAccount.Items.Clear();
            try
            {
                foreach (EAccountType objType in objBChartOfAccountModify.GetAllAccountType())
                {
                    TreeViewItem treeItem = new TreeViewItem();
                    treeItem.Header = objType.AccTypeName;
                    treeItem.Name = "_" + objType.AccTypeId;
                    ProcessParentTree(objType.AccTypeId, treeItem);
                    trvAccount.Items.Add(treeItem);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error :\n" + ex.Message, "Chart of Account [Modify]", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void ProcessParentTree(long pId, TreeViewItem newChild)
        {
            foreach (EChartOfAccount obj in objBChartOfAccountModify.GetSubAccount(pId))
            {
                TreeViewItem newChield = new TreeViewItem();
                newChield.Header = obj.SubAccName;
                newChield.Name = "_" + obj.SubAccId;
                newChild.Items.Add(newChield);
                ProcessParentTree(obj.SubAccId, newChield);
            }
        }

        long NewParentAccId = 0; string NewrootAccountName = "";
        private void trvAccount_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
             var trvItem = trvAccount.SelectedItem as TreeViewItem;
             if (trvItem != null)
             {
                 if (MessageBox.Show("Your parent account name will be change.\nAre sure want to change account name ?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                 {
                     txtNewParentAccountName.Text = trvItem.Header.ToString();
                     string acciuntTypeId = trvItem.Name.ToString();
                     string[] splitedId = acciuntTypeId.Trim().Split('_');
                     NewParentAccId = Convert.ToInt64(splitedId[1]);

                     if (NewParentAccId >= 100 && NewParentAccId < 200)
                     {
                         NewrootAccountName = trvItem.Header.ToString();
                     }
                     else
                     {
                         GetNewRootAccountName(trvItem);
                     }
                     trvAccount.Visibility = Visibility.Hidden;
                 }
             }
        }

        private void GetNewRootAccountName(TreeViewItem trvItem)
        {
            TreeViewItem tree = trvItem.Parent as TreeViewItem;
            if (tree == null)
            {
                return;
            }
            else
            {
                NewrootAccountName = tree.Header.ToString();
                GetNewRootAccountName(tree);
            }
        }

        #endregion

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtNewParentAccountName.Text != "")
                {
                    UpdateChartAccountNewParent();
                }
                else
                {
                    UpdateChartAccount();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error :\n" + ex.Message, "Chart of Account [Modify]", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateChartAccount()
        {
            if (CheckField() == true)
            {
                EChartOfAccount objEChartAccount = new EChartOfAccount();

                objEChartAccount.SubAccName = txtAccountName.Text;
                objEChartAccount.SubAccId = accId;
                objEChartAccount.AccStatus = cmbStatus.Text;
                objEChartAccount.ParentAccId = parent_accId;
                objEChartAccount.RootAccName = rootAccountName;

                if (objBChartOfAccountModify.UpdateChartAccount(objEChartAccount) == true)
                {
                    MessageBox.Show("Chart of account updated successfully.", "Chart of Account [Modify]", MessageBoxButton.OK, MessageBoxImage.Information);
                    ResetFields();
                }
                else
                {
                    MessageBox.Show("Chart of account updating failed.", "Chart of Account [Modify]", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void UpdateChartAccountNewParent()
        {
            if (CheckFieldNewParent() == true)
            {
                EChartOfAccount objEChartAccount = new EChartOfAccount();

                objEChartAccount.SubAccName = txtAccountName.Text;
                objEChartAccount.SubAccId = accId;
                objEChartAccount.AccStatus = cmbStatus.Text;
                objEChartAccount.ParentAccId = NewParentAccId;
                objEChartAccount.RootAccName = NewrootAccountName;

                if (objBChartOfAccountModify.UpdateChartAccountNewParent(objEChartAccount,parent_accId) == true)
                {
                    MessageBox.Show("Chart of account updated successfully.", "Chart of Account [Modify]", MessageBoxButton.OK, MessageBoxImage.Information);
                    ResetFields();
                }
                else
                {
                    MessageBox.Show("Chart of account updating failed.", "Chart of Account [Modify]", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        #region CHECK-FIELD & RESET

        private void ResetFields()
        {
            txtAccountName.Text = string.Empty;
            txtAccountCode.Text = string.Empty;
            txtParentAccountName.Text = string.Empty;
            txtParentAccountNo.Text = string.Empty;
            txtNewParentAccountName.Text = string.Empty;
            cmbStatus.SelectedIndex = -1;
            LoadAccountTypeTree();
            LoadParentAccountTree();
            accId = 0;
            rootAccountName = "";
            parent_accId = 0;
            myAccountName = "";
            NewParentAccId = 0;
            NewrootAccountName = "";
        }

        private bool CheckField()
        {
            if (txtAccountName.Text == string.Empty)
            {
                MessageBox.Show("Your account name should not be empty.", "Chart of Account [Modify]", MessageBoxButton.OK, MessageBoxImage.Information);
                txtAccountName.Focus();
                return false;
            }
            if (txtAccountCode.Text == string.Empty)
            {
                MessageBox.Show("Your account no should not be empty.", "Chart of Account [Modify]", MessageBoxButton.OK, MessageBoxImage.Information);
                txtAccountCode.Focus();
                return false;
            }
            if (cmbStatus.Text == string.Empty)
            {
                MessageBox.Show("Select your account status.", "Chart of Account [Modify]", MessageBoxButton.OK, MessageBoxImage.Information);
                cmbStatus.Focus();
                return false;
            }
            return true;
        }

        private bool CheckFieldNewParent()
        {
            if (txtAccountName.Text == string.Empty)
            {
                MessageBox.Show("Your account name should not be empty.", "Chart of Account [Modify]", MessageBoxButton.OK, MessageBoxImage.Information);
                txtAccountName.Focus();
                return false;
            }
            if (txtAccountCode.Text == string.Empty)
            {
                MessageBox.Show("Your account no should not be empty.", "Chart of Account [Modify]", MessageBoxButton.OK, MessageBoxImage.Information);
                txtAccountCode.Focus();
                return false;
            }
            if (cmbStatus.Text == string.Empty)
            {
                MessageBox.Show("Select your account status.", "Chart of Account [Modify]", MessageBoxButton.OK, MessageBoxImage.Information);
                cmbStatus.Focus();
                return false;
            }
            if (objBChartOfAccountModify.DoesExistInJournal(myAccountName) == true)
            {
                MessageBox.Show("You cannot change this account.\nBeacuse this account have some transaction", "Chart of Account [Modify]", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            return true;
        }

        #endregion

        #region WINDOW EVENT

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            ResetFields();
            LoadAccountTypeTree();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.ShowInTaskbar = true;
            this.Owner = null;
        }

        private void btnNewParentAccount_Click(object sender, RoutedEventArgs e)
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

        private void Window_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            trvAccount.Visibility = Visibility.Hidden;
        }

        #endregion

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (accId > 0)
                {
                    if (objBChartOfAccountModify.DoesExistInJournal(myAccountName) == false)
                    {
                        if (MessageBox.Show("Are you want to delete this  account?\nThis account's information will be permanently deleted.", "Chart of Account [Modify]", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        {
                            if (objBChartOfAccountModify.DeleteChartAccount(accId) == true)
                            {
                                MessageBox.Show("This account deleted successfully..", "Chart of Account [Modify]", MessageBoxButton.OK, MessageBoxImage.Information);
                                objBChartOfAccountModify.UpdateOldParentHeaderStat(parent_accId);
                                ResetFields();
                            }
                            else
                            {
                                MessageBox.Show("Account delete operation failed.", "Chart of Account [Modify]", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("You cannot delete this account.\nBeacuse this account have some transaction", "Chart of Account [Modify]", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Select any child account, which you want to delete.", "Chart of Account [Modify]", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : DeleteChartAccount().\n" + ex.Message, "Chart of Account [Modify]", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
