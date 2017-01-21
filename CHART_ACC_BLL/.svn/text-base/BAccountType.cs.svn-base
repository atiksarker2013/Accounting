using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CHART_ACC_DAL;
using CHART_ACC_ENTITY;

namespace CHART_ACC_BLL
{
    public class BAccountType
    {
        AccountTypeDAL objAccountTypeDal = new AccountTypeDAL();

        public bool SaveAccountType(string AccountType)
        {
            return objAccountTypeDal.SaveAccountType(AccountType);
        }
        public bool DoesExist(string AccountType)
        {
            return objAccountTypeDal.DoesExist(AccountType);
        }
        public List<EAccountType> GetAllAccountType()
        {
            return objAccountTypeDal.GetAllAccountType();
        }        
        public void DeleteAccType(long Accid)
        {
            objAccountTypeDal.DeleteAccType(Accid);
        }
        public void UpdateAccType(EAccountType eAccType)
        {
            objAccountTypeDal.UpdateAccType(eAccType);
        }
    }
}
