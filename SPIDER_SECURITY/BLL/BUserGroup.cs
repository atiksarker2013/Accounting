using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SPIDER_SECURITY.DAL;
using SPIDER_SECURITY.Entity;
//Author:REFAT
namespace SPIDER_SECURITY.BLL
{
    public class BUserGroup
    {
        UserGroupDAL anUserGroupDal=new UserGroupDAL();

        public bool SaveUserGroup(EUserGroup objEuserGroup)
        {
            return anUserGroupDal.SaveUserGroup(objEuserGroup);
        }

        public bool DoesExistGroupinSaveMode(string groupName)
        {
            foreach (var anUserGroup in GetAllUserGroup())
            {
                if (anUserGroup.GroupName.ToLower() == groupName.ToLower())
                {
                    return true;
                }
            }
            return false;
        }

        public bool DoesExistGroupinUpdateMode(string groupName, long userGroupId)
        {
            foreach (var anUserGroup in GetAllUserGroup())
            {
                if (anUserGroup.Id!=userGroupId && anUserGroup.GroupName.ToLower() == groupName.ToLower())
                {
                    return true;
                }
            }
            return false;
        }

        public bool UpdateUserGroup(EUserGroup anUserGroup)
        {
            return anUserGroupDal.UpdateUserGroup(anUserGroup);
        }

        public bool DeleteUserGroup(long userGroupId)
        {
            return anUserGroupDal.DeleteUserGroup(userGroupId);
        }

        public List<EUserGroup> GetAllUserGroup()
        {
            return anUserGroupDal.GetAllUserGroup();
        }

        public bool DoesExistAnyUserUnderThisGroup(long groupId)
        {
            return anUserGroupDal.DoesExistAnyUserUnderThisGroup(groupId);
        }
    }
}
