using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CHART_ACC_REPORT.ENTITY;
//Author:REFAT
namespace CHART_ACC_REPORT.DAL
{
    public class AccountStatementReportDAL
    {
        ReportDataContext objDataContext;
        public List<EAccountStatement> GetAllJournalByAccount(DateTime fromDate, DateTime toDate, string accTitle, long accId)
        {
            objDataContext = new ReportDataContext();
            List<EAccountStatement> listAS = new List<EAccountStatement>();
            /*first row of the list to show current balance before fromdate*/

            EAccountStatement objEASBasicInfo = GetBasicInfo(accId);
            objEASBasicInfo.COAName = accTitle;
            objEASBasicInfo.AccountCurrentBalance = GetCurrentBalanceOfAccount(accId, fromDate);//, ref IsCreationDateUse);
            objEASBasicInfo.Notes = "Opening Balance";
            objEASBasicInfo.Address = GetAddress(accId);
            objEASBasicInfo.DisplayDate = fromDate.ToShortDateString();

            listAS.Add(objEASBasicInfo);

            /*Transaction row*/
            foreach (var accountState in objDataContext.SP_ACCOUNT_STATEMENT(accId, fromDate, toDate))
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
                objEAS.AccountCurrentBalance = objEASBasicInfo.AccountCurrentBalance;
                objEAS.COACode = objEASBasicInfo.COACode;
                objEAS.COADebit = objEASBasicInfo.COADebit;
                objEAS.COACredit = objEASBasicInfo.COACredit;
                objEAS.IsCOADebit = objEAS.COADebit > objEAS.COACredit;
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

        public decimal GetCurrentBalanceOfAccount(long accId, DateTime fromDate)//, ref bool IsCreationDateUse)
        {
            objDataContext = new ReportDataContext();
            decimal openBalance = 0;
            decimal amount = 0;
            foreach (var c in objDataContext.SP_CURRENT_BALANCE(accId, fromDate))
            {
                if (c.JV_DATE == null || c.JV_DATE < fromDate.Date)
                {
                    //IsCreationDateUse = c.JV_DATE == null ? true : false;

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
            }
            amount = openBalance + amount;

            return amount;
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
            foreach(var AS in (from j in listGetAS where j.JournalDetailsId==JournalId select j))
            {
                listJournal.Add(AS);
            }
            return listJournal;
        }
    }
}
