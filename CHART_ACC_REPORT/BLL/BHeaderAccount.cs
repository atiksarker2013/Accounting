﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CHART_ACC_REPORT.DAL;
using CHART_ACC_REPORT.ENTITY;
//Author:Amin
//Modified by:Refat(19th feb'2012)
namespace CHART_ACC_REPORT.BLL
{
    public class BHeaderAccount
    {
        HeaderAccountDAL objHeaderAccountDal = new HeaderAccountDAL();

        public List<EAccountStatement> GetHeaderAccountName()
        {
            return objHeaderAccountDal.GetHeaderAccountName();
        }

        private List<EAccountStatement> GetAllJournalByAccount(string accTitle, long accId)
        {
            List<EAccountStatement> listGetAS = objHeaderAccountDal.GetAllJournalByAccount(accTitle, accId);
            List<EAccountStatement> listFinalAS = new List<EAccountStatement>();
            List<string> listofJVId = new List<string>();

            /*Get All Distinct Id of Journal*/
            string checkId = "";
            foreach (EAccountStatement objJVId in listGetAS)
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
                    listFinalAS.Add(listGetAS[0]);
                }
                else
                {
                    bool isAccTypeAbsent = objHeaderAccountDal.CheckAccTypeAbsent(accId, id);//Check same type of account
                    foreach (var objEAS in objHeaderAccountDal.GetJournalInfo(listGetAS, id))
                    {
                        EAccountStatement objECreateAS = new EAccountStatement();
                        objECreateAS.ID = objEAS.ID;
                        objECreateAS.JournalDetailsId = objEAS.JournalDetailsId;
                        objECreateAS.AccountId = objEAS.AccountId;
                        objECreateAS.AccountCode = objEAS.AccountCode;
                        objECreateAS.AccountTitle = objEAS.AccountTitle;
                        objECreateAS.Debit = objEAS.Debit;
                        objECreateAS.Credit = objEAS.Credit;
                        objECreateAS.IsDebit = objEAS.IsDebit;
                        objECreateAS.Date = objEAS.Date;
                        objECreateAS.DisplayDate = objEAS.DisplayDate;
                        objECreateAS.AccountCurrentBalance = objEAS.AccountCurrentBalance;
                        objECreateAS.COAName = objEAS.COAName;
                        objECreateAS.COACode = objEAS.COACode;
                        objECreateAS.COADebit = objEAS.COADebit;
                        objECreateAS.COACredit = objEAS.COACredit;
                        objECreateAS.IsCOADebit = objEAS.IsCOADebit;
                        objECreateAS.COACreationDate = objEAS.COACreationDate;
                        objECreateAS.Notes = objEAS.Notes;
                        objECreateAS.ChequeNo = objEAS.ChequeNo;
                        objECreateAS.Remarks = objEAS.Remarks;
                        objECreateAS.Address = objEAS.Address;
                        if (isAccTypeAbsent == true)
                        {
                            if (objECreateAS.Debit > objECreateAS.Credit)
                            {
                                objECreateAS.Credit = objECreateAS.Debit;
                                objECreateAS.Debit = 0;
                            }
                            else
                            {
                                objECreateAS.Debit = objECreateAS.Credit;
                                objECreateAS.Credit = 0;
                            }
                            if (accId != objECreateAS.AccountId)
                            {
                                /*if (jv != objECreateAS.JournalDetailsId)//Preventing duplicate notes for same voucher
                                {
                                    jv = objECreateAS.JournalDetailsId;
                                    objECreateAS.Notes = objEAS.Notes;
                                }*/
                                if (objECreateAS.Remarks == @"Cheque Deposit" || objECreateAS.Remarks == @"Cheque Payment" && objECreateAS.ChequeNo == null)
                                {
                                    objECreateAS.ChequeNo = objHeaderAccountDal.GetOpponetAccInfo(objECreateAS.ID, id, objECreateAS.IsDebit).ChequeNo;
                                }
                                listFinalAS.Add(objECreateAS);
                            }
                        }
                        else
                        {
                            if (accId == objECreateAS.AccountId)
                            {
                                /*if (jv != objECreateAS.JournalDetailsId)//Preventing duplicate notes for same voucher
                                {
                                    jv = objECreateAS.JournalDetailsId;
                                    objECreateAS.Notes = objEAS.Notes;
                                }*/
                                EAccountStatement objOpponentInfo = objHeaderAccountDal.GetOpponetAccInfo(objECreateAS.ID, id, objECreateAS.IsDebit);
                                objECreateAS.AccountCode = objOpponentInfo.AccountCode;
                                objECreateAS.AccountTitle = objOpponentInfo.AccountTitle;
                                listFinalAS.Add(objECreateAS);
                                //break;
                            }
                        }
                    }
                }
            }
            return listFinalAS;
        }

        public List<EAccountStatement> GetAccStatement(string accTitle,long accId)
        {
            List<EAccountStatement> listFinal = GetAllJournalByAccount(accTitle, accId);
            decimal displayBalance = 0;
            //For Display Balance
            for (int i = 0; i < listFinal.Count; i++)
            {
                if (i == 0)
                {
                    displayBalance = listFinal[i].AccountCurrentBalance;
                    listFinal[i].DisplayBalance = displayBalance; 
                    listFinal[i].DisplayDate=listFinal[1].DisplayDate;//Display date
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
