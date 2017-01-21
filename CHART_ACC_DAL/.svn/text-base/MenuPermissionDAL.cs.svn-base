using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CHART_ACC_ENTITY;
using CHART_ACC_DAL.DBML;
//Author:REFAT
namespace CHART_ACC_DAL
{
    public class MenuPermissionDAL
    {
        public bool IsExistParentMenu(string menuName, long UserGroupId)
        {
            CHART_ACCDataContext objDataContext = new CHART_ACCDataContext();
            return objDataContext.ROLEWISE_MENUs.Any(RM => RM.PARENT_MENU_NAME == menuName && RM.USER_GROUP_ID == UserGroupId && RM.MODULE_PERMISSION.MODULE_NAME == "Accounting");
        }
        public bool IsExistChildMenu(string menuName, long UserGroupId)
        {
            CHART_ACCDataContext objDataContext = new CHART_ACCDataContext();
            return objDataContext.ROLEWISE_MENUs.Any(RM => RM.CHILD_MENU_NAME == menuName && RM.USER_GROUP_ID == UserGroupId && RM.MODULE_PERMISSION.MODULE_NAME == "Accounting");
        }
    }
}
