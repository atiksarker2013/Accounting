using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CHART_ACC_DAL.DBML;
using CHART_ACC_ENTITY;

namespace CHART_ACC_DAL
{
    public class DepriciationSetupDAL
    {
        CHART_ACCDataContext objDataContext;

        public bool SaveDepriciationSetup(EDepriciationSetup objDepriciationSetup)
        {
            objDataContext = new CHART_ACCDataContext();
            var depriciationSetup = new DEPRICIATION_SETUP
            {
                ACCOUNT_ID =Convert.ToInt64(objDepriciationSetup.Account_Id),
                DEP_PERCENTAGE =objDepriciationSetup.Dep_Percentage,
                DEBIT_ACC_ID = objDepriciationSetup.Debit_Account_Id,
                CREDIT_ACC_ID = objDepriciationSetup.Credit_Account_Id,
                ACCESS_BY = objDepriciationSetup.Access_By,
                ACCESS_DATE = objDepriciationSetup.Access_Date
            };
            objDataContext.DEPRICIATION_SETUPs.InsertOnSubmit(depriciationSetup);
            objDataContext.SubmitChanges();
            return true;
        }

        public List<EDepriciationSetup> GetAllDepriciationSetup()
        {
            CHART_ACCDataContext objDataContext = new CHART_ACCDataContext();
            List<EDepriciationSetup> depriciationSetupList = new List<EDepriciationSetup>();

            foreach (var c in objDataContext.DEPRICIATION_SETUPs)
            {
                EDepriciationSetup objDepriciationSetup = new EDepriciationSetup();
                objDepriciationSetup.Depriciation_Id = c.DEP_SET_ID;
                objDepriciationSetup.Account_Id = c.ACCOUNT_ID;
                objDepriciationSetup.Account_Name = c.CHART_ACCOUNT.CHART_ACC_NAME;
                objDepriciationSetup.Dep_Percentage = c.DEP_PERCENTAGE; 
                objDepriciationSetup.Debit_Account_Id = c.DEBIT_ACC_ID;
                objDepriciationSetup.Credit_Account_Id = c.CREDIT_ACC_ID;
                objDepriciationSetup.Access_By = c.ACCESS_BY;
                objDepriciationSetup.Access_Date = c.ACCESS_DATE;
                depriciationSetupList.Add(objDepriciationSetup);
            }
            return depriciationSetupList;
        }

        public bool DoesAccountExist(long AccountId)
        {
            objDataContext = new CHART_ACCDataContext();
            return (objDataContext.DEPRICIATION_SETUPs.Any(subAccount => subAccount.ACCOUNT_ID.Equals(AccountId)));
        }

        public bool UpdateDepriciationSetup(EDepriciationSetup objDepriciationSetup)
        {
            objDataContext = new CHART_ACCDataContext();
            var UpdateDepri = objDataContext.DEPRICIATION_SETUPs.First(C => C.DEP_SET_ID==objDepriciationSetup.Depriciation_Id);
            UpdateDepri.ACCESS_BY = objDepriciationSetup.Access_By;
            UpdateDepri.ACCESS_DATE = objDepriciationSetup.Access_Date;
            UpdateDepri.ACCOUNT_ID =Convert.ToInt64(objDepriciationSetup.Account_Id);
            UpdateDepri.CREDIT_ACC_ID = objDepriciationSetup.Credit_Account_Id;
            UpdateDepri.DEBIT_ACC_ID = objDepriciationSetup.Debit_Account_Id;
            UpdateDepri.DEP_PERCENTAGE = objDepriciationSetup.Dep_Percentage;
            objDataContext.SubmitChanges();
            return true;
        }
    }
}
