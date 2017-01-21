using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CHART_ACC_ENTITY;
using CHART_ACC_DAL;
//Author:REFAT
namespace CHART_ACC_BLL
{
    public class BMenuPermission
    {
        MenuPermissionDAL objMenuDal = new MenuPermissionDAL();

        public bool IsExistParentMenu(string menuName, long UserGroupId)
        {
            return objMenuDal.IsExistParentMenu(menuName, UserGroupId);
        }
        public bool IsExistChildMenu(string menuName, long UserGroupId)
        {
            return objMenuDal.IsExistChildMenu(menuName, UserGroupId);
        }
    }
}
