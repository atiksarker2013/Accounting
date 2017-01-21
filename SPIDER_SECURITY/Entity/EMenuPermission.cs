using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//Author:Refat
namespace SPIDER_SECURITY.Entity
{
    public class EMenuPermission
    {
        public long Id { get; set; }

        public string ModuleName { get; set; }

        public int ModuleId { get; set; }
        
        public long UserGroupId { get; set; }     
        
        public string ParentMenuName { get; set; }
        
        public string ParentMenuContent { get; set; }     
        
        public string ChildMenuName { get; set; }
        
        public string ChildMenuContent { get; set; }        
    }
}
