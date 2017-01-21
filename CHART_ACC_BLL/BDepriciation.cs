using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CHART_ACC_ENTITY;
using CHART_ACC_DAL;

namespace CHART_ACC_BLL
{
    public class BDepriciation
    {
        DepriciationDAL objDepriciationDal = new DepriciationDAL();
        decimal debitBalance = 0, creditbalance = 0;

        List<EDepriciation> listDepriciation = new List<EDepriciation>();

        private void Ptree(long pId)
        {
            foreach (EDepriciation obj in objDepriciationDal.GetSubAccount(pId))
            {
                if (obj.IsHeader == "Yes")
                {
                    GetBalance(obj.AccountId);
                    if (debitBalance != 0 || creditbalance != 0)
                    {
                        obj.Debit += debitBalance;
                        obj.Credit += creditbalance;
                    }
                }
                listDepriciation.Add(obj);
                debitBalance = 0; creditbalance = 0;
            }
        }

        private void GetBalance(long pId)
        {
            foreach (EDepriciation obj in objDepriciationDal.GetSubAccount(pId))
            {
                debitBalance += Convert.ToDecimal(obj.Debit);
                creditbalance += Convert.ToDecimal(obj.Credit);
                if (obj.IsHeader == "Yes")
                {
                    GetBalance(obj.AccountId);
                }
            }
        }

        public List<EDepriciation> GetFixedAssets()
        {
            Ptree(objDepriciationDal.GetFixedAssetId());
            return listDepriciation;
        }

        public bool SaveJournalVoucharDR(EDepriciation objDepriciation)
        {
            return objDepriciationDal.SaveJournalVoucharDR(objDepriciation);
        }
        public bool SaveJournalVoucharCR(EDepriciation objDepriciation)
        {
            return objDepriciationDal.SaveJournalVoucharCR(objDepriciation);
        }
        public bool SaveDepriciation(EDepriciation objDepriciation)
        {
            return objDepriciationDal.SaveDepriciation(objDepriciation);
        }

        public List<EDepriciation> GetAllDepriciationSetup()
        {
            return objDepriciationDal.GetAllDepriciationSetup();
        }

        public List<EDepriciation> GetDepriciationForCurrentYear()
        {
            return objDepriciationDal.GetDepriciationForCurrentYear();
        }

        public bool DeleteDepriciation()
        {
            return objDepriciationDal.DeleteDepriciation();
        }

        public bool DeleteJournalOfDepriciation(string Journal_Id)
        {
            return objDepriciationDal.DeleteJournalOfDepriciation(Journal_Id);
        }

        /*************/

        public List<EChartOfAccount> GetChartAccountIdWise(long accId)
        {
            return objDepriciationDal.GetChartAccountIdWise(accId);
        }
        decimal amnt = 0;
        public void GetBalanceFixedAsset(long pId)
        {
            foreach (EDepriciation obj in objDepriciationDal.GetSubAccount(pId))
            {                
                amnt += obj.Debit - obj.Credit;
                if (obj.IsHeader == "Yes")
                {
                    GetBalanceFixedAsset(obj.AccountId);
                }
            }            
        }
        public decimal FixedAssetsBalance(long pId)
        {
            decimal cost = 0;
            EDepriciation obj = objDepriciationDal.GetCurrentDebitCredit(pId);

            GetBalanceFixedAsset(pId);
            cost = obj.Debit - obj.Credit;
            cost += amnt;
            amnt = 0;
            return cost;
        }
    }
}
