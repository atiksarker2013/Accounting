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
using SPIDER_SECURITY.BLL;
using SPIDER_SECURITY.Entity;
//Author:REFAT
namespace SPIDER_SECURITY.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        EUser anUser = new EUser();

        public MainWindow()
        {
            InitializeComponent();
           //this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            btnAccounting.IsEnabled = true;
            //btnHRM.IsEnabled = true;
            btnSecurity.IsEnabled = true;
            btnAdminTools.IsEnabled = true;
        }
        public MainWindow(EUser objEUser):this()
        {
            anUser = objEUser;
            if (objEUser.UserGroupName != "Super Admin")
            {
                btnAccounting.IsEnabled = false;
                //btnHRM.IsEnabled = false;
                btnSecurity.IsEnabled = false;
                btnAdminTools.IsEnabled = false;                
                LoadUserRoleControl();
            }
        }
        private void LoadUserRoleControl()
        {
            try
            {
                BModulePermission objBModule = new BModulePermission();
                List<EModulePermission> listModule = objBModule.GetPermittedModule(anUser.UserGroupId);
                foreach (var item in listModule)
                {
                    if (item.ModuleName == "Accounting")
                    {
                        btnAccounting.IsEnabled = true;
                    }
                    else if (item.ModuleName == "HRM")
                    {
                        //btnHRM.IsEnabled = true;
                    }
                    else if (item.ModuleName == "Security")
                    {
                        btnSecurity.IsEnabled = true;
                    }
                    else if (item.ModuleName == "Admin Tools")
                    {
                        btnAdminTools.IsEnabled = true;
                    }                    
                }
            }
            catch (Exception)
            {              
            }            
        }

        private void btnAccounting_Click(object sender, RoutedEventArgs e)
        {
            CHART_ACC_UI.MainWindow aMain;
            if (anUser.UserGroupName == "Super Admin")
            {
                aMain = new CHART_ACC_UI.MainWindow();                
            }
            else
            {
                aMain = new CHART_ACC_UI.MainWindow(anUser.UserGroupId);
            }
            aMain.Show();
        }

        private void btnHRM_Click(object sender, RoutedEventArgs e)
        {
            //IECL_HRM_UI.HR_MainUI aMain = new IECL_HRM_UI.HR_MainUI();
            //aMain.Show();
        }

        private void btnSecurity_Click(object sender, RoutedEventArgs e)
        {
            SecurityControl objSecurityControl = new SecurityControl();
            objSecurityControl.Show();
        }

        private void btnAdminTools_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {                       
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            foreach (Window wnd in Application.Current.Windows)
            {
                if (wnd.ToString() == "CHART_ACC_UI.MainWindow")
                {
                    wnd.Close();
                }
                if (wnd.ToString() == "SPIDER_SECURITY.UI.SecurityControl")
                {
                    wnd.Close();
                }
            }
            login objLogin = new login();
            objLogin.Show();
        }
    }
}
