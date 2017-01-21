using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CHART_ACC_REPORT.ENTITY;
//Author:REFAT
namespace CHART_ACC_REPORT.DAL
{
    public class BankReconciliationDAL
    {
        ReportDataContext objDataContext;
        
        public List<EBankReconciliation> GetAllJournalByAccount(DateTime fromDate, DateTime toDate, string accTitle,long accId)
        {
            objDataContext = new ReportDataContext();
            List<EBankReconciliation> listBS = new List<EBankReconciliation>();
            
            /*first row of the list to show current balance before fromdate*/
            //bool IsCreationDateUse = false;
            EBankReconciliation objEBRBasicInfo = GetBasicInfo(accId);
            objEBRBasicInfo.COAName = accTitle;
            objEBRBasicInfo.BankCurrentBalance = GetCurrentBalanceOfAccount(accId, fromDate);//, ref IsCreationDateUse);
            objEBRBasicInfo.TransactionGroup = " ";
            objEBRBasicInfo.Remarks = "Opening Balance";            
                objEBRBasicInfo.DisplayDate = fromDate.ToShortDateString();
            
            listBS.Add(objEBRBasicInfo);

            /*Transaction row*/
            foreach (var bankState in objDataContext.SP_BANK_STATEMENT(accId, fromDate, toDate))
            {
                EBankReconciliation objEBR = new EBankReconciliation();
                objEBR.ID = bankState.ID;
                objEBR.JournalDetailsId = bankState.JV_ID;
                objEBR.AccountId = bankState.JV_ACCOUNT_ID;
                objEBR.AccountCode = bankState.JV_ACCOUNT_CODE;
                objEBR.AccountTitle = bankState.JV_ACCOUNT_NAME;
                objEBR.Debit = Convert.ToDecimal(bankState.JV_DEBIT_AMOUNT);
                objEBR.Credit = Convert.ToDecimal(bankState.JV_CREDIT_AMOUNT);
                objEBR.IsDebit = objEBR.Debit > objEBR.Credit;
                objEBR.Date = Convert.ToDateTime(bankState.JV_DATE);
                if (objEBR.Date == new DateTime())
                {
                    objEBR.DisplayDate = "";
                }
                else
                {
                    objEBR.DisplayDate = objEBR.Date.ToShortDateString();
                }
                objEBR.Notes = bankState.JV_NOTES;
                objEBR.ChequeNo = bankState.JV_CHEQUE_NO;
                objEBR.ChequeDate = Convert.ToDateTime(bankState.JV_CHEQUE_DATE);
                objEBR.Remarks = bankState.JV_BANK_REMARK;
                objEBR.TransactionGroup = " ";
                objEBR.BankCurrentBalance = objEBRBasicInfo.BankCurrentBalance;
                objEBR.COAName = accTitle;
                objEBR.COACode = objEBRBasicInfo.COACode;
                objEBR.COADebit = objEBRBasicInfo.COADebit;
                objEBR.COACredit = objEBRBasicInfo.COACredit;
                objEBR.IsCOADebit = objEBR.COADebit > objEBR.COACredit;
                objEBR.COACreationDate = objEBRBasicInfo.COACreationDate;
                listBS.Add(objEBR);
            }
            return listBS;
        }

        private EBankReconciliation GetBasicInfo(long accId)
        {
            objDataContext = new ReportDataContext();
            var query = from j in objDataContext.CHART_ACCOUNTs where j.CHART_ACC_ID == accId select j;
            EBankReconciliation objEBR = new EBankReconciliation();
            foreach (var c in query)
            {
                objEBR.COACode = c.CHART_ACC_CODE;
                objEBR.COADebit = Convert.ToDecimal(c.CHART_ACC_OPENING_BALANCE_DR);
                objEBR.COACredit = Convert.ToDecimal(c.CHART_ACC_OPENING_BALANCE_CR);
                objEBR.COACreationDate = Convert.ToDateTime(c.CHART_ACC_CREATION_DATE);
            }
            return objEBR;
        }

