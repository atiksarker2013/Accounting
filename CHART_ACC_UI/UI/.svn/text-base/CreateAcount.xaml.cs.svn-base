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
using System.Data.SqlClient;

namespace CHART_ACC_UI.UI
{
    /// <summary>
    /// Interaction logic for CreateAcount.xaml
    /// </summary>
    public partial class CreateAcount : Window
    {
        BAccountType objBAccountType = new BAccountType();

        public CreateAcount()
        {
            InitializeComponent();
            LoadAccountTypeListView();
        }

        private void LoadAccountTypeListView()
        {
            try
            {
                lvAccountTypeName.ItemsSource = objBAccountType.GetAllAccountType();
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show("Network related problem occured while retriving VAT type.\n Please check your network connectivity." + sqlEx.Message, "Account Type", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unknown problem occured while retriving account type.\n" + ex.Message, "Account Type", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (CheckField())
            {
                if (btnSave.Content.ToString() == "Save")
                {
                    if (objBAccountType.DoesExist(txtAccountTypeName.Text) == false)
                    {
                        if (objBAccountType.SaveAccountType(txtAccountTypeName.Text) == true)
                        {
                            MessageBox.Show("Account type name saved successfully.", "Account Type", MessageBoxButton.OK, MessageBoxImage.Information);
                            ResetField();
                        }
                        else
                        {
                            MessageBox.Show("Account type name saving failed.", "Account Type", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Account type name already Exist.", "Account Type", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    UpdateAccType();
                }
            }
        }

        private bool CheckField()
        {
            if (txtAccountTypeName.Text == string.Empty)
            {
                MessageBox.Show("Give your account head name.", "Account Type", MessageBoxButton.OK, MessageBoxImage.Information);
                txtAccountTypeName.Focus();
                return false;
            }
            return true;
        }

        private void ResetField()
        {
            txtAccountTypeName.Text = "";
            btnSave.Content = "Save";
            LoadAccountTypeListView();
        }

        private long accId;
        private void editData_Click(object sender, RoutedEventArgs e)
        {
            if (lvAccountTypeName.SelectedIndex > -1)
            {
                accId = ((EAccountType)lvAccountTypeName.SelectedItem).AccTypeId;
                txtAccountTypeName.Text = ((EAccountType)lvAccountTypeName.SelectedItem).AccTypeName;
                btnSave.Content = "Update";
            }
            else
            {
                MessageBox.Show("Select your account type name.","Account Name",MessageBoxButton.OK,MessageBoxImage.Information);
            }
        }

        private void UpdateAccType()
        {
            EAccountType accType = new EAccountType();
            accType.AccTypeId = accId;
            accType.AccTypeName = txtAccountTypeName.Text;
            objBAccountType.UpdateAccType(accType);
            MessageBox.Show("Account type name updated.", "Account Type", MessageBoxButton.OK, MessageBoxImage.Information);
            ResetField();
            btnSave.Content = "Save";
        }

        private void deleteData_Click(object sender, RoutedEventArgs e)
        {
            if (lvAccountTypeName.SelectedIndex > -1)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Do you really want to delete " + ((EAccountType)(lvAccountTypeName.SelectedItem)).AccTypeName + " account type?", "Account Type", MessageBoxButton.YesNo, MessageBoxImage.Information);
                if (messageBoxResult.ToString() == MessageBoxResult.Yes.ToString())
                {
                    DeleteAccType();
                }
            }
            else
            {
                MessageBox.Show("Select your account type name.", "Account Name", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void DeleteAccType()
        {
            try
            {
                objBAccountType.DeleteAccType(((EAccountType)(lvAccountTypeName.SelectedItem)).AccTypeId);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error : \n" + ex.Message, "Account Type", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            ResetField();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.ShowInTaskbar = true;
            this.Owner = null;
        }
    }
}
