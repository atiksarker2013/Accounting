using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CHART_ACC_ENTITY;
using CHART_ACC_DAL;

namespace CHART_ACC_BLL
{
    public class BChartOfAccountModify
    {
        ChartOfAccountModifyDAL objChartOfAccountModifyDal = new ChartOfAccountModifyDAL();

        public List<EAccountType> GetAllAccountType()
        {
            return objChartOfAccountModifyDal.GetAllAccountType();
        }
        public List<EChartOfAccount> GetSubAccount(long parentId)
        {
            return objChartOfAccountModifyDal.GetSubAccount(parentId);
        }
        public List<EChartOfAccount> GetAccountInfo(long accId)
        {
            return objChartOfAccountModifyDal.GetAccountInfo(accId);
        }
        public bool UpdateChartAccount(EChartOfAccount objEChartAccount)
        {
            return objChartOfAccountModifyDal.UpdateChartAccount(objEChartAccount);
        }
        public bool DoesExistInJournal(string myAccount)
        {
            return objChartOfAccountModifyDal.DoesExistInJournal(myAccount);
        }

        public bool UpdateChartAccountNewParent(EChartOfAccount objEChartAccount, long accId)
        {
            bool stat = false;
            if (UpdateChartAccount(objEChartAccount))
            {
                stat=objChartOfAccountModifyDal.UpdateOldParentHeaderStat(accId);
            }
                return stat;
        }

        public bool UpdateOldParentHeaderStat(long accId)
        {
            return objChartOfAccountModifyDal.UpdateOldParentHeaderStat(accId);
        }

        public bool DeleteChartAccount(long accId)
        {
            return objChartOfAccountModifyDal.DeleteChartAccount(accId);
        }

        public bool UpdateBalanceDebit(EChartOfAccount objEChartAccount)
        {
            return objChartOfAccountModifyDal.UpdateBalanceDebit(objEChartAccount);
        }

        public bool UpdateBalanceCredit(EChartOfAccount objEChartAccount)
        {
            return objChartOfAccountModifyDal.UpdateBalanceCredit(objEChartAccount);
        }
    }
}
