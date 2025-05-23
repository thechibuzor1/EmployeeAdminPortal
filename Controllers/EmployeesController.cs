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


        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetEmployeeById(Guid id)
        {
            var employee =  dbContext.Employees.Find(id);

            if (employee is null)
            {
                return NotFound("Employee not found");
            }
            return Ok(employee);
        }

        [HttpGet]
        [Route("{name}")]
        public IActionResult GetEmployeeByName(string name)
        {
            var employee = dbContext.Employees.FirstOrDefault(e => e.Name == name);
            if (employee is null)
            {
                return NotFound("Employee not found");
            }
            return Ok(employee);
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


        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdateEmployee(Guid id, UpdateEmployeeDTO updateEmployeeDTO)
        {
            var employee = dbContext.Employees.Find(id);

            if (employee is null)
            {
                return NotFound("Employee not found");
            }


            employee.Name = updateEmployeeDTO.Name;
            employee.Email= updateEmployeeDTO.Email;
            employee.Phone = updateEmployeeDTO.Phone;
            employee.Salary = updateEmployeeDTO.Salary;

            dbContext.SaveChanges();

            return Ok(employee);

        }


        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult DeleteEmployee(Guid id)
        {
            var employee = dbContext.Employees.Find(id);

            if (employee is null)
            {
                return NotFound("Employee not found");
            }

            dbContext.Employees.Remove(employee);
            dbContext.SaveChanges();

            return Ok("Employee deleted successfully");
        }

    }
}
