using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CHART_ACC_REPORT.ENTITY
{
    public class EDepriciationReport
    {
        public long Acc_Id { get; set; }
        public long Debit_Acc_Id { get; set; }
        public string Acc_Name { get; set; }
        public decimal Original_Cost { get; set; }
        public decimal Dep_Percentage { get; set; }
        public decimal Dep_Value { get; set; }
        public decimal New_Original_Cost { get; set; }
        public decimal CurrentYear_Cost { get; set; }
        public decimal UpTo_Year_Value { get; set; }
        public decimal UpTo_Year_Depriciation { get; set; }
        public string Access_By { get; set; }
        public DateTime Access_Date { get; set; }
    }
}
