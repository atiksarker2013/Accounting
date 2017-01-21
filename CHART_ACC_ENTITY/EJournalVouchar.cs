using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CHART_ACC_ENTITY
{
    public class EJournalVouchar
    {
        public string Journal_Id { get; set; }

        public long Journal_Acc_Id { get; set; }

        public string Journal_Acc_Code { get; set; }

        public string Journal_Acc_Name { get; set; }

        public string Journal_Notes { get; set; }

        public DateTime Journal_Date { get; set; }

        public decimal Journal_Debit_Amount { get; set; }

        public decimal Jounal_Credit_Amount { get; set; }

        public string Journal_ChequeBank { get; set; }

        public string Journal_Cheque { get; set; }

        public DateTime Journal_ChequeDate { get; set; }

        public string Journal_BankRemarks { get; set; }

        public string Access_By { get; set; }

        public DateTime Access_Date { get; set; }

        public string Journal_DisplayChequeDate { get; set; }
    }
}