        public decimal GetCurrentBalanceOfAccount(long accId, DateTime fromDate)//, ref bool IsCreationDateUse)
        {
            objDataContext = new ReportDataContext();
            decimal openBalance = 0;
            decimal amount = 0;
            foreach (var c in objDataContext.SP_CURRENT_BALANCE(accId, fromDate))
            {
                if (c.JV_DATE==null || c.JV_DATE < fromDate.Date)
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

        public EBankReconciliation GetOpponetAccInfo(long Id, string journalId, bool isDebit)
        {
            objDataContext = new ReportDataContext();
            EBankReconciliation objEBR = new EBankReconciliation();
            var checkQuery = from j in objDataContext.JOURNAL_VOUCHARs where j.ID != Id && j.JV_ID == journalId select j;            
            foreach (var check in checkQuery)
            {
                bool stat = check.JV_DEBIT_AMOUNT > check.JV_CREDIT_AMOUNT;
                if (isDebit == true && stat == false)
                {
                    objEBR.AccountCode = check.JV_ACCOUNT_CODE;
                    objEBR.AccountTitle = check.JV_ACCOUNT_NAME;
                    objEBR.ChequeNo = check.JV_CHEQUE_NO;
                    return objEBR;
                }
                if (isDebit == false && stat == true)
                {
                    objEBR.AccountCode = check.JV_ACCOUNT_CODE;
                    objEBR.AccountTitle = check.JV_ACCOUNT_NAME;
                    objEBR.ChequeNo = check.JV_CHEQUE_NO;
                    return objEBR;
                }
            }
            return objEBR;
        }
        public List<EBankReconciliation> GetJournalInfo(List<EBankReconciliation> listGetBR, string JournalId)
        {
            List<EBankReconciliation> listJournal = new List<EBankReconciliation>();
            foreach (var BR in (from j in listGetBR where j.JournalDetailsId == JournalId select j))
            {
                listJournal.Add(BR);
            }
            return listJournal;
        }
        /************************Get Pending Journal of parties deposit/payment **************************************/

        public List<EBankReconciliation> GetPendingPartiesDepositPaymentJournal(long accId, DateTime fromDate, DateTime toDate)
        {
            objDataContext = new ReportDataContext();
            List<EBankReconciliation> listBS = new List<EBankReconciliation>();
            var query = from j in objDataContext.DEPOSIT_PAYMENTs where j.DEPOSIT_PAYMENT_DATE >= fromDate.Date && j.DEPOSIT_PAYMENT_DATE <= toDate.Date && j.STATUS == "Pending" && j.BANK_ACCOUNT_ID == accId select j;
            foreach (var bankState in query)
            {
                EBankReconciliation objEBR = new EBankReconciliation();
                objEBR.JournalDetailsId = bankState.JOURNAL_VOUCHER;
                objEBR.AccountId = bankState.ACCOUNT_ID;                
                listBS.Add(objEBR);
            }
            List<EBankReconciliation> listFinal = new List<EBankReconciliation>();
            foreach (var objPending in (from j in listBS select new { j.JournalDetailsId, j.AccountId }).Distinct())
            {
                EBankReconciliation objEBR = new EBankReconciliation();
                objEBR.JournalDetailsId = objPending.JournalDetailsId;
                objEBR.AccountId = objPending.AccountId;
                listFinal.Add(objEBR);
            }
            return listFinal;
        }
        
        public List<EBankReconciliation> GetJournalVoucharIdWise(EBankReconciliation objEBank)
        {
            objDataContext = new ReportDataContext();
            List<EBankReconciliation> listBR = new List<EBankReconciliation>();
            foreach (var bankState in (from j in objDataContext.JOURNAL_VOUCHARs where j.JV_ID == objEBank.JournalDetailsId && j.JV_ACCOUNT_ID == objEBank.AccountId select j))
            {
                EBankReconciliation objEBR = new EBankReconciliation();              
                objEBR.JournalDetailsId = bankState.JV_ID;
                objEBR.AccountCode = bankState.JV_ACCOUNT_CODE;
                objEBR.AccountTitle = bankState.JV_ACCOUNT_NAME;
                objEBR.Debit = Convert.ToDecimal(bankState.JV_DEBIT_AMOUNT);
                objEBR.Credit = Convert.ToDecimal(bankState.JV_CREDIT_AMOUNT);
                objEBR.IsDebit = objEBR.Debit > objEBR.Credit;
                objEBR.Date = Convert.ToDateTime(bankState.JV_DATE);
                if (objEBR.Date == new DateTime())
                {
                    objEBR.DisplayDate = "";
                }
                else
                {
                    objEBR.DisplayDate = objEBR.Date.ToShortDateString();
                }
                objEBR.Notes = bankState.JV_NOTES;
                objEBR.ChequeNo = bankState.JV_CHEQUE_NO;
                objEBR.ChequeDate = Convert.ToDateTime(bankState.JV_CHEQUE_DATE);
                objEBR.Remarks = bankState.JV_BANK_REMARK;
                objEBR.TransactionGroup = "Pending";
                listBR.Add(objEBR);
            }
            return listBR;
        }
    }
}
