using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CHART_ACC_DAL;
using CHART_ACC_ENTITY;

namespace CHART_ACC_BLL
{
    public class BFiscalYear
    {
        FiscalYearDAL objFiscalYearDal = new FiscalYearDAL();

        public bool SaveFiscalYear(EFiscalYear objEFiscalYear)
        {
            return objFiscalYearDal.SaveFiscalYear(objEFiscalYear);
        }
        public List<EFiscalYear> CheckFiscalYear()
        {
            return objFiscalYearDal.CheckFiscalYear();
        }
        public List<EFiscalYear> GetAllFiscalYear()
        {
            return objFiscalYearDal.GetAllFiscalYear();
        }
        public bool UpdateFiscalYear(EFiscalYear objEFiscalYear)
        {
            return objFiscalYearDal.UpdateFiscalYear(objEFiscalYear);
        }
        public decimal GetDepriciationForCurrentYear()
        {
            return objFiscalYearDal.GetDepriciationForCurrentYear();
        }

    }
}
