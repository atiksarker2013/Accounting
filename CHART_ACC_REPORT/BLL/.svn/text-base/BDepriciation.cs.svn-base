using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CHART_ACC_REPORT.DAL;
using CHART_ACC_REPORT.ENTITY;

namespace CHART_ACC_REPORT.BLL
{
    public class BDepriciation
    {
        DepriciationDAL objDepriciationDal = new DepriciationDAL();
        public List<EFiscalYear> GetAllFiscalYear()
        {
            return objDepriciationDal.GetAllFiscalYear();
        }
        public List<EDepriciationReport> GetDepriciationDateWise(DateTime StartDate, DateTime EndDate)
        {
            List<EDepriciationReport> listDepriciationReport = objDepriciationDal.GetDepriciationDateWise(StartDate, EndDate);

            foreach (var obj in listDepriciationReport)
            {
                obj.CurrentYear_Cost = FixedAssetsBalance(obj.Acc_Id, StartDate, EndDate);
                obj.UpTo_Year_Value = objDepriciationDal.GetCurrentBalanceOfAccount(obj.Acc_Id, StartDate);
                obj.UpTo_Year_Depriciation = objDepriciationDal.GetUptoDepriciation(obj.Debit_Acc_Id,StartDate);
            }
            return listDepriciationReport;
        }
        public List<EDepriciationReport> GetDepriciationALL()
        {
            return objDepriciationDal.GetDepriciationALL();
        }

        // For Current Year Value


        decimal amnt = 0;
        public void GetBalanceFixedAsset(long pId, DateTime fromDate, DateTime toDate)
        {
            foreach (EDep obj in objDepriciationDal.GetSubAccount(pId,fromDate,toDate))
            {
                amnt += obj.Debit - obj.Credit;
                if (obj.IsHeader == "Yes")
                {
                    GetBalanceFixedAsset(obj.AccountId,fromDate,toDate);
                }
            }
        }
        public decimal FixedAssetsBalance(long pId, DateTime fromDate, DateTime toDate)
        {
            decimal cost = 0;
            EDep obj = objDepriciationDal.GetCurrentDebitCredit(pId,fromDate,toDate);

            GetBalanceFixedAsset(pId,fromDate,toDate);
            cost = obj.Debit - obj.Credit;
            cost += amnt;
            amnt = 0;
            return cost;
        }
    }
}
