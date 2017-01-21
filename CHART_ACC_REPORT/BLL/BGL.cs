using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CHART_ACC_REPORT.ENTITY;
using CHART_ACC_REPORT.DAL;
//Author:REFAT
namespace CHART_ACC_REPORT.BLL
{
    public class BGL
    {
        GLDAL objGLDAL=new GLDAL();
        BChartAccount objBChartOfAccount = new BChartAccount();        
        int lvl = 0,depth=0; decimal balance = 0;
        List<EGL> listGL = new List<EGL>();
        DateTime fromDate, toDate;
        bool IsCompanyStartToCurrentBalance;
        public List<EGL> LoadListofGL(DateTime from, DateTime to, bool IsCompanyStartToCB)
        {
            listGL.Clear();
            fromDate = from;
            toDate = to;
            IsCompanyStartToCurrentBalance = IsCompanyStartToCB;
            foreach (EAccountType objType in objBChartOfAccount.GetAllAccountType())
            {
                int count = listGL.Count;
                EGL objGL = new EGL();
                objGL.AccTypeName = objType.AccTypeName;
                objGL.AccTypeId = objType.AccTypeId;
                GetAccountTypeBalance(objGL.AccTypeId);
                objGL.TypeTotalBalance = balance;
                balance = 0;
                Ptree(objGL.AccTypeId, objGL.AccTypeName, objGL.TypeTotalBalance);                
                if (count == listGL.Count)
                {
                    objGL.CurrentBalance = "";
                    listGL.Add(objGL);
                }
            }
            return listGL;
        }

        private void Ptree(long pId, string groupName, decimal typeTotBalance)
        {
            List<EGL> getList = objGLDAL.GetSubAccount(pId, fromDate, toDate, IsCompanyStartToCurrentBalance);
            int lnth=GetMaxBalanceLenth(getList);
            foreach (EGL obj in getList)
            {                
                string space = "";
                depth += 1;
                lvl += 4;
                for (int i = 0; i < lvl; i++)
                {
                    space += "  ";
                }
                obj.SubAccName = space + obj.SubAccName;
                obj.AccTypeName = groupName;
                obj.TypeTotalBalance = typeTotBalance;
                obj.Depth = depth;
                /*check digit for right align*/
                if (obj.CurrentBalance.Length != lnth)
                {
                    string marginalSpace = "";
                    for (int j = 0; j < lnth - obj.CurrentBalance.Length; j++)
                    {
                        marginalSpace += "  ";
                    }
                    space+=marginalSpace;
                }
                obj.CurrentBalance = space + obj.CurrentBalance;
                listGL.Add(obj);
                if (obj.AccHeader == "Yes")
                {
                    Ptree(obj.SubAccId, groupName, typeTotBalance);
                }
                depth -= 1;
                lvl -= 4;
            }
        }

        private int GetMaxBalanceLenth(List<EGL> list)
        {
            List<int> listLenth = new List<int>();
            foreach (var obj in list)
            {
                listLenth.Add(obj.CurrentBalance.Length);
            }
            if (listLenth.Count > 0)
            {
                return listLenth.Max();
            }
            return 0;
        }

        private void GetAccountTypeBalance(long pId)
        {
            foreach (EGL obj in objGLDAL.GetAccountforBalance(pId, fromDate, toDate,IsCompanyStartToCurrentBalance))
            {
                balance += Convert.ToDecimal(obj.CurrentBalance);
                if (obj.AccHeader == "Yes")
                {
                    GetAccountTypeBalance(obj.SubAccId);
                }
            }
        }
        public List<EGL> LoadListofGLParticaularIdwise(long typeId, string typeName, DateTime from, DateTime to,bool IsCompanyStartToCB)
        {
            listGL.Clear();
            fromDate = from;
            toDate = to;
            IsCompanyStartToCurrentBalance = IsCompanyStartToCB;
            int count = listGL.Count;
            EGL objGL = new EGL();
            objGL.AccTypeName = typeName;
            objGL.AccTypeId = typeId;
            objGL.TypeTotalBalance = objGLDAL.GetAccountInfo(typeId, from, to,IsCompanyStartToCB).TypeTotalBalance;
            GetAccountTypeBalance(objGL.AccTypeId);
            objGL.TypeTotalBalance += balance;
            balance = 0;
            Ptree(objGL.AccTypeId, objGL.AccTypeName, objGL.TypeTotalBalance);
            if (count == listGL.Count)
            {
                objGL.CurrentBalance = "";
                listGL.Add(objGL);
            }
            return listGL;
        }

    }
}
