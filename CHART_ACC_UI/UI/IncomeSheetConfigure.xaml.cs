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

namespace CHART_ACC_UI.UI
{
    /// <summary>
    /// Interaction logic for IncomeSheetConfigure.xaml
    /// </summary>
    public partial class IncomeSheetConfigure : Window
    {
        BIncomeConfigure objBIncomeSheetConfigure = new BIncomeConfigure();
        string loginUser = string.Empty;
        string caption = "Income Statement Configure";
        bool Status = false;

        public IncomeSheetConfigure()
        {
            InitializeComponent();
            InitialTask();
        }

        private void InitialTask()
        {
            try
            {
                dgExpense.ItemsSource = objBIncomeSheetConfigure.LoadListofISConfig("EXPENSE");
                dgRevenue.ItemsSource = objBIncomeSheetConfigure.LoadListofISConfig("REVENUE");
                if (objBIncomeSheetConfigure.HasValueinISConfig())
                {
                    btnSave.Content = "Update";
                }
                else
                {
                    btnSave.Content = "Save";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem Occur to Retrieve Data.\n" + ex.Message, caption, MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        #region WINDOW EVENT
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
        #endregion

        private void CheckboxExpense_Checked(object sender, RoutedEventArgs e)
        {
            if (dgExpense.SelectedIndex > -1)
            {
                CheckBox chkBox = sender as CheckBox;
                AssignAccount(chkBox, dgExpense);
            }
        }
        private void AssignAccount(CheckBox chkBox, DataGrid dg)
        {
            EIncomeConfigure objEISConfig = dg.SelectedItem as EIncomeConfigure;
            Status = false;
            ParenIsCheked(objEISConfig.ParentAccId, dg);
            if (Status == true)
            {
                objEISConfig.Status = false;
                chkBox.IsChecked = false;
            }
            Status = false;
            ChildChecked(objEISConfig.SubAccId, dg);
            if (Status == true)
            {
                objEISConfig.Status = false;
                chkBox.IsChecked = false;
            }
        }
        private void ChildChecked(long id, DataGrid dg)
        {
            if (id > 200)
            {
                EIncomeConfigure objEISConfig = new EIncomeConfigure();
                for (int i = 0; i < dg.Items.Count; i++)
                {
                    EIncomeConfigure objEIS = dg.Items[i] as EIncomeConfigure;
                    if (objEIS.ParentAccId == id && objEIS.Status == true)
                    {
                        objEISConfig = objEIS;
                        break;
                    }
                }
                if (objEISConfig.Status)
                {
                    Status = true;
                }
            }
        }

        private void ParenIsCheked(long id, DataGrid dg)
        {
            if (id > 200)
            {
                EIncomeConfigure objEISConfig = new EIncomeConfigure();
                for (int i = 0; i < dg.Items.Count; i++)
                {
                    objEISConfig = dg.Items[i] as EIncomeConfigure;
                    if (objEISConfig.SubAccId == id)
                    {
                        break;
                    }
                }
                if (objEISConfig.Status)
                {
                    Status = true;
                }
                else
                {
                    if (objEISConfig.ParentAccId > 200)
                    {
                        ParenIsCheked(objEISConfig.ParentAccId, dg);
                    }
                }
            }
        }

        private void CheckboxRevenue_Checked(object sender, RoutedEventArgs e)
        {
            if (dgRevenue.SelectedIndex > -1)
            {
                CheckBox chkBox = sender as CheckBox;
                AssignAccount(chkBox, dgRevenue);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<EIncomeConfigure> listEincomeSheet = new List<EIncomeConfigure>();
                ListPopulate(listEincomeSheet, dgExpense);
                ListPopulate(listEincomeSheet, dgRevenue);
                if (listEincomeSheet.Count == 0)
                {
                    dgExpense.SelectedIndex = -1;
                    dgRevenue.SelectedIndex = -1;
                    MessageBox.Show("Please Select Account to be Configured.", caption, MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                if (btnSave.Content.ToString() == "Save")
                {
                    if (objBIncomeSheetConfigure.SaveISConfig(listEincomeSheet))
                    {
                        MessageBox.Show("Successfully Configured.", caption, MessageBoxButton.OK, MessageBoxImage.Information);
                        InitialTask();
                    }
                }
                else if (btnSave.Content.ToString() == "Update")
                {
                    if (objBIncomeSheetConfigure.DeleteISConfig() && objBIncomeSheetConfigure.SaveISConfig(listEincomeSheet))
                    {
                        MessageBox.Show("Successfully Configured.", caption, MessageBoxButton.OK, MessageBoxImage.Information);
                        InitialTask();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem Occur to Configure.\n" + ex.Message, caption, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ListPopulate(List<EIncomeConfigure> listEincomeSheet, DataGrid dg)
        {
            for (int i = 0; i < dg.Items.Count; i++)
            {
                EIncomeConfigure objBS = dg.Items[i] as EIncomeConfigure;
                if (objBS.Status)
                {
                    listEincomeSheet.Add(objBS);
                }
            }
        }
    }
}
