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
using CHART_ACC_REPORT.ENTITY;
using CHART_ACC_REPORT.BLL;
using CHART_ACC_REPORT.RPT;
using CHART_ACC_REPORT.UTILITY;

namespace CHART_ACC_REPORT.UI
{
    /// <summary>
    /// Interaction logic for Depriciation.xaml
    /// </summary>
    public partial class Depriciation : Window
    {
        BDepriciation objBDepriciation = new BDepriciation();

        public Depriciation()
        {
            InitializeComponent();
            LoadFiscalYearCombo();
        }

        #region WINDOW
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

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion

        private void LoadFiscalYearCombo()
        {
            cmbFiscalYear.Items.Clear();
            try
            {
                List<EFiscalYear> listFiscalYear = objBDepriciation.GetAllFiscalYear();
                foreach (EFiscalYear objFiscalYear in listFiscalYear)
                {
                    cmbFiscalYear.Items.Add(objFiscalYear);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in get fiscal year.\n" + ex.Message, "Depreciation Report", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnPreview_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<EDepriciationReport> listDepriciationReport = new List<EDepriciationReport>();
                if (cmbFiscalYear.Text != string.Empty)
                {
                    if (cmbFiscalYear.Text == "Select All")
                    {
                        listDepriciationReport = objBDepriciation.GetDepriciationALL();
                        if (listDepriciationReport.Count > 0)
                        {
                            CrptDepriciationList objCrptDepriciationList = new CrptDepriciationList();
                            AccountingRptUtility.SetDate(objCrptDepriciationList, listDepriciationReport[0].Access_Date.ToShortDateString(), listDepriciationReport[listDepriciationReport.Count-1].Access_Date.ToShortDateString());
                            AccountingRptUtility.setCompanyDeifinition(objCrptDepriciationList, "Report on Depreciation");
                            AccountingRptUtility.Display_report(objCrptDepriciationList, listDepriciationReport, this);
                        }

                    }
                    else
                    {
                        DateTime StartDate = ((EFiscalYear)(cmbFiscalYear.SelectedItem)).F_year_Start_Date;
                        DateTime EndDate = ((EFiscalYear)(cmbFiscalYear.SelectedItem)).F_Year_End_Date;
                        listDepriciationReport = objBDepriciation.GetDepriciationDateWise(StartDate, EndDate);
                        if (listDepriciationReport.Count > 0)
                        {
                            CrptDepriciation objCrptDepriciation = new CrptDepriciation();
                            AccountingRptUtility.SetDate(objCrptDepriciation, StartDate.ToShortDateString(), EndDate.ToShortDateString());
                            AccountingRptUtility.setCompanyDeifinition(objCrptDepriciation, "Report on Depreciation");
                            AccountingRptUtility.Display_report(objCrptDepriciation, listDepriciationReport, this);
                        }
                        else
                        {
                            MessageBox.Show("You have no Depriciation on this Fiscal Year.", "Depreciation Report", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Select your Fiscal Year.", "Depreciation Report", MessageBoxButton.OK, MessageBoxImage.Information );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: btnPreview_Click()\n" + ex.Message, "Depreciation Report", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void cmbFiscalYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbFiscalYear.SelectedIndex > -1)
            {
                EFiscalYear objFiscalYear = (EFiscalYear)cmbFiscalYear.SelectedItem;
                string date = objFiscalYear.F_year_Start_Date.ToShortDateString() + "  " + "To " + "  " + objFiscalYear.F_Year_End_Date.ToShortDateString();
                textBlock1.Text = date;
            }
        }
    }
}
