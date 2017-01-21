using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CHART_ACC_ENTITY;
using CHART_ACC_DAL.DBML;

namespace CHART_ACC_DAL
{
    public class DepriciationDAL
    {
        CHART_ACCDataContext objDataContext;

        public bool SaveJournalVoucharDR(EDepriciation objDepriciation)
        {
            objDataContext = new CHART_ACCDataContext();
            var journalVouchar = new JOURNAL_VOUCHAR
            {
                JV_ID = objDepriciation.Journal_Id,
                JV_ACCOUNT_ID = objDepriciation.Debit_Id,
                JV_ACCOUNT_NAME = objDepriciation.Debit_Account_Name,
                JV_ACCOUNT_CODE = objDepriciation.Debit_AccountCode,
                JV_NOTES = objDepriciation.Journal_Notes,
                JV_DATE = objDepriciation.Access_Date,
                JV_DEBIT_AMOUNT = objDepriciation.Debit_Amount,
                JV_CREDIT_AMOUNT=0,
                JV_BANK_REMARK="Journal",
                JV_ACCESS_BY = objDepriciation.Access_By,
                JV_ACCESS_DATE = objDepriciation.Access_Date
            };
            objDataContext.JOURNAL_VOUCHARs.InsertOnSubmit(journalVouchar);
            objDataContext.SubmitChanges();
            return true;
        }

        public bool SaveJournalVoucharCR(EDepriciation objDepriciation)
        {
            objDataContext = new CHART_ACCDataContext();
            var journalVouchar = new JOURNAL_VOUCHAR
            {
                JV_ID = objDepriciation.Journal_Id,
                JV_ACCOUNT_ID = objDepriciation.Credit_Id,
                JV_ACCOUNT_NAME = objDepriciation.Credit_Account_Name,
                JV_ACCOUNT_CODE = objDepriciation.Credit_AccountCode,
                JV_NOTES = objDepriciation.Journal_Notes,
                JV_DATE = objDepriciation.Access_Date,
                JV_CREDIT_AMOUNT = objDepriciation.Credit_Amount,
                JV_DEBIT_AMOUNT=0,
                JV_BANK_REMARK = "Journal",
                JV_ACCESS_BY = objDepriciation.Access_By,
                JV_ACCESS_DATE = objDepriciation.Access_Date
            };
            objDataContext.JOURNAL_VOUCHARs.InsertOnSubmit(journalVouchar);
            objDataContext.SubmitChanges();
            return true;
        }

        public bool SaveDepriciation(EDepriciation objDepriciation)
        {
            objDataContext = new CHART_ACCDataContext();
            var depriciation = new DEPRICIATION
            {
                ACCOUNT_ID = objDepriciation.AccountId,
                ORIGINAL_COST = objDepriciation.Original_Cost,
                DEP_VALUE = objDepriciation.Dep_Value,
                NEW_ORIGINAL_COST = objDepriciation.New_Original_Cost,
                ACCESS_BY = objDepriciation.Access_By,
                ACCESS_DATE = objDepriciation.Access_Date,
                JOURNAL_ID = objDepriciation.Journal_Id
            };
            objDataContext.DEPRICIATIONs.InsertOnSubmit(depriciation);
            objDataContext.SubmitChanges();
            return true;
        }

        public EDepriciation GetCurrentDebitCredit(long id)
        {
            objDataContext = new CHART_ACCDataContext();
            EDepriciation objEDepriciation = new EDepriciation();

            var balance = (from c in objDataContext.SP_CURRENT_DEBIT_CREDIT_DEPRICIATION(id) select c);
            foreach (var item in balance)
            {
                objEDepriciation.Debit = Convert.ToDecimal(item.DEBIT);
                objEDepriciation.Credit = Convert.ToDecimal(item.CREDIT);
            }

            return objEDepriciation;
        }

        public List<EDepriciation> GetSubAccount(long pId)
        {
            objDataContext = new CHART_ACCDataContext();
            List<EDepriciation> list = new List<EDepriciation>();
            foreach (var c in (from j in objDataContext.CHART_ACCOUNTs where j.CHART_ACC_PARENT_ID == pId orderby j.CHART_ACC_NAME select j))
            {
                EDepriciation obj = new EDepriciation();
                obj.AccountName = c.CHART_ACC_NAME;
                obj.AccountId = c.CHART_ACC_ID;
                obj.AccountCode = c.CHART_ACC_CODE;
                obj.AccType = c.CHART_ACC_PARENT_TYPE;
                obj.IsHeader = c.CHART_ACC_HEADER;
                EDepriciation objGetDebitCredit = GetCurrentDebitCredit(obj.AccountId);
                obj.Debit = objGetDebitCredit.Debit;
                obj.Credit = objGetDebitCredit.Credit;
                list.Add(obj);
            }
            return list;
        }

