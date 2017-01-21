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
using System.Windows.Controls.Primitives;

namespace CHART_ACC_REPORT.UI
{
    /// <summary>
    /// Interaction logic for ReportViewer.xaml
    /// </summary>
    public partial class ReportViewer : Window
    {
        public ReportViewer()
        {
            InitializeComponent();
            var sidepanel = crystalReportsViewer.FindName("btnToggleSidePanel") as ToggleButton;
            if (sidepanel != null)
            {
                crystalReportsViewer.ViewChange += (x, y) => sidepanel.IsChecked = false;
            }

        }
        public void setReportSource(CrystalDecisions.CrystalReports.Engine.ReportDocument aReport)
        {
            this.crystalReportsViewer.ViewerCore.ReportSource = aReport;
        }
    }
}
