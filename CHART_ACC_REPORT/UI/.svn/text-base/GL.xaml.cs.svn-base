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
using CHART_ACC_REPORT.RPT;
using CHART_ACC_REPORT.UTILITY;
//Author:REFAT
namespace CHART_ACC_REPORT.UI
{
    /// <summary>
    /// Interaction logic for GL.xaml
    /// </summary>
    public partial class GL : Window
    {
        BGL objBGL = new BGL();
        BChartAccount objBChartOfAccount = new BChartAccount();
        string caption = "General Ledger";
        public GL()
        {
            InitializeComponent();
            InitialTask();
        }
        private void InitialTask()
        {            
            SetFiscalDate();
            dtpTo.SelectedDate = DateTime.Now;
            dtpFrom.SelectedDate = DateTime.Now;
            LoadAccountType();
            rdbAll.IsChecked = true;
        }

        private void SetFiscalDate()
        {
            try
            {
                EChartOfAccount obj = objBChartOfAccount.GetFisDate();
                txtFiscalStartDate.Text = obj.FiscalStartDate.ToShortDateString();
                txtFiscalEndDate.Text = obj.FisCalEndDate.ToShortDateString();                
            }
            catch (Exception)
            {
                MessageBox.Show("Problem Occur While Loading Fiscal Information.", caption, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void LoadAccountType()
        {
            try
            {
                foreach (EAccountType objType in objBChartOfAccount.GetAllAccountType())
                {
                    cmbAccType.Items.Add(objType);                 
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Problem Occur While Loading Account Information.", caption, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnPreview_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string header="";
                if (AreAllFieldsCompleted())
                {
                    List<EGL> listGL = new List<EGL>();
                    if (rdbAll.IsChecked == true)
                    {
                        listGL = objBGL.LoadListofGL(Convert.ToDateTime(dtpFrom.SelectedDate), Convert.ToDateTime(dtpTo.SelectedDate),true);
                        header = caption;
                    }
                    else if (rdbSelect.IsChecked == true)
                    {
                        listGL=objBGL.LoadListofGLParticaularIdwise((cmbAccType.SelectedItem as EAccountType).AccTypeId, cmbAccType.Text, Convert.ToDateTime(dtpFrom.SelectedDate), Convert.ToDateTime(dtpTo.SelectedDate),true);
                        header = caption + " for "+cmbAccType.Text;
                    }
                    if (listGL.Count > 0)
                    {
                        CrptGL objCrptGL = new CrptGL();
                        AccountingRptUtility.SetDate(objCrptGL, Convert.ToDateTime(dtpFrom.SelectedDate).ToShortDateString(), Convert.ToDateTime(dtpTo.SelectedDate).ToShortDateString());
                        AccountingRptUtility.setCompanyDeifinition(objCrptGL, header);
                        AccountingRptUtility.Display_report(objCrptGL, listGL, this);
                    }
                    else
                    {
                        MessageBox.Show("No record found", caption, MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem Occur While Showing Report.\n"+ex.Message, caption, MessageBoxButton.OK, MessageBoxImage.Error);
            }
           
        }
        private bool AreAllFieldsCompleted()
        {
            if (dtpFrom.Text == string.Empty)
            {
                dtpFrom.Focus();
                MessageBox.Show("Please select from date.", caption, MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            if (dtpTo.Text == string.Empty)
            {
                dtpTo.Focus();
                MessageBox.Show("Please select to date.", caption, MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            //if (dtpTo.SelectedDate > Convert.ToDateTime(txtFiscalEndDate.Text))
            //{
            //    dtpTo.Focus();
            //    MessageBox.Show("Please select date between the current fiscal year", "General Ledger", MessageBoxButton.OK, MessageBoxImage.Information);
            //    return false;
            //}
            if (dtpTo.SelectedDate < dtpFrom.SelectedDate)
            {
                MessageBox.Show("Please verify your date selection.", caption, MessageBoxButton.OK, MessageBoxImage.Information);
                return false;
            }
            if (rdbSelect.IsChecked == true)
            {
                if (cmbAccType.Text == string.Empty)
                {
                    MessageBox.Show("Please Select Account.", caption, MessageBoxButton.OK, MessageBoxImage.Information);
                    cmbAccType.Focus();
                    return false;
                }
            }
            return true;
        }
        private void rdbAll_Checked(object sender, RoutedEventArgs e)
        {
            cmbAccType.SelectedIndex = -1;
            cmbAccType.IsEnabled = false;
        }
        private void rdbSelect_Click(object sender, RoutedEventArgs e)
        {
            cmbAccType.IsEnabled = true;
        }
        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            InitialTask();            
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
    }
}
