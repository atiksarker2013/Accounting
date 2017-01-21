using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CHART_ACC_REPORT.ENTITY;
//Author:REFAT
namespace CHART_ACC_REPORT.DAL
{
    public class Receive_PaymentDAL
    {
        public List<EReceive_Payment> GetRevenueExpense(DateTime fromDate, DateTime toDate, string accTypeName)
        {
            ReportDataContext objDataContext = new ReportDataContext();

            List<EReceive_Payment> listRecPay = new List<EReceive_Payment>();
            foreach (var RP in objDataContext.SP_RECEIVE_PAYMENT(fromDate, toDate, accTypeName))
            {
                EReceive_Payment objERP = new EReceive_Payment();
                objERP.AccId = RP.JV_ACCOUNT_ID;
                EChartOfAccount objCA = new ChartAccountDAL().GetAccountInfo(objERP.AccId);
                objERP.AccCode = objCA.SubAccCode;
                objERP.AccName = objCA.SubAccName;
                objERP.AccType = accTypeName;
                if (accTypeName == "REVENUE")
                {
                    objERP.Balance = Convert.ToDecimal(RP.Credit - RP.Debit);                    
                    objERP.CurrentBalance = objERP.Balance.ToString();
                }
                else
                {
                    objERP.Balance = Convert.ToDecimal(RP.Debit - RP.Credit);
                    objERP.CurrentBalance = objERP.Balance.ToString();
                }
                listRecPay.Add(objERP);
            }
            return listRecPay;
        }
    }
}
