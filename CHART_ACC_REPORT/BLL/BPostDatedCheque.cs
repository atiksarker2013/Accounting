using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CHART_ACC_REPORT.ENTITY;
using CHART_ACC_REPORT.DAL;
//Author:REFAT
namespace CHART_ACC_REPORT.BLL
{
    public class BPostDatedCheque
    {
        PostDatedChequeDAL objPostDatedChequeDAL = new PostDatedChequeDAL();
        
        public List<EPostDatedCheque> GetAllPostDatedCheque(DateTime startdate, DateTime endDate)
        {
            return objPostDatedChequeDAL.GetAllPostDatedCheque(startdate,endDate);
        }
    }
}
