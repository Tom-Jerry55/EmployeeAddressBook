using EmployeeAddressBook.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAddressBook.ViewComponents
{
    public class EmployeeListViewComponent : ViewComponent
    {
        private readonly IEmployeeRepository employeeRepository;

        public EmployeeListViewComponent(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;

        }

        public IViewComponentResult Invoke()
        {
            return View(employeeRepository.GetAllEmployees());
        }
    }
}
