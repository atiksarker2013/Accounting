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
    /// Interaction logic for MemberAccountType.xaml
    /// </summary>
    public partial class MemberAccountType : Window
    {
        BMemberAccountType objBMemberAccountType = new BMemberAccountType();
        BChartOfAccount objBChartOfAccount = new BChartOfAccount();
        string User_Name = "AMIN";
        public MemberAccountType()
        {
            InitializeComponent();
            LoadParentAccountTree();
            trvAccount.Visibility = Visibility.Hidden;
            populateStatusCombo();
        }

        #region WINDOW
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
        #endregion

        #region PARENT TREE

        private void LoadParentAccountTree()
        {
            trvAccount.Items.Clear();
            try
            {
                foreach (EAccountType objType in objBChartOfAccount.GetAllAccountType())
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
                MessageBox.Show("Error : LoadParentAccountTree().\n" + ex.Message, "Member/Customer Account Type", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void ProcessParentTree(long pId, TreeViewItem newChild)
        {
            foreach (EChartOfAccount obj in objBChartOfAccount.GetSubAccount(pId))
            {
                TreeViewItem newChield = new TreeViewItem();
                newChield.Header = obj.SubAccName;
                newChield.Name = "_" + obj.SubAccId;
                newChild.Items.Add(newChield);
                ProcessParentTree(obj.SubAccId, newChield);
            }
        }

        long ParentAccId; string rootAccountName = "";

        private void GetNewRootAccountName(TreeViewItem trvItem)
        {
            TreeViewItem tree = trvItem.Parent as TreeViewItem;
            if (tree == null)
            {
                return;
            }
            else
            {
                rootAccountName = tree.Header.ToString();
                GetNewRootAccountName(tree);
            }
        }

        private void trvAccount_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                var trvItem = trvAccount.SelectedItem as TreeViewItem;
                if (trvItem != null)
                {
                    txtParentAccountName.Text = trvItem.Header.ToString();
                    string acciuntTypeId = trvItem.Name.ToString();
                    string[] splitedId = acciuntTypeId.Trim().Split('_');
                    ParentAccId = Convert.ToInt64(splitedId[1]);
                    if (ParentAccId >= 100 && ParentAccId < 200)
                    {
                        txtParentAccountNo.Text = ParentAccId.ToString();
                    }
                    else
                    {
                        txtParentAccountNo.Text = objBChartOfAccount.GetAccount_Code(ParentAccId);
                    }
                    if (ParentAccId >= 100 && ParentAccId < 200)
                    {
                        rootAccountName = trvItem.Header.ToString();
                    }
                    else
                    {
                        GetNewRootAccountName(trvItem);
                    }
                    trvAccount.Visibility = Visibility.Hidden;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : trvAccount_SelectedItemChanged().\n" + ex.Message, "Member/Customer Account Type", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Window_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            trvAccount.Visibility = Visibility.Hidden;
        }

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

        #endregion

        private void populateStatusCombo()
        {
            string[] Status = { "Active", "Inactive" };
            cmbStatus.Items.Add(Status[0]);
            cmbStatus.Items.Add(Status[1]);
            cmbStatus.SelectedIndex = 0;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (CheckField() == true)
            {
                SaveMemberAccountType();
            }
        }

        private void SaveMemberAccountType()
        {
            try
            {
                EMemberAccountType objMemberAccountType = new EMemberAccountType();

                objMemberAccountType.Account_Name = txtAccountTypeName.Text;
                objMemberAccountType.Account_No = txtAccountNo.Text;
                objMemberAccountType.Parent_Account_No = ParentAccId;
                objMemberAccountType.Parent_Account_Type = rootAccountName;
                objMemberAccountType.Account_Status = cmbStatus.Text;
                objMemberAccountType.Access_By = User_Name;

                long Acc_Type_Id = 0;
                if (objBMemberAccountType.DoesPrefixExist(txtAccountPrefix.Text) == false)
                {
                    if (objBMemberAccountType.DoesAccountNoExist(txtAccountNo.Text) == false)
                    {
                        Acc_Type_Id = objBMemberAccountType.SaveAccountTypeINChartOfAccount(objMemberAccountType);
                    }
                    else
                    {
                        MessageBox.Show("Your given account code/no already exist.\nPLease change account no.", "Member/Customer Account Type", MessageBoxButton.OK, MessageBoxImage.Information);  
                    }
                }
                else
                {
                    MessageBox.Show("Your given account prefix already exist.\nPLease change account prefix.", "Member/Customer Account Type", MessageBoxButton.OK, MessageBoxImage.Information);                    
                }
                if (Acc_Type_Id > 0)
                {
                    if (objBMemberAccountType.SaveMemberAccountType(Acc_Type_Id,txtAccountPrefix.Text) == true)
                    {
                        MessageBox.Show("Account type saved successfully.", "Member/Customer Account Type", MessageBoxButton.OK, MessageBoxImage.Information);
                        ResetField();
                    }
                    else
                    {
                        MessageBox.Show("Account type saving failed.", "Member/Customer Account Type", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : SaveMemberAccountType().\n" + ex.Message, "Member/Customer Account Type", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #region CHECK & RESET

        private bool CheckField()
        {
            if (txtParentAccountName.Text == string.Empty)
            {
                MessageBox.Show("Please select your parent account.", "Member/Customer Account Type", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            if (txtAccountTypeName.Text == string.Empty)
            {
                MessageBox.Show("Please enter your account type name.", "Member/Customer Account Type", MessageBoxButton.OK, MessageBoxImage.Information);
                txtAccountTypeName.Focus();
                return false;
            }
            if (txtAccountPrefix.Text == string.Empty)
            {
                MessageBox.Show("Please enter your account type prefix.", "Member/Customer Account Type", MessageBoxButton.OK, MessageBoxImage.Information);
                txtAccountPrefix.Focus();
                return false;
            }
            if (txtAccountNo.Text == string.Empty)
            {
                MessageBox.Show("Please enter your account type no.", "Member/Customer Account Type", MessageBoxButton.OK, MessageBoxImage.Information);
                txtAccountNo.Focus();
                return false;
            }
            if (cmbStatus.Text == string.Empty)
            {
                MessageBox.Show("Please select your account status.", "Member/Customer Account Type", MessageBoxButton.OK, MessageBoxImage.Information);
                cmbStatus.Focus();
                return false;
            }
            return true;
        }

        private void ResetField()
        {
            txtParentAccountName.Text = string.Empty;
            txtParentAccountNo.Text = string.Empty;
            txtAccountTypeName.Text = string.Empty;
            txtAccountPrefix.Text = string.Empty;
            txtAccountNo.Text = string.Empty;
            LoadParentAccountTree();
            trvAccount.Visibility = Visibility.Hidden;
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            ResetField();
        }

        #endregion
    }
}
