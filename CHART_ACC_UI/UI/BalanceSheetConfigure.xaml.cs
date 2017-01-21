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
using CHART_ACC_BLL;
using CHART_ACC_ENTITY;
namespace CHART_ACC_UI.UI
{
    /// <summary>
    /// Interaction logic for BalanceSheetConfigurexaml.xaml
    /// </summary>
    public partial class BalanceSheetConfigure : Window
    {
        BBalanceSheetConfigure objBBalanceSheetConfigure = new BBalanceSheetConfigure();        
        string loginUser = string.Empty;
        string caption = "Balance Sheet Configure";
        bool Status = false;

        public BalanceSheetConfigure()
        {
            InitializeComponent();
            InitialTask();                
        }
        private void InitialTask()
        {
            try
            {
                dgAsset.ItemsSource = objBBalanceSheetConfigure.LoadListofBSConfig("ASSET");
                dgLiability.ItemsSource = objBBalanceSheetConfigure.LoadListofBSConfig("LIABILITY");
                dgOwnersEquity.ItemsSource = objBBalanceSheetConfigure.LoadListofBSConfig("OWNERS EQUITY");
                if (objBBalanceSheetConfigure.HasValueinBSConfig())
                {
                    btnSave.Content = "Update";
                }
                else
                {
                    btnSave.Content = "Save";
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Problem Occur to Retrieve Data.",caption,MessageBoxButton.OK,MessageBoxImage.Error);
            }
            
        }
        private void CheckboxAsset_Checked(object sender, RoutedEventArgs e)
        {
            if (dgAsset.SelectedIndex > -1)
            {
                CheckBox chkBox = sender as CheckBox;
                AssignAccount(chkBox,dgAsset);
            }
        }

        private void CheckboxLiability_Checked(object sender, RoutedEventArgs e)
        {
            if (dgLiability.SelectedIndex > -1)
            {
                CheckBox chkBox = sender as CheckBox;
                AssignAccount(chkBox, dgLiability);
            }
        }

        private void CheckboxOwnersEquity_Checked(object sender, RoutedEventArgs e)
        {
            if (dgOwnersEquity.SelectedIndex > -1)
            {
                CheckBox chkBox = sender as CheckBox;
                AssignAccount(chkBox, dgOwnersEquity);
            }
        }        
        
        private void AssignAccount(CheckBox chkBox,DataGrid dg)
        {
            EBalanceSheetConfigure objEBSConfig = dg.SelectedItem as EBalanceSheetConfigure;
            Status = false;
            ParenIsCheked(objEBSConfig.ParentAccId,dg);
            if (Status == true)
            {
                objEBSConfig.Status = false;
                chkBox.IsChecked = false;
            }
            Status = false;
            ChildChecked(objEBSConfig.SubAccId,dg);
            if (Status == true)
            {
                objEBSConfig.Status = false;
                chkBox.IsChecked = false;
            }
        }

        private void ChildChecked(long id, DataGrid dg)
        {
            if (id > 200)
            {
                EBalanceSheetConfigure objEBSConfig = new EBalanceSheetConfigure();
                for (int i = 0; i < dg.Items.Count; i++)
                {
                    EBalanceSheetConfigure objEBS = dg.Items[i] as EBalanceSheetConfigure;
                    if (objEBS.ParentAccId == id && objEBS.Status == true)
                    {
                        objEBSConfig = objEBS;
                        break;
                    }
                }
                if (objEBSConfig.Status)
                {
                    Status = true;
                }                
            }
        }

        void ParenIsCheked(long id,DataGrid dg)
        {
            if (id > 200)
            {
                EBalanceSheetConfigure objEBSConfig = new EBalanceSheetConfigure();
                for (int i = 0; i < dg.Items.Count; i++)
                {
                    objEBSConfig = dg.Items[i] as EBalanceSheetConfigure;
                    if (objEBSConfig.SubAccId == id)
                    {
                        break;
                    }
                }
                if (objEBSConfig.Status)
                {
                    Status = true;
                }
                else
                {
                    if (objEBSConfig.ParentAccId > 200)
                    {
                        ParenIsCheked(objEBSConfig.ParentAccId,dg);
                    }
                }
            }
        }      

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<EBalanceSheetConfigure> listEbalanceSheet = new List<EBalanceSheetConfigure>();
                ListPopulate(listEbalanceSheet, dgAsset);
                ListPopulate(listEbalanceSheet, dgLiability);
                ListPopulate(listEbalanceSheet,dgOwnersEquity);
                if (listEbalanceSheet.Count == 0)
                {
                    dgAsset.SelectedIndex = -1;
                    dgLiability.SelectedIndex = -1;
                    dgOwnersEquity.SelectedIndex = -1;
                    MessageBox.Show("Please Select Account to be Configured.", caption, MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                if(btnSave.Content.ToString()=="Save")
                {
                    if (objBBalanceSheetConfigure.SaveBSConfig(listEbalanceSheet))
                    {
                        MessageBox.Show("Successfully Configured.", caption, MessageBoxButton.OK, MessageBoxImage.Information);
                        InitialTask();
                    }
                }
                else if (btnSave.Content.ToString() == "Update")
                {                    
                    if (objBBalanceSheetConfigure.DeleteBSConfig() && objBBalanceSheetConfigure.SaveBSConfig(listEbalanceSheet))
                    {
                        MessageBox.Show("Successfully Configured.", caption, MessageBoxButton.OK, MessageBoxImage.Information);
                        InitialTask();
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Problem Occur to Configure.",caption,MessageBoxButton.OK,MessageBoxImage.Error);
            }
        }       

        private void ListPopulate(List<EBalanceSheetConfigure> listEbalanceSheet,DataGrid dg)
        {
            for (int i = 0; i < dg.Items.Count; i++)
            {
                EBalanceSheetConfigure objBS = dg.Items[i] as EBalanceSheetConfigure;
                if (objBS.Status)
                {
                    listEbalanceSheet.Add(objBS);
                }
            }
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
    }
}
