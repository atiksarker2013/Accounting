using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SPIDER_SECURITY.DAL;
using SPIDER_SECURITY.Entity;
//Author:REFAT
namespace SPIDER_SECURITY.BLL
{
    public class BUser
    {
        UserDAL anUserDAL = new UserDAL();

        public bool SaveUserInfo(EUser anUser)
        {
            return anUserDAL.SaveUserInfo(anUser);
        }
        public List<EUser> GetAllUser()
        {
            return anUserDAL.GetAllUser();
        }
        public bool DeleteUser(long userId)
        {
            return anUserDAL.DeleteUser(userId);
        }
        public bool DoesExistUserinSaveMode(string userName)
        {
            return anUserDAL.DoesExistUserinSaveMode(userName);
        }
        public bool DoesExistUserinLoginMode(EUser anUser)
        {
            if (anUserDAL.DoesExistUserinLoginMode(anUser))
            {
                GetAllInfoforSingleUser(anUser);
                return true;
            }
            else return false;
        }
        public EUser GetAllInfoforSingleUser(EUser anUser)
        {
            return anUserDAL.GetAllInfoforSingleUser(anUser);
        }
        public bool UpdateUserPassword(EUser anUser)
        {
            return anUserDAL.UpdateUserPassword(anUser);
        }

        public bool UpdateUserInfo(EUser anUser)
        {
            return anUserDAL.UpdateUserInfo(anUser);
        }

        public bool DoesExistUserinUpdateMode(string userName, long userId)
        {
            foreach (var anUser in anUserDAL.GetAllUser())
            {
                if (anUser.Id != userId && anUser.UserName.ToLower()==userName.ToLower())
                {
                    return true;
                }
            }
            return false;
        }
    }
}
