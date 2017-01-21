using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CHART_ACC_ENTITY;
using CHART_ACC_DAL.DBML;

namespace CHART_ACC_DAL
{
    public class MemberAccountTypeDAL
    {
        CHART_ACCDataContext objDataContext = new CHART_ACCDataContext();

        public long SaveAccountTypeINChartOfAccount(EMemberAccountType objEMemberAccountType)
        {
            objDataContext = new CHART_ACCDataContext();

            var chartOfAccount = new CHART_ACCOUNT
            {
                CHART_ACC_CODE = objEMemberAccountType.Account_No,
                CHART_ACC_NAME = objEMemberAccountType.Account_Name,
                CHART_ACC_PARENT_ID = objEMemberAccountType.Parent_Account_No,
                CHART_ACC_PARENT_TYPE = objEMemberAccountType.Parent_Account_Type,
                CHART_ACC_OPENING_BALANCE_DR = 0,
                CHART_ACC_OPENING_BALANCE_CR = 0,
                CHART_ACC_STATUS = objEMemberAccountType.Account_Status,
                CHART_ACC_HEADER = "No",
                CHART_ACC_CREATION_DATE = DateTime.Now,
                ACCESS_BY = objEMemberAccountType.Access_By,
                ACCESS_DATE = DateTime.Now
            };
            objDataContext.CHART_ACCOUNTs.InsertOnSubmit(chartOfAccount);            
            objDataContext.SubmitChanges();
            UpdateParentHeaderStatus(objEMemberAccountType.Parent_Account_No);
            return chartOfAccount.CHART_ACC_ID;
        }

        public bool UpdateParentHeaderStatus(long id)//Refat
        {            
            var chart = objDataContext.CHART_ACCOUNTs.FirstOrDefault(c => c.CHART_ACC_ID == id);
            if (chart != null)
            {
                chart.CHART_ACC_HEADER = "Yes";
                objDataContext.SubmitChanges();
            }
            return true;
        }
        public bool SaveMemberAccountType(long Acc_Type_Id,string Account_Prefix)
        {
            objDataContext = new CHART_ACCDataContext();

            var AccountType = new MEMBER_ACC_TYPE
            {
                MEMBER_ACCOUNT_TYPE_ID = Acc_Type_Id,
                ACCOUNT_PREFIX=Account_Prefix
            };

            objDataContext.MEMBER_ACC_TYPEs.InsertOnSubmit(AccountType);
            objDataContext.SubmitChanges();
            return true;
        }
        public List<EMemberAccountType> GetAllMemberAccountType()
        {
            objDataContext = new CHART_ACCDataContext();
            List<EMemberAccountType> listMAccType = new List<EMemberAccountType>();
            foreach (var MType in objDataContext.MEMBER_ACC_TYPEs)
            {
                EMemberAccountType objEMType = new EMemberAccountType();
                objEMType.Account_Id = MType.MEMBER_ACCOUNT_TYPE_ID;
                objEMType.Account_Name = MType.CHART_ACCOUNT.CHART_ACC_NAME;
                objEMType.Account_Prefix = MType.ACCOUNT_PREFIX;
                objEMType.Account_No = MType.CHART_ACCOUNT.CHART_ACC_CODE;
                objEMType.Parent_Account_Type = MType.CHART_ACCOUNT.CHART_ACC_PARENT_TYPE;
                listMAccType.Add(objEMType);
            }
            return listMAccType;
        }

        public bool DoesAccountNoExist(string account_No)
        {
            objDataContext = new CHART_ACCDataContext();
            return (objDataContext.CHART_ACCOUNTs.Any(subAccount => subAccount.CHART_ACC_CODE.Equals(account_No)));
        }
        public bool DoesPrefixExist(string account_Prefix)
        {
            objDataContext = new CHART_ACCDataContext();
            return (objDataContext.MEMBER_ACC_TYPEs.Any(subAccount => subAccount.ACCOUNT_PREFIX.Equals(account_Prefix)));
        } 
    }
}
