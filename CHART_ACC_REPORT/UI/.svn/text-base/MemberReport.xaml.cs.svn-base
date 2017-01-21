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

namespace CHART_ACC_REPORT.UI
{
    /// <summary>
    /// Interaction logic for MemberReport.xaml
    /// </summary>
    public partial class MemberReport : Window
    {
        public MemberReport()
        {
            InitializeComponent();
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
        #endregion

        private void btnPreview_Click(object sender, RoutedEventArgs e)
        {
            if (chkAll.IsChecked == true)
            {
                GetAllMemberInfo();
            }
            else
            {
                GetInfoMemberNoWise();
            }
        }

        private void GetInfoMemberNoWise()
        {
            try
            {
                if (txtMemberNo.Text != string.Empty)
                {
                    if (new BMemberReport().DoesExist(txtMemberNo.Text) == true)
                    {
                        List<EMemberReport> memberList = new BMemberReport().GetInfoMemberNoWise(txtMemberNo.Text);

                        CrptMemberReport objCrptMemberReport = new CrptMemberReport();
                        AccountingRptUtility.setCompanyDeifinition(objCrptMemberReport, "Members Report");
                        AccountingRptUtility.Display_report(objCrptMemberReport, memberList, this);
                    }
                    else
                    {
                        MessageBox.Show("Invalid member no.", "Member Report", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Please enter member no.", "Member Report", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Problem occur : GetInfoMemberNoWise().\n" + ex.Message, "Member Report", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void GetAllMemberInfo()
        {
            try
            {
                List<EMemberReport> memberList = new BMemberReport().GetInfoAllMember();

                CrptMemberReport objCrptMemberReport = new CrptMemberReport();
                AccountingRptUtility.setCompanyDeifinition(objCrptMemberReport, "Members Report");
                AccountingRptUtility.Display_report(objCrptMemberReport, memberList, this);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Problem occur : GetAllMemberInfo().\n" + ex.Message, "Member Report", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
