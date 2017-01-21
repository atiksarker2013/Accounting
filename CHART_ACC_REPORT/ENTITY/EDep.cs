using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CHART_ACC_REPORT.ENTITY
{
    public class EDep
    {
        public string AccType { get; set; }

        public long AccountId { get; set; }

        public string AccountName { get; set; }

        public string AccountCode { get; set; }

        public Decimal Debit { get; set; }

        public Decimal Credit { get; set; }

        public string IsHeader { get; set; }

        // For saving purpose
        public string Journal_Id { get; set; }
        public string Journal_Notes { get; set; }
        public long Debit_Id { get; set; }
        public long Credit_Id { get; set; }
        public string Debit_Account_Name { get; set; }
        public string Credit_Account_Name { get; set; }
        public Decimal Dep_Percentage { get; set; }
        public Decimal Original_Cost { get; set; }
        public Decimal Dep_Value { get; set; }
        public Decimal New_Original_Cost { get; set; }

        public Decimal Debit_Amount { get; set; }
        public string Debit_AccountCode { get; set; }

        public string Credit_AccountCode { get; set; }
        public Decimal Credit_Amount { get; set; }

        public string Access_By { get; set; }
        public DateTime Access_Date { get; set; }
    }
}
