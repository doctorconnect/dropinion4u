using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessEntities
{
    public class Base
    {
        public bool IsActive { get; set; }
        public string UserCode { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}