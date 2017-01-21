using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CHART_ACC_REPORT.ENTITY;
//Author:REFAT
namespace CHART_ACC_REPORT.DAL
{
    public class MemberDAL
    {
        ReportDataContext objDataContext;
        public List<EMember> GetAllMemberAccountType()
        {
            objDataContext = new ReportDataContext();
            List<EMember> listMAccType = new List<EMember>();
            foreach (var MType in objDataContext.MEMBER_ACC_TYPEs)
            {
                EMember objEMType = new EMember();
                objEMType.AccId = MType.MEMBER_ACCOUNT_TYPE_ID;
                objEMType.AccountName = MType.CHART_ACCOUNT.CHART_ACC_NAME;
                objEMType.Prefix = MType.ACCOUNT_PREFIX;
                objEMType.AccountNo = MType.CHART_ACCOUNT.CHART_ACC_CODE;
                objEMType.ParentAccountName = MType.CHART_ACCOUNT.CHART_ACC_PARENT_TYPE;
                listMAccType.Add(objEMType);
            }
            return listMAccType;
        }
        public List<EMember> GetAllMember(long ParentAccId)
        {
            objDataContext = new ReportDataContext();
            List<EMember> listMA = new List<EMember>();
            foreach (var MA in (from j in objDataContext.MEMBER_ASSIGNs where j.MEMBER_PARENT_ACC_ID==ParentAccId orderby j.CHART_ACCOUNT.CHART_ACC_CODE select j))
            {
                EMember objEMA = new EMember();
                objEMA.AccId = Convert.ToInt64(MA.ACC_ID);
                objEMA.AccountName = MA.CHART_ACCOUNT.CHART_ACC_NAME;
                objEMA.AccountNo = MA.CHART_ACCOUNT.CHART_ACC_CODE;
                listMA.Add(objEMA);
            }
            return listMA;
        }

        public bool DoesExistMemberAccount(string accNo, long parentAccId)
        {
            objDataContext = new ReportDataContext();
            return objDataContext.MEMBER_ASSIGNs.Any(ma=>ma.CHART_ACCOUNT.CHART_ACC_CODE==accNo && ma.MEMBER_PARENT_ACC_ID==parentAccId);
        }
    }
}
