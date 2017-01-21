using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CHART_ACC_ENTITY;
using CHART_ACC_DAL.DBML;
//Author:REFAT
namespace CHART_ACC_DAL
{
    public class MemberAssignDAL
    {
        CHART_ACCDataContext objDataContext;

        public long SaveChartOfAccount(EMemberAssign objEMemberAssign)
        {
            objDataContext = new CHART_ACCDataContext();
            var chartOfAccount = new CHART_ACCOUNT
            {
                CHART_ACC_CODE = objEMemberAssign.AccountNo,
                CHART_ACC_NAME = objEMemberAssign.AccountName,
                CHART_ACC_PARENT_ID = objEMemberAssign.ParentAccountId,
                CHART_ACC_PARENT_TYPE = objEMemberAssign.RootAccName,
                CHART_ACC_OPENING_BALANCE_DR = objEMemberAssign.DrBalance,
                CHART_ACC_OPENING_BALANCE_CR = objEMemberAssign.CrBalance,
                CHART_ACC_STATUS = objEMemberAssign.Status,
                CHART_ACC_HEADER = "No",
                CHART_ACC_CREATION_DATE = DateTime.Now,                
                ACCESS_DATE = DateTime.Now
            };
            objDataContext.CHART_ACCOUNTs.InsertOnSubmit(chartOfAccount);
            objDataContext.SubmitChanges();
            UpdateParentHeaderStatus(objEMemberAssign.ParentAccountId);
            return chartOfAccount.CHART_ACC_ID;
        }

        public bool UpdateParentHeaderStatus(long id)
        {           
            var chart = objDataContext.CHART_ACCOUNTs.FirstOrDefault(c => c.CHART_ACC_ID == id);
            if (chart != null)
            {
                chart.CHART_ACC_HEADER = "Yes";
                objDataContext.SubmitChanges();
            }
            return true;
        }
        public bool SaveMemberAssign(EMemberAssign objEMemberAssign)
        {
            objDataContext = new CHART_ACCDataContext();
            var member = new MEMBER_ASSIGN
            {
                MEMBER_ID=objEMemberAssign.MemberId,
                MEMBER_PARENT_ACC_ID=objEMemberAssign.ParentAccountId,
                ACC_ID=objEMemberAssign.AccId
            };
            objDataContext.MEMBER_ASSIGNs.InsertOnSubmit(member);
            objDataContext.SubmitChanges();
            return true;
        }

        public bool DoesExistMemberAssign(long MemberId, long MemberParentAccId)
        {
            objDataContext = new CHART_ACCDataContext();
            return objDataContext.MEMBER_ASSIGNs.Any(member=>member.MEMBER_ID==MemberId && member.MEMBER_PARENT_ACC_ID==MemberParentAccId);
        }

        public bool DoesExistsInAssign(long MemberId)
        {
            objDataContext = new CHART_ACCDataContext();
            return objDataContext.MEMBER_ASSIGNs.Any(member => member.MEMBER_ID == MemberId);
        }

        public List<EMemberAssign> GetMemberAllAccount(long MemberId)
        {
            objDataContext = new CHART_ACCDataContext();
            List<EMemberAssign> listMemberAccount = new List<EMemberAssign>();
            foreach (var MA in (from j in objDataContext.MEMBER_ASSIGNs where j.MEMBER_ID == MemberId select j))
            {
                EMemberAssign objEMA = new EMemberAssign();
                objEMA.AccId = Convert.ToInt64(MA.ACC_ID);
                objEMA.AccountNo = MA.CHART_ACCOUNT.CHART_ACC_CODE;
                objEMA.ParentAccountPrefix = GetParentAccountInfo(Convert.ToInt64(MA.CHART_ACCOUNT.CHART_ACC_PARENT_ID)).ParentAccountPrefix;
                objEMA.Balance = GetCurrentBalanceOfAccount(objEMA.AccId);
                listMemberAccount.Add(objEMA);
            }
            return listMemberAccount;
        }
        private decimal GetCurrentBalanceOfAccount(long accID)
        {
            List<EJournalVouchar> listJournalDetails = new List<EJournalVouchar>();            
            decimal openBalance = 0;
            decimal amount = 0;

            foreach (var c in objDataContext.SP_CURRENT_BALANCE(accID, DateTime.Now.Date))
            {
                if (c.OP_DR > c.OP_CR)
                {
                    openBalance = Convert.ToDecimal(c.OP_DR);
                }
                else
                {
                    openBalance = Convert.ToDecimal(-1 * c.OP_CR);
                }
                amount += Convert.ToDecimal(c.JV_DEBIT_AMOUNT - c.JV_CREDIT_AMOUNT);
            }
            amount = openBalance + amount;

            return amount;
        }
        private EMemberAssign GetParentAccountInfo(long accId)
        {
            CHART_ACCDataContext objDataContext = new CHART_ACCDataContext();
            EMemberAssign obj = new EMemberAssign();
            foreach (var c in (from j in objDataContext.MEMBER_ACC_TYPEs where j.MEMBER_ACCOUNT_TYPE_ID == accId select j))
            {
                obj.ParentAccountName = c.CHART_ACCOUNT.CHART_ACC_NAME;
                obj.ParentAccountId = c.MEMBER_ACCOUNT_TYPE_ID;
                obj.ParentAccountPrefix = c.ACCOUNT_PREFIX;                
            }
            return obj;
        }
    }
}
