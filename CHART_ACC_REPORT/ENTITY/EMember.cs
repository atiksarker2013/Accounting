using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//Author:REFAT
namespace CHART_ACC_REPORT.ENTITY
{
    public class EMember
    {
        public long AccId { get; set; }

        public long MemberId { get; set; }

        public string MemberNo { get; set; }

        public string MemberName { get; set; }

        public string AccountName { get; set; }

        public string AccountNo { get; set; }

        public long ParentAccountId { get; set; }

        public string ParentAccountName { get; set; }

        public string Prefix { get; set; }        
    }
}
