using Dapper;
using EmployeeAddressBook.Interfaces;
using EmployeeAddressBook.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAddressBook.Services
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IConfiguration _config;

        public EmployeeRepository(IConfiguration config)
        {

            _config = config;
        }

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("MyConnectionString"));
            }
        }

        public Employee GetEmployee(int id)
        {
            using (IDbConnection conn = Connection)
            {
                string sQuery = "SELECT * FROM Employee emp INNER JOIN Department dept ON emp.DeptId = dept.ID INNER JOIN Location location ON  location.ID= emp.LocationID and  emp.ID = @ID";
                conn.Open();
                var result = conn.Query<Employee, Department, Location, Employee>(sQuery,
                     (emp, dept, location) =>
                     {
                         emp.DeptId = dept.ID;
                         emp.Dept = dept;
                         emp.LocationID = location.ID;
                         emp.Location = location;
                         return emp;
                     },
                  new { ID = id }).FirstOrDefault();
                return result;
            }

        }



        public List<Employee> GetAllEmployees()
        {
            using (IDbConnection conn = Connection)
            {
                string sQuery = "SELECT * FROM Employee emp INNER JOIN Department dept ON emp.DeptId = dept.ID INNER JOIN Location location ON  location.ID= emp.LocationID ";
                conn.Open();
                var result = conn.Query<Employee, Department, Location, Employee>(sQuery,
                     (emp, dept, location) =>
                     {
                         emp.DeptId = dept.ID;
                         emp.Dept = dept;
                         emp.LocationID = location.ID;
                         emp.Location = location;
                         return emp;
                     }
                  ).ToList();
                return result;
            }

        }

        public void AddEmployee(Employee emp)
        {
            if (emp.LocationID == 0)
            {
                emp.LocationID = 1;
            }
            using (IDbConnection conn = Connection)
            {
                string sql = "INSERT INTO Employee (Name,Email,Mobile,Landline,Website,Address,DeptId,LocationId) Values (@Name,@Email,@Mobile,@Landline,@Website,@Address,@DeptId,@LocationId);";
                conn.Open();
                var affectedRows = conn.Execute(sql, new { Name = emp.Name, Email = emp.Email, Mobile = emp.Mobile, Landline = emp.Landline, Website = emp.Website, Address = emp.Address, DeptId=emp.DeptId , LocationId=emp.LocationID});
            }
        }

        public void UpdateEmployee(Employee emp)
        {
            if (emp.LocationID == 0)
            {
                emp.LocationID = 1;
            }
            using (IDbConnection conn = Connection)
            {
                string sqlquery = "UPDATE Employee set Name=@Name, Email=@Email,Mobile=@Mobile,Landline=@Landline,Website=@Website,Address=@Address,DeptId=@DeptId,LocationID=@LocationID WHERE ID= @ID";
                conn.Open();
                var affectedRows = conn.Execute(sqlquery, new { Name = emp.Name, Email = emp.Email, Mobile = emp.Mobile, Landline = emp.Landline, Website = emp.Website, Address = emp.Address, DeptId = emp.DeptId, LocationId = emp.LocationID,ID = emp.ID });
            }
        }

        public void DeleteEmployee(int id)
        {
            using (IDbConnection conn = Connection)
            {
                string sQuery = "Delete FROM Employee where ID=@ID";
                conn.Open();
                conn.Query<Employee>(sQuery, new { ID = id });
            }

        }

        public List<Department> GetEmpDepts()
        {
            using (IDbConnection conn = Connection)
            {
                string sQuery = "Select * from Department";
                conn.Open();
               return conn.Query<Department>(sQuery).ToList();
            }

        }
    }
}

