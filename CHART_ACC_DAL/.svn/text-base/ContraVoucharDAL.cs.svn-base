using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CHART_ACC_DAL.DBML;
using CHART_ACC_ENTITY;

namespace CHART_ACC_DAL
{
    public class ContraVoucharDAL
    {
        CHART_ACCDataContext objDataContext;

        public List<EAccountType> GetAllAccountType()
        {
            objDataContext = new CHART_ACCDataContext();
            List<EAccountType> listOfType = new List<EAccountType>();

            var query = from accType in objDataContext.ACCOUNT_TYPEs select accType;
            foreach (ACCOUNT_TYPE acctype in query)
            {
                EAccountType objEAccountType = new EAccountType();
                objEAccountType.AccTypeId = acctype.ACC_TYPE_ID;
                objEAccountType.AccTypeName = acctype.ACC_NAME;
                listOfType.Add(objEAccountType);
            }
            return listOfType;
        }

        public List<EChartOfAccount> GetSubAccount(long parentId)
        {
            objDataContext = new CHART_ACCDataContext();
            List<EChartOfAccount> list = new List<EChartOfAccount>();
            foreach (var c in (from j in objDataContext.CHART_ACCOUNTs where j.CHART_ACC_PARENT_ID == parentId && j.CHART_ACC_STATUS == "Active" orderby j.CHART_ACC_NAME select j))
            {
                EChartOfAccount obj = new EChartOfAccount();
                obj.SubAccName = c.CHART_ACC_NAME;
                obj.SubAccId = c.CHART_ACC_ID;
                obj.SubAccCode = c.CHART_ACC_CODE;
                obj.RootAccName = c.CHART_ACC_PARENT_TYPE;
                obj.AccStatus = c.CHART_ACC_STATUS;
                obj.AccHeader = c.CHART_ACC_HEADER;
                obj.ParentAccId = Convert.ToInt64(c.CHART_ACC_PARENT_ID);
                list.Add(obj);
            }
            return list;
        }

        public List<EChartOfAccount> GetAccountInfo(long accId)
        {
            objDataContext = new CHART_ACCDataContext();
            List<EChartOfAccount> list = new List<EChartOfAccount>();
            foreach (var c in (from j in objDataContext.CHART_ACCOUNTs where j.CHART_ACC_ID == accId && j.CHART_ACC_STATUS == "Active" select j))
            {
                EChartOfAccount obj = new EChartOfAccount();
                obj.SubAccName = c.CHART_ACC_NAME;
                obj.SubAccId = c.CHART_ACC_ID;
                obj.SubAccCode = c.CHART_ACC_CODE;
                obj.AccHeader = c.CHART_ACC_HEADER;
                obj.RootAccName = c.CHART_ACC_PARENT_TYPE;
                obj.ParentAccId = Convert.ToInt64(c.CHART_ACC_PARENT_ID);
                list.Add(obj);
            }
            return list;
        }

        public decimal GetCurrentBalanceOfAccount(long accID)
        {
            List<EJournalVouchar> listJournalDetails = new List<EJournalVouchar>();
            DateTime fromDate = new DateTime();
            decimal openBalance = 0;
            decimal amount = 0;

            foreach (var c in objDataContext.SP_CURRENT_BALANCE(accID, DateTime.Now.Date))
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
            amount = openBalance + amount;

            return amount;
        }
    }
}
