using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SPIDER_SECURITY.Entity;
//Author:REFAT
namespace SPIDER_SECURITY.DAL
{
    public class UserGroupDAL
    {
        public bool SaveUserGroup(EUserGroup anEuserGroup)
        {
            Spider_AccDataContext objDataContext = new Spider_AccDataContext();

            var newUserGroup = new USER_GROUP
            {
                GROUP_NAME = anEuserGroup.GroupName
            };
            objDataContext.USER_GROUPs.InsertOnSubmit(newUserGroup);
            objDataContext.SubmitChanges();
            return true;
        }

        public bool UpdateUserGroup(EUserGroup anUserGroup)
        {
            Spider_AccDataContext objDataContext = new Spider_AccDataContext();

            var group = objDataContext.USER_GROUPs.FirstOrDefault(u => u.GROUP_ID == anUserGroup.Id);
            group.GROUP_NAME = anUserGroup.GroupName;
            objDataContext.SubmitChanges();
            return true;
        }

        public bool DeleteUserGroup(long userGroupId)
        {
            Spider_AccDataContext objDataContext = new Spider_AccDataContext();

            USER_GROUP userGroupObj = objDataContext.USER_GROUPs.FirstOrDefault(u => u.GROUP_ID == userGroupId);
            objDataContext.USER_GROUPs.DeleteOnSubmit(userGroupObj);
            objDataContext.SubmitChanges();
            return true;
        }

        public List<EUserGroup> GetAllUserGroup()
        {
            Spider_AccDataContext objDataContext = new Spider_AccDataContext();

            List<EUserGroup> listUserGroup = new List<EUserGroup>();
            var query = from j in objDataContext.USER_GROUPs select j;
            foreach (var userGroup in query)
            {
                EUserGroup anUserGroup = new EUserGroup();
                anUserGroup.Id = userGroup.GROUP_ID;
                anUserGroup.GroupName = userGroup.GROUP_NAME;
                listUserGroup.Add(anUserGroup);
            }
            return listUserGroup;
        }

        public bool DoesExistAnyUserUnderThisGroup(long groupId)
        {
            Spider_AccDataContext objDataContext = new Spider_AccDataContext();

            return (objDataContext.USERs.Any(u => u.USER_GROUP_ID == groupId));  
        }
    }
}
