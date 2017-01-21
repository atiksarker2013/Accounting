using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CHART_ACC_DAL.DBML;
using CHART_ACC_ENTITY;

namespace CHART_ACC_DAL
{
    public class JournalVoucharDAL
    {
        CHART_ACCDataContext objDataContext = new CHART_ACCDataContext();

        public string GetJVoucherLastID()
        {
            string JournalID = "";
            foreach (var c in objDataContext.SP_JOURNAL_VOUCHAR_LAST_ID())
            {
                JournalID = c.JOURNAL_ID;
            }
            return JournalID;
        }

        public bool SaveJournalVouchar(EJournalVouchar objEJournalVouchar)
        {
            objDataContext = new CHART_ACCDataContext();
            var journalVouchar = new JOURNAL_VOUCHAR 
            {
             JV_ID = objEJournalVouchar.Journal_Id,
             JV_ACCOUNT_ID = objEJournalVouchar.Journal_Acc_Id,
             JV_ACCOUNT_NAME =objEJournalVouchar.Journal_Acc_Name,
             JV_ACCOUNT_CODE=objEJournalVouchar.Journal_Acc_Code,
             JV_NOTES=objEJournalVouchar.Journal_Notes,
             JV_DATE=objEJournalVouchar.Journal_Date,
             JV_DEBIT_AMOUNT =objEJournalVouchar.Journal_Debit_Amount,
             JV_CREDIT_AMOUNT =objEJournalVouchar.Jounal_Credit_Amount,
             JV_BANK_REMARK = objEJournalVouchar.Journal_BankRemarks,
             JV_ACCESS_BY=objEJournalVouchar.Access_By,
             JV_ACCESS_DATE=objEJournalVouchar.Access_Date
            };
            objDataContext.JOURNAL_VOUCHARs.InsertOnSubmit(journalVouchar);
            objDataContext.SubmitChanges();
            return true;
        }

        public bool DeleteJournalVouchar(string voucharId)
        {
            objDataContext = new CHART_ACCDataContext();
            var objVouchar = from j in  objDataContext.JOURNAL_VOUCHARs where  j.JV_ID == voucharId select j;
            objDataContext.JOURNAL_VOUCHARs.DeleteAllOnSubmit(objVouchar);
            objDataContext.SubmitChanges();
            return true;
        }

