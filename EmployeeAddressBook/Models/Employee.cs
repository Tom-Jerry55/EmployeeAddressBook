using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAddressBook.Models
{
    public class Employee
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Landline { get; set; }
        public string Website { get; set; }
        public string Address { get; set; }
        public int DeptId { get; set; }
        public int LocationID { get; set; }
        public Department Dept { get; set; }
        public Location Location { get; set; }
    }
}
