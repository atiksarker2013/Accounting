using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CHART_ACC_REPORT.ENTITY;
//Author:REFAT
namespace CHART_ACC_REPORT.DAL
{
    public class ChartAccountDAL
    {
        public List<EAccountType> GetAllAccountType()
        {
            ReportDataContext objDataContext = new ReportDataContext();
            List<EAccountType> listOfType = new List<EAccountType>();

            var query = from accType in objDataContext.ACCOUNT_TYPEs select accType;
            foreach (ACCOUNT_TYPE acctype in query)
            {
                EAccountType objEAccountType = new EAccountType();
                objEAccountType.AccTypeId = acctype.ACC_TYPE_ID;
                objEAccountType.AccTypeName = acctype.ACC_NAME;
                listOfType.Add(objEAccountType);
            }
            return listOfType;
        }
        public List<EChartOfAccount> GetSubAccount(long parentId)
        {
            ReportDataContext objDataContext = new ReportDataContext();
            List<EChartOfAccount> list = new List<EChartOfAccount>();
            foreach (var c in (from j in objDataContext.CHART_ACCOUNTs where j.CHART_ACC_PARENT_ID == parentId orderby j.CHART_ACC_NAME select j))
            {
                EChartOfAccount obj = new EChartOfAccount();
                obj.SubAccName = c.CHART_ACC_NAME;
                obj.SubAccId = c.CHART_ACC_ID;
                obj.SubAccCode = c.CHART_ACC_CODE;
                obj.ParentAccId = Convert.ToInt64(c.CHART_ACC_PARENT_ID);
                obj.AccOpeningBalanceDR =(Decimal) c.CHART_ACC_OPENING_BALANCE_DR;
                obj.AccOpeningBalanceCR = (Decimal)c.CHART_ACC_OPENING_BALANCE_CR;
                obj.AccHeader = c.CHART_ACC_HEADER;
                obj.RootAccName = c.CHART_ACC_PARENT_TYPE;
                obj.CreationDate = Convert.ToDateTime(c.CHART_ACC_CREATION_DATE);
                obj.AccessBy = c.ACCESS_BY;
                list.Add(obj);
            }
            return list;
        }
        public EChartOfAccount GetAccountInfo(long accId)
        {
            ReportDataContext objDataContext = new ReportDataContext();
            EChartOfAccount obj = new EChartOfAccount();
            foreach (var c in (from j in objDataContext.CHART_ACCOUNTs where j.CHART_ACC_ID == accId select j))
            {                
                obj.SubAccName = c.CHART_ACC_NAME;
                obj.SubAccId = c.CHART_ACC_ID;
                obj.SubAccCode = c.CHART_ACC_CODE;
                obj.AccHeader = c.CHART_ACC_HEADER;
                obj.CreationDate = Convert.ToDateTime(c.CHART_ACC_CREATION_DATE);
                obj.ParentAccId = Convert.ToInt64(c.CHART_ACC_PARENT_ID);
                obj.RootAccName = c.CHART_ACC_PARENT_TYPE;
            }
            return obj;
        }
        public EChartOfAccount GetAccountInfoCodeWise(string accCode)
        {
            ReportDataContext objDataContext = new ReportDataContext();
            EChartOfAccount obj = new EChartOfAccount();
            foreach (var c in (from j in objDataContext.CHART_ACCOUNTs where j.CHART_ACC_CODE == accCode select j))
            {
                obj.SubAccName = c.CHART_ACC_NAME;
                obj.SubAccId = c.CHART_ACC_ID;
                obj.SubAccCode = c.CHART_ACC_CODE;
                obj.AccHeader = c.CHART_ACC_HEADER;
                obj.CreationDate = Convert.ToDateTime(c.CHART_ACC_CREATION_DATE);
                obj.ParentAccId = Convert.ToInt64(c.CHART_ACC_PARENT_ID);
                obj.RootAccName = c.CHART_ACC_PARENT_TYPE;
            }
            return obj;
        }
        public long GetAccountID(string name)
        {
            ReportDataContext objDataContext = new ReportDataContext();
            long id = 0;
            foreach (var acc in (from j in objDataContext.CHART_ACCOUNTs where j.CHART_ACC_NAME == name select j))
            {
                id = acc.CHART_ACC_ID;
            }
            return id;
        }
        public EChartOfAccount GetFisDate()
        {
            ReportDataContext objDataContext = new ReportDataContext();
            EChartOfAccount objEChartOfAccount = new EChartOfAccount();
            var b = from fiscalYear in objDataContext.FISCAL_YEARs where fiscalYear.F_YEAR_STATUS == "Active" select fiscalYear;
            foreach (var item in b)
            {
                objEChartOfAccount.FiscalStartDate = Convert.ToDateTime(item.F_YEAR_START_DATE);
                objEChartOfAccount.FisCalEndDate = Convert.ToDateTime(item.F_YEAR_END_DATE);                
            }
            return objEChartOfAccount;
        }
        public EChartOfAccount GetCompanyFirstFisDate()
        {
            ReportDataContext objDataContext = new ReportDataContext();
            EChartOfAccount objEChartOfAccount = new EChartOfAccount();
            var b = from fiscalYear in objDataContext.FISCAL_YEARs orderby fiscalYear.F_YEAR_ID select fiscalYear;
            foreach (var item in b)
            {
                objEChartOfAccount.FiscalStartDate = Convert.ToDateTime(item.F_YEAR_START_DATE);
                objEChartOfAccount.FisCalEndDate = Convert.ToDateTime(item.F_YEAR_END_DATE);
                break;
            }
            return objEChartOfAccount;
        }
        public List<EChartOfAccount> GetAccountInfoLikeNameWise(string accName)
        {
            ReportDataContext objDataContext = new ReportDataContext();
            List<EChartOfAccount> list = new List<EChartOfAccount>();
            foreach (var c in (from j in objDataContext.CHART_ACCOUNTs where j.CHART_ACC_NAME.ToUpper().StartsWith(accName.ToUpper()) select j))
            {
                EChartOfAccount obj = new EChartOfAccount();
                obj.SubAccName = c.CHART_ACC_NAME;
                obj.SubAccId = c.CHART_ACC_ID;
                obj.SubAccCode = c.CHART_ACC_CODE;
                obj.AccHeader = c.CHART_ACC_HEADER;
                obj.ParentAccId = Convert.ToInt64(c.CHART_ACC_PARENT_ID);
                obj.RootAccName = c.CHART_ACC_PARENT_TYPE;
                list.Add(obj);
            }
            return list;
        }

