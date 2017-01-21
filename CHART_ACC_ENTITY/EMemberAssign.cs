using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//Author:Refat
namespace CHART_ACC_ENTITY
{
    public class EMemberAssign
    {
        public long AccId { get; set; }

        public long MemberId { get; set; }

        public string MemberNo { get; set; }

        public string MemberName { get; set; }

        public string AccountName { get; set; }

        public string AccountNo { get; set; }

        public long ParentAccountId { get; set; }

        public string ParentAccountName { get; set; }

        public string ParentAccountPrefix { get; set; }

        public decimal DrBalance{get;set;}

        public decimal CrBalance { get; set; }

        public string Status { get; set; }

        public string RootAccName { get; set; }

        public decimal Balance { get; set; }
    }
}
