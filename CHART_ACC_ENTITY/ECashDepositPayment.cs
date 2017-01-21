using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CHART_ACC_ENTITY
{
    public class ECashDepositPayment
    {
        public long Id { get; set; }

        public long BankAccountId { get; set; }

        public string BankAccountName { get; set; }

        public long OthersAccountId { get; set; }

        public string OthersAccountCode { get; set; }

        public string OthersAccountName { get; set; }

        public DateTime DepositePaymentDate { get; set; }

        public decimal DepositPaymentAmount { get; set; }        

        public string Description { get; set; }

        public string RootAccName { get; set; }
    }
}
