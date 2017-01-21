using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CHART_ACC_REPORT.ENTITY
{
    public class EMemberReport
    {
        public long Id { get; set; }
        public string Member_No { get; set; }
        public string No_Of_Share { get; set; }
        public string Member_Full_Name { get; set; }
        public string Father_Name { get; set; }
        public string Mother_Name { get; set; }
        public string Husband_Name { get; set; }
        public string Present_Address { get; set; }
        public string Permanent_Address { get; set; }
        public int Mobile_No { get; set; }
        public string Education_Status { get; set; }
        public DateTime Member_Birth_Date { get; set; }
        public string Religion { get; set; }
        public string Member_Occupation { get; set; }
        public string Marital_Status { get; set; }
        public string Nationality { get; set; }
        public string Nominee_Name { get; set; }
        public DateTime Nominee_Birth_Date { get; set; }
        public string Nominee_Occupation { get; set; }
        public string Relation_With_Nominee { get; set; }
        public byte[] Member_Photo { get; set; }
        public byte[] Digital_Signature { get; set; }
        public byte[] Nominee_Photo { get; set; }

        public string Access_By { get; set; }
        public DateTime Access_Date { get; set; }     
    }
}