        public List<EChartOfAccount> GetAccountInfo(long accId)
        {
            objDataContext = new CHART_ACCDataContext();
            List<EChartOfAccount> list = new List<EChartOfAccount>();
            foreach (var c in (from j in objDataContext.CHART_ACCOUNTs where j.CHART_ACC_ID == accId && j.CHART_ACC_STATUS=="Active" select j))
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

        public List<EChartOfAccount> GetAccountInfoCodeWise(string accCode)
        {
            objDataContext = new CHART_ACCDataContext();
            List<EChartOfAccount> list = new List<EChartOfAccount>();
            foreach (var c in (from j in objDataContext.CHART_ACCOUNTs where j.CHART_ACC_CODE == accCode && j.CHART_ACC_STATUS == "Active" select j))
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

        public List<EChartOfAccount> GetAccountInfoNameWise(string accName)
        {
            objDataContext = new CHART_ACCDataContext();
            List<EChartOfAccount> list = new List<EChartOfAccount>();
            foreach (var c in (from j in objDataContext.CHART_ACCOUNTs where j.CHART_ACC_NAME.ToUpper() == accName.ToUpper() && j.CHART_ACC_STATUS == "Active" select j))
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

        public List<EChartOfAccount> GetAccountInfoLikeNameWise(string accName)
        {
            objDataContext = new CHART_ACCDataContext();
            List<EChartOfAccount> list = new List<EChartOfAccount>();
            foreach (var c in (from j in objDataContext.CHART_ACCOUNTs where j.CHART_ACC_NAME.ToUpper().StartsWith(accName.ToUpper()) && j.CHART_ACC_STATUS == "Active" && j.CHART_ACC_HEADER=="No" select j))
            {
                EChartOfAccount obj = new EChartOfAccount();
                obj.SubAccName = c.CHART_ACC_NAME;
                obj.SubAccId = c.CHART_ACC_ID;
                obj.SubAccCode = c.CHART_ACC_CODE;
                obj.AccHeader = c.CHART_ACC_HEADER;
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
            
            //var fiscalQuery = from j in objDataContext.FISCAL_YEARs where j.F_YEAR_STATUS == "Active" select j;
            //foreach (var f in fiscalQuery)
            //{
            //    fromDate = Convert.ToDateTime(f.F_YEAR_START_DATE).Date;
            //}
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

        public List<EJournalVouchar> GetAllJournalVoucharID()
        {
            objDataContext = new CHART_ACCDataContext();
            List<EJournalVouchar> VoucharList = new List<EJournalVouchar>();
            foreach (var c in (from j in objDataContext.JOURNAL_VOUCHARs select j.JV_ID).Distinct())
            {
                EJournalVouchar objEJVouchar = new EJournalVouchar();
                objEJVouchar.Journal_Id = c;
                VoucharList.Add(objEJVouchar);
            }
            return VoucharList;
        }

        public List<EJournalVouchar> GetAllJournalVoucharIdWise(string Jv_Id)
        {
            objDataContext = new CHART_ACCDataContext();
            List<EJournalVouchar> VoucharList = new List<EJournalVouchar>();
            foreach (var c in (from j in objDataContext.JOURNAL_VOUCHARs where j.JV_ID==Jv_Id select j))
            {
                EJournalVouchar objEJVouchar = new EJournalVouchar();
                objEJVouchar.Journal_Id = c.JV_ID;
                objEJVouchar.Journal_Acc_Name = c.JV_ACCOUNT_NAME;
                objEJVouchar.Journal_Acc_Id = c.JV_ACCOUNT_ID;
                objEJVouchar.Journal_Acc_Code = c.JV_ACCOUNT_CODE;
                objEJVouchar.Journal_Debit_Amount = Convert.ToDecimal(c.JV_DEBIT_AMOUNT);
                objEJVouchar.Jounal_Credit_Amount = Convert.ToDecimal(c.JV_CREDIT_AMOUNT);
                objEJVouchar.Journal_Date = Convert.ToDateTime(c.JV_DATE);
                objEJVouchar.Journal_Notes = c.JV_NOTES;
                objEJVouchar.Journal_BankRemarks = c.JV_BANK_REMARK;                
                objEJVouchar.Access_By = c.JV_ACCESS_BY;
                if (Convert.ToDateTime(c.JV_CHEQUE_DATE) != new DateTime())
                {
                    objEJVouchar.Journal_DisplayChequeDate = Convert.ToDateTime(c.JV_CHEQUE_DATE).ToShortDateString();
                    objEJVouchar.Journal_Cheque = c.JV_CHEQUE_NO;
                    objEJVouchar.Journal_ChequeBank=GetChequeBank(objEJVouchar);
                }
                VoucharList.Add(objEJVouchar);
            }
            return VoucharList;
        }
        string GetChequeBank(EJournalVouchar objEJV)
        {
            string chk = "";
            objDataContext = new CHART_ACCDataContext();
            foreach(var query in (from j in objDataContext.DEPOSIT_PAYMENTs where j.JOURNAL_VOUCHER==objEJV.Journal_Id && j.BANK_ACCOUNT_ID==objEJV.Journal_Acc_Id && j.BANK_CHEQUE==objEJV.Journal_Cheque && j.BANK_CHEQUE_DATE==Convert.ToDateTime(objEJV.Journal_DisplayChequeDate) select j))
            {
                chk = query.BANK_INFO_CHEQUE;
            }
            return chk;
        }
        // For Tree .

        public List<EChartOfAccount> GetSubAccount(long parentId)
        {
            objDataContext = new CHART_ACCDataContext();
            List<EChartOfAccount> list = new List<EChartOfAccount>();
            foreach (var c in (from j in objDataContext.CHART_ACCOUNTs where j.CHART_ACC_PARENT_ID == parentId && j.CHART_ACC_STATUS == "Active" && j.CHART_ACC_NAME != "Cash at Bank" orderby j.CHART_ACC_NAME select j))
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
        public bool DoesExistVoucharWithoutBank(string jvId)
        {
            objDataContext = new CHART_ACCDataContext();
            var query = from j in objDataContext.SP_GET_JVID_WITHOUT_BANK() where j.JV_ID == jvId select j;
            foreach (var jv in query)
            {
                return true;
            }
            return false;
        }

        public bool DoesExistContraVoucher(string jvId)
        {
            objDataContext = new CHART_ACCDataContext();
            return objDataContext.JOURNAL_VOUCHARs.Any(C => C.JV_BANK_REMARK == "Contra Voucher" && C.JV_ID==jvId);
        }
        //by Refat for cash Deposit/Payment
        public bool DoesExistVoucharforCashDepojitPayment(string jvId, string remark)
        {
            objDataContext = new CHART_ACCDataContext();
            return objDataContext.JOURNAL_VOUCHARs.Any(c => c.JV_ID == jvId && c.JV_BANK_REMARK == remark);
        }

        public bool SaveJournalVoucharWithBankCheque(EJournalVouchar objEJournalVouchar)
        {
            objDataContext = new CHART_ACCDataContext();
            var journalVouchar = new JOURNAL_VOUCHAR
            {
                JV_ID = objEJournalVouchar.Journal_Id,
                JV_ACCOUNT_ID = objEJournalVouchar.Journal_Acc_Id,
                JV_ACCOUNT_NAME = objEJournalVouchar.Journal_Acc_Name,
                JV_ACCOUNT_CODE = objEJournalVouchar.Journal_Acc_Code,
                JV_CHEQUE_NO = objEJournalVouchar.Journal_Cheque,
                JV_CHEQUE_DATE = Convert.ToDateTime(objEJournalVouchar.Journal_DisplayChequeDate),
                JV_NOTES = objEJournalVouchar.Journal_Notes,
                JV_DATE = objEJournalVouchar.Journal_Date,
                JV_DEBIT_AMOUNT = objEJournalVouchar.Journal_Debit_Amount,
                JV_CREDIT_AMOUNT = objEJournalVouchar.Jounal_Credit_Amount,
                JV_BANK_REMARK = objEJournalVouchar.Journal_BankRemarks,
                JV_ACCESS_BY = objEJournalVouchar.Access_By,
                JV_ACCESS_DATE = objEJournalVouchar.Access_Date
            };
            objDataContext.JOURNAL_VOUCHARs.InsertOnSubmit(journalVouchar);
            objDataContext.SubmitChanges();
            return true;
        }
    }
}
