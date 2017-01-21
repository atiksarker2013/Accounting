using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SPIDER_SECURITY.DAL;
using SPIDER_SECURITY.Entity;
//Author:REFAT
namespace SPIDER_SECURITY.BLL
{
    public class BMenuPermission
    {
        MenuPermissionDAL objMenuDal = new MenuPermissionDAL();

        public bool SaveMenuUserGroupWise(EMenuPermission objMenu)
        {
            return objMenuDal.SaveMenuUserGroupWise(objMenu);
        }
        public bool DeleteUserRole(long groupId,long moduleId)
        {
            return objMenuDal.DeleteUserRole(groupId,moduleId);
        }

        public List<EMenuPermission> GetAccParentMenuGroupWise(EMenuPermission objEMP)
        {
            return objMenuDal.GetAccParentMenuGroupWise(objEMP);
        }

        public List<EMenuPermission> GetAccChildMenuGroupWise(EMenuPermission objEMP)
        {
            return objMenuDal.GetAccChildMenuGroupWise(objEMP);
        }
    }
}
