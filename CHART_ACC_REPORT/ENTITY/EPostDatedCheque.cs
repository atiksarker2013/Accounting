using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//Author:REFAT
namespace CHART_ACC_REPORT.ENTITY
{
    public class EPostDatedCheque
    {
        public long Id { get; set; }

        public long AccountID { get; set; }

        public string AccountCode { get; set; }

        public string AccountName { get; set; }

        public string DepositePaymentDate { get; set; }

        public decimal DepositPaymentAmount { get; set; }

        public long BankAccountId { get; set; }

        public string BankAccountName { get; set; }

        public string BankAccountCode { get; set; }

        public string Description { get; set; }

        public string BankInfoinCheque { get; set; }

        public string BankCheque { get; set; }

        public string BankChequeDate { get; set; }
     
        public string Status { get; set; }

        public string TransactionType { get; set; }

        public string JournalVoucherId { get; set; }       
    }
}
