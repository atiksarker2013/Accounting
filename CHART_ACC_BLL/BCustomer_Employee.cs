using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CHART_ACC_ENTITY;
using CHART_ACC_DAL;
//Author:REFAT
namespace CHART_ACC_BLL
{
    public class BCustomer_Employee
    {
        Cutomer_EmployeeDAL objCustEmpDAL = new Cutomer_EmployeeDAL();
        public bool SaveCustomerEmployee(ECustomer_Employee objEcustEmplyee)
        {
            objEcustEmplyee.AccountId = objCustEmpDAL.SaveChartOfAccount(objEcustEmplyee);
            if (objEcustEmplyee.AccountId > 0)
            {
                return objCustEmpDAL.SaveCustomerEmployee(objEcustEmplyee);
            }
            else
            {
                return false;
            }
        }
        public List<ECustomer_Employee> GetAllCustomerEmployee()
        {
            return objCustEmpDAL.GetAllCustomerEmployee();
        }
        public ECustomer_Employee GetSingleCustomerEmployee(string accCode)
        {
            return objCustEmpDAL.GetSingleCustomerEmployee(accCode);
        }

        public bool UpdateCustEmp(ECustomer_Employee objECustEmp)
        {
            return objCustEmpDAL.UpdateCustEmp(objECustEmp);
        }
    }
}
