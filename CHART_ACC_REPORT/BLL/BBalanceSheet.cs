using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CHART_ACC_REPORT.DAL;
using CHART_ACC_REPORT.ENTITY;

namespace CHART_ACC_REPORT.BLL
{
    public class BBalanceSheet
    {
        BalanceSheetDAL objBalanceSheetDAL = new BalanceSheetDAL();
        BChartAccount objBChartOfAccount = new BChartAccount();
        DateTime fromDate, toDate;
        decimal debitBalance = 0,creditbalance=0;        

        public List<EBalanceSheet> GetBalanceSheet(DateTime startDate,DateTime endDate)
        {
            List<EBalanceSheet> listBalanceSheet = new List<EBalanceSheet>();            
            fromDate = startDate;
            toDate = endDate;
            foreach (EBalanceSheet obj in objBalanceSheetDAL.GetSubAccount(fromDate, toDate))
            {
                if (obj.IsHeader == "Yes")
                {
                    GetBalance(obj.AccountId);
                    obj.Debit += debitBalance;
                    obj.Credit += creditbalance;
                }
                if (obj.AccType == "ASSET")
                {
                    obj.Debit = obj.Debit - obj.Credit;
                }
                else
                {
                    obj.Credit = obj.Credit - obj.Debit;
                }
                listBalanceSheet.Add(obj);
                debitBalance = 0; creditbalance = 0;
            }
            listBalanceSheet.Add(objBalanceSheetDAL.ProcessIncomeLoss(fromDate, toDate));
            return listBalanceSheet;
        }
        private void GetBalance(long pId)
        {
            foreach (EBalanceSheet obj in objBalanceSheetDAL.GetBalance(pId, fromDate, toDate))
            {
                debitBalance += Convert.ToDecimal(obj.Debit);
                creditbalance += Convert.ToDecimal(obj.Credit);
                if (obj.IsHeader == "Yes")
                {
                    GetBalance(obj.AccountId);
                }
            }
        }
    }
}
