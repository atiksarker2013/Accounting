using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SPIDER_SECURITY.Entity;
//Author:Refat
namespace SPIDER_SECURITY.DAL
{
    public class MenuPermissionDAL
    {
        public bool SaveMenuUserGroupWise(EMenuPermission objMenu)
        {
            Spider_AccDataContext objDataContext = new Spider_AccDataContext();            
            var newMenu = new ROLEWISE_MENU
            {
                MODULE_ID=objMenu.ModuleId,
                USER_GROUP_ID=objMenu.UserGroupId,
                PARENT_MENU_NAME=objMenu.ParentMenuName,
                PARENT_MENU_CONTENT=objMenu.ParentMenuContent,
                CHILD_MENU_NAME=objMenu.ChildMenuName,
                CHILD_MENU_CONTENT=objMenu.ChildMenuContent
            };
            objDataContext.ROLEWISE_MENUs.InsertOnSubmit(newMenu);
            objDataContext.SubmitChanges();
            return true;

        }

        public List<EMenuPermission> GetAccParentMenuGroupWise(EMenuPermission objEMenu)
        {
            List<EMenuPermission> listAccParentMenu = new List<EMenuPermission>();

            Spider_AccDataContext objDataContext = new Spider_AccDataContext();
            var query = (from j in objDataContext.ROLEWISE_MENUs where j.USER_GROUP_ID == objEMenu.UserGroupId && j.MODULE_ID == objEMenu.ModuleId select new { j.PARENT_MENU_NAME, j.PARENT_MENU_CONTENT }).Distinct();
            foreach (var c in query)
            {
                EMenuPermission objAccMenu = new EMenuPermission();                
                objAccMenu.ParentMenuName = c.PARENT_MENU_NAME;
                objAccMenu.ParentMenuContent = c.PARENT_MENU_CONTENT;
                listAccParentMenu.Add(objAccMenu);
            }

            return listAccParentMenu;
        }

        public List<EMenuPermission> GetAccChildMenuGroupWise(EMenuPermission objEMenu)
        {
            List<EMenuPermission> listAccChildMenu = new List<EMenuPermission>();

            Spider_AccDataContext objDataContext = new Spider_AccDataContext();
            var query = from j in objDataContext.ROLEWISE_MENUs where (j.PARENT_MENU_NAME == objEMenu.ParentMenuName && j.USER_GROUP_ID == objEMenu.UserGroupId && j.MODULE_ID == objEMenu.ModuleId) select j;
            foreach (var c in query)
            {
                EMenuPermission objAccMenu = new EMenuPermission();
                objAccMenu.Id = Convert.ToInt64(c.ID);
                objAccMenu.ChildMenuName = c.CHILD_MENU_NAME;
                objAccMenu.ChildMenuContent = c.CHILD_MENU_CONTENT;
                listAccChildMenu.Add(objAccMenu);
            }
            return listAccChildMenu;
        }

        public bool DeleteUserRole(long groupId, long moduleId)
        {
            Spider_AccDataContext objDataContext = new Spider_AccDataContext();           
            var query = from r in objDataContext.ROLEWISE_MENUs where r.USER_GROUP_ID == groupId && r.MODULE_ID == moduleId select r;
            objDataContext.ROLEWISE_MENUs.DeleteAllOnSubmit(query);
            objDataContext.SubmitChanges();
            return true;
        }
    }
}
