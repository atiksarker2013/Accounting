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
    /// Interaction logic for CustomerEmployeeModify.xaml
    /// </summary>
    public partial class CustomerEmployeeModify : Window
    {
        BChartOfAccount objBChartOfAccount = new BChartOfAccount();
        BCustomer_Employee objBcustEmployee = new BCustomer_Employee();
        string loginUserName, caption = "Customer/Employee Setup Modify";
        long Id = 0;

        public CustomerEmployeeModify()
        {
            InitializeComponent();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CheckField())
                {
                    UpdateInfo();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Problem Occur While Updating", caption, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateInfo()
        {
            ECustomer_Employee objECustEmp = new ECustomer_Employee();
            objECustEmp.Id = Id;
            objECustEmp.EployeeName = txtName.Text.Trim();
            objECustEmp.Contact_No = txtContactNo.Text.Trim();
            objECustEmp.ContactPerson = txtContactPerson.Text.Trim();
            objECustEmp.NationalId = txtNationalId.Text.Trim();
            objECustEmp.Address = txtAddress.Text.Trim();
            if (objBcustEmployee.UpdateCustEmp(objECustEmp))
            {
                MessageBox.Show("Update Success", caption, MessageBoxButton.OK, MessageBoxImage.Information);
                ResetField();
            }
        }

        private bool CheckField()
        {
            if (txtAccountCode.Text == string.Empty)
            {
                MessageBox.Show("Please Select Customer/Employee.", caption, MessageBoxButton.OK, MessageBoxImage.Information);
                btnSearch.Focus();
                return false;
            }  
            if (txtName.Text == string.Empty)
            {
                MessageBox.Show("Please Enter Your Name.", caption, MessageBoxButton.OK, MessageBoxImage.Information);
                txtName.Focus();
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
            Id = 0;
            txtName.Text = string.Empty;
            txtContactNo.Text = string.Empty;
            txtContactPerson.Text = string.Empty;
            txtNationalId.Text = string.Empty;
            txtAddress.Text = string.Empty;           
            txtAccountCode.Text = string.Empty;           
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
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

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            PopupCustomerList objPopupCustomerList = new PopupCustomerList();
            if (objPopupCustomerList.ShowDialog() == true)
            {
                ECustomer_Employee objECustEmp = objPopupCustomerList.lvCustomerEmployee.SelectedItem as ECustomer_Employee;
                Id = objECustEmp.Id;
                txtAccountCode.Text = objECustEmp.AccountCode;
                txtName.Text = objECustEmp.EployeeName;
                txtContactNo.Text = objECustEmp.Contact_No;
                txtContactPerson.Text = objECustEmp.ContactPerson;
                txtNationalId.Text = objECustEmp.NationalId;
                txtAddress.Text = objECustEmp.Address;                
            }
            
        }
    }
}
