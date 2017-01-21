using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//Author:REFAT
namespace CHART_ACC_ENTITY
{
    public class EBalanceSheetConfigure
    {
        public long AccTypeId { get; set; }

        public string AccTypeName { get; set; }
       
        public long ParentAccId { get; set; }

        public string AccHeader { get; set; }

        public long SubAccId { get; set; }

        public string SubAccCode { get; set; }

        public string SubAccName { get; set; }
       
        public bool Status { get; set; }
       
    }
}
