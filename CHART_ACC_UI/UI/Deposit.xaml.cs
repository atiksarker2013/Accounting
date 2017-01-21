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
//Author:REFAT(20/02/2012)
namespace CHART_ACC_UI.UI
{
    /// <summary>
    /// Interaction logic for DepositPaymentByCashCheque.xaml
    /// </summary>
    public partial class Deposit : Window
    {
        BDeposit_Payment objBDepositPayment = new BDeposit_Payment();
        BChartOfAccount objBChartOfAccount = new BChartOfAccount();
        BJournalVouchar objBJournalVouchar = new BJournalVouchar();
        string loginUser = string.Empty;
        string TRANSACTION_TYPE = "Deposit";

        public Deposit()
        {
            InitializeComponent();
            InitialTaskCheque();
            InitialTaskCash();
        }

        public Deposit(string user)
            : this()
        {
            loginUser = user;
        }
        /********************************************************************************************************************************/
        /****************************************************Cheque TabItem**************************************************************/
        #region Cheque TabItem

        EChartOfAccount objECAforChk = new EChartOfAccount();
        int indexOfSelectedItemChk = -1;

        private void InitialTaskCheque()
        {
            try
            {
                txtChkVouchar.Text = objBJournalVouchar.GetJVoucherLastID();
                dtpChkDeposit.SelectedDate = DateTime.Now;
                dtpChkChequeDate.SelectedDate = DateTime.Now;
                trvChkBankAccountType.Visibility = Visibility.Hidden;
                trvChkAccount.Visibility = Visibility.Hidden;
                LoadBankAccountTypeTreeCheque();
                LoadAccountTypeTreeCheque();
            }
            catch (Exception)
            {
            }
        }

