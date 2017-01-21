using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CHART_ACC_ENTITY;
using CHART_ACC_DAL;
//Author:REFAT
namespace CHART_ACC_BLL
{
    public class BMemberAssign
    {
        MemberAssignDAL objMemberAssignDAL = new MemberAssignDAL();

        public long SaveChartOfAccount(EMemberAssign objEMemberAssign)
        {
            return objMemberAssignDAL.SaveChartOfAccount(objEMemberAssign);
        }
        public bool SaveMemberAssign(EMemberAssign objEMemberAssign)
        {
            return objMemberAssignDAL.SaveMemberAssign(objEMemberAssign);
        }

        public bool DoesExistMemberAssign(long MemberId, long MemberParentAccId)
        {
            return objMemberAssignDAL.DoesExistMemberAssign(MemberId,MemberParentAccId);
        }
        public bool DoesExistsInAssign(long MemberId)
        {
            return objMemberAssignDAL.DoesExistsInAssign(MemberId);
        }

        public List<EMemberAssign> GetMemberAllAccount(long MemberId)
        {
            return objMemberAssignDAL.GetMemberAllAccount(MemberId);
        }
    }
}
