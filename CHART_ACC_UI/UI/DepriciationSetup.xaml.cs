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
    /// Interaction logic for DepriciationSetup.xaml
    /// </summary>
    public partial class DepriciationSetup : Window
    {
        BDepriciation objBDepriciation = new BDepriciation();
        BChartOfAccount objBChartOfAccount = new BChartOfAccount();
        BJournalVouchar objBJournalVouchar = new BJournalVouchar();

        BDepriciationSetup objBDepriciationSetup = new BDepriciationSetup();
        string userName = "Amin";

        public DepriciationSetup()
        {
            InitializeComponent();
            LoadAccountCombo();
            trvDRAccountType.Visibility = Visibility.Hidden;
            LoadDRAccountTypeTree();
            LoadCRAccountTypeTree();
            trvCRAccountType.Visibility = Visibility.Hidden;
            LoadListView();
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

        private void LoadAccountCombo()
        {
            cmbFixedAccountName.Items.Clear();
            try
            {
                List<EDepriciation> listDepriciation = objBDepriciation.GetFixedAssets();
                foreach (EDepriciation obj in listDepriciation)
                {
                    cmbFixedAccountName.Items.Add(obj);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in retrieve fixed asset account name.\n" + ex.Message, "Depriciation Setup");
            }
        }
        private void cmbFixedAccountName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbFixedAccountName.SelectedIndex > -1)
            {
                txtBalance.Text = ((EDepriciation)(cmbFixedAccountName.SelectedItem)).Debit.ToString();
            }
        }

        private void LoadListView()
        {
            lvDepriciationSetup.Items.Clear();
            try
            {
                List<EDepriciationSetup> listDepriciationSetup = objBDepriciationSetup.GetAllDepriciationSetup();
                foreach (EDepriciationSetup objDS in listDepriciationSetup)
                {
                    lvDepriciationSetup.Items.Add(objDS);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in get depriciation data.\n" + ex.Message, "Depriciation Setup", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #region DEBIT ACCOUNT TREE

        private void LoadDRAccountTypeTree()
        {
            trvDRAccountType.Items.Clear();
            try
            {
                foreach (EAccountType objType in objBChartOfAccount.GetAllAccountType())
                {
                    if (objType.AccTypeName.ToUpper() == "EXPENSE")
                    {
                        TreeViewItem treeItem = new TreeViewItem();
                        treeItem.Header = objType.AccTypeName;
                        treeItem.Name = "_" + objType.AccTypeId;
                        DRProcessTree(objType.AccTypeId, treeItem);
                        trvDRAccountType.Items.Add(treeItem);
                        break;
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Problem Occur While Loading Parent Account", "Depriciation Setup", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void DRProcessTree(long pId, TreeViewItem newChild)
        {
            foreach (EChartOfAccount obj in objBChartOfAccount.GetSubAccount(pId))
            {
                TreeViewItem trChild = new TreeViewItem();
                trChild.Header = obj.SubAccName;
                trChild.Name = "_" + obj.SubAccId;
                newChild.Items.Add(trChild);
                DRProcessTree(obj.SubAccId, trChild);
            }
        }

        long accIdDR = 0;
        string accCodeDR;
        private void trvDRAccountType_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var trvItem = (sender as TreeView).SelectedItem as TreeViewItem;
            if (trvItem != null && (trvItem.HasItems == false))
            {
                txtDRAccountName.Text = trvItem.Header.ToString();
                string[] splitedId = (trvItem.Name.ToString()).Split('_');
                accIdDR = Convert.ToInt64(splitedId[1]);
                trvDRAccountType.Visibility = Visibility.Hidden;
                List<EChartOfAccount> CodeList = objBJournalVouchar.GetAccountInfo(accIdDR);
                foreach (var code in CodeList)
                {
                    if (code.AccHeader == "No")
                    {
                        accCodeDR = code.SubAccCode;
                    }
                    else
                    {
                        MessageBox.Show("You can't make transaction with parent account.\n Please use its child account", "Depriciation Setup", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
        }

        #endregion

        #region CREDIT ACCOUNT TREE

        private void LoadCRAccountTypeTree()
        {
            trvCRAccountType.Items.Clear();
            try
            {
                foreach (EAccountType objType in objBChartOfAccount.GetAllAccountType())
                {
                    if (objType.AccTypeName.ToUpper() == "LIABILITY")
                    {
                        TreeViewItem treeItem = new TreeViewItem();
                        treeItem.Header = objType.AccTypeName;
                        treeItem.Name = "_" + objType.AccTypeId;
                        CRProcessTree(objType.AccTypeId, treeItem);
                        trvCRAccountType.Items.Add(treeItem);
                        break;
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Problem Occur While Loading Parent Account.", "Depriciation Setup", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void CRProcessTree(long pId, TreeViewItem newChild)
        {
            foreach (EChartOfAccount obj in objBChartOfAccount.GetSubAccount(pId))
            {
                TreeViewItem trChild = new TreeViewItem();
                trChild.Header = obj.SubAccName;
                trChild.Name = "_" + obj.SubAccId;
                newChild.Items.Add(trChild);
                CRProcessTree(obj.SubAccId, trChild);
            }
        }

        long accIdCR = 0;
        string accCodeCR;
        private void trvCRAccountType_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var trvItem = (sender as TreeView).SelectedItem as TreeViewItem;
            if (trvItem != null && (trvItem.HasItems == false))
            {
                txtCRAccountName.Text = trvItem.Header.ToString();
                string[] splitedId = (trvItem.Name.ToString()).Split('_');
                accIdCR = Convert.ToInt64(splitedId[1]);
                trvCRAccountType.Visibility = Visibility.Hidden;
                List<EChartOfAccount> CodeList = objBJournalVouchar.GetAccountInfo(accIdCR);
                foreach (var code in CodeList)
                {
                    if (code.AccHeader == "No")
                    {
                        accCodeCR = code.SubAccCode;
                    }
                    else
                    {
                        MessageBox.Show("You can't make transaction with parent account.\n Please use its child account.", "Depriciation Setup", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
        }

        #endregion

        private void btnDRAccount_Click(object sender, RoutedEventArgs e)
        {
            if (trvDRAccountType.IsVisible == false)
            {
                trvDRAccountType.Visibility = Visibility.Visible;
            }
            else
            {
                trvDRAccountType.Visibility = Visibility.Hidden;
            }
        }

        private void btnCRAccount_Click(object sender, RoutedEventArgs e)
        {
            if (trvCRAccountType.IsVisible == false)
            {
                trvCRAccountType.Visibility = Visibility.Visible;
            }
            else
            {
                trvCRAccountType.Visibility = Visibility.Hidden;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (CheckField() == true)
            {
                if (btnSave.Content.ToString() == "Save")
                {
                    SaveDepriciationSetup();
                }
                else
                {
                    UpdateDepriciationSetup();
                }
            }
        }

        private void SaveDepriciationSetup()
        {
            try
            {
                if (objBDepriciationSetup.DoesAccountExist(((EDepriciation)(cmbFixedAccountName.SelectedItem)).AccountId) == false)
                {
                    EDepriciationSetup objDepriciationSetup = new EDepriciationSetup();

                    objDepriciationSetup.Account_Id = ((EDepriciation)(cmbFixedAccountName.SelectedItem)).AccountId;
                    objDepriciationSetup.Dep_Percentage = Convert.ToDecimal(txtDepriciationPercentage.Text);
                    objDepriciationSetup.Debit_Account_Id = accIdDR;
                    objDepriciationSetup.Credit_Account_Id = accIdCR;
                    objDepriciationSetup.Access_By = userName;
                    objDepriciationSetup.Access_Date = DateTime.Now;

                    if (objBDepriciationSetup.SaveDepriciationSetup(objDepriciationSetup) == true)
                    {
                        MessageBox.Show("Depriciation saved successfully.", "Depriciation Setup", MessageBoxButton.OK, MessageBoxImage.Information);
                        ResetField();
                        LoadListView();
                    }
                    else
                    {
                        MessageBox.Show("Depriciation saving failed.", "Depriciation Setup", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    MessageBox.Show("You have already saved this account.\nIf needed, you can modify this account.", "Depriciation Setup", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error :\n"+ex.Message, "Depriciation Setup", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #region CHECK & RESET FIELD

        private void ResetField()
        {
            cmbFixedAccountName.Text = "";
            txtBalance.Text = "";
            txtDepriciationPercentage.Text = "";
            txtDRAccountName.Text = "";
            txtCRAccountName.Text = "";
            btnSave.Content = "Save";
        }

        private bool CheckField()
        {
            if (cmbFixedAccountName.Text == "")
            {
                MessageBox.Show("Please select your account.", "Depriciation Setup", MessageBoxButton.OK, MessageBoxImage.Information);
                cmbFixedAccountName.Focus();
                return false;
            }
            if (txtDepriciationPercentage.Text == "")
            {
                MessageBox.Show("Give depriciation percentage.", "Depriciation Setup", MessageBoxButton.OK, MessageBoxImage.Information);
                txtDepriciationPercentage.Focus();
                return false;
            }
            if (txtDRAccountName.Text == "")
            {
                MessageBox.Show("Please select your debit account.", "Depriciation Setup", MessageBoxButton.OK, MessageBoxImage.Information);
                txtDRAccountName.Focus();
                return false;
            }
            if (txtCRAccountName.Text == "")
            {
                MessageBox.Show("Please select your credit account.", "Depriciation Setup", MessageBoxButton.OK, MessageBoxImage.Information);
                txtCRAccountName.Focus();
                return false;
            }
            return true;
        }

        #endregion

        #region UPDATE DEPRICIATION
        private void editData_Click(object sender, RoutedEventArgs e)
        {
            if (lvDepriciationSetup.SelectedIndex > -1)
            {
                EDepriciationSetup objDepriciationSetup = lvDepriciationSetup.SelectedItem as EDepriciationSetup;
                FillDepriciationSetup(objDepriciationSetup);
                btnSave.Content = "Update";
            }        
        }
        private long Depriciation_Id = 0;
        private void FillDepriciationSetup(EDepriciationSetup objDepriciationSetup)
        {
            List<EChartOfAccount> CodeList = new List<EChartOfAccount>();
            Depriciation_Id = objDepriciationSetup.Depriciation_Id;
            accIdDR =Convert.ToInt64(objDepriciationSetup.Debit_Account_Id);
            accIdCR = Convert.ToInt64(objDepriciationSetup.Credit_Account_Id);
            txtDepriciationPercentage.Text = objDepriciationSetup.Dep_Percentage.ToString();
            for (int i = 0; i < cmbFixedAccountName.Items.Count; i++)
            {
                EDepriciation objEIM = (EDepriciation)cmbFixedAccountName.Items[i];
                if (objDepriciationSetup.Account_Id == objEIM.AccountId)
                {
                    cmbFixedAccountName.SelectedIndex = i;
                    break;
                }
            }
            foreach (TreeViewItem trD in trvDRAccountType.Items)
            {
                trD.IsExpanded = true;
                SelectedIndexForDR(trD);
            }
            foreach (TreeViewItem trC in trvCRAccountType.Items)
            {
                trC.IsExpanded = true;
                SelectedIndexForCR(trC);
            }
        }

        private void SelectedIndexForCR(TreeViewItem tr)
        {
            foreach (TreeViewItem tr1 in tr.Items)
            {
                if (tr1.Name == "_" + accIdCR.ToString())
                {
                    tr1.IsSelected = true;
                    txtCRAccountName.Text = tr1.Header.ToString();
                    ParentExpand(tr1);
                    break;
                }
                else
                {
                    SelectedIndexForCR(tr1);
                }
            }
        }
        private void SelectedIndexForDR(TreeViewItem tr)
        {
            foreach (TreeViewItem tr1 in tr.Items)
            {
                if (tr1.Name == "_" + accIdDR.ToString())
                {
                    tr1.IsSelected = true;
                    txtDRAccountName.Text = tr1.Header.ToString();
                    ParentExpand(tr1);
                    break;
                }
                else
                {
                    SelectedIndexForDR(tr1);
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

        private void UpdateDepriciationSetup()
        {
            try
            {
                    EDepriciationSetup objDepriciationSetup = new EDepriciationSetup();
                    objDepriciationSetup.Depriciation_Id = Depriciation_Id;
                    objDepriciationSetup.Account_Id = ((EDepriciation)(cmbFixedAccountName.SelectedItem)).AccountId;
                    objDepriciationSetup.Dep_Percentage = Convert.ToDecimal(txtDepriciationPercentage.Text);
                    objDepriciationSetup.Debit_Account_Id = accIdDR;
                    objDepriciationSetup.Credit_Account_Id = accIdCR;
                    objDepriciationSetup.Access_By = userName;
                    objDepriciationSetup.Access_Date = DateTime.Now;
                    if (objBDepriciationSetup. UpdateDepriciationSetup(objDepriciationSetup) == true)
                    {
                        MessageBox.Show("Depriciation updated successfully.", "Depriciation Setup", MessageBoxButton.OK, MessageBoxImage.Information);
                        ResetField();
                        LoadListView();
                    }
                    else
                    {
                        MessageBox.Show("Depriciation updating failed.", "Depriciation Setup", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error :\n" + ex.Message, "Depriciation Setup", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            ResetField();
        }

        private void Window_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            trvDRAccountType.Visibility = Visibility.Hidden;
            trvCRAccountType.Visibility = Visibility.Hidden;
        }

        #region TEXT -INPUT

        private void txtDepriciationPercentage_PreviewTextInput(object sender, TextCompositionEventArgs e)
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
