using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SPIDER_SECURITY.Entity;
//Author:REFAT
namespace SPIDER_SECURITY.DAL
{
   public class UserDAL
    {
       public bool SaveUserInfo(EUser anUser)
       {
           Spider_AccDataContext objDataContext = new Spider_AccDataContext();
           var newUser = new USER
           {
               USER_NAME = anUser.UserName,
               USER_PASSWORD = anUser.UserPassword,
               USER_GROUP_ID = anUser.UserGroupId
           };
           objDataContext.USERs.InsertOnSubmit(newUser);
           objDataContext.SubmitChanges();
           return true;
       }

       public bool DoesExistUserinSaveMode(string userName)
       {
           Spider_AccDataContext objDataContext = new Spider_AccDataContext();
           return (objDataContext.USERs.Any(user => user.USER_NAME.Equals(userName)));
       }
       
       public bool UpdateUserInfo(EUser anUser)
       {
           Spider_AccDataContext objDataContext = new Spider_AccDataContext();

           var user = objDataContext.USERs.FirstOrDefault(u => u.USER_ID == anUser.Id);
           user.USER_NAME = anUser.UserName;
           user.USER_PASSWORD = anUser.UserPassword;
           user.USER_GROUP_ID = anUser.UserGroupId;
           objDataContext.SubmitChanges();
           return true;
       }

       public bool DeleteUser(long userId)
       {
           Spider_AccDataContext objDataContext = new Spider_AccDataContext();

           USER userObj = objDataContext.USERs.FirstOrDefault(u => u.USER_ID == userId);
           objDataContext.USERs.DeleteOnSubmit(userObj);
           objDataContext.SubmitChanges();
           return true;
       }

       public List<EUser> GetAllUser()
       {
           Spider_AccDataContext objDataContext = new Spider_AccDataContext();

           List<EUser> listUser = new List<EUser>();
           foreach (var user in objDataContext.USERs)
           {
               EUser anUser = new EUser();
               anUser.Id = user.USER_ID;
               anUser.UserName = user.USER_NAME;
               anUser.UserPassword = user.USER_PASSWORD;
               anUser.UserGroupId = user.USER_GROUP_ID;
               anUser.UserGroupName = user.USER_GROUP.GROUP_NAME;
               listUser.Add(anUser);
           }
           return listUser;
       }
       public EUser GetAllInfoforSingleUser( EUser anUser)
       {

           Spider_AccDataContext objDataContext = new Spider_AccDataContext();
          
           var query = from j in objDataContext.USERs where j.USER_NAME == anUser.UserName.ToLower() select j;
           foreach (var user in query)
           {               
               anUser.Id = user.USER_ID;
               anUser.UserName = user.USER_NAME;
               anUser.UserPassword = user.USER_PASSWORD;
               anUser.UserGroupId = user.USER_GROUP_ID;
               anUser.UserGroupName = user.USER_GROUP.GROUP_NAME;               
           }
           return anUser;
       }
       public bool UpdateUserPassword(EUser anUser)
       {
           Spider_AccDataContext objDataContext = new Spider_AccDataContext();

           var user = objDataContext.USERs.FirstOrDefault(u => u.USER_ID == anUser.Id);
           user.USER_PASSWORD = anUser.UserPassword;
           objDataContext.SubmitChanges();
           return true;
       }
       /******************Login Purpose*****************/
       public bool DoesExistUserinLoginMode(EUser anUser)
       {
           Spider_AccDataContext objDataContext = new Spider_AccDataContext();
           return (objDataContext.USERs.Any(user => user.USER_NAME.Equals(anUser.UserName) && user.USER_PASSWORD.Equals(anUser.UserPassword)));
       }
    }
}
