using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CHART_ACC_DAL.DBML;
using CHART_ACC_ENTITY;

namespace CHART_ACC_DAL
{
    public class AccountTypeDAL
    {
        CHART_ACCDataContext objDataContext = new CHART_ACCDataContext();

        public bool SaveAccountType(string AccountType)
        {
            var accountType = new ACCOUNT_TYPE
            {
             ACC_NAME = AccountType
            };
            objDataContext.ACCOUNT_TYPEs.InsertOnSubmit(accountType);
            objDataContext.SubmitChanges();
            return true;
        }
        public bool DoesExist(string AccountType)
        {
            objDataContext = new CHART_ACCDataContext();
            return (objDataContext.ACCOUNT_TYPEs.Any(accType => accType.ACC_NAME.Equals(AccountType)));
        }

        public List<EAccountType> GetAllAccountType()
        {
            objDataContext = new CHART_ACCDataContext();
            List<EAccountType> listOfType = new List<EAccountType>();

            var query = from accType in objDataContext.ACCOUNT_TYPEs orderby accType.ACC_NAME select accType;
            foreach (ACCOUNT_TYPE acctype in query)
            {
                EAccountType objEAccountType = new EAccountType();
                objEAccountType.AccTypeId = acctype.ACC_TYPE_ID;
                objEAccountType.AccTypeName = acctype.ACC_NAME;
                listOfType.Add(objEAccountType);
            }
            return listOfType;
        }        
        public void DeleteAccType(long  Accid)
        {
            objDataContext = new CHART_ACCDataContext();
            var query = from acc in objDataContext.ACCOUNT_TYPEs where acc.ACC_TYPE_ID == Accid select acc;
            objDataContext.ACCOUNT_TYPEs.DeleteOnSubmit(query.FirstOrDefault());
            objDataContext.SubmitChanges();
        }

        public void UpdateAccType(EAccountType eAccType)
        {
            objDataContext = new CHART_ACCDataContext();
            var query = (from acc in objDataContext.ACCOUNT_TYPEs where acc.ACC_TYPE_ID == eAccType.AccTypeId select acc).FirstOrDefault();
            query.ACC_NAME = eAccType.AccTypeName;
            
            objDataContext.SubmitChanges();
        }
    }
}
