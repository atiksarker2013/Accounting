using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SPIDER_SECURITY.Entity;
//Author:Refat
namespace SPIDER_SECURITY.DAL
{
    public class ModulePermissionDAL
    {
        public bool SaveModulePermission(EModulePermission objEModPer)
        {
            Spider_AccDataContext objDataContext = new Spider_AccDataContext();
            var Module = new MODULE_PERMISSION
            {
                MODULE_NAME = objEModPer.ModuleName,
                USER_GROUP_ID = objEModPer.UserGroupId
            };
            objDataContext.MODULE_PERMISSIONs.InsertOnSubmit(Module);
            objDataContext.SubmitChanges();
            return true;
        }

        public bool DeleteModulePermission(long userGroupId)
        {
            Spider_AccDataContext objDataContext = new Spider_AccDataContext();
            var module = from j in objDataContext.MODULE_PERMISSIONs where j.USER_GROUP_ID == userGroupId select j;
            if (module != null)
            {
                objDataContext.MODULE_PERMISSIONs.DeleteAllOnSubmit(module);
                objDataContext.SubmitChanges();
                return true;
            }
            return false;
        }


        public List<EModulePermission> GetPermittedModule(long groupId)
        {
            Spider_AccDataContext objDataContext = new Spider_AccDataContext();
            List<EModulePermission> listPM = new List<EModulePermission>();
            
            foreach (var module in (from j in objDataContext.MODULE_PERMISSIONs where j.USER_GROUP_ID==groupId select j))
            {
                EModulePermission objEMPerm = new EModulePermission();
                objEMPerm.Id = module.ID;
                objEMPerm.ModuleName = module.MODULE_NAME;
                objEMPerm.UserGroupId =Convert.ToInt64(module.USER_GROUP_ID);
                objEMPerm.UserGroupName = module.USER_GROUP.GROUP_NAME;
                listPM.Add(objEMPerm);
            }
            return listPM;
        }

        public List<string> GetNOTPermittedModule(List<string> listAvailablemodule, List<string> listPermittedModule)
        {
            Spider_AccDataContext objDataContext = new Spider_AccDataContext();
            List<string> listnotPM = new List<string>();

            var query = from module in ((from avail in listAvailablemodule select avail).Union(from permitted in listPermittedModule select permitted)).Except(from peMod in listPermittedModule select peMod) select module;
            foreach (var module in query)
            {
                listnotPM.Add(module);
            }
            return listnotPM;
        }

        public bool DeleteSingleModulePermission(EModulePermission objEmodule)
        {
            Spider_AccDataContext objDataContext = new Spider_AccDataContext();

            MODULE_PERMISSION module = objDataContext.MODULE_PERMISSIONs.FirstOrDefault(mp => mp.USER_GROUP_ID == objEmodule.UserGroupId && mp.MODULE_NAME==objEmodule.ModuleName);
            if (module != null)
            {
                objDataContext.MODULE_PERMISSIONs.DeleteOnSubmit(module);
                objDataContext.SubmitChanges();
                return true;
            }
            return false;
        }
    }
}
