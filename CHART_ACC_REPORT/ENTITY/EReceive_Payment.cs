using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//Author:REFAT
namespace CHART_ACC_REPORT.ENTITY
{
    public class EReceive_Payment
    {
        public long AccId { get; set; }

        public string AccName { get; set; }

        public string AccCode { get; set; }

        public string AccType { get; set; }

        public decimal Balance { get; set; }

        public string CurrentBalance { get; set; }

        
    }
}
