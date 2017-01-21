using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CHART_ACC_ENTITY;
using CHART_ACC_DAL;

namespace CHART_ACC_BLL
{
    public class BIncomeConfigure
    {
        IncomeConfigureDAL objIncomeConfigureDAL = new IncomeConfigureDAL();

        public List<EIncomeConfigure> LoadListofISConfig(string typeName)
        {
            int lvl = 0;
            List<EIncomeConfigure> listIncomeSheetConfig = new List<EIncomeConfigure>();
            BChartOfAccount objBChartOfAccount = new BChartOfAccount();
            foreach (EAccountType objType in objBChartOfAccount.GetAllAccountType())
            {
                if (objType.AccTypeName == typeName)
                {
                    Ptree(objType.AccTypeId, objType.AccTypeName, lvl, listIncomeSheetConfig);
                    break;
                }
            }
            return listIncomeSheetConfig;
        }

        private void Ptree(long pId, string groupName, int lvl, List<EIncomeConfigure> listIncomeSheetConfig)
        {
            List<EIncomeConfigure> getList = objIncomeConfigureDAL.GetSubAccount(pId);

            foreach (EIncomeConfigure obj in getList)
            {
                string space = "";

                lvl += 4;
                for (int i = 0; i < lvl; i++)
                {
                    space += "  ";
                }
                obj.SubAccName = space + obj.SubAccName;
                obj.AccTypeName = groupName;

                listIncomeSheetConfig.Add(obj);
                if (obj.AccHeader == "Yes")
                {
                    Ptree(obj.SubAccId, groupName, lvl, listIncomeSheetConfig);
                }
                lvl -= 4;
            }
        }

        public bool HasValueinISConfig()
        {
            return objIncomeConfigureDAL.HasValueinISConfig();
        }

        public bool SaveISConfig(List<EIncomeConfigure> listEincomeSheet)
        {
            return objIncomeConfigureDAL.SaveISConfig(listEincomeSheet);
        }

        public bool DeleteISConfig()
        {
            return objIncomeConfigureDAL.DeleteISConfig();
        }
    }
}
