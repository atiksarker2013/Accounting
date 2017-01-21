using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CHART_ACC_REPORT.ENTITY;
using CHART_ACC_REPORT.DAL;
//Author:REFAT
namespace CHART_ACC_REPORT.BLL
{
    public class BMember
    {
        MemberDAL objMemberDAL = new MemberDAL();
        
        public List<EMember> GetAllMemberAccountType()
        {
            return objMemberDAL.GetAllMemberAccountType();
        }
        public List<EMember> GetAllMember(long ParentAccId)
        {
            return objMemberDAL.GetAllMember(ParentAccId);
        }
        public bool DoesExistMemberAccount(string accNo, long parentAccId)
        {
            return objMemberDAL.DoesExistMemberAccount(accNo,parentAccId);
        }
    }
}