        public long GetFixedAssetId()
        {
            objDataContext = new CHART_ACCDataContext();
            foreach (var c in (from j in objDataContext.CHART_ACCOUNTs where j.CHART_ACC_NAME == "FIXED ASSETS" orderby j.CHART_ACC_NAME select j))
            {
                EChartOfAccount obj = new EChartOfAccount();
               return obj.SubAccId = c.CHART_ACC_ID;
            }
            return 0;
        }

        public List<EDepriciation> GetAllDepriciationSetup()
        {
            CHART_ACCDataContext objDataContext = new CHART_ACCDataContext();
            List<EDepriciation> depriciationList = new List<EDepriciation>();
            foreach (var c in objDataContext.DEPRICIATION_SETUPs)
            {
                EDepriciation objDepriciation = new EDepriciation();
                objDepriciation.AccountId = c.ACCOUNT_ID;
                objDepriciation.AccountName = c.CHART_ACCOUNT.CHART_ACC_NAME;
                objDepriciation.Dep_Percentage =Convert.ToDecimal(c.DEP_PERCENTAGE);
                objDepriciation.Debit_Id =Convert.ToInt64(c.DEBIT_ACC_ID);
                objDepriciation.Credit_Id =Convert.ToInt64(c.CREDIT_ACC_ID);
                objDepriciation.Access_By = c.ACCESS_BY;
                objDepriciation.Access_Date =Convert.ToDateTime(c.ACCESS_DATE);
                depriciationList.Add(objDepriciation);
            }
            return depriciationList;
        }

        public List<EChartOfAccount> GetChartAccountIdWise(long accId)
        {
            objDataContext = new CHART_ACCDataContext();
            List<EChartOfAccount> list = new List<EChartOfAccount>();
            foreach (var c in (from j in objDataContext.CHART_ACCOUNTs where j.CHART_ACC_ID == accId && j.CHART_ACC_STATUS == "Active" orderby j.CHART_ACC_NAME select j))
            {
                EChartOfAccount obj = new EChartOfAccount();
                obj.SubAccName = c.CHART_ACC_NAME;
                obj.SubAccId = c.CHART_ACC_ID;
                obj.SubAccCode = c.CHART_ACC_CODE;
                list.Add(obj);
            }
            return list;
        }

        public List<EDepriciation> GetDepriciationForCurrentYear()
        {
            CHART_ACCDataContext objDataContext = new CHART_ACCDataContext();
            List<EDepriciation> listDepriciation = new List<EDepriciation>();

            foreach (var c in objDataContext.SP_GET_ALL_DEPRICIATION())
            {
                EDepriciation objDepriciation = new EDepriciation();
                objDepriciation.AccountId = c.ACCOUNT_ID;
                objDepriciation.Original_Cost =Convert.ToDecimal(c.ORIGINAL_COST);
                objDepriciation.New_Original_Cost =Convert.ToDecimal(c.NEW_ORIGINAL_COST);
                objDepriciation.Dep_Value =Convert.ToDecimal(c.DEP_VALUE);
                objDepriciation.Access_By = c.ACCESS_BY;
                objDepriciation.Access_Date =Convert.ToDateTime(c.ACCESS_DATE);
                objDepriciation.Journal_Id = c.JOURNAL_ID;
                listDepriciation.Add(objDepriciation);
            }

            return listDepriciation;
        }

        public bool DeleteDepriciation()
        {
            objDataContext.SP_DELETE_DEPRICIATION();
            return true;
        }

        public bool DeleteJournalOfDepriciation(string Journal_Id)
        { 
          var query = from r in objDataContext.JOURNAL_VOUCHARs where r.JV_ID == Journal_Id select r;
          objDataContext.JOURNAL_VOUCHARs.DeleteAllOnSubmit(query);
          objDataContext.SubmitChanges();
          return true;
        }
    }
}
