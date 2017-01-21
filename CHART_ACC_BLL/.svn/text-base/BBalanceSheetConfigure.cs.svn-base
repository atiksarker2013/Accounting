using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CHART_ACC_ENTITY;
using CHART_ACC_DAL;
//Author:REFAT
namespace CHART_ACC_BLL
{
    public class BBalanceSheetConfigure
    {
        BalanceSheetConfigureDAL objBalanceSheetConfigureDAL = new BalanceSheetConfigureDAL();
       
       
        public List<EBalanceSheetConfigure> LoadListofBSConfig(string typeName)
        {
            int lvl = 0;
            List<EBalanceSheetConfigure> listBalanceSheetConfig = new List<EBalanceSheetConfigure>();           
            BChartOfAccount objBChartOfAccount = new BChartOfAccount();
            foreach (EAccountType objType in objBChartOfAccount.GetAllAccountType())
            {
                if (objType.AccTypeName == typeName)
                {
                    Ptree(objType.AccTypeId, objType.AccTypeName,lvl,listBalanceSheetConfig);
                    break;
                }
            }
            return listBalanceSheetConfig;
        }

        private void Ptree(long pId, string groupName, int lvl, List<EBalanceSheetConfigure> listBalanceSheetConfig)
        {
            List<EBalanceSheetConfigure> getList = objBalanceSheetConfigureDAL.GetSubAccount(pId);

            foreach (EBalanceSheetConfigure obj in getList)
            {
                string space = "";
               
                lvl += 4;
                for (int i = 0; i < lvl; i++)
                {
                    space += "  ";
                }
                obj.SubAccName = space + obj.SubAccName;
                obj.AccTypeName = groupName;               
               
                listBalanceSheetConfig.Add(obj);
                if (obj.AccHeader == "Yes")
                {
                    Ptree(obj.SubAccId, groupName,lvl,listBalanceSheetConfig);
                }               
                lvl -= 4;
            }
        }

        public bool SaveBSConfig(List<EBalanceSheetConfigure> listEbalanceSheet)
        {
            return objBalanceSheetConfigureDAL.SaveBSConfig(listEbalanceSheet);
        }

        public bool DeleteBSConfig()
        {
            return objBalanceSheetConfigureDAL.DeleteBSConfig();
        }

        public bool HasValueinBSConfig()
        {
            return objBalanceSheetConfigureDAL.HasValueinBSConfig();
        }
    }
}
