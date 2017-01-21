using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CHART_ACC_DAL;
using CHART_ACC_ENTITY;

namespace CHART_ACC_BLL
{
    public class BContraVoucher
    {
        ContraVoucharDAL objContraVoucherDal = new ContraVoucharDAL();

        public List<EAccountType> GetAllAccountType()
        {
            return objContraVoucherDal.GetAllAccountType();
        }

        public List<EChartOfAccount> GetSubAccount(long parentId)
        {
            return objContraVoucherDal.GetSubAccount(parentId);
        }

        public List<EChartOfAccount> GetAccountInfo(long accId)
        {
            return objContraVoucherDal.GetAccountInfo(accId);
        }

        public decimal GetCurrentBalanceOfAccount(long accID)
        {
            return objContraVoucherDal.GetCurrentBalanceOfAccount(accID);
        }
    }
}