        private void LoadBankAccountTypeTreeCheque()
        {
            trvChkBankAccountType.Items.Clear();
            try
            {
                long id = objBChartOfAccount.GetAccountID("Cash at Bank");
                TreeViewItem treeItem = new TreeViewItem();
                treeItem.IsExpanded = true;
                treeItem.Header = "Cash at Bank";
                treeItem.Name = "_" + id;
                ProcessTreeCheque(id, treeItem);
                trvChkBankAccountType.Items.Add(treeItem);
            }
            catch (Exception)
            {
                MessageBox.Show("Problem Occur While Loading Bank Account", TRANSACTION_TYPE, MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void LoadAccountTypeTreeCheque()
        {
            trvChkAccount.Items.Clear();
            try
            {
                foreach (EAccountType objType in objBChartOfAccount.GetAllAccountType())
                {
                    TreeViewItem treeItem = new TreeViewItem();
                    treeItem.Header = objType.AccTypeName;
                    treeItem.Name = "_" + objType.AccTypeId;
                    ProcessTreeCheque(objType.AccTypeId, treeItem);
                    trvChkAccount.Items.Add(treeItem);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Problem Occur While Loading Account", TRANSACTION_TYPE, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ProcessTreeCheque(long pId, TreeViewItem newChild)
        {
            foreach (EChartOfAccount obj in objBDepositPayment.GetSubAccount(pId))
            {
                TreeViewItem trChild = new TreeViewItem();
                //trChild.IsExpanded = true;
                trChild.Header = obj.SubAccName;
                trChild.Name = "_" + obj.SubAccId;
                newChild.Items.Add(trChild);
                if (obj.AccHeader == "Yes")
                {
                    ProcessTreeCheque(obj.SubAccId, trChild);
                }
            }
        }

        long parentIdChk = 0;
        private void trvChkBankAccountType_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var trvItem = (sender as TreeView).SelectedItem as TreeViewItem;
            if (trvItem != null && (trvItem.HasItems == false))
            {
                txtBlockChkBankAccountName.Text = trvItem.Header.ToString();
                string[] splitedId = (trvItem.Name.ToString()).Split('_');
                parentIdChk = Convert.ToInt64(splitedId[1]);
                trvChkBankAccountType.Visibility = Visibility.Hidden;
            }
        }

        private void btnChkBankAccount_Click(object sender, RoutedEventArgs e)
        {
            if (trvChkBankAccountType.IsVisible == false)
            {
                trvChkBankAccountType.Visibility = Visibility.Visible;
            }
            else
            {
                trvChkBankAccountType.Visibility = Visibility.Hidden;
            }
        }

        private void btnChkSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CheckfieldCheque())
                {
                    if (btnChkSave.Content.ToString() == "Save")
                    {
                        txtChkVouchar.Text = objBJournalVouchar.GetJVoucherLastID();
                        SaveDepositInfoCheque();
                    }
                    else if (btnChkSave.Content.ToString() == "Update")
                    {
                        List<EDeposit_Payment> listStatus_clDate_clJv = objBDepositPayment.GetStatus_ClearingDate_CancelJV(txtChkVouchar.Text.Trim());
                        objBDepositPayment.DeleteDepositPayment(txtChkVouchar.Text.Trim());
                        objBJournalVouchar.DeleteJournalVouchar(txtChkVouchar.Text.Trim());
                        SaveDepositPaymentinUpdateMode(listStatus_clDate_clJv);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Problem Occur While Storing Information.", TRANSACTION_TYPE, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool CheckfieldCheque()
        {
            if (txtChkVouchar.Text == string.Empty)
            {
                MessageBox.Show("Vouchar Should not be Blank.", TRANSACTION_TYPE, MessageBoxButton.OK, MessageBoxImage.Information);
                txtChkVouchar.Focus();
                return false;
            }
            if (dtpChkDeposit.Text == string.Empty)
            {
                MessageBox.Show("Please Select Date.", TRANSACTION_TYPE, MessageBoxButton.OK, MessageBoxImage.Information);
                dtpChkDeposit.Focus();
                return false;
            }
            if (txtBlockChkBankAccountName.Text == string.Empty)
            {
                MessageBox.Show("Please Select Bank Account Name.", TRANSACTION_TYPE, MessageBoxButton.OK, MessageBoxImage.Information);
                btnChkBankAccount.Focus();
                return false;
            }
            if (txtChkDescription.Text == string.Empty)
            {
                MessageBox.Show("Please Enter Description.", TRANSACTION_TYPE, MessageBoxButton.OK, MessageBoxImage.Information);
                txtChkDescription.Focus();
                return false;
            }
            if (lvChkPartyList.Items.Count == 0)
            {
                MessageBox.Show("Please Fill-up Account List.", TRANSACTION_TYPE, MessageBoxButton.OK, MessageBoxImage.Information);
                lvChkPartyList.Focus();
                return false;
            }
            return true;
        }

        private void SaveDepositInfoCheque()
        {
            bool stat = false;
            decimal totalDeposit = 0;
            for (int i = 0; i < lvChkPartyList.Items.Count; i++)
            {
                EDeposit_Payment objEDepositPayment = lvChkPartyList.Items[i] as EDeposit_Payment;
                objEDepositPayment.DepositePaymentDate = Convert.ToDateTime(dtpChkDeposit.SelectedDate);
                objEDepositPayment.BankAccountId = parentIdChk;
                objEDepositPayment.Description = txtChkDescription.Text.Trim();
                objEDepositPayment.Status = "Pending";
                objEDepositPayment.TransactionType = TRANSACTION_TYPE;
                objEDepositPayment.JournalVoucherId = txtChkVouchar.Text.Trim();
                objEDepositPayment.AccessBy = loginUser;
                totalDeposit += objEDepositPayment.DepositPaymentAmount;
                stat = objBDepositPayment.SaveDepositPayment(objEDepositPayment);
            }
            if (stat)
            {
                ProcessJournalforDepositCheque(totalDeposit, txtChkVouchar.Text.Trim());
                MessageBox.Show(TRANSACTION_TYPE + " Success.", TRANSACTION_TYPE, MessageBoxButton.OK, MessageBoxImage.Information);
                ResetFieldCheque();
            }
        }

        private void SaveDepositPaymentinUpdateMode(List<EDeposit_Payment> listgetStatus_clDate_clJv)
        {
            bool stat = false;
            decimal totalDeposit = 0;
            for (int i = 0; i < lvChkPartyList.Items.Count; i++)
            {
                EDeposit_Payment objEDepositPayment = lvChkPartyList.Items[i] as EDeposit_Payment;
                objEDepositPayment.DepositePaymentDate = Convert.ToDateTime(dtpChkDeposit.SelectedDate);
                objEDepositPayment.BankAccountId = parentIdChk;
                objEDepositPayment.Description = txtChkDescription.Text.Trim();
                objEDepositPayment.TransactionType = TRANSACTION_TYPE;
                objEDepositPayment.JournalVoucherId = txtChkVouchar.Text.Trim();
                objEDepositPayment.Status = "Pending";
                for (int j = 0; j < listgetStatus_clDate_clJv.Count; j++)
                {
                    EDeposit_Payment objegetStatus_clDate_clJv = listgetStatus_clDate_clJv[j];
                    if (objEDepositPayment.AccountID == objegetStatus_clDate_clJv.AccountID)
                    {
                        objEDepositPayment.Status = objegetStatus_clDate_clJv.Status;
                        objEDepositPayment.ClearingDate = objegetStatus_clDate_clJv.ClearingDate;
                        objEDepositPayment.CancellingJV = objegetStatus_clDate_clJv.CancellingJV;
                        break;
                    }
                }
                objEDepositPayment.AccessBy = loginUser;
                totalDeposit += objEDepositPayment.DepositPaymentAmount;
                stat = objBDepositPayment.SaveDepositPaymentinUpdateMode(objEDepositPayment);
                if (stat && objEDepositPayment.Status == "Cancel")//for cancel journal update
                {
                    ProcessJournalforCancelDeposit(objEDepositPayment);
                }
            }
            if (stat)
            {
                ProcessJournalforDepositCheque(totalDeposit, txtChkVouchar.Text.Trim());
                MessageBox.Show(TRANSACTION_TYPE + " Updated Success.", TRANSACTION_TYPE, MessageBoxButton.OK, MessageBoxImage.Information);
                ResetFieldCheque();
            }
        }

        private void ProcessJournalforDepositCheque(decimal totalDeposit, string JVId)
        {
            //Debit
            EJournalVouchar objDebitJV = new EJournalVouchar();
            objDebitJV.Journal_Id = JVId;
            objDebitJV.Journal_Acc_Id = parentIdChk;

            EChartOfAccount objCA = objBDepositPayment.GetAccountInfo(objDebitJV.Journal_Acc_Id);/*Get Account info.*/

            objDebitJV.Journal_Acc_Name = objCA.SubAccName;
            objDebitJV.Journal_Acc_Code = objCA.SubAccCode;
            objDebitJV.Journal_Notes = txtChkDescription.Text.Trim();
            objDebitJV.Journal_Date = Convert.ToDateTime(dtpChkDeposit.SelectedDate);
            objDebitJV.Journal_Debit_Amount = totalDeposit;
            objDebitJV.Jounal_Credit_Amount = 0;
            objDebitJV.Journal_BankRemarks = "Cheque Deposit";
            objDebitJV.Access_By = loginUser;
            objDebitJV.Access_Date = DateTime.Now;
            objBJournalVouchar.SaveJournalVouchar(objDebitJV);/*BJournalVoucher call because no cheque related info. need*/

            //Credit
            for (int i = 0; i < lvChkPartyList.Items.Count; i++)
            {
                EDeposit_Payment objEDeposit = lvChkPartyList.Items[i] as EDeposit_Payment;

                EJournalVouchar objCreditJV = new EJournalVouchar();
                objCreditJV.Journal_Id = JVId;
                objCreditJV.Journal_Acc_Id = objEDeposit.AccountID;
                objCreditJV.Journal_Acc_Name = objEDeposit.AccountName;
                objCreditJV.Journal_Acc_Code = objEDeposit.AccountCode;
                objCreditJV.Journal_Notes = objEDeposit.Description;
                objCreditJV.Journal_Date = Convert.ToDateTime(objEDeposit.DepositePaymentDate);
                objCreditJV.Journal_Debit_Amount = 0;
                objCreditJV.Jounal_Credit_Amount = objEDeposit.DepositPaymentAmount;
                objCreditJV.Journal_Cheque = objEDeposit.BankCheque;
                objCreditJV.Journal_ChequeDate = Convert.ToDateTime(objEDeposit.BankChequeDate);
                objCreditJV.Journal_BankRemarks = "Cheque Deposit";
                objCreditJV.Access_By = objEDeposit.AccessBy;
                objCreditJV.Access_Date = DateTime.Now;
                objBDepositPayment.SaveJournalVouchar(objCreditJV);
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

        private void btnChkCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnChkReset_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ResetFieldCheque();
            }
            catch (Exception)
            {

            }
        }

        private void ResetFieldCheque()
        {
            txtChkVouchar.Text = objBJournalVouchar.GetJVoucherLastID();
            dtpChkDeposit.SelectedDate = DateTime.Now;
            trvChkBankAccountType.Visibility = Visibility.Hidden;
            trvChkAccount.Visibility = Visibility.Hidden;
            LoadBankAccountTypeTreeCheque();
            parentIdChk = 0;
            txtBlockChkBankAccountName.Text = string.Empty;
            txtChkDescription.Text = string.Empty;
            txtChkTotalDepositAmount.Text = string.Empty;
            ClearAccountInfoCheque();
            lvChkPartyList.Items.Clear();
            btnChkSave.Content = "Save";
        }

        private void txtChkAccountCode_TextChanged(object sender, TextChangedEventArgs e)
        {
           
        }

        private void txtChkAccountCode_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    if (txtChkAccountCode.Text != string.Empty)
                    {
                        List<EChartOfAccount> listCA = objBJournalVouchar.GetAccountInfoCodeWise(txtChkAccountCode.Text.Trim());
                        foreach (var obj in listCA)
                        {
                            objECAforChk = obj;
                        }
                        if (listCA.Count > 0)
                        {
                            foreach (TreeViewItem tree in trvChkAccount.Items)
                            {
                                if (tree.Header.ToString() == objECAforChk.RootAccName)
                                {
                                    tree.IsExpanded = true;
                                    SetSelectedIndexCheque(tree);
                                    break;
                                }
                            }
                        }
                        else
                        {
                            LoadAccountTypeTreeCheque();
                            objECAforChk = new EChartOfAccount();
                            txtBoxChkAccountName.Text = string.Empty;
                            txtChkMotherAccountName.Text = string.Empty;
                            txtChkBalance.Text = string.Empty;
                        }
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Problem Occur While Searching.", TRANSACTION_TYPE, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void txtBoxChkAccountName_TextChanged(object sender, TextChangedEventArgs e)
        {
            listBoxChkSuggestion.Items.Clear();
            if (txtBoxChkAccountName.Text != "")
            {
                List<EChartOfAccount> namelist = objBJournalVouchar.GetAccountInfoLikeNameWise(txtBoxChkAccountName.Text);
                if (namelist.Count > 0)
                {
                    listBoxChkSuggestion.Visibility = Visibility.Visible;
                    foreach (var obj in namelist)
                    {
                        listBoxChkSuggestion.Items.Add(obj);
                    }
                }
                else
                {
                    listBoxChkSuggestion.Visibility = Visibility.Hidden;
                }
            }
            else
            {
                listBoxChkSuggestion.Visibility = Visibility.Hidden;
            }
        }

        private void txtBoxChkAccountName_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down)
            {
                listBoxChkSuggestion.Focus();
            }
        }

        private void listBoxChkSuggestion_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (listBoxChkSuggestion.SelectedIndex == 0 && e.Key == Key.Up)
            {
                txtBoxChkAccountName.Focus();
            }
        }

        private void listBoxChkSuggestion_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (listBoxChkSuggestion.SelectedIndex > -1)
                {
                    if (e.Key == Key.Enter)
                    {
                        EChartOfAccount objSelectedCa = listBoxChkSuggestion.SelectedItem as EChartOfAccount;
                        txtBoxChkAccountName.Text = objSelectedCa.SubAccName;
                        listBoxChkSuggestion.Visibility = Visibility.Hidden;
                        txtChkAccountCode.Text = objSelectedCa.SubAccCode;
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void txtChkDepositAmount_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Validations.IsDecimal(e.Text);
            base.OnPreviewTextInput(e);
        }

        private bool CheckFieldforAccountCheque()
        {
            if (objECAforChk.SubAccId == 0)
            {
                MessageBox.Show("Please Select Any Account.", TRANSACTION_TYPE, MessageBoxButton.OK, MessageBoxImage.Information);
                trvChkAccount.Focus();
                return false;
            }
            if (txtChkAccountCode.Text == string.Empty)
            {
                MessageBox.Show("Please Enter Account Code.", TRANSACTION_TYPE, MessageBoxButton.OK, MessageBoxImage.Information);
                txtChkAccountCode.Focus();
                return false;
            }
            if (txtChkDepositAmount.Text == string.Empty)
            {
                MessageBox.Show("Please Enter Amount.", TRANSACTION_TYPE, MessageBoxButton.OK, MessageBoxImage.Information);
                txtChkDepositAmount.Focus();
                return false;
            }
            if (txtChkBankInfo.Text == string.Empty)
            {
                MessageBox.Show("Please Enter Bank Information.", TRANSACTION_TYPE, MessageBoxButton.OK, MessageBoxImage.Information);
                txtChkBankInfo.Focus();
                return false;
            }
            if (txtChkCheque.Text == string.Empty)
            {
                MessageBox.Show("Please Enter Cheque No.", TRANSACTION_TYPE, MessageBoxButton.OK, MessageBoxImage.Information);
                txtChkCheque.Focus();
                return false;
            }
            if (dtpChkChequeDate.Text == string.Empty)
            {
                MessageBox.Show("Please Select Cheque Date.", TRANSACTION_TYPE, MessageBoxButton.OK, MessageBoxImage.Information);
                dtpChkChequeDate.Focus();
                return false;
            }
            return true;
        }

        private void btnChkAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CheckFieldforAccountCheque())
                {
                    EDeposit_Payment objEDepositPayment = new EDeposit_Payment();
                    objEDepositPayment.AccountID = objECAforChk.SubAccId;
                    objEDepositPayment.AccountCode = objECAforChk.SubAccCode;
                    objEDepositPayment.AccountName = objECAforChk.SubAccName;
                    objEDepositPayment.RootAccName = objECAforChk.RootAccName;
                    objEDepositPayment.DepositPaymentAmount = Convert.ToDecimal(txtChkDepositAmount.Text.Trim());
                    objEDepositPayment.BankInfoinCheque = txtChkBankInfo.Text.Trim();
                    objEDepositPayment.BankCheque = txtChkCheque.Text.Trim();
                    objEDepositPayment.BankChequeDate = Convert.ToDateTime(dtpChkChequeDate.SelectedDate);
                    if (btnChkAdd.Content.ToString() == "Add")
                    {
                        lvChkPartyList.Items.Add(objEDepositPayment);
                    }
                    else
                    {
                        lvChkPartyList.Items.RemoveAt(indexOfSelectedItemChk);
                        lvChkPartyList.Items.Insert(indexOfSelectedItemChk, objEDepositPayment);
                    }
                    ClearAccountInfoCheque();
                    ItemTotalCalculationCheque();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Problem Occur.", TRANSACTION_TYPE, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ItemTotalCalculationCheque()
        {
            decimal total = 0;
            for (int i = 0; i < lvChkPartyList.Items.Count; i++)
            {
                total += (lvChkPartyList.Items[i] as EDeposit_Payment).DepositPaymentAmount;
            }
            txtChkTotalDepositAmount.Text = total.ToString();
        }

        private void btnChkClear_Click(object sender, RoutedEventArgs e)
        {
            ClearAccountInfoCheque();
        }

        private void ClearAccountInfoCheque()
        {
            objECAforChk = new EChartOfAccount();
            indexOfSelectedItemChk = -1;
            txtChkAccountCode.Text = string.Empty;
            txtBoxChkAccountName.Text = string.Empty;
            txtChkMotherAccountName.Text = string.Empty;
            txtChkBalance.Text = string.Empty;
            txtChkDepositAmount.Text = string.Empty;
            txtChkBankInfo.Text = string.Empty;
            txtChkCheque.Text = string.Empty;
            dtpChkChequeDate.SelectedDate = DateTime.Now;
            LoadAccountTypeTreeCheque();
            btnChkAdd.Content = "Add";
        }

        private void EditChkAccount_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lvChkPartyList.SelectedIndex > -1)
                {
                    EDeposit_Payment objEDP = lvChkPartyList.SelectedItem as EDeposit_Payment;
                    indexOfSelectedItemChk = lvChkPartyList.SelectedIndex;
                    txtChkAccountCode.Text = objEDP.AccountCode;
                    txtChkDepositAmount.Text = objEDP.DepositPaymentAmount.ToString();
                    txtChkBankInfo.Text = objEDP.BankInfoinCheque;
                    txtChkCheque.Text = objEDP.BankCheque;
                    dtpChkChequeDate.SelectedDate = objEDP.BankChequeDate;
                    listBoxChkSuggestion.Visibility = Visibility.Hidden;
                    btnChkAdd.Content = "Modify";
                }
                else
                {
                    MessageBox.Show("Select an Account First.", TRANSACTION_TYPE, MessageBoxButton.OK, MessageBoxImage.Information);
                    lvChkPartyList.Focus();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Problem Occur While Selection.", TRANSACTION_TYPE, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RemoveChkAccount_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lvChkPartyList.SelectedIndex > -1)
                {
                    if (MessageBox.Show("Are you sure want to Remove the Record?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        if (lvChkPartyList.SelectedIndex == indexOfSelectedItemChk)
                        {
                            MessageBox.Show("This Record is Running with Modify mode So You can't Remove This.", TRANSACTION_TYPE, MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        else
                        {
                            lvChkPartyList.Items.Remove(lvChkPartyList.SelectedItem);
                            ItemTotalCalculationCheque();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Select an Account First.", TRANSACTION_TYPE, MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Problem Occur While Deletion.", TRANSACTION_TYPE, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ResetFieldforLostFocusCheque()
        {
            dtpChkDeposit.SelectedDate = DateTime.Now;
            dtpChkChequeDate.SelectedDate = DateTime.Now;
            trvChkBankAccountType.Visibility = Visibility.Hidden;
            LoadAccountTypeTreeCheque();

            parentIdChk = 0;
            txtBlockChkBankAccountName.Text = string.Empty;
            txtChkDescription.Text = string.Empty;
            txtChkTotalDepositAmount.Text = string.Empty;
            ClearAccountInfoCheque();
            lvChkPartyList.Items.Clear();
            btnChkSave.Content = "Save";
        }

        private void txtChkVouchar_KeyUp(object sender, KeyEventArgs e)
        {
            //try
            //{
            //    if (txtChkVouchar.Text != string.Empty)
            //    {
            //        ResetFieldforLostFocusCheque();
            //        if (objBDepositPayment.DoesExistVouchar(txtChkVouchar.Text, TRANSACTION_TYPE))
            //        {
            //            FillUpAllFieldCheque();
            //        }
            //    }
            //}
            //catch (Exception)
            //{
            //}
        }

        private void txtChkVouchar_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtChkVouchar.Text != string.Empty)
                {
                    ResetFieldforLostFocusCheque();
                    if (objBDepositPayment.DoesExistVouchar(txtChkVouchar.Text, TRANSACTION_TYPE))
                    {
                        FillUpAllFieldCheque();
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void FillUpAllFieldCheque()
        {
            foreach (EDeposit_Payment objEDP in objBDepositPayment.GetAllDepositPaymentByVouchar(txtChkVouchar.Text, TRANSACTION_TYPE))
            {
                parentIdChk = objEDP.BankAccountId;
                txtBlockChkBankAccountName.Text = objEDP.BankAccountName;
                dtpChkDeposit.SelectedDate = objEDP.DepositePaymentDate;
                txtChkDescription.Text = objEDP.Description;
                lvChkPartyList.Items.Add(objEDP);
            }
            foreach (TreeViewItem tree in trvChkBankAccountType.Items)
            {
                tree.IsExpanded = true;
                SetSelectedIndexforBankCheque(tree);
            }
            ItemTotalCalculationCheque();
            btnChkSave.Content = "Update";
        }

        private void SetSelectedIndexCheque(TreeViewItem tr)
        {
            foreach (TreeViewItem trItem in tr.Items)
            {
                if (trItem.Name == "_" + objECAforChk.SubAccId.ToString())
                {
                    trItem.IsExpanded = true;
                    trItem.IsSelected = true;
                    ParentExpandCheque(trItem);
                    break;
                }
                else
                {
                    if (trItem.HasItems)
                    {
                        SetSelectedIndexCheque(trItem);
                    }
                }
            }
        }

        private void SetSelectedIndexforBankCheque(TreeViewItem tr)
        {
            foreach (TreeViewItem trItem in tr.Items)
            {
                if (trItem.Name == "_" + parentIdChk)
                {
                    trItem.IsExpanded = true;
                    trItem.IsSelected = true;
                    ParentExpandCheque(trItem);
                    break;
                }
                else
                {
                    if (trItem.HasItems)
                    {
                        SetSelectedIndexforBankCheque(trItem);
                    }
                }
            }
        }

        private void ParentExpandCheque(TreeViewItem trItem)
        {
            TreeViewItem tr = trItem.Parent as TreeViewItem;
            if (tr != null)
            {
                tr.IsExpanded = true;
                ParentExpandCheque(tr);
            }
        }

        private void trvChkAccount_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                var trvItem = (sender as TreeView).SelectedItem as TreeViewItem;
                if (trvItem != null && (trvItem.HasItems == false))
                {
                    objECAforChk = new EChartOfAccount();

                    txtBoxChkAccountName.Text = trvItem.Header.ToString();
                    string[] splitedId = (trvItem.Name.ToString()).Split('_');
                    objECAforChk = objBChartOfAccount.GetAccountInfo(Convert.ToInt64(splitedId[1]));
                    txtChkAccountCode.Text = objECAforChk.SubAccCode;
                    txtChkBalance.Text = objBJournalVouchar.GetCurrentBalanceOfAccount(objECAforChk.SubAccId).ToString();
                    txtChkMotherAccountName.Text = (trvItem.Parent as TreeViewItem).Header.ToString();
                    listBoxChkSuggestion.Visibility = Visibility.Hidden;
                    trvChkAccount.Visibility = Visibility.Hidden;
                }
                else if (trvItem != null && (trvItem.HasItems == true))
                {
                    MessageBox.Show("You can't make transaction with this account.\n Please use its child account", TRANSACTION_TYPE, MessageBoxButton.OK, MessageBoxImage.Information);
                    objECAforChk = new EChartOfAccount();
                    txtChkAccountCode.Text = string.Empty;
                    txtBoxChkAccountName.Text = string.Empty;
                    txtChkBalance.Text = string.Empty;
                    txtChkMotherAccountName.Text = string.Empty;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Problem Occur While Selection.", TRANSACTION_TYPE, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnChkAccountName_Click(object sender, RoutedEventArgs e)
        {
            if (trvChkAccount.IsVisible == false)
            {
                trvChkAccount.Visibility = Visibility.Visible;
            }
            else
            {
                trvChkAccount.Visibility = Visibility.Hidden;
            }
        }

        private void tabItemCheque_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            trvChkBankAccountType.Visibility = Visibility.Hidden;
            trvChkAccount.Visibility = Visibility.Hidden;
        }
        #endregion

        /*******************************************************End Cheque TabItem*********************************************************/
        /**********************************************************************************************************************************/
        EChartOfAccount objECA = new EChartOfAccount();
        int indexOfSelectedItemCash = -1;
        private void InitialTaskCash()
        {
            GetVoucharLastId();
            dtpCashDeposit.SelectedDate = DateTime.Now;
            trvCashBankAccountType.Visibility = Visibility.Hidden;
            trvCashOthersAccount.Visibility = Visibility.Hidden;
            LoadBankAccountTypeTreeCash();
            LoadOthersAccountTypeTreeCash();
        }

        private void LoadBankAccountTypeTreeCash()
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
                MessageBox.Show("Problem Occur While Loading Bank Account", TRANSACTION_TYPE, MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void LoadOthersAccountTypeTreeCash()
        {
            trvCashOthersAccount.Items.Clear();
            try
            {
                foreach (EAccountType objType in objBChartOfAccount.GetAllAccountType())
                {
                    TreeViewItem treeItem = new TreeViewItem();
                    treeItem.Header = objType.AccTypeName;
                    treeItem.Name = "_" + objType.AccTypeId;
                    ProcessTreeCash(objType.AccTypeId, treeItem);
                    trvCashOthersAccount.Items.Add(treeItem);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Problem Occur While Loading Parent Account", TRANSACTION_TYPE, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ProcessTreeCash(long pId, TreeViewItem newChild)
        {
            foreach (EChartOfAccount obj in objBDepositPayment.GetSubAccount(pId))
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

        long parentIdCash = 0;
        private void trvCashBankAccountType_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var trvItem = (sender as TreeView).SelectedItem as TreeViewItem;
            if (trvItem != null && (trvItem.HasItems == false))
            {
                txtBlockCashAccountName.Text = trvItem.Header.ToString();
                string[] splitedId = (trvItem.Name.ToString()).Split('_');
                parentIdCash = Convert.ToInt64(splitedId[1]);
                trvCashBankAccountType.Visibility = Visibility.Hidden;
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

        private void EditCashAccount_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lvCashAccountList.SelectedIndex > -1)
                {
                    ECashDepositPayment objEcash = lvCashAccountList.SelectedItem as ECashDepositPayment;
                    txtCashAccountCode.Text = objEcash.OthersAccountCode;
                    txtCashDepositAmount.Text = objEcash.DepositPaymentAmount.ToString();
                    indexOfSelectedItemCash = lvCashAccountList.SelectedIndex;
                    listBoxCashSuggestion.Visibility = Visibility.Hidden;
                    btnCashAdd.Content = "Modify";
                }
                else
                {
                    MessageBox.Show("Select an Account First.", TRANSACTION_TYPE, MessageBoxButton.OK, MessageBoxImage.Information);
                    lvCashAccountList.Focus();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Problem Occur While Selection.", TRANSACTION_TYPE, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SetSelectedIndexCash(TreeViewItem tr)
        {
            foreach (TreeViewItem trItem in tr.Items)
            {
                if (trItem.Name == "_" + objECA.SubAccId.ToString())
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

        private void RemoveCashAccount_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (lvCashAccountList.SelectedIndex > -1)
                {
                    if (MessageBox.Show("Are you sure want to Remove the Record?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        if (lvCashAccountList.SelectedIndex == indexOfSelectedItemCash)
                        {
                            MessageBox.Show("This Record is Running with Modify mode So You can't Remove This.", TRANSACTION_TYPE, MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        else
                        {
                            lvCashAccountList.Items.Remove(lvCashAccountList.SelectedItem);
                            ItemTotalCalculationCash();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Select an Account First.", TRANSACTION_TYPE, MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Problem Occur While Deletion.", TRANSACTION_TYPE, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void txtCashDepositAmount_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Validations.IsDecimal(e.Text);
            base.OnPreviewTextInput(e);
        }

        private void trvCashOthersAccount_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                var trvItem = (sender as TreeView).SelectedItem as TreeViewItem;
                if (trvItem != null && (trvItem.HasItems == false))
                {
                    objECA = new EChartOfAccount();

                    txtBoxCashOthersAccountName.Text = trvItem.Header.ToString();
                    string[] splitedId = (trvItem.Name.ToString()).Split('_');
                    objECA = objBChartOfAccount.GetAccountInfo(Convert.ToInt64(splitedId[1]));
                    txtCashAccountCode.Text = objECA.SubAccCode;
                    txtCashBalance.Text = objBJournalVouchar.GetCurrentBalanceOfAccount(objECA.SubAccId).ToString();
                    txtCashMotherAccountName.Text = (trvItem.Parent as TreeViewItem).Header.ToString();
                    listBoxCashSuggestion.Visibility = Visibility.Hidden;
                    trvCashOthersAccount.Visibility = Visibility.Hidden;
                }
                else if (trvItem != null && (trvItem.HasItems == true))
                {
                    MessageBox.Show("You can't make transaction with this account.\n Please use its child account", TRANSACTION_TYPE, MessageBoxButton.OK, MessageBoxImage.Information);
                    objECA = new EChartOfAccount();
                    txtCashAccountCode.Text = string.Empty;
                    txtBoxCashOthersAccountName.Text = string.Empty;
                    txtCashMotherAccountName.Text = string.Empty;
                    txtCashBalance.Text = string.Empty;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Problem Occur While Selection.", TRANSACTION_TYPE, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCashOthersAccount_Click(object sender, RoutedEventArgs e)
        {
            if (trvCashOthersAccount.IsVisible == false)
            {
                trvCashOthersAccount.Visibility = Visibility.Visible;
            }
            else
            {
                trvCashOthersAccount.Visibility = Visibility.Hidden;
            }
        }

        private void btnCashAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CheckFieldforOthersAccountCash())
                {
                    ECashDepositPayment objECashDepositPayment = new ECashDepositPayment();
                    objECashDepositPayment.OthersAccountId = objECA.SubAccId;
                    objECashDepositPayment.OthersAccountCode = objECA.SubAccCode;
                    objECashDepositPayment.RootAccName = objECA.RootAccName;
                    objECashDepositPayment.OthersAccountName = objECA.SubAccName;
                    objECashDepositPayment.DepositPaymentAmount = Convert.ToDecimal(txtCashDepositAmount.Text.Trim());
                    if (btnCashAdd.Content.ToString() == "Add")
                    {
                        lvCashAccountList.Items.Add(objECashDepositPayment);
                    }
                    else
                    {
                        lvCashAccountList.Items.RemoveAt(indexOfSelectedItemCash);
                        lvCashAccountList.Items.Insert(indexOfSelectedItemCash, objECashDepositPayment);
                    }
                    ItemTotalCalculationCash();
                    ClearOthersAccountInfoCash();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Problem Occur.", TRANSACTION_TYPE, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void ItemTotalCalculationCash()
        {
            decimal total = 0;
            for (int i = 0; i < lvCashAccountList.Items.Count; i++)
            {
                total += (lvCashAccountList.Items[i] as ECashDepositPayment).DepositPaymentAmount;
            }
            txtCashTotalDepositAmount.Text = total.ToString();
        }
        private bool CheckFieldforOthersAccountCash()
        {
            if (objECA.SubAccId == 0)
            {
                MessageBox.Show("Please Select Any Account.", TRANSACTION_TYPE, MessageBoxButton.OK, MessageBoxImage.Information);
                trvCashOthersAccount.Focus();
                return false;
            }
            if (txtCashAccountCode.Text == string.Empty)
            {
                MessageBox.Show("Please Enter Account Code.", TRANSACTION_TYPE, MessageBoxButton.OK, MessageBoxImage.Information);
                txtCashAccountCode.Focus();
                return false;
            }
            if (txtCashDepositAmount.Text == string.Empty)
            {
                MessageBox.Show("Please Enter Amount.", TRANSACTION_TYPE, MessageBoxButton.OK, MessageBoxImage.Information);
                txtCashDepositAmount.Focus();
                return false;
            }

            return true;
        }

        private void btnCashClear_Click(object sender, RoutedEventArgs e)
        {
            ClearOthersAccountInfoCash();
        }

        private void ClearOthersAccountInfoCash()
        {
            objECA = new EChartOfAccount();
            indexOfSelectedItemCash = -1;
            txtCashAccountCode.Text = string.Empty;
            txtBoxCashOthersAccountName.Text = string.Empty;
            txtCashBalance.Text = string.Empty;
            txtCashDepositAmount.Text = string.Empty;
            txtCashMotherAccountName.Text = string.Empty;
            LoadOthersAccountTypeTreeCash();
            btnCashAdd.Content = "Add";
        }

        private void txtCashAccountCode_TextChanged(object sender, TextChangedEventArgs e)
        {
           
        }
        private void txtCashAccountCode_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    if (txtCashAccountCode.Text != string.Empty)
                    {
                        List<EChartOfAccount> listCA = objBJournalVouchar.GetAccountInfoCodeWise(txtCashAccountCode.Text.Trim());
                        foreach (var obj in listCA)
                        {
                            objECA = obj;
                        }
                        if (listCA.Count > 0)
                        {
                            foreach (TreeViewItem tree in trvCashOthersAccount.Items)
                            {
                                if (tree.Header.ToString() == objECA.RootAccName)
                                {
                                    tree.IsExpanded = true;
                                    SetSelectedIndexCash(tree);
                                    break;
                                }
                            }
                        }
                        else
                        {
                            LoadOthersAccountTypeTreeCash();
                            objECA = new EChartOfAccount();
                            txtCashBalance.Text = string.Empty;
                            txtBoxCashOthersAccountName.Text = string.Empty;
                            txtCashMotherAccountName.Text = string.Empty;
                        }
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Problem Occur While Searching.", TRANSACTION_TYPE, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void txtBoxCashOthersAccountName_TextChanged(object sender, TextChangedEventArgs e)
        {
            listBoxCashSuggestion.Items.Clear();
            if (txtBoxCashOthersAccountName.Text != "")
            {
                List<EChartOfAccount> namelist = objBJournalVouchar.GetAccountInfoLikeNameWise(txtBoxCashOthersAccountName.Text);
                if (namelist.Count > 0)
                {
                    listBoxCashSuggestion.Visibility = Visibility.Visible;
                    foreach (var obj in namelist)
                    {
                        listBoxCashSuggestion.Items.Add(obj);
                    }
                }
                else
                {
                    listBoxCashSuggestion.Visibility = Visibility.Hidden;
                }
            }
            else
            {
                listBoxCashSuggestion.Visibility = Visibility.Hidden;
            }
        }

        private void txtBoxCashOthersAccountName_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Down)
            {
                listBoxCashSuggestion.Focus();
            }
        }

        private void listBoxCashSuggestion_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (listBoxCashSuggestion.SelectedIndex == 0 && e.Key == Key.Up)
            {
                txtBoxCashOthersAccountName.Focus();
            }
        }

        private void listBoxCashSuggestion_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (listBoxCashSuggestion.SelectedIndex > -1)
                {
                    if (e.Key == Key.Enter)
                    {
                        EChartOfAccount objSelectedCa = listBoxCashSuggestion.SelectedItem as EChartOfAccount;
                        txtBoxCashOthersAccountName.Text = objSelectedCa.SubAccName;
                        listBoxCashSuggestion.Visibility = Visibility.Hidden;
                        txtCashAccountCode.Text = objSelectedCa.SubAccCode;
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void btnCashSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CheckfieldCash())
                {
                    if (btnCashSave.Content.ToString() == "Save")
                    {
                        GetVoucharLastId();
                        SaveDepositInfoCash();
                    }
                    else if (btnCashSave.Content.ToString() == "Update")
                    {
                        objBJournalVouchar.DeleteJournalVouchar(txtCashVouchar.Text.Trim());
                        SaveDepositInfoCash();
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Problem Occur While Storing Information.", TRANSACTION_TYPE, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool CheckfieldCash()
        {
            if (txtCashVouchar.Text == string.Empty)
            {
                MessageBox.Show("Vouchar Should not be Blank.", TRANSACTION_TYPE, MessageBoxButton.OK, MessageBoxImage.Information);
                txtCashVouchar.Focus();
                return false;
            }
            if (dtpCashDeposit.Text == string.Empty)
            {
                MessageBox.Show("Please Select Date.", TRANSACTION_TYPE, MessageBoxButton.OK, MessageBoxImage.Information);
                dtpCashDeposit.Focus();
                return false;
            }
            if (txtBlockCashAccountName.Text == string.Empty)
            {
                MessageBox.Show("Please Select Bank Account Name.", TRANSACTION_TYPE, MessageBoxButton.OK, MessageBoxImage.Information);
                btnCashBankAccount.Focus();
                return false;
            }
            if (txtCashDescription.Text == string.Empty)
            {
                MessageBox.Show("Please Enter Description.", TRANSACTION_TYPE, MessageBoxButton.OK, MessageBoxImage.Information);
                txtCashDescription.Focus();
                return false;
            }
            if (lvCashAccountList.Items.Count == 0)
            {
                MessageBox.Show("Please Fill-up Account List.", TRANSACTION_TYPE, MessageBoxButton.OK, MessageBoxImage.Information);
                lvCashAccountList.Focus();
                return false;
            }
            return true;
        }

        private void SaveDepositInfoCash()
        {
            decimal totalDeposit = 0;
            for (int i = 0; i < lvCashAccountList.Items.Count; i++)
            {
                totalDeposit += (lvCashAccountList.Items[i] as ECashDepositPayment).DepositPaymentAmount;
            }
            ProcessJournalforDepositCash(totalDeposit, txtCashVouchar.Text.Trim());
            MessageBox.Show(TRANSACTION_TYPE + " Success.", TRANSACTION_TYPE, MessageBoxButton.OK, MessageBoxImage.Information);
            ResetFieldCash();
        }

        private void ProcessJournalforDepositCash(decimal totalDeposit, string JVId)
        {
            //Debit
            EJournalVouchar objDebitJV = new EJournalVouchar();
            objDebitJV.Journal_Id = JVId;
            objDebitJV.Journal_Acc_Id = parentIdCash;

            EChartOfAccount objCA = objBChartOfAccount.GetAccountInfo(objDebitJV.Journal_Acc_Id);/*Get Account info.*/

            objDebitJV.Journal_Acc_Name = objCA.SubAccName;
            objDebitJV.Journal_Acc_Code = objCA.SubAccCode;
            objDebitJV.Journal_Notes = txtCashDescription.Text.Trim();
            objDebitJV.Journal_Date = Convert.ToDateTime(dtpCashDeposit.SelectedDate);
            objDebitJV.Journal_Debit_Amount = totalDeposit;
            objDebitJV.Jounal_Credit_Amount = 0;
            objDebitJV.Journal_BankRemarks = "Cash Deposit";
            objDebitJV.Access_By = loginUser;
            objDebitJV.Access_Date = DateTime.Now;
            objBJournalVouchar.SaveJournalVouchar(objDebitJV);

            //Credit
            for (int i = 0; i < lvCashAccountList.Items.Count; i++)
            {
                ECashDepositPayment objEDeposit = lvCashAccountList.Items[i] as ECashDepositPayment;

                EJournalVouchar objCreditJV = new EJournalVouchar();
                objCreditJV.Journal_Id = JVId;
                objCreditJV.Journal_Acc_Id = objEDeposit.OthersAccountId;
                objCreditJV.Journal_Acc_Name = objEDeposit.OthersAccountName;
                objCreditJV.Journal_Acc_Code = objEDeposit.OthersAccountCode;
                objCreditJV.Journal_Notes = txtCashDescription.Text.Trim();
                objCreditJV.Journal_Date = Convert.ToDateTime(dtpCashDeposit.SelectedDate);
                objCreditJV.Journal_Debit_Amount = 0;
                objCreditJV.Jounal_Credit_Amount = objEDeposit.DepositPaymentAmount;
                objCreditJV.Journal_BankRemarks = "Cash Deposit";
                objCreditJV.Access_By = loginUser;
                objCreditJV.Access_Date = DateTime.Now;
                objBJournalVouchar.SaveJournalVouchar(objCreditJV);
            }
        }

        private void btnCashReset_Click(object sender, RoutedEventArgs e)
        {
            ResetFieldCash();
        }

        private void ResetFieldCash()
        {
            GetVoucharLastId();
            parentIdCash = 0;
            dtpCashDeposit.SelectedDate = DateTime.Now;
            txtCashDescription.Text = string.Empty;
            txtBlockCashAccountName.Text = string.Empty;
            LoadBankAccountTypeTreeCash();
            lvCashAccountList.Items.Clear();
            txtCashTotalDepositAmount.Text = string.Empty;
            ClearOthersAccountInfoCash();
            btnCashSave.Content = "Save";
        }

        private void GetVoucharLastId()
        {
            try
            {
                txtCashVouchar.Text = objBJournalVouchar.GetJVoucherLastID();
            }
            catch (Exception)
            {
            }
        }

        private void btnCashCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ResetFieldforLostFocusCash()
        {
            parentIdCash = 0;
            dtpCashDeposit.SelectedDate = DateTime.Now;
            txtCashDescription.Text = string.Empty;
            txtBlockCashAccountName.Text = string.Empty;
            LoadBankAccountTypeTreeCash();
            txtCashTotalDepositAmount.Text = string.Empty;
            lvCashAccountList.Items.Clear();
            ClearOthersAccountInfoCash();
            btnCashSave.Content = "Save";
        }

        private void txtCashVouchar_LostFocus(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtCashVouchar.Text != string.Empty)
                {
                    string remark = "Cash Deposit";

                    ResetFieldforLostFocusCash();
                    if (objBJournalVouchar.DoesExistVoucharforCashDepojitPayment(txtCashVouchar.Text.Trim(), remark))
                    {
                        FillUpAllFieldCash();
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Problem Occur.", TRANSACTION_TYPE, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void FillUpAllFieldCash()
        {
            foreach (EJournalVouchar objEJV in objBJournalVouchar.GetAllJournalVoucharIdWise(txtCashVouchar.Text.Trim()))
            {
                if (objEJV.Jounal_Credit_Amount == 0)
                {
                    parentIdCash = objEJV.Journal_Acc_Id;
                    txtBlockCashAccountName.Text = objEJV.Journal_Acc_Name;
                    dtpCashDeposit.SelectedDate = objEJV.Journal_Date;
                    txtCashDescription.Text = objEJV.Journal_Notes;
                }
                else
                {
                    ECashDepositPayment objECashDepositPayment = new ECashDepositPayment();
                    objECashDepositPayment.OthersAccountId = objEJV.Journal_Acc_Id;
                    objECashDepositPayment.OthersAccountCode = objEJV.Journal_Acc_Code;
                    objECashDepositPayment.OthersAccountName = objEJV.Journal_Acc_Name;
                    if (objEJV.Journal_Debit_Amount > objEJV.Jounal_Credit_Amount)
                    {
                        objECashDepositPayment.DepositPaymentAmount = objEJV.Journal_Debit_Amount;
                    }
                    else
                    {
                        objECashDepositPayment.DepositPaymentAmount = objEJV.Jounal_Credit_Amount;
                    }
                    lvCashAccountList.Items.Add(objECashDepositPayment);
                }
            }
            foreach (TreeViewItem tree in trvCashBankAccountType.Items)
            {
                tree.IsExpanded = true;
                SetSelectedIndexforBankCash(tree);
            }
            ItemTotalCalculationCash();
            btnCashSave.Content = "Update";
        }

        private void SetSelectedIndexforBankCash(TreeViewItem tr)
        {
            foreach (TreeViewItem trItem in tr.Items)
            {
                if (trItem.Name == "_" + parentIdCash)
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
                        SetSelectedIndexforBankCash(trItem);
                    }
                }
            }
        }

        private void tabItemCash_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            trvCashBankAccountType.Visibility = Visibility.Hidden;
            trvCashOthersAccount.Visibility = Visibility.Hidden;
        }
        /**********************************************************************************************************************************/
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

        private void tabControlDeposit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (e.Source is TabControl)
                {
                    if (tabItemCheque.IsSelected)
                    {
                        if (btnChkSave.Content.ToString() == "Save")
                        {
                            txtChkVouchar.Text = objBJournalVouchar.GetJVoucherLastID();
                        }
                    }
                    else if (tabItemCash.IsSelected)
                    {
                        if (btnCashSave.Content.ToString() == "Save")
                        {
                            txtCashVouchar.Text = objBJournalVouchar.GetJVoucherLastID();
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void Window_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            trvChkBankAccountType.Visibility = Visibility.Hidden;
            trvChkAccount.Visibility = Visibility.Hidden;

            trvCashBankAccountType.Visibility = Visibility.Hidden;
            trvCashOthersAccount.Visibility = Visibility.Hidden;
        }
    }
}
