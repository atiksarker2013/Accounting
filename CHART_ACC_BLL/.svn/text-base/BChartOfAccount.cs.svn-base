using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CHART_ACC_ENTITY;
using CHART_ACC_DAL;

namespace CHART_ACC_BLL
{
    public class BChartOfAccount
    {
        ChartOfAccountDAL objChartOfAccountDal = new ChartOfAccountDAL();

        public List<EAccountType> GetAllAccountType()
        {
            return objChartOfAccountDal.GetAllAccountType();
        }

        public EAccountType GetSingleAccountType(long id)
        {
            return objChartOfAccountDal.GetSingleAccountType(id);
        }

        public bool SaveChartOfAccount(EChartOfAccount objEChartOfAccount)
        {
            return objChartOfAccountDal.SaveChartOfAccount(objEChartOfAccount);
        }

        public bool DoesExist(string SubAccount)
        {
            return objChartOfAccountDal.DoesExist(SubAccount);
        }

        public bool DoesCodeExist(string code)
        {
            return objChartOfAccountDal.DoesCodeExist(code);
        }

        public List<EChartOfAccount> GetSubAccount(long parentId)
        {
            return objChartOfAccountDal.GetSubAccount(parentId);
        }

        public List<EChartOfAccount> GetChartAccountAll()
        {
            return objChartOfAccountDal.GetChartAccountAll();
        }

        public long GetAccountID(string name)
        {
            return objChartOfAccountDal.GetAccountID(name);
        }

        public string GetAccount_Code(long accId)
        {
            return objChartOfAccountDal.GetAccount_Code(accId);
        }

        public EChartOfAccount GetAccountInfo(long accId)
        {
            return objChartOfAccountDal.GetAccountInfo(accId);
        }
    }
}
