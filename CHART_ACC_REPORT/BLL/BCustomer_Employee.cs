using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CHART_ACC_REPORT.ENTITY;
using CHART_ACC_REPORT.DAL;
//Author:REFAT
namespace CHART_ACC_REPORT.BLL
{
    public class BCustomer_Employee
    {
        Cutomer_EmployeeDAL objCustEmpDAL = new Cutomer_EmployeeDAL();
       
        public List<ECustomer_Employee> GetAllCustomerEmployee()
        {
            return objCustEmpDAL.GetAllCustomerEmployee();
        }
        public ECustomer_Employee GetSingleCustomerEmployee(string accCode)
        {
            return objCustEmpDAL.GetSingleCustomerEmployee(accCode);
        }        
    }
}
