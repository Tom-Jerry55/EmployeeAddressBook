using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EmployeeAddressBook.Models;
using EmployeeAddressBook.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmployeeAddressBook.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;

        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetEmployee(int id)
        {
            return View(employeeRepository.GetEmployee(id));
        }

        [HttpGet]
        public IActionResult AddForm()
        {
             ViewData["Departments"] = new SelectList(employeeRepository.GetEmpDepts(), "ID", "Name");
            return View("Form");
        }

        [HttpGet]
        public IActionResult EditForm(int id)
        {
            var emp = employeeRepository.GetEmployee(id);
            ViewData["Departments"] = new SelectList(employeeRepository.GetEmpDepts(), "ID", "Name",emp.DeptId);
            return View("Form", emp);
        }


        [HttpPost]
        public IActionResult SaveEmployeeDetails(Employee emp)
        {
            if (emp.ID > 0)
            {
                employeeRepository.UpdateEmployee(emp);
            }
            else
            {
                employeeRepository.AddEmployee(emp);
            }
            return RedirectToAction("Index");
        }


        public IActionResult DeleteEmployee(int id)
        {
            employeeRepository.DeleteEmployee(id);
            return RedirectToAction("Index", "Employee");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
