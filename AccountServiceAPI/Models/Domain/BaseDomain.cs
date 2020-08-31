using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountService.Models
{
    public class BaseDomain
    {
        public DateTime CreatedWhen { get; set; }  /* Date Applied */
        public Guid CreatedById { get; set; }
        public User CreatedBy { get; set; }
        public DateTime? LastEditedWhen { get; set; }
        public Guid? LastEditedById { get; set; }
        public User LastEditedBy { get; set; }
        public bool Deleted { get; set; }

    }
}
