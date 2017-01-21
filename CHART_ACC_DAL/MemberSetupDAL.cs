using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CHART_ACC_ENTITY;
using CHART_ACC_DAL.DBML;

namespace CHART_ACC_DAL
{
    public class MemberSetupDAL
    {
        CHART_ACCDataContext objDataContext;

        public bool SaveMemberInfo(EMemberSetup objEMemberSetup)
        {
            objDataContext = new CHART_ACCDataContext();

            var member_info = new MEMBER_INFO
            {
                MEMBER_NO = objEMemberSetup.Member_No,
                NO_OF_SHARE = objEMemberSetup.No_Of_Share,
                MEMBER_FULL_NAME = objEMemberSetup.Member_Full_Name,
                FATHER_NAME = objEMemberSetup.Father_Name,
                MOTHER_NAME = objEMemberSetup.Mother_Name,
                HUSBAND_NAME = objEMemberSetup.Husband_Name,
                PRESENT_ADDRESS = objEMemberSetup.Present_Address,
                PERMANENT_ADDRESS = objEMemberSetup.Permanent_Address,
                MOBILE_NO = objEMemberSetup.Mobile_No,
                EDUCATION_STATUS = objEMemberSetup.Education_Status,
                MEMBER_BIRTH_DATE = objEMemberSetup.Member_Birth_Date,
                RELIGION = objEMemberSetup.Religion,
                MEMBER_OCCUPATION = objEMemberSetup.Member_Occupation,
                MARITAL_STATUS = objEMemberSetup.Marital_Status,
                NATIONALITY = objEMemberSetup.Nationality,
                NOMINEE_NAME = objEMemberSetup.Nominee_Name,
                NOMINEE_BIRTH_DATE = objEMemberSetup.Nominee_Birth_Date,
                NOMINEE_OCCUPATION = objEMemberSetup.Nominee_Occupation,
                RELATION_WITH_NOMINEE = objEMemberSetup.Relation_With_Nominee,
                MEMBER_PHOTO = objEMemberSetup.Member_Photo,
                DIGITAL_SIGNATURE = objEMemberSetup.Digital_Signature,
                NOMINEE_PHOTO = objEMemberSetup.Nominee_Photo,
                ACCESS_BY = objEMemberSetup.Access_By,
                ACCESS_DATE = DateTime.Now
            };

            objDataContext.MEMBER_INFOs.InsertOnSubmit(member_info);
            objDataContext.SubmitChanges();
            return true;
        }

        public bool UpdateMemberInfo(EMemberSetup objEMemberSetup)
        {
            objDataContext = new CHART_ACCDataContext();

            var updateMember = objDataContext.MEMBER_INFOs.Where(C => C.ID == objEMemberSetup.Id).First();

            updateMember.MEMBER_NO = objEMemberSetup.Member_No;
            updateMember.NO_OF_SHARE = objEMemberSetup.No_Of_Share;
            updateMember.MEMBER_FULL_NAME = objEMemberSetup.Member_Full_Name;
            updateMember.FATHER_NAME = objEMemberSetup.Father_Name;
            updateMember.MOTHER_NAME = objEMemberSetup.Mother_Name;
            updateMember.HUSBAND_NAME = objEMemberSetup.Husband_Name;
            updateMember.PRESENT_ADDRESS = objEMemberSetup.Present_Address;
            updateMember.PERMANENT_ADDRESS = objEMemberSetup.Permanent_Address;
            updateMember.MOBILE_NO = objEMemberSetup.Mobile_No;
            updateMember.EDUCATION_STATUS = objEMemberSetup.Education_Status;
            updateMember.MEMBER_BIRTH_DATE = objEMemberSetup.Member_Birth_Date;
            updateMember.RELIGION = objEMemberSetup.Religion;
            updateMember.MEMBER_OCCUPATION = objEMemberSetup.Member_Occupation;
            updateMember.MARITAL_STATUS = objEMemberSetup.Marital_Status;
            updateMember.NATIONALITY = objEMemberSetup.Nationality;
            updateMember.NOMINEE_NAME = objEMemberSetup.Nominee_Name;
            updateMember.NOMINEE_BIRTH_DATE = objEMemberSetup.Nominee_Birth_Date;
            updateMember.NOMINEE_OCCUPATION = objEMemberSetup.Nominee_Occupation;
            updateMember.RELATION_WITH_NOMINEE = objEMemberSetup.Relation_With_Nominee;
            updateMember.MEMBER_PHOTO = objEMemberSetup.Member_Photo;
            updateMember.DIGITAL_SIGNATURE = objEMemberSetup.Digital_Signature;
            updateMember.NOMINEE_PHOTO = objEMemberSetup.Nominee_Photo;

            objDataContext.SubmitChanges();
            return true;
        }

        public bool DoesExist(string MemberNo)
        {
            objDataContext = new CHART_ACCDataContext();
            return (objDataContext.MEMBER_INFOs.Any(C => C.MEMBER_NO.Equals(MemberNo)));
        }

        public List<EMemberSetup> GetInfoMemberNoWise(string Member_No)
        {
            objDataContext = new CHART_ACCDataContext();
            List<EMemberSetup> memberList = new List<EMemberSetup>();

            var query = from j in objDataContext.MEMBER_INFOs where j.MEMBER_NO == Member_No select j;

            foreach (var C in query)
            {
                EMemberSetup objMember = new EMemberSetup();
                objMember.Id = C.ID;
                objMember.Member_No = C.MEMBER_NO;
                objMember.No_Of_Share = C.NO_OF_SHARE;
                objMember.Member_Full_Name = C.MEMBER_FULL_NAME;
                objMember.Father_Name = C.FATHER_NAME;
                objMember.Mother_Name = C.MOTHER_NAME;
                objMember.Husband_Name = C.HUSBAND_NAME;
                objMember.Present_Address = C.PRESENT_ADDRESS;
                objMember.Permanent_Address = C.PERMANENT_ADDRESS;
                objMember.Mobile_No =Convert.ToInt32(C.MOBILE_NO);
                objMember.Education_Status = C.EDUCATION_STATUS;
                objMember.Member_Birth_Date =Convert.ToDateTime(C.MEMBER_BIRTH_DATE);
                objMember.Religion = C.RELIGION;
                objMember.Member_Occupation = C.MEMBER_OCCUPATION;
                objMember.Marital_Status = C.MARITAL_STATUS;
                objMember.Nationality = C.NATIONALITY;
                objMember.Nominee_Name = C.NOMINEE_NAME;
                objMember.Nominee_Birth_Date =Convert.ToDateTime(C.NOMINEE_BIRTH_DATE);
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

        public List<EMemberSetup> GetInfoAllMember()
        {
            objDataContext = new CHART_ACCDataContext();
            List<EMemberSetup> memberList = new List<EMemberSetup>();

            var query = from j in objDataContext.MEMBER_INFOs orderby j.MEMBER_NO select j;

            foreach (var C in query)
            {
                EMemberSetup objMember = new EMemberSetup();
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

        public bool DeleteMember(long MemberId)
        {
            objDataContext = new CHART_ACCDataContext();
            var query = from j in objDataContext.MEMBER_INFOs where j.ID == MemberId select j;
            objDataContext.MEMBER_INFOs.DeleteAllOnSubmit(query);
            objDataContext.SubmitChanges();
            return true;
        }
    }
}
