using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//Author:Refat
namespace SPIDER_SECURITY.Entity
{
    public class EModulePermission
    {
        public int Id { get; set; }

        public string ModuleName { get; set; }

        public long UserGroupId { get; set; }

        public string UserGroupName { get; set; }
    }
}
