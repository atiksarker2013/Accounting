using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CHART_ACC_REPORT.ENTITY;
//Author:REFAT
namespace CHART_ACC_REPORT.DAL
{
    public class GLDAL
    {
        decimal balance = 0;
        public List<EGL> GetSubAccount(long parentId, DateTime fromDate, DateTime toDate, bool IsCompanyStartToCB)
        {
            ReportDataContext objDataContext = new ReportDataContext();
            List<EGL> list = new List<EGL>();
            foreach (var c in (from j in objDataContext.CHART_ACCOUNTs where j.CHART_ACC_PARENT_ID == parentId orderby j.CHART_ACC_NAME select j))
            {
                EGL obj = new EGL();
                obj.SubAccName = c.CHART_ACC_NAME;
                obj.SubAccId = c.CHART_ACC_ID;
                obj.SubAccCode = c.CHART_ACC_CODE;
                obj.ParentAccId = Convert.ToInt64(c.CHART_ACC_PARENT_ID);
                obj.AccHeader = c.CHART_ACC_HEADER;
                GetBalance(obj.SubAccId, fromDate, toDate,IsCompanyStartToCB);
                obj.CurrentBalance = (GetCurrentBalanceOfAccount(obj.SubAccId, fromDate,toDate, IsCompanyStartToCB) + balance).ToString();
                balance = 0;
                list.Add(obj);
            }
            return list;
        }
        private decimal GetCurrentBalanceOfAccount(long accId,DateTime fromDate ,DateTime toDate, bool IsCompanyStartToCB)
        {
            ReportDataContext objDataContext = new ReportDataContext();
            decimal openBalance = 0;
            decimal amount = 0;
            if (IsCompanyStartToCB)
            {
                foreach (var c in objDataContext.SP_CURRENT_BALANCE(accId, toDate))
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
            else
            {
                foreach (var c in (from j in objDataContext.SP_CURRENT_BALANCE(accId, toDate) where j.JV_DATE>=fromDate select j))
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
        private void GetBalance(long pId, DateTime fromDate, DateTime toDate,bool IsCompanyStartToCB)
        {
            foreach (EGL obj in GetAccountforBalance(pId, fromDate, toDate,IsCompanyStartToCB))
            {
                balance += Convert.ToDecimal(obj.CurrentBalance);
                if (obj.AccHeader == "Yes")
                {
                    GetBalance(obj.SubAccId,fromDate,toDate,IsCompanyStartToCB);
                }
            }
        }

        public List<EGL> GetAccountforBalance(long pId, DateTime fromDate, DateTime toDate,bool IsCompanyStartToCB)
        {
            ReportDataContext objDataContext = new ReportDataContext();
            List<EGL> list = new List<EGL>();
            foreach (var c in (from j in objDataContext.CHART_ACCOUNTs where j.CHART_ACC_PARENT_ID == pId orderby j.CHART_ACC_NAME select j))
            {
                EGL obj = new EGL();
                obj.SubAccName = c.CHART_ACC_NAME;
                obj.SubAccId = c.CHART_ACC_ID;
                obj.SubAccCode = c.CHART_ACC_CODE;
                obj.ParentAccId = Convert.ToInt64(c.CHART_ACC_PARENT_ID);
                obj.AccHeader = c.CHART_ACC_HEADER;
                obj.CurrentBalance = GetCurrentBalanceOfAccount(obj.SubAccId,fromDate ,toDate,IsCompanyStartToCB).ToString();
                list.Add(obj);
            }
            return list;
        }

        public EGL GetAccountInfo(long accId, DateTime fromDate, DateTime toDate, bool IsCompanyStartToCB)
        {
            ReportDataContext objDataContext = new ReportDataContext();
            EGL obj = new EGL();
            foreach (var c in (from j in objDataContext.CHART_ACCOUNTs where j.CHART_ACC_ID == accId select j))
            {
                obj.SubAccName = c.CHART_ACC_NAME;
                obj.SubAccId = c.CHART_ACC_ID;
                obj.SubAccCode = c.CHART_ACC_CODE;
                obj.AccHeader = c.CHART_ACC_HEADER;
                obj.ParentAccId = Convert.ToInt64(c.CHART_ACC_PARENT_ID);
                obj.TypeTotalBalance = GetCurrentBalanceOfAccount(obj.SubAccId,fromDate ,toDate,IsCompanyStartToCB);
            }
            return obj;
        }
    }
}
