using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CHART_ACC_ENTITY;
using CHART_ACC_DAL.DBML;
//Author:REFAT
namespace CHART_ACC_DAL
{
    public class Deposit_PaymentDAL
    {
        CHART_ACCDataContext objDataContext;

        public bool SaveDepositPayment(EDeposit_Payment objEDepositPayment)
        {
            objDataContext = new CHART_ACCDataContext();
            var deposit = new DEPOSIT_PAYMENT
            {
                ACCOUNT_ID = objEDepositPayment.AccountID,
                DEPOSIT_PAYMENT_DATE = objEDepositPayment.DepositePaymentDate,
                AMOUNT = objEDepositPayment.DepositPaymentAmount,
                BANK_ACCOUNT_ID = objEDepositPayment.BankAccountId,
                DESCRIPTION = objEDepositPayment.Description,                
                BANK_INFO_CHEQUE = objEDepositPayment.BankInfoinCheque,
                BANK_CHEQUE = objEDepositPayment.BankCheque,
                BANK_CHEQUE_DATE = objEDepositPayment.BankChequeDate,
                STATUS = objEDepositPayment.Status,
                TRANSACTION_TYPE=objEDepositPayment.TransactionType,
                JOURNAL_VOUCHER=objEDepositPayment.JournalVoucherId,
                ACCESS_BY = objEDepositPayment.AccessBy,
                ACCESS_DATETIME=DateTime.Now
            };
            objDataContext.DEPOSIT_PAYMENTs.InsertOnSubmit(deposit);
            objDataContext.SubmitChanges();
            return true;
        }

        public List<EDeposit_Payment> GetAllPendingDepositPayment(string Transactiontype)
        {
            objDataContext = new CHART_ACCDataContext();
            List<EDeposit_Payment> listDeposit = new List<EDeposit_Payment>();
            foreach (var dep in (from j in objDataContext.DEPOSIT_PAYMENTs where j.STATUS == "Pending" && j.TRANSACTION_TYPE == Transactiontype select j))
            {
                EDeposit_Payment objEDepositPayment = new EDeposit_Payment
                {
                    Id=dep.ID,                    
                    AccountID=dep.CHART_ACCOUNT.CHART_ACC_ID,
                    AccountCode=dep.CHART_ACCOUNT.CHART_ACC_CODE,
                    AccountName=dep.CHART_ACCOUNT.CHART_ACC_NAME,
                    DepositePaymentDate=dep.DEPOSIT_PAYMENT_DATE,
                    DepositPaymentAmount = Convert.ToDecimal(dep.AMOUNT),
                    BankAccountId = Convert.ToInt64(dep.BANK_ACCOUNT_ID),
                    BankAccountName = GetAccountInfo(Convert.ToInt64(dep.BANK_ACCOUNT_ID)).SubAccName,
                    Description=dep.DESCRIPTION,                    
                    BankInfoinCheque = dep.BANK_INFO_CHEQUE,
                    BankCheque = dep.BANK_CHEQUE,
                    BankChequeDate = dep.BANK_CHEQUE_DATE,                   
                    ClearingDate=dep.CLEARING_DATE,
                    Status=dep.STATUS,
                    JournalVoucherId=dep.JOURNAL_VOUCHER,
                    AccessBy=dep.ACCESS_BY,
                    AccessDatetime=dep.ACCESS_DATETIME
                };
                listDeposit.Add(objEDepositPayment);
            }
            return listDeposit;
        }

        public bool ClearingDepositPayment(EDeposit_Payment objEDP)
        {
            objDataContext = new CHART_ACCDataContext();

            var updateClearing = objDataContext.DEPOSIT_PAYMENTs.First(d => d.ID == objEDP.Id);
            updateClearing.STATUS = objEDP.Status;
            updateClearing.CLEARING_DATE = objEDP.ClearingDate;
            updateClearing.CANCEL_JVOUCHAR = objEDP.CancellingJV;
            objDataContext.SubmitChanges();
            return true;
        }

