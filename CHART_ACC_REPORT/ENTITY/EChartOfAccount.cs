using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CHART_ACC_REPORT.ENTITY
{
    public class EChartOfAccount
    {
        public long ParentAccId { get; set; }

        public long SubAccId { get; set; }

        public string SubAccCode { get; set; }

        public string SubAccName { get; set; }

        public decimal AccOpeningBalanceDR { get; set; }

        public decimal AccOpeningBalanceCR { get; set; }

        public string RootAccName { get; set; }

        public string AccHeader { get; set; }

        public string AccStatus { get; set; }

        public DateTime CreationDate { get; set; }

        public string AccessBy { get; set; }

        public DateTime AccessDateTime { get; set; }

        public DateTime FiscalStartDate { get; set; }

        public DateTime FisCalEndDate { get; set; }
    }
}
