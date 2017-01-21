using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CHART_ACC_DAL.DBML;
using CHART_ACC_ENTITY;
namespace CHART_ACC_DAL
{
    public class ChartOfAccountModifyDAL
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
            foreach (var c in (from j in objDataContext.CHART_ACCOUNTs where j.CHART_ACC_PARENT_ID == parentId orderby j.CHART_ACC_NAME select j))
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
            foreach (var c in (from j in objDataContext.CHART_ACCOUNTs where j.CHART_ACC_ID == accId  select j))
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

        public bool UpdateChartAccount(EChartOfAccount objEChartAccount)
        {
            objDataContext = new CHART_ACCDataContext();

            var updateChartAccount = objDataContext.CHART_ACCOUNTs.Where(C => C.CHART_ACC_ID == objEChartAccount.SubAccId).First();

            updateChartAccount.CHART_ACC_NAME = objEChartAccount.SubAccName;
            updateChartAccount.CHART_ACC_PARENT_ID = objEChartAccount.ParentAccId;
            updateChartAccount.CHART_ACC_PARENT_TYPE = objEChartAccount.RootAccName;
            updateChartAccount.CHART_ACC_STATUS = objEChartAccount.AccStatus;
            //updateChartAccount.CHART_ACC_HEADER = "No";

            objDataContext.SubmitChanges();
            UpdateParentHeaderStatus(objEChartAccount.ParentAccId);
            return true;
        }

        public bool UpdateParentHeaderStatus(long id)
        {
            objDataContext = new CHART_ACCDataContext();
            var chart = objDataContext.CHART_ACCOUNTs.FirstOrDefault(c => c.CHART_ACC_ID == id);
            if (chart != null)
            {
                chart.CHART_ACC_HEADER = "Yes";
                objDataContext.SubmitChanges();
            }
            return true;
        }

        public bool DoesExistInJournal(string myAccount)
        {
            objDataContext = new CHART_ACCDataContext();
            return (objDataContext.JOURNAL_VOUCHARs.Any(Account => Account.JV_ACCOUNT_NAME.Equals(myAccount)));
        }

        public bool UpdateOldParentHeaderStat(long accId)
        {
            objDataContext = new CHART_ACCDataContext();
            int count = 0;
            foreach (var c in (from j in objDataContext.CHART_ACCOUNTs where j.CHART_ACC_PARENT_ID == accId select j))
            {
                count++;
            }
            if (count == 0)
            {
                var chart = objDataContext.CHART_ACCOUNTs.FirstOrDefault(c => c.CHART_ACC_ID == accId);
                if (chart != null)
                {
                    chart.CHART_ACC_HEADER = "No";
                    objDataContext.SubmitChanges();
                }
            }
            return true;
        }

        public bool DeleteChartAccount(long accId)
        {
            objDataContext = new CHART_ACCDataContext();

            var query = from j in objDataContext.CHART_ACCOUNTs  where j.CHART_ACC_ID==accId select j;
            objDataContext.CHART_ACCOUNTs.DeleteAllOnSubmit(query);
            objDataContext.SubmitChanges();
            return true;
        }

        public bool UpdateBalanceDebit(EChartOfAccount objEChartAccount)
        {
            objDataContext = new CHART_ACCDataContext();

            var updateChartAccount = objDataContext.CHART_ACCOUNTs.Where(C => C.CHART_ACC_ID == objEChartAccount.SubAccId).First();

            updateChartAccount.CHART_ACC_OPENING_BALANCE_DR = objEChartAccount.AccOpeningBalanceDR;
            objDataContext.SubmitChanges();
            return true;
        }

        public bool UpdateBalanceCredit(EChartOfAccount objEChartAccount)
        {
            objDataContext = new CHART_ACCDataContext();

            var updateChartAccount = objDataContext.CHART_ACCOUNTs.Where(C => C.CHART_ACC_ID == objEChartAccount.SubAccId).First();

            updateChartAccount.CHART_ACC_OPENING_BALANCE_CR = objEChartAccount.AccOpeningBalanceCR;
            objDataContext.SubmitChanges();
            return true;
        }
    }
}
