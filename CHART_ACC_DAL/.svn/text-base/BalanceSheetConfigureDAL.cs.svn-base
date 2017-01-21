using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CHART_ACC_ENTITY;
using CHART_ACC_DAL.DBML;
//Author:REFAT
namespace CHART_ACC_DAL
{
   public class BalanceSheetConfigureDAL
    {
        
        public List<EBalanceSheetConfigure> GetSubAccount(long parentId)
        {
            CHART_ACCDataContext objDataContext = new CHART_ACCDataContext();
            List<EBalanceSheetConfigure> list = new List<EBalanceSheetConfigure>();
            foreach (var c in (from j in objDataContext.CHART_ACCOUNTs where j.CHART_ACC_PARENT_ID == parentId orderby j.CHART_ACC_NAME select j))
            {
                EBalanceSheetConfigure obj = new EBalanceSheetConfigure();
                obj.SubAccName = c.CHART_ACC_NAME;
                obj.SubAccId = c.CHART_ACC_ID;
                obj.SubAccCode = c.CHART_ACC_CODE;
                obj.ParentAccId = Convert.ToInt64(c.CHART_ACC_PARENT_ID);
                obj.AccHeader = c.CHART_ACC_HEADER;
                obj.Status = GetStatus(obj.SubAccId);
                list.Add(obj);
            }
            return list;
        }
        private bool GetStatus(long accId)
        {
            CHART_ACCDataContext objDataContext = new CHART_ACCDataContext();
            foreach (var c in (from j in objDataContext.BALANCE_SHEET_CONFIGUREs where j.ACCID == accId select j))
            {
                return true;
            }
            return false;
        }
        public bool SaveBSConfig(List<EBalanceSheetConfigure> listEbalanceSheet)
        {
            CHART_ACCDataContext objDataContext = new CHART_ACCDataContext();
            foreach (var objBS in listEbalanceSheet)
            {
                var BS = new BALANCE_SHEET_CONFIGURE
                {
                    ACCID=objBS.SubAccId
                };
                objDataContext.BALANCE_SHEET_CONFIGUREs.InsertOnSubmit(BS);
                objDataContext.SubmitChanges();
            }
            return true;
        }

        public bool DeleteBSConfig()
        {
            CHART_ACCDataContext objDataContext = new CHART_ACCDataContext();
            var objBS = from j in objDataContext.BALANCE_SHEET_CONFIGUREs select j;
            objDataContext.BALANCE_SHEET_CONFIGUREs.DeleteAllOnSubmit(objBS);
            objDataContext.SubmitChanges();
            return true;
        }

        public bool HasValueinBSConfig()
        {
            CHART_ACCDataContext objDataContext = new CHART_ACCDataContext();
            return objDataContext.BALANCE_SHEET_CONFIGUREs.Any();
        }
    }
}