        public int GetFiscalId(DateTime dateTime)
        {
            ReportDataContext objDataContext = new ReportDataContext();
            int id = 0;
            foreach (var fiscalYear in (from j in objDataContext.FISCAL_YEARs where j.F_YEAR_START_DATE <= dateTime.Date && j.F_YEAR_END_DATE >= dateTime.Date select j))
            {
                id = fiscalYear.F_YEAR_ID;
            }
            return id;
        }
        public EChartOfAccount GetSingleFiscalInfo(int id)
        {
            ReportDataContext objDataContext = new ReportDataContext();
            EChartOfAccount objEChartOfAccount = new EChartOfAccount();
            var b = from fiscalYear in objDataContext.FISCAL_YEARs where fiscalYear.F_YEAR_ID==id select fiscalYear;
            foreach (var item in b)
            {
                objEChartOfAccount.FiscalStartDate = Convert.ToDateTime(item.F_YEAR_START_DATE);
                objEChartOfAccount.FisCalEndDate = Convert.ToDateTime(item.F_YEAR_END_DATE);                
            }
            return objEChartOfAccount;
        }

        public bool DoesAccountExist(string AccountNo)
        {
            ReportDataContext objDataContext = new ReportDataContext();

            return objDataContext.CHART_ACCOUNTs.Any(ma => ma.CHART_ACC_CODE.ToUpper() == AccountNo.ToUpper());
        }
    }
}
