using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CHART_ACC_REPORT.ENTITY;
using CHART_ACC_REPORT.DAL;

namespace CHART_ACC_REPORT.BLL
{
    
    public class BIncomeStatement
    {
        IncomeStatementDAL objIncomeStatementDal = new IncomeStatementDAL();
        BChartAccount objBChartOfAccount = new BChartAccount();
        DateTime fromDate, toDate;
        decimal debitBalance = 0, creditbalance = 0;
        List<EIncomeStatement> listIncomeStatement = new List<EIncomeStatement>();

        public List<EIncomeStatement> GetIncomeStatement(DateTime startDate, DateTime endDate)
        {
            listIncomeStatement.Clear();
            fromDate = startDate;
            toDate = endDate;         

            foreach (EIncomeStatement obj in objIncomeStatementDal.GetSubAccount(fromDate, toDate))
            {
                if (obj.IsHeader == "Yes")
                {
                    GetBalance(obj.AccountId);
                    obj.Debit += debitBalance;
                    obj.Credit += creditbalance;                
                }
                listIncomeStatement.Add(obj);
                debitBalance = 0; creditbalance = 0;
            }
            return listIncomeStatement;
        }
        public decimal GetNetIncomeLoss(DateTime startDate, DateTime endDate)
        {
            return objIncomeStatementDal.GetNetIncomeLoss(startDate, endDate);
        }
        private void GetBalance(long pId)
        {
            foreach (EIncomeStatement obj in objIncomeStatementDal.GetBalance(pId, fromDate, toDate))
            {
                debitBalance += Convert.ToDecimal(obj.Debit);
                creditbalance += Convert.ToDecimal(obj.Credit);
                if (obj.IsHeader == "Yes")
                {
                    GetBalance(obj.AccountId);
                }
            }
        }

        public List<ETrialBalanceReport> GetFisDate()
        {
            return objIncomeStatementDal.GetFisDate();
        }

        public decimal GetDepriciationForCurrentYear()
        {
            return objIncomeStatementDal.GetDepriciationForCurrentYear();
        }

    }
}
