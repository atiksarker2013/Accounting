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
    /// Interaction logic for MotherAccountStatement.xaml
    /// </summary>
    public partial class HeaderAccountStatement : Window
    {
        BHeaderAccount objBHeaderAccount = new BHeaderAccount();

        public HeaderAccountStatement()
        {
            InitializeComponent();
            LoadHeaderComboBox();
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

        private void LoadHeaderComboBox()
        {
            cmbParentAccountName.Items.Clear();

            try
            {
                List<EAccountStatement> ListHeadAccount = objBHeaderAccount.GetHeaderAccountName();
                foreach (EAccountStatement objHeaderAccount in ListHeadAccount)
                {
                    cmbParentAccountName.Items.Add(objHeaderAccount);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error to get head account name.\n" + ex.Message,"Account Statement [Header]",MessageBoxButton.OK,MessageBoxImage.Information);
            }
        }

        private void btnPreview_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cmbParentAccountName.Text != string.Empty)
                {
                    List<EAccountStatement> listECreateAccountStatement = objBHeaderAccount.GetAccStatement(cmbParentAccountName.Text, (cmbParentAccountName.SelectedItem as EAccountStatement).AccountId);
                    if (listECreateAccountStatement.Count > 0)
                    {
                        CrptAccountStatement objCrptAccountStatement = new CrptAccountStatement();
                        AccountingRptUtility.SetDate(objCrptAccountStatement, listECreateAccountStatement[0].DisplayDate, listECreateAccountStatement[listECreateAccountStatement.Count - 1].DisplayDate);
                        AccountingRptUtility.setCompanyDeifinition(objCrptAccountStatement, "Account Statement [Header]");
                        AccountingRptUtility.Display_report(objCrptAccountStatement, listECreateAccountStatement, this);
                    }
                    else
                    {
                        MessageBox.Show("You have no transaction on this account.", "Account Statement [Header]", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Select your account name.", "Account Statement [Header]", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in get transaction.\n" + ex.Message , "Account Statement [Header]", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
