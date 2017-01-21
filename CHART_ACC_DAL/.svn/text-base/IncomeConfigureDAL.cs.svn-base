using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CHART_ACC_ENTITY;
using CHART_ACC_DAL.DBML;

namespace CHART_ACC_DAL
{
    public class IncomeConfigureDAL
    {
        public List<EIncomeConfigure> GetSubAccount(long parentId)
        {
            CHART_ACCDataContext objDataContext = new CHART_ACCDataContext();
            List<EIncomeConfigure> list = new List<EIncomeConfigure>();
            foreach (var c in (from j in objDataContext.CHART_ACCOUNTs where j.CHART_ACC_PARENT_ID == parentId orderby j.CHART_ACC_NAME select j))
            {
                EIncomeConfigure obj = new EIncomeConfigure();
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
            foreach (var c in (from j in objDataContext.INCOME_SHEET_CONFIGUREs where j.ACCID == accId select j))
            {
                return true;
            }
            return false;
        }
        public bool HasValueinISConfig()
        {
            CHART_ACCDataContext objDataContext = new CHART_ACCDataContext();
            return objDataContext.INCOME_SHEET_CONFIGUREs.Any();
        }

        public bool SaveISConfig(List<EIncomeConfigure> listEincomeSheet)
        {
            CHART_ACCDataContext objDataContext = new CHART_ACCDataContext();
            foreach (var objBS in listEincomeSheet)
            {
                var IS = new INCOME_SHEET_CONFIGURE
                {
                    ACCID = objBS.SubAccId
                };
                objDataContext.INCOME_SHEET_CONFIGUREs.InsertOnSubmit(IS);
                objDataContext.SubmitChanges();
            }
            return true;
        }

        public bool DeleteISConfig()
        {
            CHART_ACCDataContext objDataContext = new CHART_ACCDataContext();
            var objIS = from j in objDataContext.INCOME_SHEET_CONFIGUREs select j;
            objDataContext.INCOME_SHEET_CONFIGUREs.DeleteAllOnSubmit(objIS);
            objDataContext.SubmitChanges();
            return true;
        }
    }
}
