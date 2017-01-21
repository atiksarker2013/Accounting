using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CHART_ACC_REPORT.ENTITY
{
    public class ETrialBalanceReport
    {
        public long Account_Id { get; set; }
        public string Account_Code { get; set; }
        public string AccountTitle { get; set; }
        public string AccessBy { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public DateTime FiscalStartDate { get; set; }
        public DateTime FisCalEndDate { get; set; }

        //*** // 

        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public string IsHeader { get; set; }
        public string Acc_Type { get; set; }
    }
}
