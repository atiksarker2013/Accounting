using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CHART_ACC_ENTITY;
using CHART_ACC_DAL;

namespace CHART_ACC_BLL
{
    public class BMemberSetup
    {
        MemberSetupDAL objMemberSetupDal = new MemberSetupDAL();

        public bool SaveMemberInfo(EMemberSetup objEMemberSetup)
        {
            return objMemberSetupDal.SaveMemberInfo(objEMemberSetup);
        }
        public bool DoesExist(string MemberNo)
        {
            return objMemberSetupDal.DoesExist(MemberNo);
        }

        public List<EMemberSetup> GetInfoMemberNoWise(string Member_No)
        {
            return objMemberSetupDal.GetInfoMemberNoWise(Member_No);
        }

        public bool UpdateMemberInfo(EMemberSetup objEMemberSetup)
        {
            return objMemberSetupDal.UpdateMemberInfo(objEMemberSetup);
        }
        public List<EMemberSetup> GetInfoAllMember()
        {
            return objMemberSetupDal.GetInfoAllMember();
        }

        public bool DeleteMember(long MemberId)
        {
            return objMemberSetupDal.DeleteMember(MemberId);
        }
    }
}
