using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CHART_ACC_REPORT.ENTITY;

namespace CHART_ACC_REPORT.DAL
{
    public class IncomeStatementDAL
    {
        ReportDataContext objDataContext = new ReportDataContext();

        public EIncomeStatement GetCurrentDebitCredit(long id, DateTime startDate, DateTime endDate)
        {
            ReportDataContext objDataContext = new ReportDataContext();
            EIncomeStatement objEIncomeStatement = new EIncomeStatement();

            var balance = (from c in objDataContext.SP_CURRENT_DEBIT_CREDIT(id, startDate, endDate) select c);
            foreach (var item in balance)
            {
                objEIncomeStatement.Debit = Convert.ToDecimal(item.DEBIT);
                objEIncomeStatement.Credit = Convert.ToDecimal(item.CREDIT);
            }

            return objEIncomeStatement;
        }
        public List<EIncomeStatement> GetSubAccount(DateTime fromDate, DateTime toDate)
        {
            ReportDataContext objDataContext = new ReportDataContext();
            List<EIncomeStatement> list = new List<EIncomeStatement>();
            foreach (var c in (from j in objDataContext.INCOME_SHEET_CONFIGUREs select j))
            {
                EIncomeStatement obj = new EIncomeStatement();
                obj.AccountName = c.CHART_ACCOUNT.CHART_ACC_NAME;
                obj.AccountId = c.CHART_ACCOUNT.CHART_ACC_ID;
                obj.AccountCode = c.CHART_ACCOUNT.CHART_ACC_CODE;
                obj.AccType = c.CHART_ACCOUNT.CHART_ACC_PARENT_TYPE;
                obj.IsHeader = c.CHART_ACCOUNT.CHART_ACC_HEADER;
                EIncomeStatement objGetDebitCredit = GetCurrentDebitCredit(obj.AccountId, fromDate, toDate);
                obj.Debit = objGetDebitCredit.Debit;
                obj.Credit = objGetDebitCredit.Credit;
                list.Add(obj);
            }
            return list;
        }

        public List<EIncomeStatement> GetBalance(long pId, DateTime fromDate, DateTime toDate)
        {
            ReportDataContext objDataContext = new ReportDataContext();
            List<EIncomeStatement> list = new List<EIncomeStatement>();
            foreach (var c in (from j in objDataContext.CHART_ACCOUNTs where j.CHART_ACC_PARENT_ID == pId orderby j.CHART_ACC_NAME select j))
            {
                EIncomeStatement obj = new EIncomeStatement();
                obj.AccountName = c.CHART_ACC_NAME;
                obj.AccountId = c.CHART_ACC_ID;
                obj.AccountCode = c.CHART_ACC_CODE;
                obj.AccType = c.CHART_ACC_PARENT_TYPE;
                obj.IsHeader = c.CHART_ACC_HEADER;
                EIncomeStatement objGetDebitCredit = GetCurrentDebitCredit(obj.AccountId, fromDate, toDate);
                obj.Debit = objGetDebitCredit.Debit;
                obj.Credit = objGetDebitCredit.Credit;
                list.Add(obj);
            }
            return list;
        }

        public decimal GetNetIncomeLoss(DateTime startDate, DateTime endDate)
        {
            ReportDataContext objDataContext = new ReportDataContext();
            foreach (var c in objDataContext.SP_GET_NET_INCOME_LOSS(startDate, endDate))
            {
                return Convert.ToDecimal(c.NET_INCOME_LOSS);
            }
            return 0;
        }

        public decimal GetDepriciationForCurrentYear()
        {
            decimal totalDepriciation = 0;
            ReportDataContext objDataContext = new ReportDataContext();

            foreach (var c in objDataContext.SP_GET_ALL_DEPRICIATION())
            {
                totalDepriciation +=Convert.ToDecimal(c.DEP_VALUE);
            }
            return totalDepriciation;
        }

        public List<ETrialBalanceReport> GetFisDate()
        {
            objDataContext = new ReportDataContext();
            List<ETrialBalanceReport> liFisDate = new List<ETrialBalanceReport>();
            var b = from fiscalYear in objDataContext.FISCAL_YEARs where fiscalYear.F_YEAR_STATUS == "Active" select fiscalYear;
            foreach (var item in b)
            {
                ETrialBalanceReport objETrialBalance = new ETrialBalanceReport();

                objETrialBalance.FiscalStartDate = Convert.ToDateTime(item.F_YEAR_START_DATE);
                objETrialBalance.FisCalEndDate = Convert.ToDateTime(item.F_YEAR_END_DATE);
                liFisDate.Add(objETrialBalance);
            }
            return liFisDate;
        }
    }
}
