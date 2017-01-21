using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CHART_ACC_REPORT.ENTITY;
using CHART_ACC_REPORT.DAL;

namespace CHART_ACC_REPORT.BLL
{
    public class BChartAccount
    {
        ChartAccountDAL objChartDAL = new ChartAccountDAL();
        public List<EAccountType> GetAllAccountType()
        {
            return objChartDAL.GetAllAccountType();
        }
        public List<EChartOfAccount> GetSubAccount(long parentId)
        {
            return objChartDAL.GetSubAccount(parentId);
        }
        public EChartOfAccount GetAccountInfo(long accId)
        {
            return objChartDAL.GetAccountInfo(accId);
        }
        public EChartOfAccount GetAccountInfoCodeWise(string accCode)
        {
            return objChartDAL.GetAccountInfoCodeWise(accCode);
        }
        public long GetAccountID(string name)
        {
            return objChartDAL.GetAccountID(name);
        }
        public EChartOfAccount GetFisDate()
        {
            return objChartDAL.GetFisDate();
        }
        public EChartOfAccount GetCompanyFirstFisDate()
        {
            return objChartDAL.GetCompanyFirstFisDate();
        }
        public List<EChartOfAccount> GetAccountInfoLikeNameWise(string accName)
        {
            return objChartDAL.GetAccountInfoLikeNameWise(accName);
        }

        public int GetFiscalId(DateTime dateTime)
        {
            return objChartDAL.GetFiscalId(dateTime);
        }
        public EChartOfAccount GetSingleFiscalInfo(int id)
        {
            return objChartDAL.GetSingleFiscalInfo(id);
        }

        public bool DoesAccountExist(string AccountNo)
        {
            return objChartDAL.DoesAccountExist(AccountNo);
        }
    }
}
