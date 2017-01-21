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
using CHART_ACC_UI.Validation;

namespace CHART_ACC_UI.UI
{
    /// <summary>
    /// Interaction logic for ContraVoucher.xaml
    /// </summary>
    public partial class ContraVoucher : Window
    {
        BContraVoucher objBContraVoucher = new BContraVoucher();
        BJournalVouchar objBJournalVouchar = new BJournalVouchar();
        BChartOfAccount objBChartOfAccount = new BChartOfAccount();
        BDeposit_Payment objBDepositPayment = new BDeposit_Payment();
        public string loginUserName = "Amin";
        bool bankStatus = false;

        public ContraVoucher()
        {
            InitializeComponent();
            LoadAccountTree();
            VoucherLastId();
            populateAmountTypeCombo();
            trvAccount.Visibility = Visibility.Hidden;
            dtpDate.SelectedDate = DateTime.Now;
            dtpChkChequeDate.SelectedDate = DateTime.Now;
            grpBoxChk.IsEnabled = false;
        }
        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
        }

        private void populateAmountTypeCombo()
        {
            string[] BalanceType = { "DR", "CR" };
            cmbAmountType.Items.Add(BalanceType[0]);
            cmbAmountType.Items.Add(BalanceType[1]);
        }
        private void VoucherLastId()
        {
            try
            {
                txtVoucherNo.Text = objBJournalVouchar.GetJVoucherLastID();
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
                long id = objBChartOfAccount.GetAccountID("Cash at Bank");
                TreeViewItem treeItem = new TreeViewItem();
                treeItem.IsExpanded = true;
                treeItem.Header = "Cash at Bank";
                treeItem.Name = "_" + id;
                ProcessTree(id, treeItem);
                trvAccount.Items.Add(treeItem);

                long idCash = objBChartOfAccount.GetAccountID("Cash in Hand");
                TreeViewItem treeItemCash = new TreeViewItem();
                treeItemCash.IsExpanded = true;
                treeItemCash.Header = "Cash in Hand";
                treeItemCash.Name = "_" + idCash;
                ProcessTree(idCash, treeItemCash);
                trvAccount.Items.Add(treeItemCash);
            }
            catch (Exception)
            {
                MessageBox.Show("Problem Occur While Loading Bank Account", "Contra Voucher", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void ProcessTree(long pId, TreeViewItem newChild)
        {
            try
            {
                foreach (EChartOfAccount obj in objBContraVoucher.GetSubAccount(pId))
                {
                    TreeViewItem newChield = new TreeViewItem();
                    newChield.Header = obj.SubAccName;
                    newChield.Name = "_" + obj.SubAccId;
                    newChild.Items.Add(newChield);
                    ProcessTree(obj.SubAccId, newChield);
                }
            }
            catch (Exception ex)
            {                
            }
        }
        long accId;
        private void trvAccount_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            decimal currentBalance = 0;
            var trvItem = trvAccount.SelectedItem as TreeViewItem;
            if (trvItem != null)
            {
                string acciuntTypeId = trvItem.Name.ToString();
                string[] splitedId = acciuntTypeId.Trim().Split('_');
                accId = Convert.ToInt64(splitedId[1]);
                if (accId >= 100 && accId < 200)
                {
                    MessageBox.Show("You can't make transaction with this account.\nPlease use its child account", "Journal Vouchar Entry", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    List<EChartOfAccount> CodeList = objBContraVoucher.GetAccountInfo(accId);
                    foreach (var code in CodeList)
                    {
                        if (code.AccHeader == "No")
                        {
                            txtAccountName.Text = trvItem.Header.ToString();
                            txtAccountNo.Text = code.SubAccCode;
                            currentBalance = objBContraVoucher.GetCurrentBalanceOfAccount(accId);
                            if (currentBalance < 0)
                            {
                                txtCurrentBalance.Text = currentBalance.ToString();
                            }
                            else
                            {
                                txtCurrentBalance.Text = currentBalance.ToString();
                            }
                            bankStatus = false;
                            CheckBank(trvItem);
                            if (bankStatus && cmbAmountType.Text == "CR")
                            {
                                grpBoxChk.IsEnabled = true;
                            }
                            else
                            {
                                grpBoxChk.IsEnabled = false;
                            }
                        }
                        else
                        {
                            MessageBox.Show("You can't make transaction with this account.\n Please use its child account", "Journal Vouchar Entry", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                }
            }
            //listBoxSuggestion.Visibility = Visibility.Hidden;
        }
       
        #endregion
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
        private void btnAccountName_Click(object sender, RoutedEventArgs e)
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

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.ShowInTaskbar = true;
            this.Owner = null;
        }

        //private void txtAccountNo_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    decimal currentBalance = 0;
        //    if (txtAccountNo.Text != string.Empty)
        //    {
        //        List<EChartOfAccount> CodeList = objBJournalVouchar.GetAccountInfoCodeWise(txtAccountNo.Text.Trim());
        //        foreach (var code in CodeList)
        //        {
        //            if (code.AccHeader == "No")
        //            {
        //                txtAccountNo.Text = code.SubAccCode;
        //                txtAccountName.Text = code.SubAccName;
        //                accId = code.SubAccId;
        //                currentBalance = objBJournalVouchar.GetCurrentBalanceOfAccount(accId);
        //                if (currentBalance < 0)
        //                {
        //                    txtCurrentBalance.Text = currentBalance.ToString();
        //                }
        //                else
        //                {
        //                    txtCurrentBalance.Text = currentBalance.ToString();
        //                }
        //                bankStatus = false;
        //                CheckBankWhileSearching(accId);
        //            }
        //            else
        //            {
        //                MessageBox.Show("You can't make transaction with this account.\n Please use its child account.", "Contra Voucher", MessageBoxButton.OK, MessageBoxImage.Information);
        //            }
        //        }
        //    }
        //}
        
        private void CheckBankWhileSearching(long acId)
        {
            EChartOfAccount objEchart = objBChartOfAccount.GetAccountInfo(acId);
            if (objEchart.ParentAccId >200)
            {
                if (objBChartOfAccount.GetAccountInfo(objEchart.ParentAccId).SubAccName == "Cash at Bank")
                {
                    bankStatus = true;
                }
                else
                {
                    CheckBankWhileSearching(objEchart.ParentAccId);
                }
            }
        }
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
                MessageBox.Show("Problem Occur While Adding Record Into List.\n" + ex.Message, "Contra Voucher", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddTransactionToList()
        {
            if (CheckField() == true)
            {
                EJournalVouchar objEJV = new EJournalVouchar();
                objEJV.Journal_Date = Convert.ToDateTime(dtpDate.SelectedDate);
                objEJV.Journal_Acc_Name = txtAccountName.Text;
                objEJV.Journal_Acc_Id = accId;
                objEJV.Journal_Acc_Code = txtAccountNo.Text;
                if (cmbAmountType.Text == "DR")
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
                if (bankStatus && cmbAmountType.Text == "CR")
                {
                    objEJV.Journal_ChequeBank = txtChkBankInfo.Text.Trim();
                    objEJV.Journal_Cheque = txtChkCheque.Text.Trim();
                    objEJV.Journal_DisplayChequeDate = Convert.ToDateTime(dtpChkChequeDate.SelectedDate).ToShortDateString();
                }
                else
                {
                    objEJV.Journal_DisplayChequeDate = null;
                }
                lvContraVoucher.Items.Add(objEJV);
                ResetFieldAdd();
            }
        }

        private void ItemTotalcalCulation()
        {
            decimal dr = 0, cr = 0;
            for (int i = 0; i < lvContraVoucher.Items.Count; i++)
            {
                EJournalVouchar obj = lvContraVoucher.Items[i] as EJournalVouchar;
                dr += Convert.ToDecimal(obj.Journal_Debit_Amount);
                cr += Convert.ToDecimal(obj.Jounal_Credit_Amount);
            }
            txtBlockDebit.Text = dr.ToString("F");
            txtBlockCredit.Text = cr.ToString("F");
        }

        #region CHECK & RESET FIELD

        private bool CheckField()
        {
            if (txtAccountNo.Text == string.Empty)
            {
                MessageBox.Show("Give your account no/code.", "Contra Voucher", MessageBoxButton.OK, MessageBoxImage.Information);
                txtAccountNo.Focus();
                return false;
            }
            if (txtAccountName.Text == string.Empty)
            {
                MessageBox.Show("Give your account type name.", "Contra Voucher", MessageBoxButton.OK, MessageBoxImage.Information);
                txtAccountName.Focus();
                return false;
            }
            if (txtAmount.Text == string.Empty)
            {
                MessageBox.Show("Transaction amount should not be empty.", "Contra Voucher", MessageBoxButton.OK, MessageBoxImage.Information);
                txtAmount.Focus();
                return false;
            }
            if (cmbAmountType.Text == string.Empty)
            {
                MessageBox.Show("Select transaction amount type.", "Contra Voucher", MessageBoxButton.OK, MessageBoxImage.Information);
                cmbAmountType.Focus();
                return false;
            }
            if (txtNotes.Text == string.Empty)
            {
                MessageBox.Show("Voucher notes should not be empty.", "Contra Voucher", MessageBoxButton.OK, MessageBoxImage.Information);
                txtNotes.Focus();
                return false;
            }
            //Refat
            if (bankStatus && cmbAmountType.Text == "CR")
            {
                if (txtChkBankInfo.Text == string.Empty)
                {
                    MessageBox.Show("Please Enter Bank Information.", "Contra Voucher", MessageBoxButton.OK, MessageBoxImage.Information);
                    txtChkBankInfo.Focus();
                    return false;
                }
                if (txtChkCheque.Text == string.Empty)
                {
                    MessageBox.Show("Please Enter Cheque No.", "Contra Voucher", MessageBoxButton.OK, MessageBoxImage.Information);
                    txtChkCheque.Focus();
                    return false;
                }
                if (dtpChkChequeDate.Text == string.Empty)
                {
                    MessageBox.Show("Please Select Cheque Date.", "Contra Voucher", MessageBoxButton.OK, MessageBoxImage.Information);
                    dtpChkChequeDate.Focus();
                    return false;
                }
            }
            return true;
        }

        private bool CheckFieldForJournalVouchar()
        {
            if (lvContraVoucher.Items.Count == 0)
            {
                MessageBox.Show("Please Enter Debit and Credit Info. Into List", "Contra Voucher", MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            if (txtBlockCredit.Text != txtBlockDebit.Text)
            {
                MessageBox.Show("Debit and Credit Balance Must Be Same", "Contra Voucher", MessageBoxButton.OK, MessageBoxImage.Stop);
                return false;
            }
            return true;
        }

        private void ResetFieldAdd()
        {
            txtAccountName.Text = "";
            txtCurrentBalance.Text = "";
            txtAccountNo.Text = "";
            txtAmount.Text = "";
            cmbAmountType.Text = "";
            bankStatus = false;
            txtChkBankInfo.Text = "";
            txtChkCheque.Text = "";
            grpBoxChk.IsEnabled = false;
            dtpChkChequeDate.SelectedDate = DateTime.Now;
            btnAdd.Content = "Add";
            LoadAccountTree();
        }

        private void ResetFieldSave()
        {
            txtVoucherNo.Text = "";
            txtNotes.Text = "";
            lvContraVoucher.Items.Clear();
            dtpDate.SelectedDate = DateTime.Now;
            txtBlockDebit.Text = string.Empty;
            txtBlockCredit.Text = string.Empty;
            txtAccountNo.Text = string.Empty;
            VoucherLastId();
            btnSave.Content = "Save";
            bankStatus = false;
        }

        #endregion

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
                    if (objBJournalVouchar.DeleteJournalVouchar(txtVoucherNo.Text.Trim()) == true)
                    {
                        UpdateJournalVouchar();
                    }
                }
            }
        }

        private void ProcessPayment()
        {
            bool IsSave = false;
            EDeposit_Payment objEdp = new EDeposit_Payment();
            for (int i = 0; i < lvContraVoucher.Items.Count; i++)
            {
                EJournalVouchar objEJournalVouchar = (EJournalVouchar)lvContraVoucher.Items[i];
                objEdp.JournalVoucherId = txtVoucherNo.Text;
                objEdp.DepositePaymentDate = Convert.ToDateTime(dtpDate.SelectedDate);
                objEdp.Description = txtNotes.Text;
                objEdp.TransactionType = "Contra";
                objEdp.AccessBy = loginUserName;
                objEdp.AccessDatetime = DateTime.Now;
                if (objEJournalVouchar.Jounal_Credit_Amount > 0 && string.IsNullOrEmpty(objEJournalVouchar.Journal_Cheque) == false)
                {
                    IsSave = true;
                    objEdp.BankAccountId = objEJournalVouchar.Journal_Acc_Id;                    
                    objEdp.BankInfoinCheque = objEJournalVouchar.Journal_ChequeBank;
                    objEdp.BankCheque = objEJournalVouchar.Journal_Cheque;
                    objEdp.BankChequeDate = Convert.ToDateTime(objEJournalVouchar.Journal_DisplayChequeDate);                    
                    objEdp.DepositPaymentAmount = objEJournalVouchar.Jounal_Credit_Amount;
                    objEdp.Status = "Pending";
                    objEdp.TransactionType = "Contra";
                }
                else
                {
                    objEdp.AccountID = objEJournalVouchar.Journal_Acc_Id;
                }
            }
            if (IsSave)
            {
                objBDepositPayment.SaveDepositPayment(objEdp);
            }
        }

        private void SaveJournalVouchar()
        {
            try
            {
                VoucherLastId();
                bool stat = false;
                for (int i = 0; i < lvContraVoucher.Items.Count; i++)
                {
                    EJournalVouchar objEJournalVouchar = (EJournalVouchar)lvContraVoucher.Items[i];
                    objEJournalVouchar.Journal_Id = txtVoucherNo.Text;
                    objEJournalVouchar.Journal_Notes = txtNotes.Text;
                    objEJournalVouchar.Journal_BankRemarks = "Contra Voucher";
                    objEJournalVouchar.Access_By = loginUserName;
                    objEJournalVouchar.Access_Date = DateTime.Now;
                    if (objEJournalVouchar.Jounal_Credit_Amount > 0 && string.IsNullOrEmpty(objEJournalVouchar.Journal_Cheque) == false)
                    {
                        stat = objBJournalVouchar.SaveJournalVoucharWithBankCheque(objEJournalVouchar);
                    }
                    else
                    {
                        stat = objBJournalVouchar.SaveJournalVouchar(objEJournalVouchar);
                    }
                }
                ProcessPayment();
                if (stat == true)
                {
                    MessageBox.Show("Contra voucher saved successfully.", "Contra Voucher", MessageBoxButton.OK, MessageBoxImage.Information);
                    ResetFieldSave();
                }
                else
                {
                    MessageBox.Show("Contra voucher saving failed.", "Contra Voucher", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem occur while saving data.\n" + ex.Message, "Contra Voucher", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
        private void ProcessPaymentForUpdate()
        {
            bool IsSave = false;
            EDeposit_Payment objEdp = new EDeposit_Payment();
            for (int i = 0; i < lvContraVoucher.Items.Count; i++)
            {
                EJournalVouchar objEJournalVouchar = (EJournalVouchar)lvContraVoucher.Items[i];
                objEdp.JournalVoucherId = txtVoucherNo.Text;
                objEdp.DepositePaymentDate = Convert.ToDateTime(dtpDate.SelectedDate);
                objEdp.Description = txtNotes.Text;
                objEdp.TransactionType = "Contra";
                objEdp.AccessBy = loginUserName;
                objEdp.AccessDatetime = DateTime.Now;
                if (objEJournalVouchar.Jounal_Credit_Amount > 0 && string.IsNullOrEmpty(objEJournalVouchar.Journal_Cheque) == false)
                {
                    IsSave = true;
                    objEdp.BankAccountId = objEJournalVouchar.Journal_Acc_Id;
                    objEdp.BankInfoinCheque = objEJournalVouchar.Journal_ChequeBank;
                    objEdp.BankCheque = objEJournalVouchar.Journal_Cheque;
                    objEdp.BankChequeDate = Convert.ToDateTime(objEJournalVouchar.Journal_DisplayChequeDate);
                    objEdp.DepositPaymentAmount = objEJournalVouchar.Jounal_Credit_Amount;
                    objEdp.Status = "Pending";
                    objEdp.TransactionType = "Contra";
                }
                else
                {
                    objEdp.AccountID = objEJournalVouchar.Journal_Acc_Id;
                }
            }
            if (IsSave)
            {
                List<EDeposit_Payment> listStatus_clDate_clJv = objBDepositPayment.GetStatus_ClearingDate_CancelJV(txtVoucherNo.Text.Trim());
                objBDepositPayment.DeleteDepositPayment(txtVoucherNo.Text.Trim());                
                for (int j = 0; j < listStatus_clDate_clJv.Count; j++)
                {
                    EDeposit_Payment objegetStatus_clDate_clJv = listStatus_clDate_clJv[j];
                    if (objEdp.AccountID == objegetStatus_clDate_clJv.AccountID)
                    {
                        objEdp.Status = objegetStatus_clDate_clJv.Status;
                        objEdp.ClearingDate = objegetStatus_clDate_clJv.ClearingDate;
                        objEdp.CancellingJV = objegetStatus_clDate_clJv.CancellingJV;
                        break;
                    }
                }
                objBDepositPayment.SaveDepositPayment(objEdp);
                if (objEdp.Status == "Cancel")//for cancel journal update
                {
                    ProcessJournalforCancelDeposit(objEdp);
                }
            }
        }
        
        private bool ProcessJournalforCancelDeposit(EDeposit_Payment objED)
        {
            string notes = (objBJournalVouchar.GetAllJournalVoucharIdWise(objED.CancellingJV))[0].Journal_Notes;//Get Notes
            objBJournalVouchar.DeleteJournalVouchar(objED.CancellingJV);//Delete Journal
            bool stat = false;

            //Credit
            EJournalVouchar objFrstJV = new EJournalVouchar();
            objFrstJV.Journal_Id = objED.CancellingJV;
            objFrstJV.Journal_Acc_Id = objED.BankAccountId;

            EChartOfAccount objCA = objBDepositPayment.GetAccountInfo(objFrstJV.Journal_Acc_Id);
            objFrstJV.Journal_Acc_Name = objCA.SubAccName;
            objFrstJV.Journal_Acc_Code = objCA.SubAccCode;
            objFrstJV.Journal_Notes = notes;
            objFrstJV.Journal_Date = Convert.ToDateTime(objED.ClearingDate);
            objFrstJV.Journal_Debit_Amount = 0;
            objFrstJV.Jounal_Credit_Amount = objED.DepositPaymentAmount;
            objFrstJV.Journal_BankRemarks = "Cheque Return";
            objFrstJV.Access_By = objED.AccessBy;
            objFrstJV.Access_Date = DateTime.Now;
            stat = objBJournalVouchar.SaveJournalVouchar(objFrstJV);

            //Debit
            EJournalVouchar objScndJV = new EJournalVouchar();
            objScndJV.Journal_Id = objED.CancellingJV;
            objScndJV.Journal_Acc_Id = objED.AccountID;

            objCA = objBDepositPayment.GetAccountInfo(objScndJV.Journal_Acc_Id);/*Get Account info.*/

            objScndJV.Journal_Acc_Name = objCA.SubAccName;
            objScndJV.Journal_Acc_Code = objCA.SubAccCode;
            objScndJV.Journal_Notes = notes;
            objScndJV.Journal_Date = Convert.ToDateTime(objED.ClearingDate);
            objScndJV.Journal_Debit_Amount = objED.DepositPaymentAmount;
            objScndJV.Jounal_Credit_Amount = 0;
            objScndJV.Journal_Cheque = objED.BankCheque;
            objScndJV.Journal_ChequeDate = Convert.ToDateTime(objED.BankChequeDate);
            objScndJV.Journal_BankRemarks = "Cheque Return";
            objScndJV.Access_By = objED.AccessBy;
            objScndJV.Access_Date = DateTime.Now;
            stat = objBDepositPayment.SaveJournalVouchar(objScndJV);
            return stat;
        }
        
        private void UpdateJournalVouchar()
        {
            try
            {
                bool stat = false;
                for (int i = 0; i < lvContraVoucher.Items.Count; i++)
                {
                    EJournalVouchar objEJournalVouchar = (EJournalVouchar)lvContraVoucher.Items[i];
                    objEJournalVouchar.Journal_Id = txtVoucherNo.Text;
                    objEJournalVouchar.Journal_Notes = txtNotes.Text;
                    objEJournalVouchar.Journal_BankRemarks = "Contra Voucher";
                    objEJournalVouchar.Access_By = loginUserName;
                    objEJournalVouchar.Access_Date = DateTime.Now;
                    if (objEJournalVouchar.Jounal_Credit_Amount > 0 && string.IsNullOrEmpty(objEJournalVouchar.Journal_Cheque) == false)
                    {
                        stat = objBJournalVouchar.SaveJournalVoucharWithBankCheque(objEJournalVouchar);
                    }
                    else
                    {
                        stat = objBJournalVouchar.SaveJournalVouchar(objEJournalVouchar);
                    }
                }
                ProcessPaymentForUpdate();
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

        int getIndex;
        //long AccId;
        private void Edit_Contra_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                decimal currentBalance = 0;
                if (lvContraVoucher.SelectedIndex > -1)
                {
                    EJournalVouchar selectedJV = (EJournalVouchar)lvContraVoucher.SelectedItem;
                    accId = selectedJV.Journal_Acc_Id;
                    FillTransactionField(selectedJV);                    
                    List<EChartOfAccount> listChart = objBJournalVouchar.GetAccountInfo(accId);
                    getIndex = lvContraVoucher.SelectedIndex;
                    btnAdd.Content = "Modify";
                    currentBalance = objBJournalVouchar.GetCurrentBalanceOfAccount(accId);
                    if (currentBalance < 0)
                    {
                        txtCurrentBalance.Text = currentBalance.ToString();
                    }
                    else
                    {
                        txtCurrentBalance.Text = currentBalance.ToString();
                    }                    
                }
                else
                {
                    MessageBox.Show("Select an account first.", "Contra Voucher", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem occur while edit data.\n" + ex.Message, "Contra Voucher", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void FillTransactionField(EJournalVouchar selectedJV)
        {
            txtAccountName.Text = selectedJV.Journal_Acc_Name;
            txtAccountNo.Text = selectedJV.Journal_Acc_Code;
            dtpDate.SelectedDate = selectedJV.Journal_Date;
            cmbAmountType.SelectedIndex = -1;
            bankStatus = false;
            CheckBankWhileSearching(accId);
            if (selectedJV.Journal_Debit_Amount == 0)
            {
                cmbAmountType.Text = "CR";
                txtAmount.Text = selectedJV.Jounal_Credit_Amount.ToString();
            }
            else
            {
                cmbAmountType.Text = "DR";
                txtAmount.Text = selectedJV.Journal_Debit_Amount.ToString();
            }
            if (bankStatus)
            {
                txtChkBankInfo.Text = selectedJV.Journal_ChequeBank;
                txtChkCheque.Text = selectedJV.Journal_Cheque;
                dtpChkChequeDate.SelectedDate = Convert.ToDateTime(selectedJV.Journal_DisplayChequeDate);
            }
        }

        private void UpdateTransaction()
        {
            if (CheckField() == true)
            {
                EJournalVouchar objEJV = new EJournalVouchar();
                objEJV.Journal_Date = Convert.ToDateTime(dtpDate.SelectedDate);
                objEJV.Journal_Acc_Name = txtAccountName.Text;
                objEJV.Journal_Acc_Id = accId;
                objEJV.Journal_Acc_Code = txtAccountNo.Text;
                if (cmbAmountType.Text == "DR")
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
                if (bankStatus && cmbAmountType.Text == "CR")
                {
                    objEJV.Journal_ChequeBank = txtChkBankInfo.Text.Trim();
                    objEJV.Journal_Cheque = txtChkCheque.Text.Trim();
                    objEJV.Journal_DisplayChequeDate = Convert.ToDateTime(dtpChkChequeDate.SelectedDate).ToShortDateString();
                }
                else
                {
                    objEJV.Journal_DisplayChequeDate = null;
                }
                lvContraVoucher.Items.RemoveAt(getIndex);
                lvContraVoucher.Items.Insert(getIndex, objEJV);
                ResetFieldAdd();
            }
        }

        private void Remove_Contra_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lvContraVoucher.SelectedIndex > -1)
                {
                    if (MessageBox.Show("Are you sure want to remove this transaction?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        lvContraVoucher.Items.Remove(lvContraVoucher.SelectedItem);
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
                MessageBox.Show("Problem occur while removing data." + ex.Message, "Journal Voucher Entry", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void txtVoucherNo_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtVoucherNo.Text != string.Empty)
            {
                ResetFieldAdd();
                lvContraVoucher.Items.Clear();
                dtpDate.SelectedDate = DateTime.Now;
                txtBlockDebit.Text = string.Empty;
                txtBlockCredit.Text = string.Empty;
                txtNotes.Text = "";
                btnSave.Content = "Save";
                if (objBJournalVouchar.DoesExistContraVoucher(txtVoucherNo.Text.Trim()))
                {
                    LoadListViewFromDataBase();
                    btnSave.Content = "Update";
                }
            }
        }

        private void LoadListViewFromDataBase()
        {
            lvContraVoucher.Items.Clear();
            try
            {
                List<EJournalVouchar> VoucharList = objBJournalVouchar.GetAllJournalVoucharIdWise(txtVoucherNo.Text.Trim());
                foreach (EJournalVouchar myJVouchar in VoucharList)
                {
                    lvContraVoucher.Items.Add(myJVouchar);
                    txtNotes.Text = myJVouchar.Journal_Notes;
                    dtpDate.SelectedDate = myJVouchar.Journal_Date;
                }
                ItemTotalcalCulation();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem occur to get Journal voucher details.\n" + ex.Message, "Contra Voucher", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void txtAmount_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Validations.IsDecimal(e.Text);
            base.OnPreviewTextInput(e);
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            ResetFieldAdd();
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            ResetFieldSave();
        }

        private void cmbAmountType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbAmountType.SelectedIndex > -1)
            {
                if (bankStatus && cmbAmountType.SelectedValue.ToString() == "CR")
                {
                    grpBoxChk.IsEnabled = true;
                }
                else
                {
                    bankStatus = false;
                    txtChkBankInfo.Text = "";
                    txtChkCheque.Text = "";
                    grpBoxChk.IsEnabled = false;
                    dtpChkChequeDate.SelectedDate = DateTime.Now;                   
                }
            }
        }
    }
}
