using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//Author:REFAT
namespace CHART_ACC_REPORT.ENTITY
{
    public class EBalanceSheet
    {
        public string AccType { get; set; }
    
        public long AccountId { get; set; }

        public string AccountName { get; set; }
        
        public string AccountCode { get; set; }

        public Decimal Debit { get; set; }

        public Decimal Credit { get; set; }

        public string IsHeader { get; set; }
    }
}
