using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CHART_ACC_DAL;
using CHART_ACC_ENTITY;

namespace CHART_ACC_BLL
{
    public class BJournalVouchar
    {
        JournalVoucharDAL objJournalVoucharDal = new JournalVoucharDAL();

        public string GetJVoucherLastID()
        {
            return objJournalVoucharDal.GetJVoucherLastID();
        }

        public bool SaveJournalVouchar(EJournalVouchar objEJournalVouchar)
        {
            return objJournalVoucharDal.SaveJournalVouchar(objEJournalVouchar);
        }

        public bool DeleteJournalVouchar(string voucharId)
        {
            return objJournalVoucharDal.DeleteJournalVouchar(voucharId);
        }

        public List<EChartOfAccount> GetAccountInfo(long accId)
        {
            return objJournalVoucharDal.GetAccountInfo(accId);
        }

        public List<EChartOfAccount> GetAccountInfoCodeWise(string accCode)
        {
            return objJournalVoucharDal.GetAccountInfoCodeWise(accCode);
        }

        public List<EChartOfAccount> GetAccountInfoNameWise(string accName)
        {
            return objJournalVoucharDal.GetAccountInfoNameWise(accName);
        }

        public List<EChartOfAccount> GetAccountInfoLikeNameWise(string accName)
        {
            return objJournalVoucharDal.GetAccountInfoLikeNameWise(accName);
        }

        public decimal GetCurrentBalanceOfAccount(long accID)
        {
            return objJournalVoucharDal.GetCurrentBalanceOfAccount(accID);
        }

        public List<EJournalVouchar> GetAllJournalVoucharID()
        {
            return objJournalVoucharDal.GetAllJournalVoucharID();
        }

        public List<EJournalVouchar> GetAllJournalVoucharIdWise(string Jv_Id)
        {
            return objJournalVoucharDal.GetAllJournalVoucharIdWise(Jv_Id);
        }

        // For Tree

        public List<EChartOfAccount> GetSubAccount(long parentId)
        {
            return objJournalVoucharDal.GetSubAccount(parentId);
        }
        public bool DoesExistVoucharWithoutBank(string jvId)
        {
            return objJournalVoucharDal.DoesExistVoucharWithoutBank(jvId);
        }
        public bool DoesExistContraVoucher(string jvId)
        {
            return objJournalVoucharDal.DoesExistContraVoucher(jvId);
        }
        //by Refat for cash Deposit/Payment
        public bool DoesExistVoucharforCashDepojitPayment(string jvId, string remark)
        {
            return objJournalVoucharDal.DoesExistVoucharforCashDepojitPayment(jvId, remark);
        }

        public bool SaveJournalVoucharWithBankCheque(EJournalVouchar objEJournalVouchar)
        {
            return objJournalVoucharDal.SaveJournalVoucharWithBankCheque(objEJournalVouchar);
        }
    }
}
