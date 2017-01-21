using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//Author:REFAT
namespace SPIDER_SECURITY.Entity
{
    public class EUser
    {
        public long Id { get; set; }

        public string UserName { get; set; }

        public string UserPassword { get; set; }

        public long UserGroupId { get; set; }

        public string UserGroupName { get; set; }
    }
}
