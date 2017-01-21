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
using CHART_ACC_REPORT.BLL;
using CHART_ACC_REPORT.ENTITY;
using CHART_ACC_REPORT.UTILITY;
using CHART_ACC_REPORT.RPT;
using System.Text.RegularExpressions;
//Author:REFAT
namespace CHART_ACC_REPORT.UI
{
    /// <summary>
    /// Interaction logic for Member_CustomerStatement.xaml
    /// </summary>
    public partial class AccountStatement : Window
    {
        BChartAccount objBChartOfAccount = new BChartAccount();
        BAccountStatement objBAccountStatement = new BAccountStatement();
        BMember objBMember = new BMember();
        string caption = "Account Statement";
        public AccountStatement()
        {
            InitializeComponent();
            InitTask();            
        }

        private void InitTask()
        {
            dtpFrom.SelectedDate = DateTime.Now;
            dtpTo.SelectedDate = DateTime.Now;
            PopulateMemberParentAccount();
            rdbSingle.IsChecked = true;
            LoadFiscalInfo();
        }

        private void LoadFiscalInfo()
        {
            try
            {
                EChartOfAccount obj = objBChartOfAccount.GetFisDate();
                txtFiscalStartDate.Text = obj.FiscalStartDate.ToShortDateString();
                txtFiscalEndDate.Text = obj.FisCalEndDate.ToShortDateString();
            }
            catch (Exception)
            {
                MessageBox.Show("Problem occur: LoadFiscalInfo().", "Account Statement", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PopulateMemberParentAccount()
        {
            try
            {
                cmbParentAccountType.ItemsSource = objBMember.GetAllMemberAccountType();
            }
            catch(Exception)
            {
                MessageBox.Show("Problem occur : PopulateMemberParentAccount().", "Account Statement", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnPreview_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (rdbSingle.IsChecked == true)
                {
                    //if (SingleMembeCheckfield())
                    //{
                    //    SingleMemberSearch();
                    //}
                    SingleMemberAccount();
                }
                else if (rdbGroupRange.IsChecked == true)
                {
                    if (GroupMemberCheckField())
                    {
                        GroupMemberSearch();
                    }
                }
                else if (rdbAll.IsChecked == true)
                {
                    if (AlMemberCheckField())
                    {
                        AllMemberSearch();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Following Problem Occur.\n"+ex.Message, caption,MessageBoxButton.OK,MessageBoxImage.Error);
            }
           
        }
        private bool AlMemberCheckField()
        {
            if (cmbParentAccountType.Text == string.Empty)
            {
                MessageBox.Show("Please Select Account Type.", caption, MessageBoxButton.OK, MessageBoxImage.Information);
                cmbParentAccountType.Focus();
                return false;
            }         
            if (rdbSummary.IsChecked == false && rdbDetails.IsChecked == false)
            {
                MessageBox.Show("Please Select Summary or Details.", caption, MessageBoxButton.OK, MessageBoxImage.Information);
                rdbSummary.Focus();
                return false;
            }
            return true;
        }
        private bool GroupMemberCheckField()
        {
            if (cmbParentAccountType.Text == string.Empty)
            {
                MessageBox.Show("Please Select Account Type.", caption, MessageBoxButton.OK, MessageBoxImage.Information);
                cmbParentAccountType.Focus();
                return false;
            }
            if (txtFromAcc.Text == string.Empty && txtToAcc.Text==string.Empty)
            {
                MessageBox.Show("Please Enter Account no.", caption, MessageBoxButton.OK, MessageBoxImage.Information);
                txtFromAcc.Focus();
                return false;
            }
            if (rdbSummary.IsChecked == false && rdbDetails.IsChecked == false)
            {
                MessageBox.Show("Please Select Summary or Details.", caption, MessageBoxButton.OK, MessageBoxImage.Information);
                rdbSummary.Focus();
                return false;
            }
            return true;
        }

        private bool SingleMembeCheckfield()
        {
            if (cmbParentAccountType.Text == string.Empty)
            {
                MessageBox.Show("Please Select Account Type.", caption, MessageBoxButton.OK, MessageBoxImage.Information);
                cmbParentAccountType.Focus();
                return false;
            }
            if (txtSingleAccNo.Text == string.Empty)
            {              
                MessageBox.Show("Please Enter Account no.", caption, MessageBoxButton.OK, MessageBoxImage.Information);
                txtSingleAccNo.Focus();
                return false;
            }
            return true;
        }

        private void GroupMemberSearch()
        {
            if (rdbSummary.IsChecked == true)
            {
                GroupMemberSummary();
            }
            if (rdbDetails.IsChecked == true)
            {
                GroupMemberDetails();
            }
        }

        private void GroupMemberDetails()
        {
            List<EAccountStatement> listECreateAccountStatement = new List<EAccountStatement>();          

            List<EMember> listMA = new List<EMember>();
            foreach (var MA in objBMember.GetAllMember((cmbParentAccountType.SelectedItem as EMember).AccId))
            {
                listMA.Add(MA);
            }
            if (txtFromAcc.Text != string.Empty && txtToAcc.Text != string.Empty)
            {
                for (int i = Convert.ToInt32(txtFromAcc.Text); i <= Convert.ToInt32(txtToAcc.Text); i++)
                {
                    long accId = 0;

                    if (checkExistAccNo(listMA, i, cmbParentAccountType.Text, ref accId))
                    {
                        EChartOfAccount objECA = objBChartOfAccount.GetAccountInfo(accId);
                        foreach (var obj in objBAccountStatement.GetAccStatement(Convert.ToDateTime(dtpFrom.SelectedDate), Convert.ToDateTime(dtpTo.SelectedDate), objECA.SubAccName, accId))
                        {
                            listECreateAccountStatement.Add(obj);
                        }
                        //End Foreach
                    }
                    //End if
                }
                //End for
            }
            //End if
            else if (txtFromAcc.Text == string.Empty && txtToAcc.Text != string.Empty)
            {
                long accId = 0;
                if (checkExistAccNo(listMA, Convert.ToInt32(txtToAcc.Text), cmbParentAccountType.Text, ref accId))
                {
                    EChartOfAccount objECA = objBChartOfAccount.GetAccountInfo(accId);
                    foreach (var obj in objBAccountStatement.GetAccStatement(Convert.ToDateTime(dtpFrom.SelectedDate), Convert.ToDateTime(dtpTo.SelectedDate), objECA.SubAccName, accId))
                    {
                        listECreateAccountStatement.Add(obj);
                    }   
                    //End Foreach
                }
            }
            else if (txtFromAcc.Text != string.Empty && txtToAcc.Text == string.Empty)
            {
                string[] acc = listMA[listMA.Count - 1].AccountNo.Split(cmbParentAccountType.Text.ToCharArray());
                for (int i = Convert.ToInt32(txtFromAcc.Text); i <= Convert.ToInt32(acc[acc.Length - 1]); i++)
                {
                    long accId = 0;
                    if (checkExistAccNo(listMA, i, cmbParentAccountType.Text, ref accId))
                    {
                        EChartOfAccount objECA = objBChartOfAccount.GetAccountInfo(accId);
                        foreach (var obj in objBAccountStatement.GetAccStatement(Convert.ToDateTime(dtpFrom.SelectedDate), Convert.ToDateTime(dtpTo.SelectedDate), objECA.SubAccName, accId))
                        {
                            listECreateAccountStatement.Add(obj);
                        }   
                        //End Foreach
                    }
                    //End if
                }
                //End for
            }

            if (listECreateAccountStatement.Count > 0)
            {
                CrptMemberAccountRange objCrptMemberAccountRange = new CrptMemberAccountRange();
                AccountingRptUtility.SetDate(objCrptMemberAccountRange, Convert.ToDateTime(dtpFrom.SelectedDate).ToShortDateString(), Convert.ToDateTime(dtpTo.SelectedDate).ToShortDateString());
                AccountingRptUtility.setCompanyDeifinition(objCrptMemberAccountRange, caption);
                AccountingRptUtility.Display_report(objCrptMemberAccountRange, listECreateAccountStatement, this);
            }
            else
            {
                MessageBox.Show("No Record Found.", caption, MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void GroupMemberSummary()
        {
            List<EGL> listFinalGl = new List<EGL>();
            EChartOfAccount objECA = objBChartOfAccount.GetAccountInfo((cmbParentAccountType.SelectedItem as EMember).AccId);
            List<EGL> listGL = new BGL().LoadListofGLParticaularIdwise(objECA.SubAccId, objECA.SubAccName, Convert.ToDateTime(dtpFrom.SelectedDate), Convert.ToDateTime(dtpTo.SelectedDate), true);

            List<EMember> listMA = new List<EMember>();
            foreach (var MA in objBMember.GetAllMember((cmbParentAccountType.SelectedItem as EMember).AccId))
            {
                listMA.Add(MA);
            }
            if (txtFromAcc.Text != string.Empty && txtToAcc.Text != string.Empty)
            {
                for (int i = Convert.ToInt32(txtFromAcc.Text); i <= Convert.ToInt32(txtToAcc.Text); i++)
                {
                    long accId = 0;
                    if (checkExistAccNo(listMA, i, cmbParentAccountType.Text, ref accId))
                    {
                        foreach (var gl in listGL)
                        {
                            if (gl.SubAccId == accId)
                            {
                                listFinalGl.Add(gl);
                                break;
                            }
                        }
                        //End Foreach
                    }
                    //End if
                }
                //End for
            }
            //End if
            else if (txtFromAcc.Text == string.Empty && txtToAcc.Text != string.Empty)
            {
                long accId = 0;
                if (checkExistAccNo(listMA, Convert.ToInt32(txtToAcc.Text), cmbParentAccountType.Text, ref accId))
                {
                    foreach (var gl in listGL)
                    {
                        if (gl.SubAccId == accId)
                        {
                            listFinalGl.Add(gl);
                            break;
                        }
                    }
                    //End Foreach
                }
            }
            else if (txtFromAcc.Text != string.Empty && txtToAcc.Text == string.Empty)
            {
                string[] acc = listMA[listMA.Count - 1].AccountNo.Split(cmbParentAccountType.Text.ToCharArray());
                for (int i = Convert.ToInt32(txtFromAcc.Text); i <= Convert.ToInt32(acc[acc.Length - 1]); i++)
                {
                    long accId = 0;
                    if (checkExistAccNo(listMA, i, cmbParentAccountType.Text, ref accId))
                    {
                        foreach (var gl in listGL)
                        {
                            if (gl.SubAccId == accId)
                            {
                                listFinalGl.Add(gl);
                                break;
                            }
                        }
                        //End Foreach
                    }
                    //End if
                }
                //End for
            }
            //This is for if no entry found
            //if (listFinalGl.Count == 0)
            //{
            //    listFinalGl.Add(new EGL { AccTypeId = objECA.SubAccId, AccTypeName = objECA.SubAccName, CurrentBalance = "" });
            //}
            if (listFinalGl.Count > 0)
            {
                CrptGL objCrptGL = new CrptGL();
                AccountingRptUtility.SetDate(objCrptGL, Convert.ToDateTime(dtpFrom.SelectedDate).ToShortDateString(), Convert.ToDateTime(dtpTo.SelectedDate).ToShortDateString());
                AccountingRptUtility.setCompanyDeifinition(objCrptGL, caption);
                AccountingRptUtility.Display_report(objCrptGL, listFinalGl, this);
            }
            else
            {
                MessageBox.Show("No Record Found.",caption,MessageBoxButton.OK,MessageBoxImage.Information);
            }
        }

        private bool checkExistAccNo(List<EMember> listMA,int i,string prefix,ref long accId)
        {
            foreach (var ma in listMA)
            {
                string[] acc = ma.AccountNo.Split(prefix.ToCharArray());
                if (i == Convert.ToInt32(acc[acc.Length - 1]))
                {
                    accId = ma.AccId;
                    return true;
                }                
            }
            return false;
        }

        private void AllMemberSearch()
        {
            if (rdbSummary.IsChecked == true)
            {
                EChartOfAccount objECA = objBChartOfAccount.GetAccountInfo((cmbParentAccountType.SelectedItem as EMember).AccId);
                List<EGL> listGL = new BGL().LoadListofGLParticaularIdwise(objECA.SubAccId, objECA.SubAccName, Convert.ToDateTime(dtpFrom.SelectedDate), Convert.ToDateTime(dtpTo.SelectedDate), true);
                if (listGL.Count > 0)
                {
                    CrptGL objCrptGL = new CrptGL();
                    AccountingRptUtility.SetDate(objCrptGL, Convert.ToDateTime(dtpFrom.SelectedDate).ToShortDateString(), Convert.ToDateTime(dtpTo.SelectedDate).ToShortDateString());
                    AccountingRptUtility.setCompanyDeifinition(objCrptGL, caption);
                    AccountingRptUtility.Display_report(objCrptGL, listGL, this);
                }
                else
                {
                    MessageBox.Show("No Record Found.", caption, MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            if (rdbDetails.IsChecked == true)
            {
                List<EAccountStatement> listECreateAccountStatement = new List<EAccountStatement>();
                foreach (var MA in objBMember.GetAllMember((cmbParentAccountType.SelectedItem as EMember).AccId))
                {
                    foreach (var obj in objBAccountStatement.GetAccStatement(Convert.ToDateTime(dtpFrom.SelectedDate), Convert.ToDateTime(dtpTo.SelectedDate), MA.AccountName, MA.AccId))
                    {
                        listECreateAccountStatement.Add(obj);
                    }
                }
                if (listECreateAccountStatement.Count > 0)
                {
                    CrptMemberAccountRange objCrptMemberAccountRange = new CrptMemberAccountRange();
                    AccountingRptUtility.SetDate(objCrptMemberAccountRange, Convert.ToDateTime(dtpFrom.SelectedDate).ToShortDateString(), Convert.ToDateTime(dtpTo.SelectedDate).ToShortDateString());
                    AccountingRptUtility.setCompanyDeifinition(objCrptMemberAccountRange, caption);
                    AccountingRptUtility.Display_report(objCrptMemberAccountRange, listECreateAccountStatement, this);
                }
                else
                {
                    MessageBox.Show("No Record Found.", caption, MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void SingleMemberSearch()
        {
            if (objBMember.DoesExistMemberAccount(cmbParentAccountType.Text + txtSingleAccNo.Text, (cmbParentAccountType.SelectedItem as EMember).AccId))
            {
                List<EAccountStatement> listECreateAccountStatement = new List<EAccountStatement>();
                EChartOfAccount objECA = objBChartOfAccount.GetAccountInfoCodeWise(cmbParentAccountType.Text + txtSingleAccNo.Text);
                foreach (var obj in objBAccountStatement.GetAccStatement(Convert.ToDateTime(dtpFrom.SelectedDate), Convert.ToDateTime(dtpTo.SelectedDate), objECA.SubAccName, objECA.SubAccId))
                {
                    listECreateAccountStatement.Add(obj);
                }

                if (listECreateAccountStatement.Count > 0)
                {
                    CrptMemberAccountRange objCrptMemberAccountRange = new CrptMemberAccountRange();
                    AccountingRptUtility.SetDate(objCrptMemberAccountRange, Convert.ToDateTime(dtpFrom.SelectedDate).ToShortDateString(), Convert.ToDateTime(dtpTo.SelectedDate).ToShortDateString());
                    AccountingRptUtility.setCompanyDeifinition(objCrptMemberAccountRange, caption);
                    AccountingRptUtility.Display_report(objCrptMemberAccountRange, listECreateAccountStatement, this);
                }
                else
                {
                    MessageBox.Show("No Record Found.", caption, MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("No Record Found.", caption, MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void SingleMemberAccount()
        {
            try
            {
                if (txtSingleAccNo.Text != string.Empty)
                {
                    if (objBChartOfAccount.DoesAccountExist(txtSingleAccNo.Text))
                    {
                        List<EAccountStatement> listECreateAccountStatement = new List<EAccountStatement>();
                        EChartOfAccount objECA = objBChartOfAccount.GetAccountInfoCodeWise(txtSingleAccNo.Text);
                        foreach (var obj in objBAccountStatement.GetAccStatement(Convert.ToDateTime(dtpFrom.SelectedDate), Convert.ToDateTime(dtpTo.SelectedDate), objECA.SubAccName, objECA.SubAccId))
                        {
                            listECreateAccountStatement.Add(obj);
                        }

                        if (listECreateAccountStatement.Count > 0)
                        {
                            CrptMemberAccountRange objCrptMemberAccountRange = new CrptMemberAccountRange();
                            AccountingRptUtility.SetDate(objCrptMemberAccountRange, Convert.ToDateTime(dtpFrom.SelectedDate).ToShortDateString(), Convert.ToDateTime(dtpTo.SelectedDate).ToShortDateString());
                            AccountingRptUtility.setCompanyDeifinition(objCrptMemberAccountRange, caption);
                            AccountingRptUtility.Display_report(objCrptMemberAccountRange, listECreateAccountStatement, this);
                        }
                        else
                        {
                            MessageBox.Show("No Record Found.", caption, MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid account no.", caption, MessageBoxButton.OK, MessageBoxImage.Information);
                        txtSingleAccNo.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Please Enter Account no.", caption, MessageBoxButton.OK, MessageBoxImage.Information);
                    txtSingleAccNo.Focus();
                }
            }
            catch (Exception ex)
            { 
             
            }
        }

        #region BASIC METHOD
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

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        #endregion

        private void rdbSingle_Checked(object sender, RoutedEventArgs e)
        {
            txtSingleAccNo.IsEnabled = true;
            txtFromAcc.Text = string.Empty;
            txtToAcc.Text = string.Empty;
            txtFromAcc.IsEnabled = false;
            txtToAcc.IsEnabled = false;
            rdbDetails.IsChecked = false;
            rdbSummary.IsChecked = false;
            rdbDetails.IsEnabled = false;
            rdbSummary.IsEnabled = false;
        }

        private void rdbGroupRange_Checked(object sender, RoutedEventArgs e)
        {
            txtSingleAccNo.Text = string.Empty;
            txtSingleAccNo.IsEnabled = false;
            txtFromAcc.Text = string.Empty;
            txtToAcc.Text = string.Empty;
            txtFromAcc.IsEnabled = true;
            txtToAcc.IsEnabled = true;
            rdbDetails.IsChecked = false;
            rdbSummary.IsChecked = false;
            rdbDetails.IsEnabled = true;
            rdbSummary.IsEnabled = true;
        }

        private void rdbAll_Checked(object sender, RoutedEventArgs e)
        {
            txtSingleAccNo.Text = string.Empty;
            txtSingleAccNo.IsEnabled = false;
            txtFromAcc.Text = string.Empty;
            txtToAcc.Text = string.Empty;
            txtFromAcc.IsEnabled = false;
            txtToAcc.IsEnabled = false;
            rdbDetails.IsChecked = false;
            rdbSummary.IsChecked = false;
            rdbDetails.IsEnabled = true;
            rdbSummary.IsEnabled = true;
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            InitTask();
        }
        public static bool IsInteger(string strInteger)
        {
            if (strInteger.Length > 18)
            {
                return false;
            }
            Regex obInteger = new Regex(@"^[0-9]*$");
            return obInteger.IsMatch(strInteger);
        }
        private void txtAccNo_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsInteger(e.Text);
            base.OnPreviewTextInput(e);
        }
    }
}
