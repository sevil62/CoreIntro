using CoreIntro.Models.Enums;
using System.Collections;
using System.Collections.Generic;

namespace CoreIntro.Models.Entites
{
    public class Employee:BaseEntity
    {
        public string FirstName { get; set; }
        public UserRole Role { get; set; }

        //Relation Properties

        public virtual EmployeeProfile EmployeeProfile { get; set; }
        public virtual IList<Order>Orders { get; set; }

    }
}
