using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CHART_ACC_REPORT.ENTITY;
//Author:Refat
namespace CHART_ACC_REPORT.DAL
{
    public class BalanceSheetDAL
    {
        public EBalanceSheet GetCurrentDebitCredit(long id,DateTime startDate, DateTime endDate)
        {
            ReportDataContext objDataContext = new ReportDataContext();
            EBalanceSheet objEBalanceSheet = new EBalanceSheet();

            var balance = (from c in objDataContext.SP_CURRENT_DEBIT_CREDIT(id,startDate,endDate) select c);
            foreach (var item in balance)
            {               
                objEBalanceSheet.Debit = Convert.ToDecimal(item.DEBIT);
                objEBalanceSheet.Credit = Convert.ToDecimal(item.CREDIT);                
            }

            return objEBalanceSheet;
        }

        public EBalanceSheet ProcessIncomeLoss(DateTime startDate, DateTime endDate)
        {
            EBalanceSheet objEBalanceSheet = new EBalanceSheet();
            decimal GetBalance=GetNetIncomeLoss(startDate, endDate);
            if (GetBalance > 0)
            {
                objEBalanceSheet.AccountName = "Net Income";
                objEBalanceSheet.Credit = GetBalance;
                objEBalanceSheet.AccType = "LIABILITY";
            }
            else
            {
                objEBalanceSheet.AccountName = "Net Loss";
                objEBalanceSheet.Debit = (-1) * GetBalance;
                objEBalanceSheet.AccType = "ASSET";
            }
            return objEBalanceSheet;
        }

        public decimal GetNetIncomeLoss(DateTime startDate, DateTime endDate)
        {
            ReportDataContext objDataContext = new ReportDataContext();
            foreach (var c in objDataContext.SP_GET_NET_INCOME_LOSS(startDate,endDate))
            {
                return Convert.ToDecimal(c.NET_INCOME_LOSS);
            }
            return 0;
        }

        public List<EBalanceSheet> GetSubAccount(DateTime fromDate, DateTime toDate)
        {
            ReportDataContext objDataContext = new ReportDataContext();
            List<EBalanceSheet> list = new List<EBalanceSheet>();
            foreach (var c in (from j in objDataContext.BALANCE_SHEET_CONFIGUREs select j))
            {
                EBalanceSheet obj = new EBalanceSheet();
                obj.AccountName = c.CHART_ACCOUNT.CHART_ACC_NAME;
                obj.AccountId = c.CHART_ACCOUNT.CHART_ACC_ID;
                obj.AccountCode = c.CHART_ACCOUNT.CHART_ACC_CODE;
                obj.AccType = c.CHART_ACCOUNT.CHART_ACC_PARENT_TYPE;
                obj.IsHeader = c.CHART_ACCOUNT.CHART_ACC_HEADER;
                EBalanceSheet objGetDebitCredit = GetCurrentDebitCredit(obj.AccountId, fromDate, toDate);
                obj.Debit = objGetDebitCredit.Debit;
                obj.Credit = objGetDebitCredit.Credit;
                list.Add(obj);
            }
            return list;
        }
        public List<EBalanceSheet> GetBalance(long pId, DateTime fromDate, DateTime toDate)
        {
            ReportDataContext objDataContext = new ReportDataContext();
            List<EBalanceSheet> list = new List<EBalanceSheet>();
            foreach (var c in (from j in objDataContext.CHART_ACCOUNTs where j.CHART_ACC_PARENT_ID == pId orderby j.CHART_ACC_NAME select j))
            {
                EBalanceSheet obj = new EBalanceSheet();
                obj.AccountName = c.CHART_ACC_NAME;
                obj.AccountId = c.CHART_ACC_ID;
                obj.AccountCode = c.CHART_ACC_CODE;
                obj.AccType = c.CHART_ACC_PARENT_TYPE;
                obj.IsHeader = c.CHART_ACC_HEADER;
                EBalanceSheet objGetDebitCredit = GetCurrentDebitCredit(obj.AccountId, fromDate, toDate);
                obj.Debit = objGetDebitCredit.Debit;
                obj.Credit = objGetDebitCredit.Credit;
                list.Add(obj);
            }
            return list;
        }
    }
}
