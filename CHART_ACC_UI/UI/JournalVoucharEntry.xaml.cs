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
using CHART_ACC_BLL;
using CHART_ACC_ENTITY;

namespace CHART_ACC_UI.UI
{
    /// <summary>
    /// Interaction logic for JournalVoucharEntry.xaml
    /// </summary>
    public partial class JournalVoucharEntry : Window
    {
        BJournalVouchar objBJournalVouchar = new BJournalVouchar();
        BChartOfAccount objBChartOfAccount = new BChartOfAccount();
        BFiscalYear objBFiscalYear = new BFiscalYear();
        string loginUserName = "Amin";
        public JournalVoucharEntry()
        {
            InitializeComponent();
            GetJournalVoucherID();
            populateDebitCreditCombo();
            LoadAccountTree();
            trvAccount.Visibility = Visibility.Hidden;
            dtpJournalDate.SelectedDate = DateTime.Now;
            CheckFiscalYear();
        }

        private void populateDebitCreditCombo()
        {
            string[] BalanceType = { "DR", "CR" };
            cmbDebitCredit.Items.Add(BalanceType[0]);
            cmbDebitCredit.Items.Add(BalanceType[1]);
        }

        private void LoadListViewFromDataBase()
        {
            lvJournalVouchar.Items.Clear();
            try
            {
                List<EJournalVouchar> VoucharList = objBJournalVouchar.GetAllJournalVoucharIdWise(txtJournalEntryNo.Text.Trim());
                foreach (EJournalVouchar myJVouchar in VoucharList)
                {
                    lvJournalVouchar.Items.Add(myJVouchar);
                    txtJournalNote.Text = myJVouchar.Journal_Notes;
                    dtpJournalDate.SelectedDate = myJVouchar.Journal_Date;
                }
                ItemTotalcalCulation();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem occur to get Journal voucher details.\n" + ex.Message, "Journal Voucher Entry", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void GetJournalVoucherID()
        {
            try
            {
                txtJournalEntryNo.Text = objBJournalVouchar.GetJVoucherLastID();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem occur to get Journal voucher new id.\n" + ex.Message, "Journal Voucher Entry", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #region TREE

        private void LoadAccountTree()
        {
            trvAccount.Items.Clear();
            try
            {
                foreach (EAccountType objType in objBChartOfAccount.GetAllAccountType())
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
                MessageBox.Show("Error :\n" + ex.Message, "Chart of Account", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }        
        
        private void ProcessTree(long pId, TreeViewItem newChild)
        {
            try
            {
                foreach (EChartOfAccount obj in objBJournalVouchar.GetSubAccount(pId))
                {
                    TreeViewItem newChield = new TreeViewItem();
                    newChield.Header = obj.SubAccName;
                    newChield.Name = "_" + obj.SubAccId;
                    newChild.Items.Add(newChield);
                    ProcessTree(obj.SubAccId, newChield);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        long parentId;
        private void trvAccount_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            decimal currentBalance = 0;
            var trvItem = trvAccount.SelectedItem as TreeViewItem;
            if (trvItem != null)
            {
                string acciuntTypeId = trvItem.Name.ToString();
                string[] splitedId = acciuntTypeId.Trim().Split('_');
                parentId = Convert.ToInt64(splitedId[1]);
                if (parentId >= 100 && parentId < 200)
                {
                    MessageBox.Show("You can't make transaction with this account.\nPlease use its child account", "Journal Vouchar Entry", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    List<EChartOfAccount> CodeList = objBJournalVouchar.GetAccountInfo(parentId);
                    foreach (var code in CodeList)
                    {
                        if (code.AccHeader == "No")
                        {
                            txtAccountName.Text = trvItem.Header.ToString();
                            txtAccountCode.Text = code.SubAccCode;
                            currentBalance = objBJournalVouchar.GetCurrentBalanceOfAccount(parentId);
                            if (currentBalance < 0)
                            {
                                txtAccountStatus.Text = currentBalance.ToString();
                            }
                            else
                            {
                                txtAccountStatus.Text = currentBalance.ToString();
                            }
                            txtParentAccName.Text = (trvItem.Parent as TreeViewItem).Header.ToString();
                        }
                        else
                        {
                            MessageBox.Show("You can't make transaction with this account.\n Please use its child account", "Journal Vouchar Entry", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                }
            }
            listBoxSuggestion.Visibility = Visibility.Hidden;
            
        }

        #endregion

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (btnAdd.Content.ToString() == "Add")
                {
                    AddTransactionToList();
                }
                else
                {
                    UpdateTransaction();
                }
                ItemTotalcalCulation();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem Occur While Adding Record Into List.\n" + ex.Message, "Journal Voucher Entry", MessageBoxButton.OK, MessageBoxImage.Error);
            }
           
        }

        private void AddTransactionToList()
        {
            if (CheckField() == true)
            {
                EJournalVouchar objEJV = new EJournalVouchar();
                objEJV.Journal_Date = Convert.ToDateTime(dtpJournalDate.SelectedDate);
                objEJV.Journal_Acc_Name = txtAccountName.Text;
                objEJV.Journal_Acc_Id = parentId;
                objEJV.Journal_Acc_Code = txtAccountCode.Text;
                objEJV.Journal_Notes = txtJournalNote.Text;
                if (cmbDebitCredit.Text == "DR")
                {
                    objEJV.Journal_Debit_Amount = Convert.ToDecimal(txtAmount.Text.Trim());
                    objEJV.Jounal_Credit_Amount = Convert.ToDecimal("0");
                }
                else
                {
                    {
                        objEJV.Journal_Debit_Amount = Convert.ToDecimal("0");
                        objEJV.Jounal_Credit_Amount = Convert.ToDecimal(txtAmount.Text.Trim());
                    }
                }
                lvJournalVouchar.Items.Add(objEJV);
                ResetFieldAdd();
            }
        }

        private void UpdateTransaction()
        {
            if (CheckField() == true)
            {
                EJournalVouchar objEJV = new EJournalVouchar();
                objEJV.Journal_Date = Convert.ToDateTime(dtpJournalDate.SelectedDate);
                objEJV.Journal_Acc_Name = txtAccountName.Text;
                objEJV.Journal_Acc_Id = parentId;
                objEJV.Journal_Acc_Code = txtAccountCode.Text;
                objEJV.Journal_Notes = txtJournalNote.Text;
                if (cmbDebitCredit.Text == "DR")
                {
                    objEJV.Journal_Debit_Amount = Convert.ToDecimal(txtAmount.Text.Trim());
                    objEJV.Jounal_Credit_Amount = Convert.ToDecimal("0");
                }
                else
                {
                    {
                        objEJV.Journal_Debit_Amount = Convert.ToDecimal("0");
                        objEJV.Jounal_Credit_Amount = Convert.ToDecimal(txtAmount.Text.Trim());
                    }
                }
                lvJournalVouchar.Items.RemoveAt(getIndex);
                lvJournalVouchar.Items.Insert(getIndex, objEJV);
                ResetFieldAdd();
            }
        }

        private void ItemTotalcalCulation()
        {
            decimal dr = 0, cr = 0;
            for (int i = 0; i < lvJournalVouchar.Items.Count; i++)
            {
                EJournalVouchar obj = lvJournalVouchar.Items[i] as EJournalVouchar;
                dr += Convert.ToDecimal(obj.Journal_Debit_Amount);
                cr += Convert.ToDecimal(obj.Jounal_Credit_Amount);
            }
            txtBlockDebit.Text = dr.ToString("F");
            txtBlockCredit.Text = cr.ToString("F");
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (CheckFieldForJournalVouchar() == true)
            {
                if (btnSave.Content.ToString() == "Save")
                {
                    SaveJournalVouchar();
                }
                else
                {
                    if (objBJournalVouchar.DeleteJournalVouchar(txtJournalEntryNo.Text.Trim()) == true)
                    {
                        UpdateJournalVouchar();
                    }
                }
            }
        }

        private void SaveJournalVouchar()
        {
            try
            {
                GetJournalVoucherID();
                bool stat = false;
                for (int i = 0; i < lvJournalVouchar.Items.Count; i++)
                {
                    EJournalVouchar objEJournalVouchar = (EJournalVouchar)lvJournalVouchar.Items[i];                    
                    objEJournalVouchar.Journal_Id = txtJournalEntryNo.Text;
                    objEJournalVouchar.Journal_BankRemarks = "Journal";
                    objEJournalVouchar.Access_By = loginUserName;
                    objEJournalVouchar.Access_Date = DateTime.Now;
                    stat=objBJournalVouchar.SaveJournalVouchar(objEJournalVouchar);                    
                }
                if (stat == true)
                {
                    MessageBox.Show("Journal vouchar saved successfully.", "Journal Voucher Entry", MessageBoxButton.OK, MessageBoxImage.Information);
                    ResetFieldSave();
                }
                else
                {
                    MessageBox.Show("Journal vouchar saving failed.", "Journal Voucher Entry", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem occur while saving data.\n" + ex.Message, "Journal Voucher Entry", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateJournalVouchar()
        {
            try
            {
                bool stat = false;
                for (int i = 0; i < lvJournalVouchar.Items.Count; i++)
                {
                    EJournalVouchar objEJournalVouchar = (EJournalVouchar)lvJournalVouchar.Items[i];
                    objEJournalVouchar.Journal_Id = txtJournalEntryNo.Text;
                    objEJournalVouchar.Access_By = loginUserName;
                    objEJournalVouchar.Access_Date = DateTime.Now;
                    stat = objBJournalVouchar.SaveJournalVouchar(objEJournalVouchar);
                }
                if (stat == true)
                {
                    MessageBox.Show("Journal vouchar updated successfully.", "Journal Voucher Entry", MessageBoxButton.OK, MessageBoxImage.Information);
                    ResetFieldSave();
                }
                else
                {
                    MessageBox.Show("Journal vouchar updating failed.", "Journal Voucher Entry", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem occur while update data.\n" + ex.Message, "Journal Voucher Entry", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #region CHECK & RESET FIELD

        private bool CheckField()
        {
            if (txtAccountCode.Text == string.Empty)
            {
                MessageBox.Show("Give your account no/code.", "Journal Voucher Entry", MessageBoxButton.OK, MessageBoxImage.Information);
                txtAccountCode.Focus();
                return false;
            }
            if (txtAccountName.Text == string.Empty)
            {
                MessageBox.Show("Give your account type name.", "Journal Voucher Entry", MessageBoxButton.OK, MessageBoxImage.Information);
                txtAccountName.Focus();
                return false;
            }
            if (txtAmount.Text == string.Empty)
            {
                MessageBox.Show("Transaction amount should not be empty.", "Journal Voucher Entry", MessageBoxButton.OK, MessageBoxImage.Information);
                txtAmount.Focus();
                return false;
            }
            if (cmbDebitCredit.Text == string.Empty)
            {
                MessageBox.Show("Select transaction amount type.", "Journal Voucher Entry", MessageBoxButton.OK, MessageBoxImage.Information);
                cmbDebitCredit.Focus();
                return false;
            }
            if (txtJournalNote.Text == string.Empty)
            {
                MessageBox.Show("Journal notes should not be empty.", "Journal Voucher Entry", MessageBoxButton.OK, MessageBoxImage.Information);
                txtJournalNote.Focus();
                return false;
            }
            return true;
        }

        private bool CheckFieldForJournalVouchar()
        {
            if (lvJournalVouchar.Items.Count == 0)
            {
                MessageBox.Show("Please Enter Debit and Credit Info. Into List", "Journal Voucher Entry", MessageBoxButton.OK, MessageBoxImage.Information);
                lvJournalVouchar.Focus();
                return false;
            }
            if (txtBlockCredit.Text != txtBlockDebit.Text)
            {
                MessageBox.Show("Debit and Credit Balance Must Be Same", "Journal Voucher Entry", MessageBoxButton.OK, MessageBoxImage.Stop);
                return false;
            }
            return true;
        }

        private void CheckFiscalYear()
        {
            List<EFiscalYear> fiscalYearList = objBFiscalYear.CheckFiscalYear();

            if (fiscalYearList.Count == 0)
            {
                MessageBox.Show("Must Have One Fiscal Year Active. Please Activate One Fiscal Year.");
            }
            else
            {
                foreach (EFiscalYear obj in fiscalYearList)
                {
                    if (obj.F_Year_Status == "Active")
                    {
                        if (Convert.ToDateTime(obj.F_Year_End_Date) < DateTime.Today)
                        {
                            MessageBox.Show("Set Up Your New Fiscal Year. Active Date is Expired");
                        }
                    }

                }
            }
        }

        private void ResetFieldAdd()
        {
            txtAccountName.Text = "";
            txtAccountStatus.Text = "";
            txtAccountCode.Text = "";
            txtAmount.Text = "";
            txtParentAccName.Text = "";
            cmbDebitCredit.Text = "";
            btnAdd.Content = "Add";
            LoadAccountTree();
        }

        private void ResetFieldSave()
        {
            txtJournalEntryNo.Text = "";
            txtJournalNote.Text = "";
            lvJournalVouchar.Items.Clear();
            dtpJournalDate.SelectedDate = DateTime.Now;
            txtBlockDebit.Text = string.Empty;
            txtBlockCredit.Text = string.Empty;
            txtAccountCode.Text = string.Empty;
            txtParentAccName.Text = string.Empty;
            GetJournalVoucherID();
            btnSave.Content = "Save";
        }

        #endregion

        int getIndex;
        string parentAccName;
        long AccId;
        private void EditJV_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                decimal currentBalance = 0;
                if (lvJournalVouchar.SelectedIndex > -1)
                {
                    EJournalVouchar selectedJV = (EJournalVouchar)lvJournalVouchar.SelectedItem;
                    FillTransactionField(selectedJV);
                    AccId = selectedJV.Journal_Acc_Id;
                    List<EChartOfAccount> listChart = objBJournalVouchar.GetAccountInfo(AccId);
                    parentAccName = listChart[0].RootAccName;
                    getIndex = lvJournalVouchar.SelectedIndex;
                    btnAdd.Content = "Modify";
                    currentBalance = objBJournalVouchar.GetCurrentBalanceOfAccount(AccId);
                    if (currentBalance < 0)
                    {
                        txtAccountStatus.Text = currentBalance.ToString();
                    }
                    else
                    {
                        txtAccountStatus.Text = currentBalance.ToString();
                    }
                    foreach (TreeViewItem tree in trvAccount.Items)
                    {
                        if (tree.Header.ToString() == parentAccName)
                        {
                            tree.IsExpanded = true;
                            SelectedIndexForTree(tree);
                            break;
                        }
                    }
                    listBoxSuggestion.Visibility = Visibility.Hidden;
                }
                else
                {
                    MessageBox.Show("Select an Item first.", "Journal Voucher Entry", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem occur while edit data.\n" + ex.Message, "Journal Voucher Entry", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SelectedIndexForTree(TreeViewItem tr)
        {
            
            foreach (TreeViewItem tr1 in tr.Items)
            {           
                if (tr1.Name == "_" + AccId.ToString())
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

        private void FillTransactionField(EJournalVouchar selectedJV)
        {
            txtAccountName.Text = selectedJV.Journal_Acc_Name;
            txtAccountCode.Text = selectedJV.Journal_Acc_Code;
           // txtJournalNote.Text = selectedJV.Journal_Notes;
            dtpJournalDate.SelectedDate = selectedJV.Journal_Date;
            if (selectedJV.Journal_Debit_Amount == 0)
            {
                cmbDebitCredit.Text = "CR";
                txtAmount.Text = selectedJV.Jounal_Credit_Amount.ToString();
            }
            else
            {
                cmbDebitCredit.Text = "DR";
                txtAmount.Text = selectedJV.Journal_Debit_Amount.ToString();
            }

        }

        private void RemoveJV_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lvJournalVouchar.SelectedIndex > -1)
                {
                    if (MessageBox.Show("Are you sure want to remove this transaction?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        lvJournalVouchar.Items.Remove(lvJournalVouchar.SelectedItem);
                        ItemTotalcalCulation();
                    }
                }
                else
                {
                    MessageBox.Show("Select an transaction first.", "Journal Voucher Entry", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem occur while removing data."+ex.Message, "Journal Voucher Entry", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #region TEXT INPUT
        private void txtAmount_PreviewTextInput(object sender, TextCompositionEventArgs e)
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

        private void Window_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            trvAccount.Visibility = Visibility.Hidden;

        }

        private void txtAccountCode_LostFocus(object sender, RoutedEventArgs e)
        {
            #region  DEAD CODE

            //decimal currentBalance = 0;
            //if (txtAccountCode.Text != string.Empty)
            //{
            //    List<EChartOfAccount> CodeList = objBJournalVouchar.GetAccountInfoCodeWise(txtAccountCode.Text.Trim());
            //    foreach (var code in CodeList)
            //    {
            //        if (code.AccHeader == "No")
            //        {
            //            txtAccountCode.Text = code.SubAccCode;
            //            txtAccountName.Text = code.SubAccName;
            //            parentId = code.SubAccId;
            //            AccId = code.SubAccId;
            //            parentAccName = code.RootAccName;
            //            currentBalance = objBJournalVouchar.GetCurrentBalanceOfAccount(AccId);
            //            if (currentBalance < 0)
            //            {
            //                txtAccountStatus.Text = currentBalance.ToString();
            //            }
            //            else
            //            {
            //                txtAccountStatus.Text = currentBalance.ToString();
            //            }

            //            foreach (TreeViewItem tree in trvAccount.Items)
            //            {
            //                if (tree.Header.ToString() == parentAccName)
            //                {
            //                    tree.IsExpanded = true;
            //                    SelectedIndexForTree(tree);
            //                    break;
            //                }
            //            }
            //            listBoxSuggestion.Visibility = Visibility.Hidden;
            //        }
            //        else
            //        {
            //            MessageBox.Show("You can't make transaction with this account.\n Please use its child account.","Journal Vouchar Entry",MessageBoxButton.OK,MessageBoxImage.Information);
            //        }
            //    }
            //}

            #endregion
        }

        private void txtJournalEntryNo_LostFocus(object sender, RoutedEventArgs e)
        {
           
        }

        private void txtJournalEntryNo_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                {
                    if (txtJournalEntryNo.Text != string.Empty)
                    {
                        ResetFieldAdd();
                        lvJournalVouchar.Items.Clear();
                        dtpJournalDate.SelectedDate = DateTime.Now;
                        txtBlockDebit.Text = string.Empty;
                        txtBlockCredit.Text = string.Empty;
                        txtJournalNote.Text = "";
                        btnSave.Content = "Save";
                        if (objBJournalVouchar.DoesExistVoucharWithoutBank(txtJournalEntryNo.Text.Trim()))
                        {
                            LoadListViewFromDataBase();
                            btnSave.Content = "Update";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem occur\n" + ex.Message, "Journal Voucher Entry", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #region ACCOUNT NAME-WISE SEARCH

        private void GetAccountInfoNameWise()
        {
            try
            {
                decimal currentBalance = 0;
                if (txtAccountName.Text != string.Empty)
                {
                    List<EChartOfAccount> CodeList = objBJournalVouchar.GetAccountInfoNameWise(txtAccountName.Text.Trim());
                    foreach (var code in CodeList)
                    {
                        if (code.AccHeader == "No")
                        {
                            txtAccountCode.Text = code.SubAccCode;
                            txtAccountName.Text = code.SubAccName;
                            parentId = code.SubAccId;
                            AccId = code.SubAccId;
                            parentAccName = code.RootAccName;
                            currentBalance = objBJournalVouchar.GetCurrentBalanceOfAccount(AccId);
                            if (currentBalance < 0)
                            {
                                txtAccountStatus.Text = currentBalance.ToString();
                                txtAccountStatus.Foreground = Brushes.Red;
                            }
                            else
                            {
                                txtAccountStatus.Text = currentBalance.ToString();
                            }
                            foreach (TreeViewItem tree in trvAccount.Items)
                            {
                                if (tree.Header.ToString() == parentAccName)
                                {
                                    tree.IsExpanded = true;
                                    SelectedIndexForTree(tree);
                                    break;
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("You can't make transaction with this account.\n Please use its child account", "Journal Vouchar Entry", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem occur\n" + ex.Message, "Journal Voucher Entry", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void txtAccountName_TextChanged(object sender, TextChangedEventArgs e)
        {
            listBoxSuggestion.Items.Clear();
            if (txtAccountName.Text != "")
            {
                List<EChartOfAccount> namelist = objBJournalVouchar.GetAccountInfoLikeNameWise(txtAccountName.Text);
                if (namelist.Count > 0)
                {
                    listBoxSuggestion.Visibility = Visibility.Visible;
                    foreach (var obj in namelist)
                    {
                        listBoxSuggestion.Items.Add(obj);
                    }
                }
            }
            else
            {
                listBoxSuggestion.Visibility = Visibility.Hidden;
            }
        }

        private void txtAccountName_PreviewKeyDown(object sender, KeyEventArgs e)
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
                txtAccountName.Focus();
            }
        }

        private void listBoxSuggestion_KeyDown(object sender, KeyEventArgs e)
        {
            if (listBoxSuggestion.SelectedIndex > -1)
            {
                if (e.Key == Key.Enter)
                {
                    EChartOfAccount objSelectedCa = listBoxSuggestion.SelectedItem as EChartOfAccount;
                    txtAccountName.Text = objSelectedCa.SubAccName;
                    listBoxSuggestion.Visibility = Visibility.Hidden;
                    txtAccountCode.Text = objSelectedCa.SubAccCode;
                    GetAccountInfoNameWise();
                }
            }
        }

        #endregion

        #region  CLICK-EVENT

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            ResetFieldAdd();
            ResetFieldSave();
            trvAccount.Visibility = Visibility.Hidden;
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            ResetFieldAdd();
            trvAccount.Visibility = Visibility.Hidden;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
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

        #endregion

        private void txtAccountCode_TextChanged(object sender, TextChangedEventArgs e)
        {
           
        }

        private void txtAccountCode_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                {
                    decimal currentBalance = 0;
                    if (txtAccountCode.Text != string.Empty)
                    {
                        List<EChartOfAccount> CodeList = objBJournalVouchar.GetAccountInfoCodeWise(txtAccountCode.Text.Trim());
                        if (CodeList.Count > 0)
                        {
                            foreach (var code in CodeList)
                            {
                                if (code.AccHeader == "No")
                                {
                                    txtAccountCode.Text = code.SubAccCode;
                                    txtAccountName.Text = code.SubAccName;
                                    parentId = code.SubAccId;
                                    AccId = code.SubAccId;
                                    parentAccName = code.RootAccName;
                                    currentBalance = objBJournalVouchar.GetCurrentBalanceOfAccount(AccId);
                                    if (currentBalance < 0)
                                    {
                                        txtAccountStatus.Text = currentBalance.ToString();
                                    }
                                    else
                                    {
                                        txtAccountStatus.Text = currentBalance.ToString();
                                    }

                                    foreach (TreeViewItem tree in trvAccount.Items)
                                    {
                                        if (tree.Header.ToString() == parentAccName)
                                        {
                                            tree.IsExpanded = true;
                                            SelectedIndexForTree(tree);
                                            break;
                                        }
                                    }
                                    listBoxSuggestion.Visibility = Visibility.Hidden;
                                }

                                else
                                {
                                    MessageBox.Show("You can't make transaction with this account.\n Please use its child account.", "Journal Vouchar Entry", MessageBoxButton.OK, MessageBoxImage.Information);
                                }
                            }
                        }
                        else
                        {
                            txtParentAccName.Text = string.Empty;
                            txtAccountStatus.Text = string.Empty;
                            txtAccountName.Text = string.Empty;
                            LoadAccountTree();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem occur\n" + ex.Message, "Journal Voucher Entry", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
    }
}
