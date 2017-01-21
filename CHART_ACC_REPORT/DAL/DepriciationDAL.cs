using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CHART_ACC_REPORT.ENTITY;
using CHART_ACC_REPORT.DAL;

namespace CHART_ACC_REPORT.DAL
{
    public class DepriciationDAL
    {
        ReportDataContext objDataContext;

        public List<EFiscalYear> GetAllFiscalYear()
        {
            objDataContext = new ReportDataContext();
            List<EFiscalYear> fiscalYearList = new List<EFiscalYear>();

            var fsYear = from fiscalYear in objDataContext.FISCAL_YEARs select fiscalYear;
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

        public List<EDepriciationReport> GetDepriciationDateWise(DateTime StartDate, DateTime EndDate)
        {
            objDataContext = new ReportDataContext();

            List<EDepriciationReport> ListDepriciationReport = new List<EDepriciationReport>();

            foreach (var c in objDataContext.SP_GET_DEPRICIATION_DATE_WISE(StartDate, EndDate))
            {
                EDepriciationReport objDepriciationReport = new EDepriciationReport();

                objDepriciationReport.Acc_Id = c.ACCOUNT_ID;
                objDepriciationReport.Debit_Acc_Id =Convert.ToInt64(c.DEBIT_ACC_ID);
                objDepriciationReport.Acc_Name = c.ACCOUNT_NAME;
                objDepriciationReport.Access_By = c.ACCESS_BY;
                objDepriciationReport.Access_Date =Convert.ToDateTime(c.ACCESS_DATE);
                objDepriciationReport.Dep_Percentage =Convert.ToDecimal(c.DEP_PERCENTAGE);
                objDepriciationReport.Dep_Value =Convert.ToDecimal(c.DEP_VALUE);
                objDepriciationReport.Original_Cost =Convert.ToDecimal(c.ORIGINAL_COST);
                objDepriciationReport.New_Original_Cost =Convert.ToDecimal(c.NEW_ORIGINAL_COST);
                ListDepriciationReport.Add(objDepriciationReport);
            }
            return ListDepriciationReport;
        }

        public List<EDepriciationReport> GetDepriciationALL()
        {
            objDataContext = new ReportDataContext();

            List<EDepriciationReport> ListDepriciationReport = new List<EDepriciationReport>();

            foreach (var c in objDataContext.SP_GET_DEPRICIATION_ALL())
            {
                EDepriciationReport objDepriciationReport = new EDepriciationReport();

                objDepriciationReport.Acc_Id = c.ACCOUNT_ID;
                objDepriciationReport.Acc_Name = c.ACCOUNT_NAME;
                objDepriciationReport.Access_By = c.ACCESS_BY;
                objDepriciationReport.Access_Date = Convert.ToDateTime(c.ACCESS_DATE);
                objDepriciationReport.Dep_Percentage = Convert.ToDecimal(c.DEP_PERCENTAGE);
                objDepriciationReport.Dep_Value = Convert.ToDecimal(c.DEP_VALUE);
                objDepriciationReport.Original_Cost = Convert.ToDecimal(c.ORIGINAL_COST);
                objDepriciationReport.New_Original_Cost = Convert.ToDecimal(c.NEW_ORIGINAL_COST);
                ListDepriciationReport.Add(objDepriciationReport);
            }
            return ListDepriciationReport;
        }

        // For Get Current Year Value--

        public decimal GetCurrentBalanceOfAccount(long accId, DateTime fromDate)
        {
            objDataContext = new ReportDataContext();
            decimal openBalance = 0;
            decimal amount = 0;
            foreach (var c in objDataContext.SP_CURRENT_BALANCE(accId, fromDate))
            {
                if (c.JV_DATE == null || c.JV_DATE < fromDate.Date)
                {

                    if (c.OP_DR > c.OP_CR)
                    {
                        openBalance = Convert.ToDecimal(c.OP_DR);
                    }
                    else
                    {
                        openBalance = Convert.ToDecimal(-1 * c.OP_CR);
                    }
                    amount += Convert.ToDecimal(c.JV_DEBIT_AMOUNT - c.JV_CREDIT_AMOUNT);
                }
            }
            amount = openBalance + amount;

            return amount;
        }

        public EDep GetCurrentDebitCredit(long id,DateTime fromDate, DateTime toDate)
        {
            objDataContext = new ReportDataContext();
            EDep objEDepriciation = new EDep();

            var balance = (from c in objDataContext.SP_CURRENT_DEBIT_CREDIT(id, fromDate,toDate) select c);
            foreach (var item in balance)
            {
                objEDepriciation.Debit = Convert.ToDecimal(item.DEBIT);
                objEDepriciation.Credit = Convert.ToDecimal(item.CREDIT);
            }

            return objEDepriciation;
        }
        public List<EDep> GetSubAccount(long pId, DateTime fromDate, DateTime toDate)
        {
            objDataContext = new ReportDataContext();
            List<EDep> list = new List<EDep>();
            foreach (var c in (from j in objDataContext.CHART_ACCOUNTs where j.CHART_ACC_PARENT_ID == pId orderby j.CHART_ACC_NAME select j))
            {
                EDep obj = new EDep();
                obj.AccountName = c.CHART_ACC_NAME;
                obj.AccountId = c.CHART_ACC_ID;
                obj.AccountCode = c.CHART_ACC_CODE;
                obj.AccType = c.CHART_ACC_PARENT_TYPE;
                obj.IsHeader = c.CHART_ACC_HEADER;
                EDep objGetDebitCredit = GetCurrentDebitCredit(obj.AccountId,fromDate,toDate);
                obj.Debit = objGetDebitCredit.Debit;
                obj.Credit = objGetDebitCredit.Credit;
                list.Add(obj);
            }
            return list;
        }
        public long GetFixedAssetId()
        {
            objDataContext = new ReportDataContext();
            foreach (var c in (from j in objDataContext.CHART_ACCOUNTs where j.CHART_ACC_NAME == "FIXED ASSETS" orderby j.CHART_ACC_NAME select j))
            {
                EChartOfAccount obj = new EChartOfAccount();
                return obj.SubAccId = c.CHART_ACC_ID;
            }
            return 0;
        }

        public decimal GetUptoDepriciation(long DebitId,DateTime fromDate)
        {
            decimal TotalDebitAmount = 0;
            objDataContext = new ReportDataContext();

            var query = from j in objDataContext.JOURNAL_VOUCHARs where j.JV_ACCOUNT_ID == DebitId && j.JV_DATE < fromDate select j;
            foreach(var c in query)
            {
                TotalDebitAmount +=Convert.ToDecimal(c.JV_DEBIT_AMOUNT);
            }
            return TotalDebitAmount;
         
        }
    }
}
