using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CHART_ACC_REPORT.ENTITY;

namespace CHART_ACC_REPORT.DAL
{
    public class MemberReportDAL
    {
        public List<EMemberReport> GetInfoMemberNoWise(string Member_No)
        {
            ReportDataContext objDataContext = new ReportDataContext();
            List<EMemberReport> memberList = new List<EMemberReport>();

            var query = from j in objDataContext.MEMBER_INFOs where j.MEMBER_NO == Member_No select j;

            foreach (var C in query)
            {
                EMemberReport objMember = new EMemberReport();
                objMember.Id = C.ID;
                objMember.Member_No = C.MEMBER_NO;
                objMember.No_Of_Share = C.NO_OF_SHARE;
                objMember.Member_Full_Name = C.MEMBER_FULL_NAME;
                objMember.Father_Name = C.FATHER_NAME;
                objMember.Mother_Name = C.MOTHER_NAME;
                objMember.Husband_Name = C.HUSBAND_NAME;
                objMember.Present_Address = C.PRESENT_ADDRESS;
                objMember.Permanent_Address = C.PERMANENT_ADDRESS;
                objMember.Mobile_No = Convert.ToInt32(C.MOBILE_NO);
                objMember.Education_Status = C.EDUCATION_STATUS;
                objMember.Member_Birth_Date = Convert.ToDateTime(C.MEMBER_BIRTH_DATE);
                objMember.Religion = C.RELIGION;
                objMember.Member_Occupation = C.MEMBER_OCCUPATION;
                objMember.Marital_Status = C.MARITAL_STATUS;
                objMember.Nationality = C.NATIONALITY;
                objMember.Nominee_Name = C.NOMINEE_NAME;
                objMember.Nominee_Birth_Date = Convert.ToDateTime(C.NOMINEE_BIRTH_DATE);
                objMember.Nominee_Occupation = C.NOMINEE_OCCUPATION;
                objMember.Relation_With_Nominee = C.RELATION_WITH_NOMINEE;
                objMember.Member_Photo = C.MEMBER_PHOTO.ToArray();
                objMember.Digital_Signature = C.DIGITAL_SIGNATURE.ToArray();
                objMember.Nominee_Photo = C.NOMINEE_PHOTO.ToArray();
                objMember.Access_Date = Convert.ToDateTime(C.ACCESS_DATE);

                memberList.Add(objMember);
            }

            return memberList;
        }

        public List<EMemberReport> GetInfoAllMember()
        {
            ReportDataContext objDataContext = new ReportDataContext();
            List<EMemberReport> memberList = new List<EMemberReport>();

            var query = from j in objDataContext.MEMBER_INFOs orderby j.MEMBER_NO select j;

            foreach (var C in query)
            {
                EMemberReport objMember = new EMemberReport();
                objMember.Id = C.ID;
                objMember.Member_No = C.MEMBER_NO;
                objMember.No_Of_Share = C.NO_OF_SHARE;
                objMember.Member_Full_Name = C.MEMBER_FULL_NAME;
                objMember.Father_Name = C.FATHER_NAME;
                objMember.Mother_Name = C.MOTHER_NAME;
                objMember.Husband_Name = C.HUSBAND_NAME;
                objMember.Present_Address = C.PRESENT_ADDRESS;
                objMember.Permanent_Address = C.PERMANENT_ADDRESS;
                objMember.Mobile_No = Convert.ToInt32(C.MOBILE_NO);
                objMember.Education_Status = C.EDUCATION_STATUS;
                objMember.Member_Birth_Date = Convert.ToDateTime(C.MEMBER_BIRTH_DATE);
                objMember.Religion = C.RELIGION;
                objMember.Member_Occupation = C.MEMBER_OCCUPATION;
                objMember.Marital_Status = C.MARITAL_STATUS;
                objMember.Nationality = C.NATIONALITY;
                objMember.Nominee_Name = C.NOMINEE_NAME;
                objMember.Nominee_Birth_Date = Convert.ToDateTime(C.NOMINEE_BIRTH_DATE);
                objMember.Nominee_Occupation = C.NOMINEE_OCCUPATION;
                objMember.Relation_With_Nominee = C.RELATION_WITH_NOMINEE;
                objMember.Member_Photo = C.MEMBER_PHOTO.ToArray();
                objMember.Digital_Signature = C.DIGITAL_SIGNATURE.ToArray();
                objMember.Nominee_Photo = C.NOMINEE_PHOTO.ToArray();
                objMember.Access_Date = Convert.ToDateTime(C.ACCESS_DATE);

                memberList.Add(objMember);
            }

            return memberList;
        }

        public bool DoesExist(string MemberNo)
        {
            ReportDataContext objDataContext = new ReportDataContext();
            return (objDataContext.MEMBER_INFOs.Any(C => C.MEMBER_NO.ToUpper()==MemberNo.ToUpper()));
        }
    }
}
