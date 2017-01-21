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
//Author:REFAT
namespace CHART_ACC_UI.UI
{
    /// <summary>
    /// Interaction logic for MemberAssign.xaml
    /// </summary>
    public partial class MemberAssign : Window
    {
        string caption = "Member Assign";
        long MemberId = 0;
        BMemberAssign objBMemberAssign = new BMemberAssign();
        BChartOfAccount objBChartOfAccount = new BChartOfAccount();
        public MemberAssign()
        {
            InitializeComponent();
            InitialTask();
        }

        private void InitialTask()
        {
            PopulateParentAccountOfMember();
            PopulateAccountType_Status();
        }

        private void PopulateParentAccountOfMember()
        {
            try
            {
                BMemberAccountType objBMemberAccountType = new BMemberAccountType();
                cmbParentAccount.ItemsSource = objBMemberAccountType.GetAllMemberAccountType();
            }
            catch (Exception)
            {             
            }
        }

        private void PopulateAccountType_Status()
        {
            cmbDrCr.Items.Add("DR");
            cmbDrCr.Items.Add("CR");

            cmbStatus.Items.Add("Active");
            cmbStatus.Items.Add("Inactive");
            cmbStatus.SelectedIndex = 0;
        }

        private void btnMemberSearch_Click(object sender, RoutedEventArgs e)
        {
            PopUpMemberList objPopUpMemberList = new PopUpMemberList();
            if (objPopUpMemberList.ShowDialog() == true)
            {
               EMemberSetup objEMemberSetup = objPopUpMemberList.lvMemberInfo.SelectedItem as EMemberSetup;
               MemberId = objEMemberSetup.Id;
               txtMemberNo.Text=objEMemberSetup.Member_No;
               txtMemberName.Text = objEMemberSetup.Member_Full_Name;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CheckField() && CheckCodeExist() && CheckExistMemberAssign())
                {
                    SaveMemberAssign();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Problem Occur While Saving.", caption, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private bool CheckCodeExist()
        {
           bool stat=objBChartOfAccount.DoesCodeExist(cmbParentAccount.Text+txtAccountNo.Text);
           if (stat)
           {
               MessageBox.Show("Account No Already Exist.", caption, MessageBoxButton.OK, MessageBoxImage.Information);
               txtAccountNo.Focus();
               return false;
           }
           return true;
        }
        private bool CheckExistMemberAssign()
        {
            bool stat = objBMemberAssign.DoesExistMemberAssign(MemberId, (cmbParentAccount.SelectedItem as EMemberAccountType).Account_Id);
            if (stat)
            {
                MessageBox.Show("Member Already Assigned to "+cmbParentAccount.Text+".", caption, MessageBoxButton.OK, MessageBoxImage.Information);
                btnMemberSearch.Focus();
                return false;
            }
            return true;
        }
        private void SaveMemberAssign()
        {
            EMemberAssign objEMemberAssign = new EMemberAssign();
            objEMemberAssign.MemberId = MemberId;
            objEMemberAssign.AccountName = txtAccName.Text;
            objEMemberAssign.AccountNo = cmbParentAccount.Text+txtAccountNo.Text;
            objEMemberAssign.ParentAccountId = (cmbParentAccount.SelectedItem as EMemberAccountType).Account_Id;
            objEMemberAssign.RootAccName = (cmbParentAccount.SelectedItem as EMemberAccountType).Parent_Account_Type;
            if (cmbDrCr.Text == "DR")
            {
                objEMemberAssign.DrBalance = Convert.ToDecimal(txtOpeningBalance.Text);
                objEMemberAssign.CrBalance = 0;
            }
            else
            {
                objEMemberAssign.DrBalance = 0;
                objEMemberAssign.CrBalance = Convert.ToDecimal(txtOpeningBalance.Text);
            }
            objEMemberAssign.Status = cmbStatus.Text;
            long accId = objBMemberAssign.SaveChartOfAccount(objEMemberAssign);
            if (accId > 0)
            {
                objEMemberAssign.AccId = accId;
                if (objBMemberAssign.SaveMemberAssign(objEMemberAssign))
                {
                    MessageBox.Show("Saved Successfully",caption,MessageBoxButton.OK,MessageBoxImage.Information);
                    ResetField();
                }
            }
        }

        private bool CheckField()
        {
            if (txtMemberNo.Text == string.Empty)
            {
                MessageBox.Show("Please Select Member", caption, MessageBoxButton.OK, MessageBoxImage.Information);
                btnMemberSearch.Focus();
                return false;
            }
            if (cmbParentAccount.Text == string.Empty)
            {
                MessageBox.Show("Please Select Parent Account", caption, MessageBoxButton.OK, MessageBoxImage.Information);
                cmbParentAccount.Focus();
                return false;
            }
            if (txtAccName.Text == string.Empty)
            {
                MessageBox.Show("Please Enter Account Name", caption, MessageBoxButton.OK, MessageBoxImage.Information);
                txtAccName.Focus();
                return false;
            }
            if (txtAccountNo.Text == string.Empty)
            {
                MessageBox.Show("Please Enter Account No", caption, MessageBoxButton.OK, MessageBoxImage.Information);
                txtAccountNo.Focus();
                return false;
            }
            if (txtOpeningBalance.Text == string.Empty)
            {
                MessageBox.Show("Please Enter Opening Balance", caption, MessageBoxButton.OK, MessageBoxImage.Information);
                txtOpeningBalance.Focus();
                return false;
            }
            if (cmbDrCr.Text == string.Empty)
            {
                MessageBox.Show("Please Select Balance Type", caption, MessageBoxButton.OK, MessageBoxImage.Information);
                cmbDrCr.Focus();
                return false;
            }
            return true;
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            ResetField();
        }

        private void ResetField()
        {
            txtMemberNo.Text = string.Empty;
            txtMemberName.Text = string.Empty;
            txtParentAccountCode.Text = string.Empty;
            txtAccName.Text = string.Empty;
            txtAccountNo.Text = string.Empty;
            txtOpeningBalance.Text = string.Empty;
            PopulateParentAccountOfMember();
            cmbDrCr.Text = string.Empty;
            cmbStatus.SelectedIndex = 0;
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

        private void cmbParentAccount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cmbParentAccount.SelectedIndex > -1)
                {
                    EMemberAccountType objEMtype = cmbParentAccount.SelectedItem as EMemberAccountType;
                    txtParentAccountCode.Text = objEMtype.Account_No;
                }
            }
            catch (Exception)
            {            
            }            
        }

        private void txtOpeningBalance_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Validations.IsDecimal(e.Text);
            base.OnPreviewTextInput(e); 
        }
    }
}
