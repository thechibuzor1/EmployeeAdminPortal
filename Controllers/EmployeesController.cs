using EmployeeAdminPortal.Data;
using EmployeeAdminPortal.Models;
using EmployeeAdminPortal.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAdminPortal.Controllers
{
    // localhost:xxxx/api/employees
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public EmployeesController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            var allEmployees =  dbContext.Employees.ToList();

            return Ok(allEmployees);

        }

        [HttpPost]
        public IActionResult CreateEmployee(CreateEmployeeDTO createEmployeeDTO)
        {
            var employee = new Employee()
            {
                Name = createEmployeeDTO.Name,
                Email = createEmployeeDTO.Email,
                Phone = createEmployeeDTO.Phone,
                Salary = createEmployeeDTO.Salary
            };

            dbContext.Employees.Add(employee);
            dbContext.SaveChanges();

            return Ok(employee);
        }

    }
}
