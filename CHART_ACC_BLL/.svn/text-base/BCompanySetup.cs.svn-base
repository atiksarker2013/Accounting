using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CHART_ACC_ENTITY;
using CHART_ACC_DAL;
//Author:REFAT
namespace CHART_ACC_BLL
{
    public class BCompanySetup
    {
        CompanySetupDAL objCompanyDal = new CompanySetupDAL();

        public bool SaveCompanyInfo(ECompanySetup objECompany)
        {
            return objCompanyDal.SaveCompanyInfo(objECompany);
        }
        public bool UpdateCompanyInfo(ECompanySetup objECompany)
        {
            return objCompanyDal.UpdateCompanyInfo(objECompany);
        }
        public ECompanySetup GetCompanyAllInfo()
        {
            return objCompanyDal.GetCompanyAllInfo();
        }
    }
}
