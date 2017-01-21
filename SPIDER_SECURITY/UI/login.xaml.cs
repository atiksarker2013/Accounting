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
using SPIDER_SECURITY.BLL;
using SPIDER_SECURITY.Entity;
using CHART_ACC_UI.UI;
//Author<--Refat(16-01-2012)--->
namespace SPIDER_SECURITY.UI
{
    /// <summary>
    /// Interaction logic for login.xaml
    /// </summary>
    public partial class login : Window
    {        
        BUser aBuser = new BUser();
        public login()
        {
            InitializeComponent();
            txtUserName.Focus();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            CheckUser();
        }

        private void CheckUser()
        {
            try
            {
                if (CheckField())
                {
                    EUser anUser = new EUser
                    {
                        UserName = txtUserName.Text.Trim(),
                        UserPassword = txtPassword.Password
                    };
                    if (aBuser.DoesExistUserinLoginMode(anUser) == true)
                    {
                        long groupId = aBuser.GetAllInfoforSingleUser(anUser).UserGroupId;
                        MainWindow aMainWindow;
                        aMainWindow = new MainWindow(anUser);
                        aMainWindow.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Invalid User Name or Password", "Login", MessageBoxButton.OK, MessageBoxImage.Information);
                        txtUserName.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem Occur : CheckUser().\n" + ex.Message, "Login", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool CheckField()
        {
            if (txtUserName.Text == string.Empty)
            {
                MessageBox.Show("Please Enter User name.", "Login", MessageBoxButton.OK, MessageBoxImage.Information);
                txtUserName.Focus();
                return false;
            }
            if (txtPassword.Password == string.Empty)
            {
                MessageBox.Show("Please Enter Password.", "Login", MessageBoxButton.OK, MessageBoxImage.Information);
                txtPassword.Focus();
                return false;
            }
            return true;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {            
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void txtUserName_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                txtPassword.Focus();
            }

        }

        private void txtPassword_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                CheckUser();               
            }
        }
    }
}
