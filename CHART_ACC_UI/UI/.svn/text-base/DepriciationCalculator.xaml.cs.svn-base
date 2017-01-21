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
    /// Interaction logic for DepriciationCalculator.xaml
    /// </summary>
    public partial class DepriciationCalculator : Window
    {
        BDepriciation objBDepriciation = new BDepriciation();
        BChartOfAccount objBChartOfAccount = new BChartOfAccount();
        BJournalVouchar objBJournalVouchar = new BJournalVouchar();
        BDepriciationSetup objBDepriciationSetup = new BDepriciationSetup();

        string userName = "Amin";
        public DepriciationCalculator()
        {
            InitializeComponent();
            GetJournalVoucherID();
            LoadListView();
        }

        #region Windows Function
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

        string journalID;
        private void GetJournalVoucherID()
        {
            try
            {
                journalID = objBJournalVouchar.GetJVoucherLastID();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem occur to get Journal voucher new id.\n" + ex.Message, "Depriciation", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadListView()
        {
            lvDepriciationCalculator.Items.Clear();
            try
            {
                List<EDepriciation> listDepriciation = objBDepriciation.GetAllDepriciationSetup();
                foreach (EDepriciation objDS in listDepriciation)
                {
                    objDS.Original_Cost = objBDepriciation.FixedAssetsBalance(objDS.AccountId);
                    objDS.Dep_Value = (objDS.Original_Cost * objDS.Dep_Percentage) / 100;
                    objDS.New_Original_Cost = objDS.Original_Cost - objDS.Dep_Value;
                    objDS.Journal_Id = journalID;
                    objDS.Journal_Notes = "Depriciation Purpose";
                    objDS.Access_By = userName;
                    objDS.Access_Date = DateTime.Now;
                    List<EChartOfAccount> liChartDebit = objBDepriciation.GetChartAccountIdWise(objDS.Debit_Id);
                    foreach (var c in liChartDebit)
                    {
                        objDS.Debit_Account_Name = c.SubAccName;
                        objDS.Debit_AccountCode = c.SubAccCode;
                        objDS.Debit_Amount = objDS.Dep_Value;
                    }
                    List<EChartOfAccount> liChartCredit = objBDepriciation.GetChartAccountIdWise(objDS.Credit_Id);
                    foreach (var c in liChartCredit)
                    {
                        objDS.Credit_Account_Name = c.SubAccName;
                        objDS.Credit_AccountCode = c.SubAccCode;
                        objDS.Credit_Amount = objDS.Dep_Value;
                    }
                    lvDepriciationCalculator.Items.Add(objDS);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in get depriciation data.\n" + ex.Message, "Depriciation", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        decimal originalCost = 0;
        decimal depriciationPercentage = 0;
        decimal depriciationValue = 0;
        decimal newOriginalCost = 0;

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            List<EDepriciation> listDepriciation = objBDepriciation.GetDepriciationForCurrentYear();
            if (listDepriciation.Count > 0)
            {
                if (MessageBox.Show("Depriciation of this year has already saved.\nAre you wnat to re-saved depriciation?", "Depriciation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    objBDepriciation.DeleteDepriciation();
                    foreach (var c in listDepriciation)
                    {
                        objBDepriciation.DeleteJournalOfDepriciation(c.Journal_Id);
                        GetJournalVoucherID();
                    }
                    if (SaveDepriciation() == true)
                    {
                        MessageBox.Show("Depriciation re-saved successfully.", "Depriciation", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }              
            }
            else
            {
                if (SaveDepriciation() == true)
                {
                    MessageBox.Show("Depriciation saved successfully.", "Depriciation", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private bool SaveDepriciation()
        {
            bool stat = false;
            for (int i = 0; i < lvDepriciationCalculator.Items.Count; i++)
            {
                EDepriciation objDepriciation = (EDepriciation)lvDepriciationCalculator.Items[i];

                stat = objBDepriciation.SaveDepriciation(objDepriciation);

                if (stat == true)
                {
                    objBDepriciation.SaveJournalVoucharDR(objDepriciation);
                    objBDepriciation.SaveJournalVoucharCR(objDepriciation);
                }
            }
            return stat;
        }
    }
}
