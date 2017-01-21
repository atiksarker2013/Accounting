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
//Author:REFAT
namespace CHART_ACC_UI.UI
{
    /// <summary>
    /// Interaction logic for OutwardClearing.xaml
    /// </summary>
    public partial class Clearing : Window
    {
        BDeposit_Payment objBDepositPayment = new BDeposit_Payment();
        BJournalVouchar objBJournalVouchar = new BJournalVouchar();
        string TRANSACTION_TYPE = string.Empty;
        string CLEARING_METHOD = string.Empty;
        public Clearing()
        {
            InitializeComponent();            
            rdbClear.IsChecked = true;
            dtpClearingDate.SelectedDate = DateTime.Now;
        }
        public Clearing(string method):this()
        {
            CLEARING_METHOD = method;
            this.Title = CLEARING_METHOD;
            if (CLEARING_METHOD == "Inward Clearing")
            {
                TRANSACTION_TYPE = "Deposit";
            }
            else
            {
                TRANSACTION_TYPE = "Payment";
            }
            LoadDepositList();
        }
        private void LoadDepositList()
        {
            try
            {
                List<EDeposit_Payment> listDP = new List<EDeposit_Payment>();

                if (TRANSACTION_TYPE == "Payment")
                {
                    foreach (var item in objBDepositPayment.GetAllPendingDepositPayment(TRANSACTION_TYPE))
                    {
                        listDP.Add(item);
                    }
                    foreach (var item in objBDepositPayment.GetAllPendingDepositPayment("Contra"))
                    {
                        listDP.Add(item);
                    }
                }
                else
                {
                    listDP = objBDepositPayment.GetAllPendingDepositPayment(TRANSACTION_TYPE);
                }
                dgvDeposit.ItemsSource = listDP;
            }
            catch (Exception)
            {
                MessageBox.Show("Problem Occur While Loading Information.", CLEARING_METHOD, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            ResetField();
        }

        private void ResetField()
        {
            LoadDepositList();                     
            txtNotes.Text = string.Empty;
            rdbClear.IsChecked = true;
            dtpClearingDate.SelectedDate = DateTime.Now;
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CheckField())
                {
                    ProcessDeposit();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Process Failed", CLEARING_METHOD, MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private bool CheckField()
        {
            bool IsCheck = false;
            foreach (EDeposit_Payment objEdep in dgvDeposit.Items)
            {
                if (objEdep.Ischeck == true)
                {
                    IsCheck = true;
                    break;
                }
            }
            if (IsCheck == false)
            {
                MessageBox.Show("Please Select Any Record.", CLEARING_METHOD, MessageBoxButton.OK, MessageBoxImage.Information);
                dgvDeposit.Focus();
                return false;
            }
            if (dtpClearingDate.Text == string.Empty)
            {
                MessageBox.Show("Please Select Clearing Date.", CLEARING_METHOD, MessageBoxButton.OK, MessageBoxImage.Information);
                dtpClearingDate.Focus();
                return false;
            }
            if (Convert.ToBoolean(rdbCancel.IsChecked)==true)
            {
                if (txtNotes.Text == string.Empty)
                {
                    MessageBox.Show("Please Enter Notes.", CLEARING_METHOD, MessageBoxButton.OK, MessageBoxImage.Information);
                    txtNotes.Focus();
                    return false;
                }
            }
            return true;
        }

        private void ProcessDeposit()
        {
            bool stat = false;
            foreach (EDeposit_Payment objEDP in dgvDeposit.Items)
            {
                if (objEDP.Ischeck == true)
                {
                    if (rdbClear.IsChecked == true)
                    {
                        objEDP.Status = "Clear";
                        objEDP.ClearingDate = Convert.ToDateTime(dtpClearingDate.SelectedDate);                        
                        stat=objBDepositPayment.ClearingDepositPayment(objEDP);
                    }
                    else if (rdbCancel.IsChecked == true)
                    {
                        string JVId = objBJournalVouchar.GetJVoucherLastID();
                        objEDP.Status = "Cancel";
                        objEDP.ClearingDate = Convert.ToDateTime(dtpClearingDate.SelectedDate);
                        objEDP.CancellingJV = JVId;
                        if (objBDepositPayment.ClearingDepositPayment(objEDP))
                        {
                            if (TRANSACTION_TYPE == "Deposit")
                            {
                                stat = ProcessJournalforDeposit(objEDP, JVId);//Inward Clearing
                            }
                            else
                            {
                                stat = ProcessJournalforPayment(objEDP, JVId);//Outward Clearing
                            }
                        }
                    }
                }
            }
            if (stat)
            {
                MessageBox.Show("Process Completed", CLEARING_METHOD, MessageBoxButton.OK, MessageBoxImage.Information);
             ResetField();
            }
        }

        private bool ProcessJournalforDeposit(EDeposit_Payment objED, string JVId)
        {
            bool stat = false;

            //Credit
            EJournalVouchar objFrstJV = new EJournalVouchar();
            objFrstJV.Journal_Id = JVId;
            objFrstJV.Journal_Acc_Id = objED.BankAccountId;

            EChartOfAccount objCA = objBDepositPayment.GetAccountInfo(objFrstJV.Journal_Acc_Id);
            objFrstJV.Journal_Acc_Name = objCA.SubAccName;
            objFrstJV.Journal_Acc_Code = objCA.SubAccCode;
            objFrstJV.Journal_Notes = txtNotes.Text.Trim();
            objFrstJV.Journal_Date = Convert.ToDateTime(objED.ClearingDate);
            objFrstJV.Journal_Debit_Amount = 0;
            objFrstJV.Jounal_Credit_Amount = objED.DepositPaymentAmount;
            objFrstJV.Journal_BankRemarks = "Cheque Return";
            objFrstJV.Access_By = objED.AccessBy;
            objFrstJV.Access_Date = DateTime.Now;
            stat=objBJournalVouchar.SaveJournalVouchar(objFrstJV);

            //Debit
            EJournalVouchar objScndJV = new EJournalVouchar();
            objScndJV.Journal_Id = JVId;
            objScndJV.Journal_Acc_Id = objED.AccountID;

            objCA = objBDepositPayment.GetAccountInfo(objScndJV.Journal_Acc_Id);/*Get Account info.*/

            objScndJV.Journal_Acc_Name = objCA.SubAccName;
            objScndJV.Journal_Acc_Code = objCA.SubAccCode;
            objScndJV.Journal_Notes = txtNotes.Text.Trim();
            objScndJV.Journal_Date = Convert.ToDateTime(objED.ClearingDate);
            objScndJV.Journal_Debit_Amount = objED.DepositPaymentAmount;
            objScndJV.Jounal_Credit_Amount = 0;
            objScndJV.Journal_Cheque = objED.BankCheque;
            objScndJV.Journal_ChequeDate = Convert.ToDateTime(objED.BankChequeDate);
            objScndJV.Journal_BankRemarks="Cheque Return";
            objScndJV.Access_By = objED.AccessBy;
            objScndJV.Access_Date = DateTime.Now;
            stat=objBDepositPayment.SaveJournalVouchar(objScndJV);
            return stat;
        }

        private bool ProcessJournalforPayment(EDeposit_Payment objED, string JVId)
        {
            bool stat = false;            
            //Debit
            EJournalVouchar objFrstJV = new EJournalVouchar();
            objFrstJV.Journal_Id = JVId;
            objFrstJV.Journal_Acc_Id = objED.BankAccountId;

            EChartOfAccount objCA = objBDepositPayment.GetAccountInfo(objFrstJV.Journal_Acc_Id);
            objFrstJV.Journal_Acc_Name = objCA.SubAccName;
            objFrstJV.Journal_Acc_Code = objCA.SubAccCode;
            objFrstJV.Journal_Notes = txtNotes.Text.Trim();
            objFrstJV.Journal_Date = Convert.ToDateTime(objED.ClearingDate);
            objFrstJV.Journal_Debit_Amount = objED.DepositPaymentAmount;
            objFrstJV.Jounal_Credit_Amount = 0;
            objFrstJV.Journal_BankRemarks = "Cheque Return";
            objFrstJV.Access_By = objED.AccessBy;
            objFrstJV.Access_Date = DateTime.Now;
            stat = objBJournalVouchar.SaveJournalVouchar(objFrstJV);

            //Debit
            EJournalVouchar objScndJV = new EJournalVouchar();
            objScndJV.Journal_Id = JVId;
            objScndJV.Journal_Acc_Id = objED.AccountID;

            objCA = objBDepositPayment.GetAccountInfo(objScndJV.Journal_Acc_Id);/*Get Account info.*/

            objScndJV.Journal_Acc_Name = objCA.SubAccName;
            objScndJV.Journal_Acc_Code = objCA.SubAccCode;
            objScndJV.Journal_Notes = txtNotes.Text.Trim();
            objScndJV.Journal_Date = Convert.ToDateTime(objED.ClearingDate);
            objScndJV.Journal_Debit_Amount = 0;
            objScndJV.Jounal_Credit_Amount = objED.DepositPaymentAmount;
            objScndJV.Journal_Cheque = objED.BankCheque;
            objScndJV.Journal_ChequeDate = Convert.ToDateTime(objED.BankChequeDate);
            objScndJV.Journal_BankRemarks = "Cheque Return";
            objScndJV.Access_By = objED.AccessBy;
            objScndJV.Access_Date = DateTime.Now;
            stat = objBDepositPayment.SaveJournalVouchar(objScndJV);
            return stat;
        }

        private void rdbClear_Checked(object sender, RoutedEventArgs e)
        {
            txtNotes.IsEnabled = false;
            txtNotes.Text = string.Empty;
        }

        private void rdbCancel_Checked(object sender, RoutedEventArgs e)
        {
            txtNotes.IsEnabled = true;
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
