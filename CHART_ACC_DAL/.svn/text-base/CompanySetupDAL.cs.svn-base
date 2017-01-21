using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CHART_ACC_DAL.DBML;
using CHART_ACC_ENTITY;
using System.Configuration;
//Author:REFAT
namespace CHART_ACC_DAL
{
    public class CompanySetupDAL
    {
        public bool SaveCompanyInfo(ECompanySetup objECompany)
        {
            CHART_ACCDataContext objDataContext = new CHART_ACCDataContext();
            var newCompany = new COMPANY_SETUP
            {
                COMPANY_NAME = objECompany.CompanyName,
                ADDRESS = objECompany.Address,
                PHONE = objECompany.Phone,
                FAX = objECompany.Fax,
                EMAIL = objECompany.Email,
                WEBSITE = objECompany.Website,
                PROP_NAME = objECompany.PropName,
                LOGO = objECompany.Logo
            };
            objDataContext.COMPANY_SETUPs.InsertOnSubmit(newCompany);
            objDataContext.SubmitChanges();
            return true;
        }
        public bool UpdateCompanyInfo(ECompanySetup objECompany)
        {
            CHART_ACCDataContext objDataContext = new CHART_ACCDataContext();
            var updateCompany = objDataContext.COMPANY_SETUPs.First(c => c.ID == objECompany.Id);
            updateCompany.COMPANY_NAME = objECompany.CompanyName;
            updateCompany.ADDRESS = objECompany.Address;
            updateCompany.PHONE = objECompany.Phone;
            updateCompany.FAX = objECompany.Fax;
            updateCompany.EMAIL = objECompany.Email;
            updateCompany.WEBSITE = objECompany.Website;
            updateCompany.PROP_NAME = objECompany.PropName;
            updateCompany.LOGO = objECompany.Logo;
            objDataContext.SubmitChanges();
            return true;
        }
        public ECompanySetup GetCompanyAllInfo()
        {
            CHART_ACCDataContext objDataContext = new CHART_ACCDataContext();
            ECompanySetup objECompany = new ECompanySetup();

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