        public EChartOfAccount GetAccountInfo(long id)
        {
            objDataContext = new CHART_ACCDataContext();
            EChartOfAccount objCA=null;
            foreach(var acc in (from j in objDataContext.CHART_ACCOUNTs where j.CHART_ACC_ID==id select j))
            {
                objCA = new EChartOfAccount { SubAccName = acc.CHART_ACC_NAME, SubAccCode = acc.CHART_ACC_CODE };
            }
            return objCA;
        }              

        public bool SaveJournalVouchar(EJournalVouchar objEJournalVouchar)
        {
            objDataContext = new CHART_ACCDataContext();
            var journalVouchar = new JOURNAL_VOUCHAR
            {
                JV_ID = objEJournalVouchar.Journal_Id,
                JV_ACCOUNT_ID = objEJournalVouchar.Journal_Acc_Id,
                JV_ACCOUNT_NAME = objEJournalVouchar.Journal_Acc_Name,
                JV_ACCOUNT_CODE = objEJournalVouchar.Journal_Acc_Code,
                JV_CHEQUE_NO=objEJournalVouchar.Journal_Cheque,
                JV_CHEQUE_DATE=objEJournalVouchar.Journal_ChequeDate,
                JV_NOTES = objEJournalVouchar.Journal_Notes,
                JV_DATE = objEJournalVouchar.Journal_Date,
                JV_DEBIT_AMOUNT = objEJournalVouchar.Journal_Debit_Amount,
                JV_CREDIT_AMOUNT = objEJournalVouchar.Jounal_Credit_Amount,
                JV_BANK_REMARK=objEJournalVouchar.Journal_BankRemarks,
                JV_ACCESS_BY = objEJournalVouchar.Access_By,
                JV_ACCESS_DATE = objEJournalVouchar.Access_Date
            };
            objDataContext.JOURNAL_VOUCHARs.InsertOnSubmit(journalVouchar);
            objDataContext.SubmitChanges();
            return true;
        }

        public bool DoesExistVouchar(string vouchar, string TRANSACTION_TYPE)
        {
            objDataContext = new CHART_ACCDataContext();
            return objDataContext.DEPOSIT_PAYMENTs.Any(DP => DP.JOURNAL_VOUCHER == vouchar && DP.TRANSACTION_TYPE == TRANSACTION_TYPE);
        }
        public List<EDeposit_Payment> GetAllDepositPaymentByVouchar(string vouchar,string Transactiontype)
        {
            objDataContext = new CHART_ACCDataContext();
            List<EDeposit_Payment> listDeposit = new List<EDeposit_Payment>();
            foreach (var dep in (from j in objDataContext.DEPOSIT_PAYMENTs where j.JOURNAL_VOUCHER == vouchar && j.TRANSACTION_TYPE == Transactiontype select j))
            {
                EDeposit_Payment objEDepositPayment = new EDeposit_Payment
                {
                    Id = dep.ID,                    
                    AccountID = dep.CHART_ACCOUNT.CHART_ACC_ID,
                    AccountCode = dep.CHART_ACCOUNT.CHART_ACC_CODE,
                    AccountName = dep.CHART_ACCOUNT.CHART_ACC_NAME,
                    DepositePaymentDate = dep.DEPOSIT_PAYMENT_DATE,
                    DepositPaymentAmount = Convert.ToDecimal(dep.AMOUNT),
                    BankAccountId = Convert.ToInt64(dep.BANK_ACCOUNT_ID),
                    BankAccountName = GetAccountInfo(Convert.ToInt64(dep.BANK_ACCOUNT_ID)).SubAccName,
                    Description = dep.DESCRIPTION,
                    BankInfoinCheque = dep.BANK_INFO_CHEQUE,
                    BankCheque = dep.BANK_CHEQUE,
                    BankChequeDate = dep.BANK_CHEQUE_DATE,
                    ClearingDate = dep.CLEARING_DATE,
                    Status = dep.STATUS,
                    JournalVoucherId = dep.JOURNAL_VOUCHER,
                    AccessBy = dep.ACCESS_BY,
                    AccessDatetime = dep.ACCESS_DATETIME
                };
                listDeposit.Add(objEDepositPayment);
            }
            return listDeposit;
        }

