using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CHART_ACC_REPORT.ENTITY
{
    public class EFiscalYear
    {
        public int F_Year_Id { get; set; }
        public string F_Year_Name { get; set; }
        public DateTime F_year_Start_Date { get; set; }
        public DateTime F_Year_End_Date { get; set; }
        public string Access_By { get; set; }
        public string F_Year_Status { get; set; }
    }
}
