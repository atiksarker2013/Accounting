using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CHART_ACC_REPORT.ENTITY;
using CHART_ACC_REPORT.DAL;
//Author:Amin
//Modified by:Refat(19th feb'2012)

namespace CHART_ACC_REPORT.DAL
{
    public class HeaderAccountDAL
    {
        ReportDataContext objDataContext;

        public List<EAccountStatement> GetHeaderAccountName()
        {
            objDataContext = new ReportDataContext();
            List<EAccountStatement> HeadAccNameList = new List<EAccountStatement>();

            foreach (var c in objDataContext.SP_HEADER_ACC_NAME())
            {
                EAccountStatement objHeaderAccount = new EAccountStatement();
                objHeaderAccount.AccountTitle = c.JV_ACCOUNT_NAME;
                objHeaderAccount.AccountId = c.JV_ACCOUNT_ID;
                HeadAccNameList.Add(objHeaderAccount);
            }

            return HeadAccNameList;
        }

        public List<EAccountStatement> GetAllJournalByAccount(string accTitle,long accId)
        {
            objDataContext = new ReportDataContext();
            List<EAccountStatement> listAS = new List<EAccountStatement>();
            /*first row of the list to show current balance*/
            EAccountStatement objEASBasicInfo = GetBasicInfo(accId);
            objEASBasicInfo.COAName = accTitle;
            objEASBasicInfo.Notes = "Opening Balance";
            objEASBasicInfo.Address = GetAddress(accId);
            if (objEASBasicInfo.COADebit > objEASBasicInfo.COACredit)
            {
                objEASBasicInfo.AccountCurrentBalance = objEASBasicInfo.COADebit;                
            }
            else
            {
                objEASBasicInfo.AccountCurrentBalance = objEASBasicInfo.COACredit;
            }            
            listAS.Add(objEASBasicInfo);

            /*Transaction row*/
            foreach (var accountState in objDataContext.SP_HEADER_STATEMENT(accId))
            {
                EAccountStatement objEAS = new EAccountStatement();
                objEAS.ID = accountState.ID;
                objEAS.JournalDetailsId = accountState.JV_ID;
                objEAS.AccountId = accountState.JV_ACCOUNT_ID;
                objEAS.AccountCode = accountState.JV_ACCOUNT_CODE;
                objEAS.AccountTitle = accountState.JV_ACCOUNT_NAME;
                objEAS.Debit = Convert.ToDecimal(accountState.JV_DEBIT_AMOUNT);
                objEAS.Credit = Convert.ToDecimal(accountState.JV_CREDIT_AMOUNT);
                objEAS.IsDebit = objEAS.Debit > objEAS.Credit;
                objEAS.Date = Convert.ToDateTime(accountState.JV_DATE);
                if (objEAS.Date == new DateTime())
                {
                    objEAS.DisplayDate = "";
                }
                else
                {
                    objEAS.DisplayDate = objEAS.Date.ToShortDateString();
                }
                objEAS.Notes = accountState.JV_NOTES;
                objEAS.ChequeNo = accountState.JV_CHEQUE_NO;
                objEAS.Remarks = accountState.JV_BANK_REMARK;
                objEAS.COAName = accTitle;
                objEAS.COACode = objEASBasicInfo.COACode;
                objEAS.COADebit = objEASBasicInfo.COADebit;
                objEAS.COACredit = objEASBasicInfo.COACredit;
                objEAS.IsCOADebit = objEAS.COADebit > objEAS.COACredit;
                objEAS.AccountCurrentBalance = objEASBasicInfo.AccountCurrentBalance;
                objEAS.COACreationDate = objEASBasicInfo.COACreationDate;
                objEAS.Address = objEASBasicInfo.Address;
                listAS.Add(objEAS);
            }
            return listAS;
        }

        private string GetAddress(long accId)
        {
            string address = null;
            objDataContext = new ReportDataContext();
            var query = from j in objDataContext.MEMBER_ASSIGNs where j.ACC_ID == accId select j;
            foreach (var c in query)
            {
                foreach (var ad in (from j in objDataContext.MEMBER_INFOs where j.ID == c.MEMBER_ID select j))
                {
                    address = ad.PRESENT_ADDRESS;

                } break;
            }
            return address;
        }

        private EAccountStatement GetBasicInfo(long accId)
        {
            objDataContext = new ReportDataContext();
            var query = from j in objDataContext.CHART_ACCOUNTs where j.CHART_ACC_ID == accId select j;
            EAccountStatement objEAS = new EAccountStatement();
            foreach (var c in query)
            {
                objEAS.COACode = c.CHART_ACC_CODE;
                objEAS.COADebit = Convert.ToDecimal(c.CHART_ACC_OPENING_BALANCE_DR);
                objEAS.COACredit = Convert.ToDecimal(c.CHART_ACC_OPENING_BALANCE_CR);
                objEAS.COACreationDate = Convert.ToDateTime(c.CHART_ACC_CREATION_DATE);
            }
            return objEAS;
        }

        public bool CheckAccTypeAbsent(long accId, string journalId)
        {
            objDataContext = new ReportDataContext();
            bool IsAccountDr = false; long id = 0;
            var query = from j in objDataContext.JOURNAL_VOUCHARs where j.JV_ACCOUNT_ID == accId && j.JV_ID == journalId select j;
            foreach (var c in query)
            {
                IsAccountDr = c.JV_DEBIT_AMOUNT > c.JV_CREDIT_AMOUNT;
                id = c.ID;
                break;
            }
            var checkQuery = from j in objDataContext.JOURNAL_VOUCHARs where j.ID != id && j.JV_ID == journalId select j;
            bool stat = false;
            foreach (var check in checkQuery)
            {
                stat = check.JV_DEBIT_AMOUNT > check.JV_CREDIT_AMOUNT;
                if (IsAccountDr == stat)
                {
                    return false;
                }
            }
            return true;
        }

        public EAccountStatement GetOpponetAccInfo(long Id, string journalId, bool isDebit)
        {
            objDataContext = new ReportDataContext();
            EAccountStatement objAc = new EAccountStatement();
            var checkQuery = from j in objDataContext.JOURNAL_VOUCHARs where j.ID != Id && j.JV_ID == journalId select j;            
            foreach (var check in checkQuery)
            {
                bool stat = check.JV_DEBIT_AMOUNT > check.JV_CREDIT_AMOUNT;
                if (isDebit == true && stat == false)
                {
                    objAc.AccountCode = check.JV_ACCOUNT_CODE;
                    objAc.AccountTitle = check.JV_ACCOUNT_NAME;
                    objAc.ChequeNo = check.JV_CHEQUE_NO;
                    return objAc;
                }
                if (isDebit == false && stat == true)
                {
                    objAc.AccountCode = check.JV_ACCOUNT_CODE;
                    objAc.AccountTitle = check.JV_ACCOUNT_NAME;
                    objAc.ChequeNo = check.JV_CHEQUE_NO;
                    return objAc;
                }
            }
            return objAc;
        }

        public List<EAccountStatement> GetJournalInfo(List<EAccountStatement> listGetAS, string JournalId)
        {
            List<EAccountStatement> listJournal = new List<EAccountStatement>();
            foreach (var AS in (from j in listGetAS where j.JournalDetailsId == JournalId select j))
            {
                listJournal.Add(AS);
            }
            return listJournal;
        }
    }
}
