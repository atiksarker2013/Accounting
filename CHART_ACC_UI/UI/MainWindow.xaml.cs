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
using System.Windows.Navigation;
using System.Windows.Shapes;
using CHART_ACC_UI.UI;
using System.Globalization;
using System.Threading;
using CHART_ACC_REPORT.UI;
using CHART_ACC_BLL;

namespace CHART_ACC_UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BMenuPermission objBMenuPermission = new BMenuPermission();
        public MainWindow()
        {
            InitializeComponent();
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            // FOR CHANGE DATE FORMAT [AMIN]
            CultureInfo ci = new CultureInfo(Thread.CurrentThread.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";
            ci.DateTimeFormat.DateSeparator = "/";
            Thread.CurrentThread.CurrentCulture = ci;
        }

        public MainWindow(long UserGroupId):this()
        {
            LoadMenuIntoTree(UserGroupId);
        }
        private void LoadMenuIntoTree(long UserGroupId)
        {
            try
            {
                foreach (MenuItem it in mnuMain.Items)
                {
                    if (objBMenuPermission.IsExistParentMenu(it.Name, UserGroupId))
                    {
                        ProcessTree(it, UserGroupId);
                    }
                    else
                    {
                        it.Visibility = Visibility.Collapsed;
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Problem Occur While Loading Available Menu", "Security", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void ProcessTree(MenuItem it,long userGroupId)
        {
            foreach (MenuItem obj in it.Items)
            {
                if (objBMenuPermission.IsExistChildMenu(obj.Name, userGroupId))
                {
                    ProcessTree(obj, userGroupId);
                }
                else
                {
                    obj.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void ShowWindow(string className)
        {
            bool isOpen = false;
            Window objWindowName = new Window();
            try
            {
                foreach (Window objWindow in Application.Current.Windows)
                {
                    string[] splitedNamespace = (objWindow.ToString()).Split('.');
                    string aClassNameFromCollection = splitedNamespace[splitedNamespace.Length - 1];

                    if (aClassNameFromCollection == className)
                    {
                        isOpen = true;
                        objWindowName = objWindow;
                        break;
                    }
                }

                if (isOpen == false)
                {
                    #region SHOW DESIRED WINDOW

                    switch (className)
                    {
                        case "CreateAcount":
                            CreateAcount objCreateAcount = new CreateAcount();
                            objCreateAcount.Owner = this;
                            objCreateAcount.Show();
                            break;
                        case "ChartOfAccount":
                            ChartOfAccount objChartofAccount = new ChartOfAccount();
                            objChartofAccount.Owner = this;
                            objChartofAccount.Show();
                            break;
                        case "ChartOfAccountModify":
                            ChartOfAccountModify objChartAccModify = new ChartOfAccountModify();
                            objChartAccModify.Owner = this;
                            objChartAccModify.Show();
                            break;
                        case "CompanySetup":
                            CompanySetup objCompany = new CompanySetup();
                            objCompany.Owner = this;
                            objCompany.Show();
                            break;
                        case "Customer_EmployeeSetup":
                            Customer_EmployeeSetup objCE = new Customer_EmployeeSetup();
                            objCE.Owner = this;
                            objCE.Show();
                            break;
                        case "CustomerEmployeeModify":
                            CustomerEmployeeModify objCEModify = new CustomerEmployeeModify();
                            objCEModify.Owner = this;
                            objCEModify.Show();
                            break;
                        case "JournalVoucharEntry":
                            JournalVoucharEntry objJournalEntry = new JournalVoucharEntry();
                            objJournalEntry.Owner = this;
                            objJournalEntry.Show();
                            break;
                        case "FiscalYear":
                            FiscalYear objFiscalYear = new FiscalYear();
                            objFiscalYear.Owner = this;
                            objFiscalYear.Show();
                            break;
                        case "GL":
                            GL objGL = new GL();
                            objGL.Owner = this;
                            objGL.Show();
                            break;
                        case "TrialBalance":
                            TrialBalance objTrialBalance = new TrialBalance();
                            objTrialBalance.Owner = this;
                            objTrialBalance.Show();
                            break;
                        case "Deposit":
                            Deposit objDeposit = new Deposit();
                            objDeposit.Owner = this;
                            objDeposit.Show();
                            break;
                        case "Payment":
                            Payment objPayment = new Payment();
                            objPayment.Owner = this;
                            objPayment.Show();
                            break;
                        case "DayBook":
                            DayBook objDayBook = new DayBook();
                            objDayBook.Owner = this;
                            objDayBook.Show();
                            break;
                        case "PopUpClearing":
                            PopUpClearing objPopUpClearing = new PopUpClearing();
                            objPopUpClearing.Owner = this;
                            objPopUpClearing.Show();
                            break;
                        case "GLHeadWiseStatement":
                            GLHeadWiseStatement objGLHeadWiseStatement = new GLHeadWiseStatement();
                            objGLHeadWiseStatement.Owner = this;
                            objGLHeadWiseStatement.Show();
                            break;
                        case "IncomeStatement":
                            IncomeStatement objIncomeStatement = new IncomeStatement();
                            objIncomeStatement.Owner = this;
                            objIncomeStatement.Show();
                            break;
                        case "BalanceSheet":
                            BalanceSheet objBalanceSheet = new BalanceSheet();
                            objBalanceSheet.Owner = this;
                            objBalanceSheet.Show();
                            break;
                        case "DepriciationCalculator":
                            DepriciationCalculator objDepriciation = new DepriciationCalculator();
                            objDepriciation.Owner = this;
                            objDepriciation.Show();
                            break;
                        case "DepriciationSetup":
                            DepriciationSetup objDepriciationSetup = new DepriciationSetup();
                            objDepriciationSetup.Owner = this;
                            objDepriciationSetup.Show();
                            break;
                        case "HeaderAccountStatement":
                            HeaderAccountStatement objHeaderAccount = new HeaderAccountStatement();
                            objHeaderAccount.Owner = this;
                            objHeaderAccount.Show();
                            break;
                        case "Depriciation":
                            Depriciation objDepriciationReport = new Depriciation();
                            objDepriciationReport.Owner = this;
                            objDepriciationReport.Show();
                            break;
                        case "BankReconciliation":
                            BankReconciliation objBankReconciliation = new BankReconciliation();
                            objBankReconciliation.Owner = this;
                            objBankReconciliation.Show();
                            break;
                        case "VoucherReport":
                            VoucherReport objVoucherReport = new VoucherReport();
                            objVoucherReport.Owner = this;
                            objVoucherReport.Show();
                            break;
                        case "ContraVoucher":
                            ContraVoucher objContra = new ContraVoucher();
                            objContra.Owner = this;
                            objContra.Show();
                            break;
                        case "Receive_Payment":
                            Receive_Payment objReceive_Payment = new Receive_Payment();
                            objReceive_Payment.Owner = this;
                            objReceive_Payment.Show();
                            break;
                        case "Cash_BankBook":
                            Cash_BankBook objCash_BankBook = new Cash_BankBook();
                            objCash_BankBook.Owner = this;
                            objCash_BankBook.Show();
                            break;
                        case "LedgerReport":
                            LedgerReport objLedgerReport = new LedgerReport();
                            objLedgerReport.Owner = this;
                            objLedgerReport.Show();
                            break;
                        case "PartyStatement":
                            PartyStatement objPartyStatement = new PartyStatement();
                            objPartyStatement.Owner = this;
                            objPartyStatement.Show();
                            break;
                        case "AccountStatement":
                            AccountStatement objAccountStatement = new AccountStatement();
                            objAccountStatement.Owner = this;
                            objAccountStatement.Show();
                            break;
                        case "PostDatedCheque":
                            PostDatedCheque objPostDatedCheque = new PostDatedCheque();
                            objPostDatedCheque.Owner = this;
                            objPostDatedCheque.Show();
                            break;
                        case "MemberForm":
                            MemberForm objMember = new MemberForm();
                            objMember.Owner = this;
                            objMember.Show();
                            break;
                        case "MemberAccountType":
                            MemberAccountType objMemberAccount = new MemberAccountType();
                            objMemberAccount.Owner = this;
                            objMemberAccount.Show();
                            break;
                        case "MemberAssign":
                            MemberAssign objMemberAssign = new MemberAssign();
                            objMemberAssign.Owner = this;
                            objMemberAssign.Show();
                            break;
                        case "BalanceSheetConfigure":
                            BalanceSheetConfigure objBalanceSheetConfigure = new BalanceSheetConfigure();
                            objBalanceSheetConfigure.Owner = this;
                            objBalanceSheetConfigure.Show();
                            break;
                        case "IncomeSheetConfigure":
                            IncomeSheetConfigure objIncomeSheetConfigure = new IncomeSheetConfigure();
                            objIncomeSheetConfigure.Owner = this;
                            objIncomeSheetConfigure.Show();
                            break;
                        case "MemberReport":
                            MemberReport objMemberReport = new MemberReport();
                            objMemberReport.Owner = this;
                            objMemberReport.Show();
                            break;
                        case "BalanceUpdate":
                            BalanceUpdate objBalanceUpdate = new BalanceUpdate();
                            objBalanceUpdate.Owner = this;
                            objBalanceUpdate.Show();
                            break;
                    }

                    #endregion SHOW DESIRED WINDOW
                }

                if (isOpen)
                {
                    foreach (Window objWindow in Application.Current.Windows)
                    {
                        string[] splitedNamespace = (objWindow.ToString()).Split('.');
                        string aClassNameFromCollection = splitedNamespace[splitedNamespace.Length - 1];

                        if (aClassNameFromCollection == className)
                        {
                            objWindowName.WindowState = WindowState.Normal;
                            objWindowName.Activate();
                            break;
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("Error Occured While Showing Window.\n" + exception.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }        
        
        private void mnuCreateAcount_Click(object sender, RoutedEventArgs e)
        {
            ShowWindow("CreateAcount");            
        }
        
        private void mnuChartofAccount_Click(object sender, RoutedEventArgs e)
        {
            ShowWindow("ChartOfAccount");            
        }

        private void mnuJournalEntry_Click(object sender, RoutedEventArgs e)
        {
            ShowWindow("JournalVoucharEntry");           
        }

        private void mnuCompanySetup_Click(object sender, RoutedEventArgs e)
        {
            ShowWindow("CompanySetup");            
        }

        private void mnuCustomerSetup_Click(object sender, RoutedEventArgs e)
        {
            ShowWindow("Customer_EmployeeSetup");
        }

        private void mnuCustomerSetupModify_Click(object sender, RoutedEventArgs e)
        {
            ShowWindow("CustomerEmployeeModify");
        }
        private void mnuFiscalYear_Click(object sender, RoutedEventArgs e)
        {
            ShowWindow("FiscalYear");
        }

        private void mnuDepriciationCalculation_Click(object sender, RoutedEventArgs e)
        {
            ShowWindow("DepriciationCalculator");
        }

        private void mnuDepriciationSetup_Click(object sender, RoutedEventArgs e)
        {
            ShowWindow("DepriciationSetup");
        }

        private void mnuGeneralLedgerReport_Click(object sender, RoutedEventArgs e)
        {
            ShowWindow("GL");            
        }

        private void mnuTrialBalanceReport_Click(object sender, RoutedEventArgs e)
        {
            ShowWindow("TrialBalance");            
        }

        private void mnuDeposit_Click(object sender, RoutedEventArgs e)
        {
            ShowWindow("Deposit");
        }

        private void mnuPayment_Click(object sender, RoutedEventArgs e)
        {
            ShowWindow("Payment");
        }

        private void mnuClearing_Click(object sender, RoutedEventArgs e)
        {
            ShowWindow("PopUpClearing");           
        }        

        private void mnuDayBookReport_Click(object sender, RoutedEventArgs e)
        {
            ShowWindow("DayBook");            
        }

        private void mnuChartofAccountModify_Click(object sender, RoutedEventArgs e)
        {
            ShowWindow("ChartOfAccountModify");            
        }

        private void mnuGLHeadWiseStatement_Click(object sender, RoutedEventArgs e)
        {
            ShowWindow("GLHeadWiseStatement");
        }

        private void mnuIncomeStatement_Click(object sender, RoutedEventArgs e)
        {
            ShowWindow("IncomeStatement");            
        }

        private void mnuBalanceSheet_Click(object sender, RoutedEventArgs e)
        {
            ShowWindow("BalanceSheet");            
        }

        private void mnuHeaderAccountStatement_Click(object sender, RoutedEventArgs e)
        {
            ShowWindow("HeaderAccountStatement");
        }

        private void mnuDepriciationReport_Click(object sender, RoutedEventArgs e)
        {
            ShowWindow("Depriciation");
        }

        private void mnuBankReconciliation_Click(object sender, RoutedEventArgs e)
        {
            ShowWindow("BankReconciliation");
        }

        private void mnuVoucherReport_Click(object sender, RoutedEventArgs e)
        {
            ShowWindow("VoucherReport");
        }

        private void mnuContra_Click(object sender, RoutedEventArgs e)
        {
            ShowWindow("ContraVoucher");
        }

        private void mnuReceivePaymentReport_Click(object sender, RoutedEventArgs e)
        {
            ShowWindow("Receive_Payment");
        }

        private void mnuCashBAnkBookReport_Click(object sender, RoutedEventArgs e)
        {
            ShowWindow("Cash_BankBook");
        }

        private void mnuLedgerReport_Click(object sender, RoutedEventArgs e)
        {
            ShowWindow("LedgerReport");
        }

        private void mnuPostDatedCheque_Click(object sender, RoutedEventArgs e)
        {
            ShowWindow("PostDatedCheque");
        }
        private void mnuMemberSetup_Click(object sender, RoutedEventArgs e)
        {
            ShowWindow("MemberForm");
        }
        private void mnuMemberAccountType_Click(object sender, RoutedEventArgs e)
        {
            ShowWindow("MemberAccountType");
        }
        private void mnuPartyStatement_Click(object sender, RoutedEventArgs e)
        {
            ShowWindow("PartyStatement");
        }

        private void mnuMemberAssign_Click(object sender, RoutedEventArgs e)
        {
            ShowWindow("MemberAssign");
        }
        private void mnuBalanceSheetConfigure_Click(object sender, RoutedEventArgs e)
        {
            ShowWindow("BalanceSheetConfigure");
        }

        private void mnuAccountStatement_Click(object sender, RoutedEventArgs e)
        {
            ShowWindow("AccountStatement");
        }

        private void mnuIncomeSheetConfigure_Click(object sender, RoutedEventArgs e)
        {
            ShowWindow("IncomeSheetConfigure");
        }
        private void mnuMemberReport_Click(object sender, RoutedEventArgs e)
        {
            ShowWindow("MemberReport");
        }
        private void mnuBalanceUpdate_Click(object sender, RoutedEventArgs e)
        {
            ShowWindow("BalanceUpdate");
        }  
        /**********************************Get All Menu*********************************************/
        public Menu GetAllAccountingMenu()
        {
            return mnuMain;
        }
        /*******************************************************************************************/
        
        private Window[] childWindows;

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            try
            {
                if (WindowState.Minimized == this.WindowState)
                {
                    int numberOfChildWindow = this.OwnedWindows.Count;
                    childWindows = new Window[numberOfChildWindow];
                    for (int count = 0; count < this.OwnedWindows.Count; count++)
                    {
                        childWindows[count] = this.OwnedWindows[count];
                    }
                }

                else if ((WindowState.Maximized == WindowState) || (System.Windows.WindowState.Normal == WindowState))
                {
                    if (childWindows != null)
                    {
                        foreach (Window aChildWindow in childWindows)
                        {
                            aChildWindow.WindowState = WindowState.Normal;
                            aChildWindow.Show();
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("Unknown problem occured while dashboard was changing state.", "Accounting.", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                foreach (Window wnd in this.OwnedWindows)
                {
                    wnd.Close();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Unknown problem occured while closing.", "Accounting", MessageBoxButton.OK, MessageBoxImage.Information);
            }  
        }    
    }
}
