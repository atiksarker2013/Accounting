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
    /// Interaction logic for FiscalYear.xaml
    /// </summary>
    public partial class FiscalYear : Window
    {
        BFiscalYear objBFiscalYear = new BFiscalYear();
        EFiscalYear objEFiscalYearG = new EFiscalYear();
        string UserName = "Amin";
        public FiscalYear()
        {
            InitializeComponent();
            populateStatusCombo();
            LoadFiscalYearListView();
            CheckFiscalYear();
            dtpStartDate.SelectedDate = DateTime.Now;
            dtpEndDate.SelectedDate = DateTime.Now;
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
        }

        private void populateStatusCombo()
        {
            string[] Status = { "Active", "Inactive" };
            cmbYearStatus.Items.Add(Status[0]);
            cmbYearStatus.Items.Add(Status[1]);
        }

        private void LoadFiscalYearListView()
        {
            lvFiscalYear.Items.Clear();
            try
            {
                List<EFiscalYear> fiscalYearList = objBFiscalYear.GetAllFiscalYear();
                foreach (EFiscalYear obj in fiscalYearList)
                {
                    lvFiscalYear.Items.Add(obj);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Catch Exception on Show Data in Listview. " + ex.Message);
            }
        }

        
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            List<EFiscalYear> fiscalYearList = objBFiscalYear.CheckFiscalYear();
            if (CheckFields() == true)
            {
                if (btnSave.Content.ToString() == "Save")
                {
                    if (fiscalYearList.Count == 0)
                    {
                        SaveFiscalYear();

                    }
                    else
                    {
                        MessageBox.Show("You are already using a Active Fiscal Year. You Can Not Activate Multiple Fiscal Year.", "Fiscal Year", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    if (objBFiscalYear.GetDepriciationForCurrentYear() <= 0 && cmbYearStatus.Text == "Inactive")
                    {
                        MessageBox.Show("You hav'nt generate depriciation for this year.\nPlease generate depriciation.", "Fiscal Year", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        if (DoesExistActiveInUpdateMode(fiscalYearList))
                        {
                            UpdateFiscalYear();
                        }
                        else
                        {
                            MessageBox.Show("You are already using a Active Fiscal Year. You Can Not Activate Multiple Fiscal Year.", "Fiscal Year", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                }
            }
        }
        private bool DoesExistActiveInUpdateMode(List<EFiscalYear> listFis)
        {     
            for (int i = 0; i < listFis.Count; i++)
            {
                EFiscalYear objEfis=listFis[i];
                if (objEfis.F_Year_Id != objEFiscalYearG.F_Year_Id)
                {
                    if (objEfis.F_Year_Status == "Active")
                    {
                        return false;
                    }
                }
            }
                return true;
        }
        private void SaveFiscalYear()
        {
            try
            {
                EFiscalYear objEFiscalYear = new EFiscalYear();
                objEFiscalYear.F_Year_Name = txtFiscalYearName.Text;
                objEFiscalYear.F_year_Start_Date = Convert.ToDateTime(dtpStartDate.SelectedDate);
                objEFiscalYear.F_Year_End_Date = Convert.ToDateTime(dtpEndDate.SelectedDate);
                objEFiscalYear.Access_By = UserName;
                objEFiscalYear.F_Year_Status = cmbYearStatus.Text;

                if (objBFiscalYear.SaveFiscalYear(objEFiscalYear) == true)
                {
                    MessageBox.Show("Fiscal year saved successfully.", "Fiscal Year", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadFiscalYearListView();
                    ResetField();
                }
                else
                {
                    MessageBox.Show("Fiscal year saving failed.", "Fiscal Year", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : Save in operation.\n" +ex.Message, "Fiscal Year", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateFiscalYear()
        {
            try
            {
                EFiscalYear objEFiscalYear = new EFiscalYear();
                objEFiscalYear.F_Year_Id = objEFiscalYearG.F_Year_Id;
                objEFiscalYear.F_Year_Name = txtFiscalYearName.Text;
                objEFiscalYear.F_year_Start_Date = Convert.ToDateTime(dtpStartDate.SelectedDate);
                objEFiscalYear.F_Year_End_Date = Convert.ToDateTime(dtpEndDate.SelectedDate);
                objEFiscalYear.Access_By = "AMIN";
                objEFiscalYear.F_Year_Status = cmbYearStatus.Text;

                if (objBFiscalYear.UpdateFiscalYear(objEFiscalYear) == true)
                {
                    MessageBox.Show("Fiscal year updated successfully.", "Fiscal Year", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadFiscalYearListView();
                    ResetField();
                }
                else
                {
                    MessageBox.Show("Fiscal year updating failed.", "Fiscal Year", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : Update operation.\n" + ex.Message, "Fiscal Year", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #region CHECK-FIELD & RESET

        public bool CheckFields()
        {

            if (txtFiscalYearName.Text == string.Empty)
            {
                MessageBox.Show("Please Enter Fiscal Year Name.", "Fiscal Year Setup", MessageBoxButton.OK, MessageBoxImage.Information);
                txtFiscalYearName.Focus();
                return false;
            }
            if (dtpStartDate.Text.ToString() == "")
            {
                MessageBox.Show("Please Select Fiscal Year Starting Date.", "Fiscal Year Setup", MessageBoxButton.OK, MessageBoxImage.Information);
                dtpStartDate.Focus();
                return false;
            }
            if (dtpEndDate.Text.ToString() == "")
            {
                MessageBox.Show("Please Select Fiscal Year End Date.", "Fiscal Year Setup", MessageBoxButton.OK, MessageBoxImage.Information);
                dtpEndDate.Focus();
                return false;
            }
            if (cmbYearStatus.Text == string.Empty)
            {
                MessageBox.Show("Please Select Fiscal Year status.", "Fiscal Year Setup", MessageBoxButton.OK, MessageBoxImage.Information);
                cmbYearStatus.Focus();
                return false;
            }
           
            return true;
        }

        private void ResetField()
        {
            txtFiscalYearName.Text = "";
            dtpStartDate.SelectedDate = DateTime.Now;
            dtpEndDate.SelectedDate = DateTime.Now;
            cmbYearStatus.Text = "";
            btnSave.Content = "Save";
        }

        #endregion

        private void dtpEndDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            DateTime startDate = Convert.ToDateTime(dtpStartDate.SelectedDate);
            DateTime endDate = Convert.ToDateTime(dtpEndDate.SelectedDate);
            if (startDate > endDate)
            {
                MessageBox.Show("Please verify your Date selection.", "Fiscal Year Setup", MessageBoxButton.OK, MessageBoxImage.Information);
                dtpEndDate.SelectedDate = DateTime.Now;
            }
        }

        private void CheckFiscalYear()
        {
            List<EFiscalYear> fiscalYearList = objBFiscalYear.CheckFiscalYear();

            if (fiscalYearList.Count == 0)
            {
                MessageBox.Show("Must Have One Fiscal Year Active. Please Activate One Fiscal Year.");
            }
            else
            {
                foreach (EFiscalYear obj in fiscalYearList)
                {
                    if (obj.F_Year_Status == "Active")
                    {
                        if (Convert.ToDateTime(obj.F_Year_End_Date) < DateTime.Today)
                        {
                            MessageBox.Show("Set Up Your New Fiscal Year. Active Date is Expired");
                        }
                    }

                }
            } 
        }

        private void editData_Click(object sender, RoutedEventArgs e)
        {
            if (lvFiscalYear.SelectedIndex > -1)
            {
                objEFiscalYearG = (EFiscalYear)lvFiscalYear.SelectedItem;
                FillFiscalYear(objEFiscalYearG);
                btnSave.Content = "Update";
            }
        }

        private void FillFiscalYear(EFiscalYear objEFiscalYear)
        {
            txtFiscalYearName.Text = objEFiscalYear.F_Year_Name;
            dtpStartDate.SelectedDate = objEFiscalYear.F_year_Start_Date;
            dtpEndDate.SelectedDate = objEFiscalYear.F_Year_End_Date;
            cmbYearStatus.Text = objEFiscalYear.F_Year_Status;
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
