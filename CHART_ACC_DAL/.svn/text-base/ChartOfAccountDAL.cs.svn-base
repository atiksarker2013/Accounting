using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CHART_ACC_DAL.DBML;
using CHART_ACC_ENTITY;

namespace CHART_ACC_DAL
{
    public class ChartOfAccountDAL
    {
        CHART_ACCDataContext objDataContext = new CHART_ACCDataContext();

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

        public EAccountType GetSingleAccountType(long id)
        {
            objDataContext = new CHART_ACCDataContext();
            EAccountType objEAccountType = new EAccountType();

            var query = from accType in objDataContext.ACCOUNT_TYPEs where accType.ACC_TYPE_ID == id select accType;
            foreach (ACCOUNT_TYPE acctype in query)
            {
                objEAccountType.AccTypeId = acctype.ACC_TYPE_ID;
                objEAccountType.AccTypeName = acctype.ACC_NAME;
            }
            return objEAccountType;
        }

        public bool SaveChartOfAccount(EChartOfAccount objEChartOfAccount)
        {
            var chartOfAccount = new CHART_ACCOUNT 
            { 
             CHART_ACC_CODE=objEChartOfAccount.SubAccCode,
             CHART_ACC_NAME = objEChartOfAccount.SubAccName,
             CHART_ACC_PARENT_ID = objEChartOfAccount.ParentAccId,
             CHART_ACC_PARENT_TYPE=objEChartOfAccount.RootAccName,
             CHART_ACC_OPENING_BALANCE_DR=objEChartOfAccount.AccOpeningBalanceDR,
             CHART_ACC_OPENING_BALANCE_CR=objEChartOfAccount.AccOpeningBalanceCR,
             CHART_ACC_STATUS=objEChartOfAccount.AccStatus,
             CHART_ACC_HEADER="No",
             CHART_ACC_CREATION_DATE=objEChartOfAccount.CreationDate,
             ACCESS_BY=objEChartOfAccount.AccessBy,
             ACCESS_DATE=objEChartOfAccount.AccessDateTime            
            };
            objDataContext.CHART_ACCOUNTs.InsertOnSubmit(chartOfAccount);
            objDataContext.SubmitChanges();
            UpdateParentHeaderStatus(objEChartOfAccount.ParentAccId);
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

        public bool DoesExist(string SubAccount)
        {
            objDataContext = new CHART_ACCDataContext();
            return (objDataContext.CHART_ACCOUNTs.Any(subAccount => subAccount.CHART_ACC_NAME.Equals(SubAccount)));
        }
        public bool DoesCodeExist(string code)
        {
            objDataContext = new CHART_ACCDataContext();
            return (objDataContext.CHART_ACCOUNTs.Any(subAccount => subAccount.CHART_ACC_CODE.Equals(code)));
        }
        public List<EChartOfAccount> GetSubAccount(long parentId)
        {
            objDataContext = new CHART_ACCDataContext();
            List<EChartOfAccount> list = new List<EChartOfAccount>();
            foreach (var c in (from j in objDataContext.CHART_ACCOUNTs where j.CHART_ACC_PARENT_ID==parentId && j.CHART_ACC_STATUS=="Active" orderby j.CHART_ACC_NAME select j))
            {
                EChartOfAccount obj = new EChartOfAccount();
                obj.SubAccName = c.CHART_ACC_NAME;
                obj.SubAccId = c.CHART_ACC_ID;
                obj.SubAccCode = c.CHART_ACC_CODE;
                obj.RootAccName = c.CHART_ACC_PARENT_TYPE;
                obj.AccStatus = c.CHART_ACC_STATUS;
                obj.AccHeader = c.CHART_ACC_HEADER;
                obj.CreationDate = Convert.ToDateTime(c.CHART_ACC_CREATION_DATE);
                obj.ParentAccId = Convert.ToInt64(c.CHART_ACC_PARENT_ID);
                list.Add(obj);
            }
            return list;
        }
        public List<EChartOfAccount> GetChartAccountAll()
        {
            objDataContext = new CHART_ACCDataContext();
            List<EChartOfAccount> list = new List<EChartOfAccount>();
            foreach (var c in (from j in objDataContext.CHART_ACCOUNTs where j.CHART_ACC_STATUS == "Active" orderby j.CHART_ACC_NAME select j))
            {
                EChartOfAccount obj = new EChartOfAccount();
                obj.SubAccName = c.CHART_ACC_NAME;
                obj.SubAccId = c.CHART_ACC_ID;
                obj.SubAccCode = c.CHART_ACC_CODE;
                obj.RootAccName = c.CHART_ACC_PARENT_TYPE;
                obj.AccStatus = c.CHART_ACC_STATUS;
                obj.AccHeader = c.CHART_ACC_HEADER;
                obj.CreationDate = Convert.ToDateTime(c.CHART_ACC_CREATION_DATE);
                obj.ParentAccId = Convert.ToInt64(c.CHART_ACC_PARENT_ID);
                list.Add(obj);
            }
            return list;
        }

        public long GetAccountID(string name)
        {
            CHART_ACCDataContext objDataContext = new CHART_ACCDataContext();
            long id = 0;
            foreach (var acc in (from j in objDataContext.CHART_ACCOUNTs where j.CHART_ACC_NAME == name select j))
            {
                id = acc.CHART_ACC_ID;
            }
            return id;
        }

        public string GetAccount_Code(long accId)
        {
            CHART_ACCDataContext objDataContext = new CHART_ACCDataContext();
            string code = "";
            foreach (var acc in (from j in objDataContext.CHART_ACCOUNTs where j.CHART_ACC_ID == accId select j))
            {
                code = acc.CHART_ACC_CODE;
            }
            return code;
        }

        public EChartOfAccount GetAccountInfo(long accId)
        {
            CHART_ACCDataContext objDataContext = new CHART_ACCDataContext();
            EChartOfAccount obj = new EChartOfAccount();
            foreach (var c in (from j in objDataContext.CHART_ACCOUNTs where j.CHART_ACC_ID == accId && j.CHART_ACC_STATUS == "Active" select j))
            {
                obj.SubAccName = c.CHART_ACC_NAME;
                obj.SubAccId = c.CHART_ACC_ID;
                obj.SubAccCode = c.CHART_ACC_CODE;
                obj.AccHeader = c.CHART_ACC_HEADER;
                obj.RootAccName = c.CHART_ACC_PARENT_TYPE;
                obj.CreationDate = Convert.ToDateTime(c.CHART_ACC_CREATION_DATE);
                obj.ParentAccId = Convert.ToInt64(c.CHART_ACC_PARENT_ID);
            }
            return obj;
        }
    }
}
