using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CHART_ACC_ENTITY;
using CHART_ACC_DAL;
//Author:REFAT
namespace CHART_ACC_BLL
{
    public class BDeposit_Payment
    {
        Deposit_PaymentDAL objDepositeDAL = new Deposit_PaymentDAL();
        JournalVoucharDAL objJVDAL = new JournalVoucharDAL();
        public bool SaveDepositPayment(EDeposit_Payment objEDepositPayment)
        {
            bool stat = false;
            stat = objDepositeDAL.SaveDepositPayment(objEDepositPayment);            
            return stat;
        }

        public List<EDeposit_Payment> GetAllPendingDepositPayment(string Transactiontype)
        {
            return objDepositeDAL.GetAllPendingDepositPayment(Transactiontype);
        }

        public bool ClearingDepositPayment(EDeposit_Payment objEDP)
        {
            return objDepositeDAL.ClearingDepositPayment(objEDP);
        }
        public EChartOfAccount GetAccountInfo(long id)
        {
            return objDepositeDAL.GetAccountInfo(id);
        }        
        public bool SaveJournalVouchar(EJournalVouchar objEJournalVouchar)
        {
            return objDepositeDAL.SaveJournalVouchar(objEJournalVouchar);
        }

        public bool DoesExistVouchar(string vouchar, string TRANSACTION_TYPE)
        {
            return objDepositeDAL.DoesExistVouchar(vouchar, TRANSACTION_TYPE);
        }
        public List<EDeposit_Payment> GetAllDepositPaymentByVouchar(string vouchar, string Transactiontype)
        {
            return objDepositeDAL.GetAllDepositPaymentByVouchar(vouchar, Transactiontype);
        }
        public bool DeleteDepositPayment(string voucharId)
        {
            return objDepositeDAL.DeleteDepositPayment(voucharId);
        }
        public List<EDeposit_Payment> GetStatus_ClearingDate_CancelJV(string jVoucher)
        {
            return objDepositeDAL.GetStatus_ClearingDate_CancelJV(jVoucher);
        }
        public List<EChartOfAccount> GetSubAccount(long parentId)
        {
            return objDepositeDAL.GetSubAccount(parentId);
        }
        public bool SaveDepositPaymentinUpdateMode(EDeposit_Payment objEDepositPayment)
        {
            return objDepositeDAL.SaveDepositPaymentinUpdateMode(objEDepositPayment);
        }
        public EDeposit_Payment GetJournalBankInfoforPayment(string journalVoucher)
        {
            return objDepositeDAL.GetJournalBankInfoforPayment(journalVoucher);
        }
    }
}
