using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CHART_ACC_REPORT.ENTITY;
//Author:REFAT
namespace CHART_ACC_REPORT.DAL
{
    public class Cutomer_EmployeeDAL
    {       
        public List<ECustomer_Employee> GetAllCustomerEmployee()
        {
            ReportDataContext objDataContext = new ReportDataContext();
            List<ECustomer_Employee> listCustemp = new List<ECustomer_Employee>();
            foreach (var CustEmp in (from j in objDataContext.CUSTOMER_EMPLOYEE_SETUPs select j))
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
                listCustemp.Add(objECustEmp);
            }
            return listCustemp;
        }
        public ECustomer_Employee GetSingleCustomerEmployee(string accCode)
        {
            ReportDataContext objDataContext = new ReportDataContext();
            ECustomer_Employee objECustEmp = null;
            foreach (var CustEmp in (from j in objDataContext.CUSTOMER_EMPLOYEE_SETUPs where j.CHART_ACCOUNT.CHART_ACC_CODE == accCode select j))
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
            }
            return objECustEmp;
        }
    }
}
