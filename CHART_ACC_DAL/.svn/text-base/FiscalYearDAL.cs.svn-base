using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CHART_ACC_DAL.DBML;
using CHART_ACC_ENTITY;

namespace CHART_ACC_DAL
{
    public class FiscalYearDAL
    {
        CHART_ACCDataContext objDataContext = new CHART_ACCDataContext();

        public bool SaveFiscalYear(EFiscalYear objEFiscalYear)
        {
            var fiscalYear = new FISCAL_YEAR 
            { 
             ACCESS_BY =objEFiscalYear.Access_By,
             ACCESS_DATE=DateTime.Now,
             F_YEAR_NAME =objEFiscalYear.F_Year_Name,
             F_YEAR_START_DATE =objEFiscalYear.F_year_Start_Date ,
             F_YEAR_END_DATE =objEFiscalYear.F_Year_End_Date,
             F_YEAR_STATUS=objEFiscalYear.F_Year_Status            
            };
            objDataContext.FISCAL_YEARs.InsertOnSubmit(fiscalYear);
            objDataContext.SubmitChanges();
            return true;
        }

        public List<EFiscalYear> CheckFiscalYear()
        {
            objDataContext = new CHART_ACCDataContext();
            List<EFiscalYear> fiscalYearList = new List<EFiscalYear>();
            EFiscalYear objEFiscalYear = new EFiscalYear();

            var fsYear = from fiscalYear in objDataContext.FISCAL_YEARs where fiscalYear.F_YEAR_STATUS=="Active" select fiscalYear;
            foreach (var c in fsYear)
            {
                objEFiscalYear.F_Year_Id = c.F_YEAR_ID;
                objEFiscalYear.F_Year_Status = c.F_YEAR_STATUS;
                objEFiscalYear.F_Year_End_Date = c.F_YEAR_END_DATE;
                fiscalYearList.Add(objEFiscalYear);
            }
            return fiscalYearList;
        }

        public List<EFiscalYear> GetAllFiscalYear()
        {
            objDataContext = new CHART_ACCDataContext();
            List<EFiscalYear> fiscalYearList = new List<EFiscalYear>();            

            var fsYear = from fiscalYear in objDataContext.FISCAL_YEARs  select fiscalYear;
            foreach (var c in fsYear)
            {
                EFiscalYear objEFiscalYear = new EFiscalYear();
                objEFiscalYear.Access_By = c.ACCESS_BY;
                objEFiscalYear.F_Year_Name = c.F_YEAR_NAME;
                objEFiscalYear.F_Year_Id = c.F_YEAR_ID;
                objEFiscalYear.F_Year_Status = c.F_YEAR_STATUS;
                objEFiscalYear.F_year_Start_Date = c.F_YEAR_START_DATE;
                objEFiscalYear.F_Year_End_Date = c.F_YEAR_END_DATE;
                fiscalYearList.Add(objEFiscalYear);
            }
            return fiscalYearList;         
        }

        public bool UpdateFiscalYear(EFiscalYear objEFiscalYear)
        {
            objDataContext = new CHART_ACCDataContext();
            var updateYear = objDataContext.FISCAL_YEARs.First(c => c.F_YEAR_ID == objEFiscalYear.F_Year_Id);
            updateYear.F_YEAR_NAME = objEFiscalYear.F_Year_Name;
            updateYear.F_YEAR_START_DATE = objEFiscalYear.F_year_Start_Date;
            updateYear.F_YEAR_END_DATE = objEFiscalYear.F_Year_End_Date;
            updateYear.F_YEAR_STATUS = objEFiscalYear.F_Year_Status;
            updateYear.ACCESS_BY = objEFiscalYear.Access_By;
            updateYear.ACCESS_DATE = DateTime.Now;
            objDataContext.SubmitChanges();
            return true;
        }

        public decimal GetDepriciationForCurrentYear()
        {
            decimal totalDepriciation = 0;
            CHART_ACCDataContext objDataContext = new CHART_ACCDataContext();

            foreach (var c in objDataContext.SP_GET_ALL_DEPRICIATION())
            {
                totalDepriciation += Convert.ToDecimal(c.DEP_VALUE);
            }
            return totalDepriciation;
        }
    }
}
