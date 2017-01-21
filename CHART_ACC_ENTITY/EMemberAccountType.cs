using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CHART_ACC_ENTITY
{
    public class EMemberAccountType
    {
        public long Account_Id { get; set; }

        public string Account_Name { get; set; }

        public string Account_Prefix { get; set; }
        
        public string Account_No { get; set; }
        
        public long Parent_Account_No { get; set; }
        
        public string Parent_Account_Type { get; set; }
        
        public string Account_Status { get; set; }
        
        public string Access_By { get; set; }

    }
}
