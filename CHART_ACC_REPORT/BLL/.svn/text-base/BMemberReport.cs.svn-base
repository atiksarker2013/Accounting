using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CHART_ACC_REPORT.ENTITY;
using CHART_ACC_REPORT.DAL;

namespace CHART_ACC_REPORT.BLL
{
    public class BMemberReport
    {
        MemberReportDAL objMemberReportDal = new MemberReportDAL();

        public List<EMemberReport> GetInfoMemberNoWise(string Member_No)
        {
            return objMemberReportDal.GetInfoMemberNoWise(Member_No);
        }

        public List<EMemberReport> GetInfoAllMember()
        {
            return objMemberReportDal.GetInfoAllMember();
        }

        public bool DoesExist(string MemberNo)
        {
            return objMemberReportDal.DoesExist(MemberNo);
        }
    }
}
