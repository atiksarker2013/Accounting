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
//Modified By:Refat(17-01-2012)

namespace CHART_ACC_UI.UI
{
    /// <summary>
    /// Interaction logic for ChartOfAccount.xaml
    /// </summary>
    public partial class ChartOfAccount : Window
    {
        BChartOfAccount objBChartOfAccount = new BChartOfAccount();
        string loginUsername = "AMIN";
        public ChartOfAccount()
        {
            InitializeComponent();
            LoadAccountTypeTree();
            populateBalanceTypeCombo();
            populateStatusCombo();            
        }

        private void populateBalanceTypeCombo()
        {
            string[] BalanceType = { "DR", "CR" };
            cmbOpBalanceType.Items.Add(BalanceType[0]);
            cmbOpBalanceType.Items.Add(BalanceType[1]);
        }

        private void populateStatusCombo()
        {
            string[] Status = { "Active", "Inactive" };
            cmbStatus.Items.Add(Status[0]);
            cmbStatus.Items.Add(Status[1]);
            cmbStatus.SelectedIndex = 0;
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CheckField() == true)
                {
                    SaveChartOfAccount();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error :\n" + ex.Message, "Chart of Account", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SaveChartOfAccount()
        {
            try
            {
                if (objBChartOfAccount.DoesCodeExist(txtAccountCode.Text) == false)
                {
                    EChartOfAccount objEChartOfAccount = new EChartOfAccount();
                    objEChartOfAccount.ParentAccId = parentId;
                    objEChartOfAccount.RootAccName = rootName;
                    objEChartOfAccount.SubAccCode = txtAccountCode.Text;
                    objEChartOfAccount.SubAccName = txtSubAccount.Text;
                    if (cmbOpBalanceType.Text == "DR")
                    {
                        objEChartOfAccount.AccOpeningBalanceDR = Convert.ToDecimal(txtOpeningBalance.Text.Trim());
                        objEChartOfAccount.AccOpeningBalanceCR = 0;
                    }
                    else if (cmbOpBalanceType.Text == "CR")
                    {
                        objEChartOfAccount.AccOpeningBalanceDR = 0;
                        objEChartOfAccount.AccOpeningBalanceCR = Convert.ToDecimal(txtOpeningBalance.Text.Trim());
                    }
                    objEChartOfAccount.AccStatus = cmbStatus.Text;
                    objEChartOfAccount.CreationDate = DateTime.Now;
                    objEChartOfAccount.AccessBy = loginUsername;
                    objEChartOfAccount.AccessDateTime = DateTime.Now;

                    if (objBChartOfAccount.SaveChartOfAccount(objEChartOfAccount) == true)
                    {
                        MessageBox.Show("Account name saved successfully.", "Chart of Account", MessageBoxButton.OK, MessageBoxImage.Information);
                        ResetField();
                    }
                    else
                    {
                        MessageBox.Show("Account name saving failed.", "Chart of Account", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Your given account code/no already exist.\nPLease change account code.", "Chart of Account", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR : SaveChartOfAccount().\n" + ex.Message, "Chart of Account", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #region TREE

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
            catch (Exception ex)
            {
                MessageBox.Show("Error :\n" + ex.Message, "Chart of Account", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void ProcessTree(long pId, TreeViewItem newChild)
        {
            foreach (EChartOfAccount obj in objBChartOfAccount.GetSubAccount(pId))
            {
                TreeViewItem newChield = new TreeViewItem();
                newChield.Header = obj.SubAccName;
                newChield.Name = "_" + obj.SubAccId;
                newChild.Items.Add(newChield);
                ProcessTree(obj.SubAccId, newChield);
            }
        }

        long parentId; string rootName = "";
        private void trvAccountType_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (txtSubAccount.Text.Trim() == "")
            {
                var trvItem = trvAccountType.SelectedItem as TreeViewItem;
                if (trvItem != null)
                {
                    txtAccountType.Text = trvItem.Header.ToString();
                    string acciuntTypeId = trvItem.Name.ToString();
                    string[] splitedId = acciuntTypeId.Trim().Split('_');
                    parentId = Convert.ToInt64(splitedId[1]);

                    if (parentId >= 100 && parentId < 200)
                    {
                        txtParentAccountNo.Text = parentId.ToString();
                    }
                    else
                    {
                        txtParentAccountNo.Text = objBChartOfAccount.GetAccount_Code(parentId);
                    }
                    if (parentId >= 100 && parentId < 200)
                    {
                        rootName = trvItem.Header.ToString();
                    }
                    else
                    {
                        GetRootName(trvItem);
                    }

                    ChangeBalanceStatus();
                }
            }
            else
            {
                if (MessageBox.Show("Your parent account name will be change.\nAre sure want to change account name ?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    var trvItem = trvAccountType.SelectedItem as TreeViewItem;
                    if (trvItem != null)
                    {
                        txtAccountType.Text = trvItem.Header.ToString();
                        string acciuntTypeId = trvItem.Name.ToString();
                        string[] splitedId = acciuntTypeId.Trim().Split('_');
                        parentId = Convert.ToInt64(splitedId[1]);
                        if (parentId >= 100 && parentId < 200)
                        {
                            txtParentAccountNo.Text = parentId.ToString();
                        }
                        else
                        {
                            txtParentAccountNo.Text = objBChartOfAccount.GetAccount_Code(parentId);
                        }
                        if (parentId >= 100 && parentId < 200)
                        {
                            rootName = trvItem.Header.ToString();
                        }
                        else
                        {
                            GetRootName(trvItem);
                        }
                        ChangeBalanceStatus();
                    }
                }
            }
        }

        private void GetRootName(TreeViewItem trvItem)
        {
            TreeViewItem tree = trvItem.Parent as TreeViewItem;
            if (tree == null)
            {
                return;
            }
            else
            {
                rootName = tree.Header.ToString();
                GetRootName(tree);
            }
        }

        private void ChangeBalanceStatus()
        {
            if (rootName == "ASSET" || rootName == "EXPENSE")
            {
                cmbOpBalanceType.SelectedIndex = 0;
            }
            else
            {
                cmbOpBalanceType.SelectedIndex = 1;
            }
        }

        #endregion

        #region TEXT INPUT

        private void txtOpeningBalance_PreviewTextInput(object sender, TextCompositionEventArgs e)
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

        #region RESET FIELD

        private void ResetField()
        {
            txtAccountType.Text = "";
            txtParentAccountNo.Text = "";
            txtAccountCode.Text = "";
            txtOpeningBalance.Text = "";
            cmbOpBalanceType.SelectedIndex = -1;
            cmbStatus.SelectedIndex = 0;
            //cmbHeader.Text = "";
            txtSubAccount.Text = "";
            txtOpeningBalance.IsEnabled = true;           
        }

        #endregion

        #region CHECK-FIELD

        private bool CheckField()
        {
            if (txtAccountType.Text == string.Empty)
            {
                MessageBox.Show("Your parent account name should not be empty.", "Chart of Account", MessageBoxButton.OK, MessageBoxImage.Information);
                txtAccountType.Focus();
                return false;
            }
            if (txtSubAccount.Text == string.Empty)
            {
                MessageBox.Show("Give your account name.", "Chart of Account", MessageBoxButton.OK, MessageBoxImage.Information);
                txtSubAccount.Focus();
                return false;
            }
            if (txtAccountCode.Text == string.Empty)
            {
                MessageBox.Show("Give your  account no/code.", "Chart of Account", MessageBoxButton.OK, MessageBoxImage.Information);
                txtAccountCode.Focus();
                return false;
            }
            if (txtOpeningBalance.Text == string.Empty)
            {
                MessageBox.Show("Give your sub account opening balance.", "Chart of Account", MessageBoxButton.OK, MessageBoxImage.Information);
                txtOpeningBalance.Focus();
                return false;
            }
            if (Convert.ToDecimal(txtOpeningBalance.Text.Trim()) > 0)
            {
                if (cmbOpBalanceType.Text == string.Empty)
                {
                    MessageBox.Show("Select your sub account opening balance type.", "Chart of Account", MessageBoxButton.OK, MessageBoxImage.Information);
                    cmbOpBalanceType.Focus();
                    return false;
                }
            }
            if (cmbStatus.Text == string.Empty)
            {
                MessageBox.Show("Select your account status.", "Chart of Account", MessageBoxButton.OK, MessageBoxImage.Information);
                cmbStatus.Focus();
                return false;
            }
            return true;
        }

        #endregion

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            ResetField();
            LoadAccountTypeTree();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.ShowInTaskbar = true;
            this.Owner = null;
        }
    }
}

