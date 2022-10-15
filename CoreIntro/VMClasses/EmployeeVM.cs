using CoreIntro.Models.Entites;
using System.Collections.Generic;

namespace CoreIntro.VMClasses
{
    public class EmployeeVM
    {
        public Employee Employee { get; set; }
        public List<Employee>Employees  { get; set; }
    }
}
