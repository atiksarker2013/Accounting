using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//Author:REFAT
namespace CHART_ACC_REPORT.ENTITY
{
    public class EAccountStatement
    {
        public long ID { get; set; }

        public string JournalDetailsId { get; set; }

        public long AccountId { get; set; }

        public string AccountCode { get; set; }

        public string AccountTitle { get; set; }

        public string Address { get; set; }

        public decimal Debit { get; set; }

        public decimal Credit { get; set; }

        public bool IsDebit { get; set; }

        public DateTime Date { get; set; }

        public string DisplayDate { get; set; }

        public string Notes { get; set; }

        public string COAName { get; set; }

        public string COACode { get; set; }

        public decimal COADebit { get; set; }

        public decimal COACredit { get; set; }

        public DateTime COACreationDate { get; set; }

        public bool IsCOADebit { get; set; }

        public decimal DisplayBalance { get; set; }

        public decimal AccountCurrentBalance { get; set; }

        public string ChequeNo { get; set; }

        public string Remarks { get; set; }
    }
}
