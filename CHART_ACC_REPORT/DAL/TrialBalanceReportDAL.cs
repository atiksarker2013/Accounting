using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CHART_ACC_REPORT.ENTITY;

namespace CHART_ACC_REPORT.DAL
{
    public class TrialBalanceReportDAL
    {
        ReportDataContext objDataContext;

        public List<ETrialBalanceReport> GetFisDate()
        {
            objDataContext = new ReportDataContext();
            List<ETrialBalanceReport> liFisDate = new List<ETrialBalanceReport>();
                var b = from fiscalYear in objDataContext.FISCAL_YEARs where fiscalYear.F_YEAR_STATUS=="Active" select fiscalYear;
                foreach (var item in b)
                {
                    ETrialBalanceReport objETrialBalance = new ETrialBalanceReport();

                    objETrialBalance.FiscalStartDate = Convert.ToDateTime(item.F_YEAR_START_DATE);
                    objETrialBalance.FisCalEndDate = Convert.ToDateTime(item.F_YEAR_END_DATE);
                    liFisDate.Add(objETrialBalance);
                }
            return liFisDate;
        }

        public List<EAccountType> GetAllAccountHeadId()
        {
            objDataContext = new ReportDataContext();
            List<EAccountType> accountTypeList = new List<EAccountType>(); 
            foreach (var c in (from j in objDataContext.ACCOUNT_TYPEs orderby j.ACC_NAME select j))
            {
                EAccountType obj = new EAccountType();
                obj.AccTypeName = c.ACC_NAME;
                obj.AccTypeId = c.ACC_TYPE_ID;
                accountTypeList.Add(obj);
            }
            return accountTypeList;
        }

        //public ETrialBalanceReport GetCurrentDebitCredit(long id)
        //{
        //    objDataContext = new ReportDataContext();
        //    ETrialBalanceReport objETrial = new ETrialBalanceReport();

        //    var balance = (from c in objDataContext.SP_CURRENT_DEBIT_CREDIT_DEPRICIATION(id) select c);
        //    foreach (var item in balance)
        //    {
        //        objETrial.Debit = Convert.ToDecimal(item.DEBIT);
        //        objETrial.Credit = Convert.ToDecimal(item.CREDIT);
        //    }

        //    return objETrial;
        //}
        public ETrialBalanceReport GetCurrentDebitCredit(long id, DateTime startDate, DateTime endDate)
        {
            objDataContext = new ReportDataContext();
            ETrialBalanceReport objETrial = new ETrialBalanceReport();

            var balance = (from c in objDataContext.SP_CURRENT_DEBIT_CREDIT(id,startDate,endDate) select c);
            foreach (var item in balance)
            {
                objETrial.Debit = Convert.ToDecimal(item.DEBIT);
                objETrial.Credit = Convert.ToDecimal(item.CREDIT);
            }

            return objETrial;
        }
        public List<ETrialBalanceReport> GetSubAccount(long pId, DateTime startDate, DateTime endDate)
        {
            objDataContext = new ReportDataContext();
            List<ETrialBalanceReport> list = new List<ETrialBalanceReport>();
            foreach (var c in (from j in objDataContext.CHART_ACCOUNTs where j.CHART_ACC_PARENT_ID == pId orderby j.CHART_ACC_NAME select j))
            {
                ETrialBalanceReport obj = new ETrialBalanceReport();
                obj.AccountTitle = c.CHART_ACC_NAME;
                obj.Account_Id = c.CHART_ACC_ID;
                obj.Account_Code = c.CHART_ACC_CODE;
                obj.Acc_Type = c.CHART_ACC_PARENT_TYPE;
                obj.IsHeader = c.CHART_ACC_HEADER;
                ETrialBalanceReport objGetDebitCredit=new ETrialBalanceReport();
                if (obj.Acc_Type == "REVENUE" || obj.Acc_Type == "EXPENSE")
                {
                    objGetDebitCredit = GetCurrentDebitCredit(obj.Account_Id,startDate,endDate);
                }
                else
                {
                    objGetDebitCredit = GetCurrentDebitCredit(obj.Account_Id, new ChartAccountDAL().GetCompanyFirstFisDate().FiscalStartDate, endDate);
                    //objGetDebitCredit = GetCurrentDebitCredit(obj.Account_Id);
                }                
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

        public ETrialBalanceReport ProcessIncomeLoss(DateTime startDate, DateTime endDate)
        {
            ETrialBalanceReport objETrialBalance = new ETrialBalanceReport();
            decimal GetBalance = GetNetIncomeLoss(startDate, endDate);
            if (GetBalance > 0)
            {
                objETrialBalance.AccountTitle = "Net Income";
                objETrialBalance.Credit = GetBalance;
                objETrialBalance.Acc_Type = "LIABILITY";
            }
            else
            {
                objETrialBalance.AccountTitle = "Net Loss";
                objETrialBalance.Debit = (-1) * GetBalance;
                objETrialBalance.Acc_Type = "ASSET";
            }
            return objETrialBalance;
        }
    }
}
