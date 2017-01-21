using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CHART_ACC_DAL.DBML;
using CHART_ACC_ENTITY;
//Author:REFAT
namespace CHART_ACC_DAL
{
    public class Cutomer_EmployeeDAL
    {
        public long SaveChartOfAccount(ECustomer_Employee objEcustEmplyee)
        {
            CHART_ACCDataContext objDataContext = new CHART_ACCDataContext();
            var newChartOfAcc = new CHART_ACCOUNT
            {
                CHART_ACC_NAME = objEcustEmplyee.AccountName,
                CHART_ACC_CODE = objEcustEmplyee.AccountCode,
                CHART_ACC_PARENT_ID = objEcustEmplyee.ParentAccountId,
                CHART_ACC_PARENT_TYPE=objEcustEmplyee.RootName,
                CHART_ACC_OPENING_BALANCE_DR = objEcustEmplyee.AccOpeningBalanceDR,
                CHART_ACC_OPENING_BALANCE_CR = objEcustEmplyee.AccOpeningBalanceCR,
                CHART_ACC_STATUS = objEcustEmplyee.Status,
                CHART_ACC_HEADER = "No",
                CHART_ACC_CREATION_DATE = DateTime.Now,
                ACCESS_BY = objEcustEmplyee.AccessBy,
                ACCESS_DATE = DateTime.Now
            };
            objDataContext.CHART_ACCOUNTs.InsertOnSubmit(newChartOfAcc);
            objDataContext.SubmitChanges();
            UpdateParentHeaderStatus(objEcustEmplyee.ParentAccountId);
            return newChartOfAcc.CHART_ACC_ID;
        }
        public bool UpdateParentHeaderStatus(long id)
        {
            CHART_ACCDataContext objDataContext = new CHART_ACCDataContext();
            var chart = objDataContext.CHART_ACCOUNTs.FirstOrDefault(c => c.CHART_ACC_ID == id);
            if (chart != null)
            {
                chart.CHART_ACC_HEADER = "Yes";
                objDataContext.SubmitChanges();
            }
            return true;
        }
        public bool SaveCustomerEmployee(ECustomer_Employee objEcustEmplyee)
        {
            CHART_ACCDataContext objDataContext = new CHART_ACCDataContext();
            var newCustEmp = new CUSTOMER_EMPLOYEE_SETUP
            {
                ACCOUNT_ID=objEcustEmplyee.AccountId,
                NAME=objEcustEmplyee.EployeeName,
                CONTACT_NO=objEcustEmplyee.Contact_No,
                CONTACT_PERSON=objEcustEmplyee.ContactPerson,
                NATIONAL_ID=objEcustEmplyee.NationalId,
                ADDRESS=objEcustEmplyee.Address
            };
            objDataContext.CUSTOMER_EMPLOYEE_SETUPs.InsertOnSubmit(newCustEmp);
            objDataContext.SubmitChanges();
            return true;
        }
        public List<ECustomer_Employee> GetAllCustomerEmployee()
        {
            CHART_ACCDataContext objDataContext = new CHART_ACCDataContext();
            List<ECustomer_Employee> listCustemp = new List<ECustomer_Employee>();
            foreach (var CustEmp in (from j in objDataContext.CUSTOMER_EMPLOYEE_SETUPs where j.CHART_ACCOUNT.CHART_ACC_STATUS=="Active" select j))
            {
                ECustomer_Employee objECustEmp = new ECustomer_Employee();
                objECustEmp.Id = CustEmp.ID;
                objECustEmp.EployeeName = CustEmp.NAME;
                objECustEmp.Contact_No = CustEmp.CONTACT_NO;
                objECustEmp.ContactPerson = CustEmp.CONTACT_PERSON;
                objECustEmp.NationalId = CustEmp.NATIONAL_ID;
                objECustEmp.Address = CustEmp.ADDRESS;
                objECustEmp.AccountId = CustEmp.CHART_ACCOUNT.CHART_ACC_ID;
                objECustEmp.AccountName = CustEmp.CHART_ACCOUNT.CHART_ACC_NAME;
                objECustEmp.AccountCode = CustEmp.CHART_ACCOUNT.CHART_ACC_CODE;
                objECustEmp.CurrentBalance = GetCurrentBalanceOfAccount(objECustEmp.AccountId);
                listCustemp.Add(objECustEmp);
            }
            return listCustemp;
        }
        public ECustomer_Employee GetSingleCustomerEmployee(string accCode)
        {
            CHART_ACCDataContext objDataContext = new CHART_ACCDataContext();
            ECustomer_Employee objECustEmp = null;
            foreach (var CustEmp in (from j in objDataContext.CUSTOMER_EMPLOYEE_SETUPs where j.CHART_ACCOUNT.CHART_ACC_CODE == accCode && j.CHART_ACCOUNT.CHART_ACC_STATUS == "Active" select j))
            {
                objECustEmp = new ECustomer_Employee();
                objECustEmp.Id = CustEmp.ID;
                objECustEmp.EployeeName = CustEmp.NAME;
                objECustEmp.Contact_No = CustEmp.CONTACT_NO;
                objECustEmp.ContactPerson = CustEmp.CONTACT_PERSON;
                objECustEmp.NationalId = CustEmp.NATIONAL_ID;
                objECustEmp.Address = CustEmp.ADDRESS;
                objECustEmp.AccountId = CustEmp.CHART_ACCOUNT.CHART_ACC_ID;
                objECustEmp.AccountName = CustEmp.CHART_ACCOUNT.CHART_ACC_NAME;
                objECustEmp.AccountCode = CustEmp.CHART_ACCOUNT.CHART_ACC_CODE;
                objECustEmp.CurrentBalance = GetCurrentBalanceOfAccount(objECustEmp.AccountId);                
            }
            return objECustEmp;
        }
        public decimal GetCurrentBalanceOfAccount(long accID)
        {
            CHART_ACCDataContext objDataContext = new CHART_ACCDataContext();
            List<EJournalVouchar> listJournalDetails = new List<EJournalVouchar>();
            DateTime fromDate = new DateTime();
            decimal openBalance = 0;
            decimal amount = 0;

            //var fiscalQuery = from j in objDataContext.FISCAL_YEARs where j.F_YEAR_STATUS == "Active" select j;
            //foreach (var f in fiscalQuery)
            //{
            //    fromDate = Convert.ToDateTime(f.F_YEAR_START_DATE).Date;
            //}
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

        public bool UpdateCustEmp(ECustomer_Employee objECustEmp)
        {
            CHART_ACCDataContext objDataContext = new CHART_ACCDataContext();
            var cust = objDataContext.CUSTOMER_EMPLOYEE_SETUPs.FirstOrDefault(c => c.ID == objECustEmp.Id);            
            cust.NAME = objECustEmp.EployeeName;
            cust.CONTACT_NO = objECustEmp.Contact_No;
            cust.CONTACT_PERSON = objECustEmp.ContactPerson;
            cust.NATIONAL_ID = objECustEmp.NationalId;
            cust.ADDRESS = objECustEmp.Address;
            objDataContext.SubmitChanges();
            return true;
        }
    }
}
