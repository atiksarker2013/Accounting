using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SPIDER_SECURITY.DAL;
using SPIDER_SECURITY.Entity;
//Author:REFAT
namespace SPIDER_SECURITY.BLL
{
    public class BModulePermission
    {
        ModulePermissionDAL objMPDal = new ModulePermissionDAL();
        
        public bool SaveModulePermission(EModulePermission objEModPer)
        {
            return objMPDal.SaveModulePermission(objEModPer);
        }

        public bool DeleteModulePermission(long userGroupId)
        {
            return objMPDal.DeleteModulePermission(userGroupId);
        }

        public List<EModulePermission> GetPermittedModule(long groupId)
        {
            return objMPDal.GetPermittedModule(groupId);
        }

        public List<string> GetNOTPermittedModule(List<string> listAvailablemodule, List<string> listPermitted)
        {
            return objMPDal.GetNOTPermittedModule(listAvailablemodule, listPermitted);
        }
        public bool DeleteSingleModulePermission(EModulePermission objEmodule)
        {
            return objMPDal.DeleteSingleModulePermission(objEmodule);
        }
    }
}