        public bool DeleteDepositPayment(string voucharId)
        {
            objDataContext = new CHART_ACCDataContext();
            var objDepositPayment = from j in objDataContext.DEPOSIT_PAYMENTs where j.JOURNAL_VOUCHER == voucharId select j;
            objDataContext.DEPOSIT_PAYMENTs.DeleteAllOnSubmit(objDepositPayment);
            objDataContext.SubmitChanges();
            return true;
        }
        public List<EDeposit_Payment> GetStatus_ClearingDate_CancelJV(string jVoucher)
        {
            objDataContext = new CHART_ACCDataContext();
            List<EDeposit_Payment> listStatus = new List<EDeposit_Payment>();

            foreach (var dep in (from j in objDataContext.DEPOSIT_PAYMENTs where j.JOURNAL_VOUCHER == jVoucher select j))
            {
                EDeposit_Payment objDep = new EDeposit_Payment();
                objDep.AccountID = dep.ACCOUNT_ID;
                objDep.Status = dep.STATUS;
                objDep.ClearingDate = dep.CLEARING_DATE;
                objDep.CancellingJV = dep.CANCEL_JVOUCHAR;
                listStatus.Add(objDep);
            }
            return listStatus;
        }
        public List<EChartOfAccount> GetSubAccount(long parentId)
        {
            objDataContext = new CHART_ACCDataContext();
            List<EChartOfAccount> list = new List<EChartOfAccount>();
            foreach (var c in (from j in objDataContext.CHART_ACCOUNTs where j.CHART_ACC_PARENT_ID == parentId && j.CHART_ACC_STATUS == "Active" && j.CHART_ACC_NAME != "Cash at Bank" && j.CHART_ACC_NAME != "Cash in Hand" orderby j.CHART_ACC_NAME select j))
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
        public bool SaveDepositPaymentinUpdateMode(EDeposit_Payment objEDepositPayment)
        {
            objDataContext = new CHART_ACCDataContext();
            var deposit = new DEPOSIT_PAYMENT
            {
                ACCOUNT_ID = objEDepositPayment.AccountID,
                DEPOSIT_PAYMENT_DATE = objEDepositPayment.DepositePaymentDate,
                AMOUNT = objEDepositPayment.DepositPaymentAmount,
                BANK_ACCOUNT_ID = objEDepositPayment.BankAccountId,
                DESCRIPTION = objEDepositPayment.Description,
                BANK_INFO_CHEQUE = objEDepositPayment.BankInfoinCheque,
                BANK_CHEQUE = objEDepositPayment.BankCheque,
                BANK_CHEQUE_DATE = objEDepositPayment.BankChequeDate,
                STATUS = objEDepositPayment.Status,
                TRANSACTION_TYPE = objEDepositPayment.TransactionType,
                JOURNAL_VOUCHER = objEDepositPayment.JournalVoucherId,
                CLEARING_DATE=objEDepositPayment.ClearingDate,
                CANCEL_JVOUCHAR=objEDepositPayment.CancellingJV,
                ACCESS_BY = objEDepositPayment.AccessBy,
                ACCESS_DATETIME = DateTime.Now
            };
            objDataContext.DEPOSIT_PAYMENTs.InsertOnSubmit(deposit);
            objDataContext.SubmitChanges();
            return true;
        }
        public EDeposit_Payment GetJournalBankInfoforPayment(string journalVoucher)
        {
            objDataContext = new CHART_ACCDataContext();
            EDeposit_Payment objEDep = new EDeposit_Payment();
            foreach (var dep in (from j in objDataContext.JOURNAL_VOUCHARs where j.JV_ID == journalVoucher && j.JV_DEBIT_AMOUNT==0 select j))
            {
                objEDep.BankAccountId = dep.JV_ACCOUNT_ID;
                objEDep.DepositPaymentAmount = Convert.ToDecimal(dep.JV_CREDIT_AMOUNT);
            }
            return objEDep;
        }
    }
}
