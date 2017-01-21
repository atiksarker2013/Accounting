using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CHART_ACC_REPORT.DAL;
using CHART_ACC_REPORT.ENTITY;
//Author:REFAT
namespace CHART_ACC_REPORT.BLL
{
    public class BBankReconciliation
    {
        BankReconciliationDAL objBankReconciliationDAL = new BankReconciliationDAL();

        public List<EBankReconciliation> GetAllJournalByAccount(DateTime fromDate, DateTime toDate, string accTitle, long accId)
        {
            List<EBankReconciliation> listGetBR = objBankReconciliationDAL.GetAllJournalByAccount(fromDate, toDate, accTitle, accId);
            List<EBankReconciliation> listFinalBR = new List<EBankReconciliation>();
            List<string> listofJVId = new List<string>();

            /*Get All Distinct Id of Journal*/
            string checkId = "";
            foreach (EBankReconciliation objJVId in listGetBR)
            {
                if (checkId != objJVId.JournalDetailsId)
                {
                    listofJVId.Add(objJVId.JournalDetailsId);
                    checkId = objJVId.JournalDetailsId;
                }
            }
            /*Get Final list for Report*/
            string jv = string.Empty;
            foreach (string id in listofJVId)
            {
                if (id == null)
                {
                    listFinalBR.Add(listGetBR[0]);
                }
                else
                {
                    bool isAccTypeAbsent = objBankReconciliationDAL.CheckAccTypeAbsent(accId, id);//Check same type of account
                    foreach (var objEAS in objBankReconciliationDAL.GetJournalInfo(listGetBR, id))
                    {
                        EBankReconciliation objECreateBR = new EBankReconciliation();
                        objECreateBR.ID = objEAS.ID;
                        objECreateBR.JournalDetailsId = objEAS.JournalDetailsId;
                        objECreateBR.AccountId = objEAS.AccountId;
                        objECreateBR.AccountCode = objEAS.AccountCode;
                        objECreateBR.AccountTitle = objEAS.AccountTitle;
                        objECreateBR.Debit = objEAS.Debit;
                        objECreateBR.Credit = objEAS.Credit;
                        objECreateBR.IsDebit = objEAS.IsDebit;
                        objECreateBR.Date = objEAS.Date;
                        objECreateBR.DisplayDate = objEAS.DisplayDate;
                        objECreateBR.BankCurrentBalance = objEAS.BankCurrentBalance;
                        objECreateBR.COAName = objEAS.COAName;
                        objECreateBR.COACode = objEAS.COACode;
                        objECreateBR.COADebit = objEAS.COADebit;
                        objECreateBR.COACredit = objEAS.COACredit;
                        objECreateBR.IsCOADebit = objEAS.IsCOADebit;
                        objECreateBR.COACreationDate = objEAS.COACreationDate;
                        objECreateBR.Notes = objEAS.Notes;
                        objECreateBR.ChequeNo = objEAS.ChequeNo;
                        objECreateBR.ChequeDate = objEAS.ChequeDate;
                        objECreateBR.Remarks = objEAS.Remarks;
                        if (string.IsNullOrEmpty(objECreateBR.Remarks))
                        {
                            objECreateBR.Remarks = objECreateBR.Notes;
                        }
                        objECreateBR.TransactionGroup = objEAS.TransactionGroup;
                        if (isAccTypeAbsent == true)
                        {
                            if (objECreateBR.Debit > objECreateBR.Credit)
                            {
                                objECreateBR.Credit = objECreateBR.Debit;
                                objECreateBR.Debit = 0;
                            }
                            else
                            {
                                objECreateBR.Debit = objECreateBR.Credit;
                                objECreateBR.Credit = 0;
                            }
                            if (accId != objECreateBR.AccountId)
                            {
                                listFinalBR.Add(objECreateBR);
                            }
                        }
                        else
                        {
                            if (accId == objECreateBR.AccountId)
                            {
                                EBankReconciliation objOpponentInfo = objBankReconciliationDAL.GetOpponetAccInfo(objECreateBR.ID, id, objECreateBR.IsDebit);
                                objECreateBR.AccountCode = objOpponentInfo.AccountCode;
                                objECreateBR.AccountTitle = objOpponentInfo.AccountTitle;
                                listFinalBR.Add(objECreateBR);
                                //break;
                            }
                        }
                    }
                }
            }
            return listFinalBR;
        }

        public List<EBankReconciliation> GetBankReconciliation(DateTime fromDate, DateTime toDate, string accTitle, long accId, ref decimal grandTotal)
        {
            List<EBankReconciliation> listFinal = GetAllJournalByAccount(fromDate, toDate, accTitle, accId);
            decimal displayBalance = 0;
            //For Display Balance
            for (int i = 0; i < listFinal.Count; i++)
            {
                if (i == 0)
                {
                    displayBalance = listFinal[i].BankCurrentBalance;
                    listFinal[i].DisplayBalance = displayBalance;
                }
                else
                {
                    displayBalance += listFinal[i].Debit - listFinal[i].Credit;
                    listFinal[i].DisplayBalance = displayBalance;
                }
            }
            grandTotal = displayBalance;
            displayBalance = 0;
            int count = listFinal.Count;
            foreach (var objEBR in objBankReconciliationDAL.GetPendingPartiesDepositPaymentJournal(accId, fromDate, toDate))
            {
                foreach (EBankReconciliation objEBankRecon in objBankReconciliationDAL.GetJournalVoucharIdWise(objEBR))
                {
                    if (objEBankRecon.IsDebit)
                    {
                        objEBankRecon.Credit = objEBankRecon.Debit;
                        objEBankRecon.Debit = 0;
                    }
                    else
                    {
                        objEBankRecon.Debit = objEBankRecon.Credit;
                        objEBankRecon.Credit = 0;
                    }
                    displayBalance += objEBankRecon.Debit - objEBankRecon.Credit;
                    objEBankRecon.DisplayBalance = displayBalance;
                    listFinal.Add(objEBankRecon);
                }
            }
            grandTotal -= displayBalance;
            if (count == listFinal.Count)
            {
                listFinal.Add(new EBankReconciliation() { TransactionGroup = "Pending" });
            }
            return listFinal;
        }

        public List<EBankReconciliation> GetBankStatement(DateTime fromDate, DateTime toDate, string accTitle, long accId)
        {
            List<EBankReconciliation> listFinal = GetAllJournalByAccount(fromDate, toDate, accTitle, accId);
            decimal displayBalance = 0;
            //For Display Balance
            for (int i = 0; i < listFinal.Count; i++)
            {
                if (i == 0)
                {
                    displayBalance = listFinal[i].BankCurrentBalance;
                    listFinal[i].DisplayBalance = displayBalance;
                }
                else
                {
                    displayBalance += listFinal[i].Debit - listFinal[i].Credit;
                    listFinal[i].DisplayBalance = displayBalance;
                }
            }            
            return listFinal;
        }
    }
}
