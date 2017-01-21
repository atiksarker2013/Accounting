using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CHART_ACC_REPORT.ENTITY;
//Author:REFAT
namespace CHART_ACC_REPORT.DAL
{
    public class CompanyDAL
    {
        public ECompany GetCompanyAllInfo()
        {
            ReportDataContext objDataContext = new ReportDataContext();
            ECompany objECompany = new ECompany();

            var query = from j in objDataContext.COMPANY_SETUPs select j;
            foreach (var company in query)
            {
                objECompany.Id = company.ID;
                objECompany.CompanyName = company.COMPANY_NAME;
                objECompany.Address = company.ADDRESS;
                objECompany.Phone = company.PHONE;
                objECompany.Fax = company.FAX;
                objECompany.Email = company.EMAIL;
                objECompany.Website = company.WEBSITE;
                objECompany.PropName = company.PROP_NAME;
                objECompany.Logo = company.LOGO.ToArray();
            }
            return objECompany;
        }
    }
}
