using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CHART_ACC_ENTITY;
using CHART_ACC_DAL;

namespace CHART_ACC_BLL
{
    public class BMemberAccountType
    {
        MemberAccountTypeDAL objMemberAccountTypeDal = new MemberAccountTypeDAL();

        public long SaveAccountTypeINChartOfAccount(EMemberAccountType objEMemberAccountType)
        {
            return objMemberAccountTypeDal.SaveAccountTypeINChartOfAccount(objEMemberAccountType);
        }

        public bool SaveMemberAccountType(long Acc_Type_Id, string Account_Prefix)
        {
            return objMemberAccountTypeDal.SaveMemberAccountType(Acc_Type_Id, Account_Prefix);
        }
        public List<EMemberAccountType> GetAllMemberAccountType()
        {
            return objMemberAccountTypeDal.GetAllMemberAccountType();
        }

        public bool DoesAccountNoExist(string account_No)
        {
            return objMemberAccountTypeDal.DoesAccountNoExist(account_No);
        }
        public bool DoesPrefixExist(string account_Prefix)
        {
            return objMemberAccountTypeDal.DoesPrefixExist(account_Prefix);
        }
    }
}
