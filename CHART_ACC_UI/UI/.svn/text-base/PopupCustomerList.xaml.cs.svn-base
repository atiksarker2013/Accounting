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
    /// Interaction logic for PopupCustomerList.xaml
    /// </summary>
    public partial class PopupCustomerList : Window
    {
        BCustomer_Employee objBCustEmp = new BCustomer_Employee();
        List<ECustomer_Employee> listCustEmp = new List<ECustomer_Employee>();
        public PopupCustomerList()
        {
            InitializeComponent();
            PopulateCustomerList();
            lvCustomerEmployee.Focus();
        }

        private void PopulateCustomerList()
        {
            try
            {
                listCustEmp = objBCustEmp.GetAllCustomerEmployee();
                lvCustomerEmployee.ItemsSource = listCustEmp;
            }
            catch (Exception)
            {

            }
        }

        private void lvCustomerEmployee_KeyDown(object sender, KeyEventArgs e)
        {
            if (lvCustomerEmployee.SelectedIndex > -1)
            {
                if (e.Key == Key.Enter)
                {
                    this.DialogResult = true;
                }
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            SearchCustEmployee();
        }

        private void SearchCustEmployee()
        {
            try
            {
                if (txtAccName.Text.Trim() != string.Empty || txtAccountCode.Text.Trim() != string.Empty)
                {
                    if (txtAccName.Text.Trim() != string.Empty)
                    {
                        List<ECustomer_Employee> listSearch = new List<ECustomer_Employee>();
                        var query = from j in listCustEmp where (j.AccountName.ToLower()).Contains(txtAccName.Text.Trim().ToLower()) orderby j.EployeeName select j;
                        foreach (var emp in query)
                        {
                            listSearch.Add(emp);
                        }
                        lvCustomerEmployee.ItemsSource = listSearch;

                    }
                    else if (txtAccountCode.Text.Trim() != string.Empty)
                    {
                        List<ECustomer_Employee> listSearch = new List<ECustomer_Employee>();
                        var query = from j in listCustEmp where (j.AccountCode.ToLower()).Contains(txtAccountCode.Text.Trim().ToLower()) orderby j.EployeeName select j;
                        foreach (var emp in query)
                        {
                            listSearch.Add(emp);
                        }
                        lvCustomerEmployee.ItemsSource = listSearch;
                    }
                }
                else if (txtAccName.Text.Trim() == string.Empty || txtAccountCode.Text.Trim() == string.Empty)
                {
                    lvCustomerEmployee.ItemsSource = listCustEmp;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Unknown Problem Occur.","Search",MessageBoxButton.OK,MessageBoxImage.Error);
            }
            
        }

        private void lvCustomerEmployee_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lvCustomerEmployee.SelectedIndex > -1)
            {
                this.DialogResult = true;
            }
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            txtAccName.Text = string.Empty;
            txtAccountCode.Text = string.Empty;
            lvCustomerEmployee.ItemsSource = listCustEmp;
        }

        private void txtAccName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnSearch.Focus();
                SearchCustEmployee();
            }
        }

        private void txtAccountCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnSearch.Focus();
                SearchCustEmployee();
            }
        }
    }
}
