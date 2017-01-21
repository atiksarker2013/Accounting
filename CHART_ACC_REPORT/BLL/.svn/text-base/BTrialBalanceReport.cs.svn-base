using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CHART_ACC_REPORT.ENTITY;
using CHART_ACC_REPORT.DAL;

namespace CHART_ACC_REPORT.BLL
{
    public class BTrialBalanceReport
    {
        TrialBalanceReportDAL objTrialBalanceReportDal = new TrialBalanceReportDAL();
        List<ETrialBalanceReport> listTrialBalance = new List<ETrialBalanceReport>();
        decimal debitBalance = 0, creditbalance = 0;

        public List<ETrialBalanceReport> GetFisDate()
        {
            return objTrialBalanceReportDal.GetFisDate();
        }
        int count = 1;
        private void Ptree(long pId, DateTime startDate, DateTime endDate)
        {
            foreach (ETrialBalanceReport obj in objTrialBalanceReportDal.GetSubAccount(pId,startDate,endDate))
            {
                count += 1;
                if (obj.IsHeader == "Yes")
                {
                    GetBalance(obj.Account_Id,startDate,endDate);
                    if (debitBalance != 0 || creditbalance != 0)
                    {
                        obj.Debit += debitBalance;
                        obj.Credit += creditbalance;
                    }
                }
                listTrialBalance.Add(obj);
                debitBalance = 0; creditbalance = 0;
                if (obj.Acc_Type == "ASSET" || obj.Acc_Type == "LIABILITY")
                {
                    if (count < 3)
                    {
                        Ptree(obj.Account_Id,startDate,endDate);

                    }
                }
                else if (count < 2)
                {
                    Ptree(obj.Account_Id,startDate,endDate);
                }
                count -= 1;
            }
        }

        private void GetBalance(long pId, DateTime startDate, DateTime endDate)
        {
            foreach (ETrialBalanceReport obj in objTrialBalanceReportDal.GetSubAccount(pId,startDate,endDate))
            {
                debitBalance += Convert.ToDecimal(obj.Debit);
                creditbalance += Convert.ToDecimal(obj.Credit);
                if (obj.IsHeader == "Yes")
                {
                    GetBalance(obj.Account_Id,startDate,endDate);
                }
            }
        }

        public List<ETrialBalanceReport> GetAllAccountHeadBalance(DateTime startDate, DateTime endDate)
        {
            List<EAccountType> accountTypeList = objTrialBalanceReportDal.GetAllAccountHeadId();
            foreach (EAccountType objAcType in accountTypeList)
            {
                Ptree(objAcType.AccTypeId,startDate,endDate);
            }
            return listTrialBalance;
        }

        public List<ETrialBalanceReport> GetFinalAccountBalance(DateTime startDate,DateTime endDate)
        {
            listTrialBalance.Clear();
            GetAllAccountHeadBalance(startDate, endDate);
            List<ETrialBalanceReport> finalAccountList = new List<ETrialBalanceReport>();

            for (int i = 0; i < listTrialBalance.Count; i++)
            { 
                ETrialBalanceReport objETR = listTrialBalance[i];
                if (objETR.IsHeader == "Yes")
                {
                    List<ETrialBalanceReport> listAccount = objTrialBalanceReportDal.GetSubAccount(objETR.Account_Id,startDate,endDate);
                    if (DoesExist(listAccount[0].Account_Id) == false)
                    {
                        finalAccountList.Add(objETR);
                    }
                }
                else
                {
                    finalAccountList.Add(objETR);
                }
            }
            return finalAccountList;
        }

        private bool DoesExist(long accId)
        {
            foreach (var obj in listTrialBalance)
            {
                if (obj.Account_Id == accId)
                {
                    return true;
                }
            }
            return false;
        }
        public ETrialBalanceReport ProcessIncomeLoss(DateTime startDate, DateTime endDate)
        {
            return objTrialBalanceReportDal.ProcessIncomeLoss(startDate, endDate);
        }
    }
}
