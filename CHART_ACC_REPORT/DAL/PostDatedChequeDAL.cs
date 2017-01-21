using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CHART_ACC_REPORT.ENTITY;
//Author:REFAT
namespace CHART_ACC_REPORT.DAL
{
    public class PostDatedChequeDAL
    {
        public List<EPostDatedCheque> GetAllPostDatedCheque(DateTime startdate,DateTime endDate)
        {
           ReportDataContext objDataContext = new ReportDataContext();
           List<EPostDatedCheque> listPostDatedCheque = new List<EPostDatedCheque>();
            foreach (var dep in (from j in objDataContext.DEPOSIT_PAYMENTs where j.DEPOSIT_PAYMENT_DATE>=startdate && j.DEPOSIT_PAYMENT_DATE<=endDate select j))
            {
                EPostDatedCheque objEPostDatedCheque = new EPostDatedCheque
                {
                    Id = dep.ID,
                    AccountID = dep.CHART_ACCOUNT.CHART_ACC_ID,
                    AccountCode = dep.CHART_ACCOUNT.CHART_ACC_CODE,
                    AccountName = dep.CHART_ACCOUNT.CHART_ACC_NAME,
                    DepositePaymentDate = Convert.ToDateTime(dep.DEPOSIT_PAYMENT_DATE).ToShortDateString(),
                    DepositPaymentAmount = Convert.ToDecimal(dep.AMOUNT),
                    BankAccountId = Convert.ToInt64(dep.BANK_ACCOUNT_ID),
                    BankAccountName = GetAccountInfo(Convert.ToInt64(dep.BANK_ACCOUNT_ID)).SubAccName,
                    BankAccountCode = GetAccountInfo(Convert.ToInt64(dep.BANK_ACCOUNT_ID)).SubAccCode,
                    Description = dep.DESCRIPTION,
                    BankInfoinCheque = dep.BANK_INFO_CHEQUE,
                    BankCheque = dep.BANK_CHEQUE,
                    BankChequeDate = Convert.ToDateTime(dep.BANK_CHEQUE_DATE).ToShortDateString(),
                    TransactionType=dep.TRANSACTION_TYPE,
                    Status = dep.STATUS,
                    JournalVoucherId = dep.JOURNAL_VOUCHER                    
                };
                listPostDatedCheque.Add(objEPostDatedCheque);
            }
            return listPostDatedCheque;
        }
        public EChartOfAccount GetAccountInfo(long id)
        {
            ReportDataContext objDataContext = new ReportDataContext();
            EChartOfAccount objCA = null;
            foreach (var acc in (from j in objDataContext.CHART_ACCOUNTs where j.CHART_ACC_ID == id select j))
            {
                objCA = new EChartOfAccount { SubAccName = acc.CHART_ACC_NAME, SubAccCode = acc.CHART_ACC_CODE };
            }
            return objCA;
        }  
    }
}
