using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using CHART_ACC_REPORT.BLL;
using CHART_ACC_REPORT.ENTITY;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using CrystalDecisions.CrystalReports.Engine;
using CHART_ACC_REPORT.UI;

namespace CHART_ACC_REPORT.UTILITY
{
    public class AccountingRptUtility
    {
        public static void Display_report(ReportClass rc, object objDataSource, Window parentWindow)
        {
            try
            {
                rc.SetDataSource(objDataSource);
                ReportViewer Viewer = new ReportViewer();
                Viewer.setReportSource(rc);                
                Viewer.ShowDialog();                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void DisplayCompanyInfo(ReportClass rc, object objDataSource, Window parentWindow)
        {
            try
            {
                rc.SetDataSource(objDataSource);
                ReportViewer Viewer = new ReportViewer();
                Viewer.setReportSource(rc);
                Viewer.ShowDialog();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void DisplayReportWithDate(ReportClass rc, object objDataSource, DateTime fromDate, DateTime toDate, Window parentWindow)
        {
            try
            {
                rc.SetDataSource(objDataSource);
                ReportViewer Viewer = new ReportViewer();
                Viewer.setReportSource(rc);
                Viewer.ShowDialog();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void SetDate(ReportClass rptDoc, string fromDate, string toDate)
        {
            string fDate = fromDate.ToString();
            string tDate = toDate.ToString();
            rptDoc.DataDefinition.FormulaFields["fromDate"].Text = "'" + fDate + "'";
            //rptDoc.DataDefinition.FormulaFields["To"].Text = "'To'";
            rptDoc.DataDefinition.FormulaFields["toDate"].Text = "'" + tDate + "'";
        }

        public static void setCompanyDeifinition(ReportClass rptDoc, string REPORT_NAME)
        {
            string address = "", phone = "", fax = "", web = "", Email = "";
            BCompany objBCompany = new BCompany();
            ECompany objECompany = objBCompany.GetCompanyAllInfo();

            rptDoc.DataDefinition.FormulaFields["HeaderName"].Text = "'" + REPORT_NAME + "'";
            rptDoc.DataDefinition.FormulaFields["CompanyName"].Text = "'" + objECompany.CompanyName + "'";
            if (string.IsNullOrEmpty(objECompany.Address) == false)
            {
                address = "Address : " + objECompany.Address;
            }
            if (string.IsNullOrEmpty(objECompany.Phone) == false)
            {
                phone = "Phone : " + objECompany.Phone;
            }
            if (string.IsNullOrEmpty(objECompany.Fax) == false)
            {
                fax = "Fax : " + objECompany.Fax;
            }
            if (string.IsNullOrEmpty(objECompany.Website) == false)
            {
                web = "Web : " + objECompany.Website;
            }
            if (string.IsNullOrEmpty(objECompany.Email) == false)
            {
                Email = "Email : " + objECompany.Email;
            }

            rptDoc.DataDefinition.FormulaFields["Address"].Text = "'" + address + "'";
            rptDoc.DataDefinition.FormulaFields["Phone"].Text = "'" + phone + "'";
            rptDoc.DataDefinition.FormulaFields["Fax"].Text = "'" + fax + "'";
            rptDoc.DataDefinition.FormulaFields["Email"].Text = "'" + Email + "'";
            rptDoc.DataDefinition.FormulaFields["Website"].Text = "'" + web + "'";
            rptDoc.DataDefinition.FormulaFields["footer"].Text = "'Computer generated report. Powered by : IECL. '";

            byte[] Logo = objECompany.Logo.ToArray();
            string directory = Directory.GetCurrentDirectory();
            directory = directory + "\\Logo.jpg";
            MemoryStream ms = new MemoryStream(Logo);
            Image img = Image.FromStream(ms);
            img.Save(directory, ImageFormat.Jpeg);
            rptDoc.DataDefinition.FormulaFields["Logo"].Text = "'" + directory + "'";
        }
    }
}
