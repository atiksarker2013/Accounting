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
    /// Interaction logic for Customer_EmployeeSetup.xaml
    /// </summary>
    public partial class Customer_EmployeeSetup : Window
    {
        BChartOfAccount objBChartOfAccount = new BChartOfAccount();
        BCustomer_Employee objBcustEmployee = new BCustomer_Employee();
        string loginUserName;
        public Customer_EmployeeSetup()
        {
            InitializeComponent();
            InitialTask();            
        }

        private void InitialTask()
        {
            txtOpeningBalance.Text = "0";
            LoadTrans_StatusCombo();
            LoadAccountTypeTree();
            trvAccountType.Visibility = Visibility.Hidden;
        }
        private void LoadTrans_StatusCombo()
        {
            cmbTransType.Items.Add("DR");
            cmbTransType.Items.Add("CR");

            cmbStatus.Items.Add("Active");
            cmbStatus.Items.Add("Inactive");

        }
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
            catch (Exception)
            {
                MessageBox.Show("Problem Occur While Loading Parent Account", "Customer/Employee Setup", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void ProcessTree(long pId, TreeViewItem newChild)
        {
            foreach (EChartOfAccount obj in objBChartOfAccount.GetSubAccount(pId))
            {
                TreeViewItem child = new TreeViewItem();
                child.Header = obj.SubAccName;
                child.Name = "_" + obj.SubAccId;
                newChild.Items.Add(child);
                if (obj.AccHeader == "Yes")
                {
                    ProcessTree(obj.SubAccId, child);
                }
            }
        }
        long parentId = 0; string rootName = "";
        private void trvAccountType_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var trvItem = (sender as TreeView).SelectedItem as TreeViewItem;
            if (trvItem != null)
            {
                txtBlockAccountName.Text = trvItem.Header.ToString();
                string[] splitedId = (trvItem.Name.ToString()).Split('_');
                parentId = Convert.ToInt64(splitedId[1]);
                trvAccountType.Visibility = Visibility.Hidden;
                if (parentId >= 100 && parentId < 200)
                {
                    rootName = trvItem.Header.ToString();
                }
                else
                {
                    GetRootName(trvItem);
                }
                txtParentAccountCode.Text = objBChartOfAccount.GetAccountInfo(parentId).SubAccCode;
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
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (trvAccountType.IsVisible == false)
            {                            
                trvAccountType.Visibility = Visibility.Visible;
            }
            else
            {
                trvAccountType.Visibility = Visibility.Hidden;
            }
        }

        private void Window_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            trvAccountType.Visibility = Visibility.Hidden;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CheckField() && CheckAccountCode())
                {
                    SaveInfo();
                }
            }
            catch (Exception)
            {
             MessageBox.Show("Problem Occur While Saving","Customer/Employee Setup", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool CheckField()
        {
            if (txtName.Text == string.Empty)
            {
                MessageBox.Show("Please Enter Your Name.", "Customer/Employee Setup", MessageBoxButton.OK, MessageBoxImage.Information);
                txtName.Focus();
                return false;
            }
            //if (txtContactNo.Text == string.Empty)
            //{
            //    MessageBox.Show("Please Enter Contact No.", "Customer/Employee Setup", MessageBoxButton.OK, MessageBoxImage.Information);
            //    txtContactNo.Focus();
            //    return false;
            //}
            //if (txtNationalId.Text == string.Empty)
            //{
            //    MessageBox.Show("Please Enter NationalId Card No.", "Customer/Employee Setup", MessageBoxButton.OK, MessageBoxImage.Information);
            //    txtNationalId.Focus();
            //    return false;
            //}
            //if (txtAddress.Text == string.Empty)
            //{
            //    MessageBox.Show("Please Enter Address.", "Customer/Employee Setup", MessageBoxButton.OK, MessageBoxImage.Information);
            //    txtAddress.Focus();
            //    return false;
            //}
            if (txtBlockAccountName.Text == string.Empty)
            {
                MessageBox.Show("Please Select Parent Account Name.", "Customer/Employee Setup", MessageBoxButton.OK, MessageBoxImage.Information);
                txtBlockAccountName.Focus();
                return false;
            }
            if (txtAccountName.Text == string.Empty)
            {
                MessageBox.Show("Please Enter Account Name.", "Customer/Employee Setup", MessageBoxButton.OK, MessageBoxImage.Information);
                txtAccountName.Focus();
                return false;
            }
            if (txtAccountCode.Text == string.Empty)
            {
                MessageBox.Show("Please Enter Account Code.", "Customer/Employee Setup", MessageBoxButton.OK, MessageBoxImage.Information);
                txtAccountCode.Focus();
                return false;
            }
            if (txtOpeningBalance.Text != string.Empty)
            {
                if (Convert.ToDecimal(txtOpeningBalance.Text) != 0)
                {
                    if (cmbTransType.Text == string.Empty)
                    {
                        MessageBox.Show("Please Select Transaction Type.", "Customer/Employee Setup", MessageBoxButton.OK, MessageBoxImage.Information);
                        cmbTransType.Focus();
                        return false;
                    }
                }
            }
            if (cmbStatus.Text == string.Empty)
            {
                MessageBox.Show("Please Select Status.", "Customer/Employee Setup", MessageBoxButton.OK, MessageBoxImage.Information);
                cmbStatus.Focus();
                return false;
            }
            return true;
        }

       /* private bool CheckAccountName()
        {
            if (objBChartOfAccount.DoesExist(txtAccountName.Text.Trim()) == true)
            {
                MessageBox.Show("Account Name Already Exist.", "Customer/Employee Setup", MessageBoxButton.OK, MessageBoxImage.Information);
                txtAccountName.Focus();
                return false;
            }
            return true;
        }*/

        private bool CheckAccountCode()
        {
            if (objBChartOfAccount.DoesCodeExist(txtAccountCode.Text.Trim()) == true)
            {
                MessageBox.Show("Account Code Already Exist.", "Customer/Employee Setup", MessageBoxButton.OK, MessageBoxImage.Information);
                txtAccountCode.Focus();
                return false;
            }
            return true;           
        }

        private void SaveInfo()
        {
            ECustomer_Employee objEcustEmplyee = new ECustomer_Employee();
            objEcustEmplyee.EployeeName = txtName.Text.Trim();
            objEcustEmplyee.Contact_No = txtContactNo.Text.Trim();
            objEcustEmplyee.ContactPerson = txtContactPerson.Text.Trim();
            objEcustEmplyee.NationalId = txtNationalId.Text.Trim();
            objEcustEmplyee.Address = txtAddress.Text.Trim();
            objEcustEmplyee.ParentAccountId = parentId;
            objEcustEmplyee.RootName = rootName;
            objEcustEmplyee.AccountName = txtAccountName.Text.Trim();
            objEcustEmplyee.AccountCode = txtAccountCode.Text.Trim();
            if (txtOpeningBalance.Text != string.Empty)
            {
                if (Convert.ToDecimal(txtOpeningBalance.Text.Trim()) != 0)
                {
                    if (cmbTransType.Text == "DR")
                    {
                        objEcustEmplyee.AccOpeningBalanceDR = Convert.ToDecimal(txtOpeningBalance.Text.Trim());
                    }
                    else if (cmbTransType.Text == "CR")
                    {
                        objEcustEmplyee.AccOpeningBalanceCR = Convert.ToDecimal(txtOpeningBalance.Text.Trim());
                    }
                }
            }
            objEcustEmplyee.Status = cmbStatus.Text;
            objEcustEmplyee.AccessBy = loginUserName;
            if (objBcustEmployee.SaveCustomerEmployee(objEcustEmplyee) == true)
            {
                MessageBox.Show("All Information has been Saved", "Customer/Employee Setup", MessageBoxButton.OK, MessageBoxImage.Information);
                ResetField();
            }
            else
            {
                MessageBox.Show("Saving Failed", "Customer/Employee Setup", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            ResetField();            
        }

        private void ResetField()
        {
            txtName.Text = string.Empty;
            txtContactNo.Text = string.Empty;
            txtContactPerson.Text = string.Empty;
            txtNationalId.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtBlockAccountName.Text = string.Empty;
            txtParentAccountCode.Text = string.Empty;
            txtAccountName.Text = string.Empty;
            txtAccountCode.Text = string.Empty;
            txtOpeningBalance.Text = "0"; 
            cmbTransType.Text = string.Empty;
            cmbStatus.Text = string.Empty;
            parentId = 0;
            rootName = string.Empty;
            LoadAccountTypeTree();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void txtOpeningBalance_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Validations.IsDecimal(e.Text);
            base.OnPreviewTextInput(e);
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
