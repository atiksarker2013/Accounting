using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CHART_ACC_DAL;
using CHART_ACC_ENTITY;

namespace CHART_ACC_BLL
{
    public class BDepriciationSetup
    {
        DepriciationSetupDAL objDepriciationSetupDal = new DepriciationSetupDAL();

        public bool SaveDepriciationSetup(EDepriciationSetup objDepriciationSetup)
        {
           return objDepriciationSetupDal.SaveDepriciationSetup(objDepriciationSetup);
        }
        public List<EDepriciationSetup> GetAllDepriciationSetup()
        {
            return objDepriciationSetupDal.GetAllDepriciationSetup();
        }
        public bool DoesAccountExist(long AccountId)
        {
            return objDepriciationSetupDal.DoesAccountExist(AccountId);
        }
        public bool UpdateDepriciationSetup(EDepriciationSetup objDepriciationSetup)
        {
            return objDepriciationSetupDal.UpdateDepriciationSetup(objDepriciationSetup);
        }
    }
}
