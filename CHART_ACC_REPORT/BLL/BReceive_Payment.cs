using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CHART_ACC_REPORT.ENTITY;
using CHART_ACC_REPORT.DAL;
//Author:REFAT
namespace CHART_ACC_REPORT.BLL
{
    public class BReceive_Payment
    {
        Receive_PaymentDAL objReceive_PaymentDAL = new Receive_PaymentDAL();
        public List<EReceive_Payment> GetRevenueExpense(DateTime fromDate, DateTime toDate, string accTypeName)
        {
            return objReceive_PaymentDAL.GetRevenueExpense(fromDate,toDate,accTypeName);
        }
    }
}
