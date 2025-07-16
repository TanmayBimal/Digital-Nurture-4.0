using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web_Api.Models;
using Web_Api.Filters;

namespace Web_Api.Controllers
{
    [Authorize(Roles = "POC,Admin")]
    [ApiController]
    [Route("[controller]")]
    //[ServiceFilter(typeof(CustomAuthFilter))] // Custom Authorization filter

    public class EmployeeController : ControllerBase
    {
        private static List<Employee> employees = new List<Employee>
        {
            new Employee
            {
                Id = 1,
                Name = "John Doe",
                Salary = 50000,
                Permanent = true,
                Department = new Department { Id = 101, Name = "IT" },
                Skills = new List<Skill>
                {
                    new Skill { Id = 1, SkillName = "C#" },
                    new Skill { Id = 2, SkillName = "SQL" }
                },
                DateOfBirth = new DateTime(1990, 5, 21)
            }
        };

        [HttpGet("GetStandard")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<Employee>> GetStandard()
        {
            // To test exception filter uncomment this line
            //throw new Exception("Test Exception");

            return Ok(employees);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Employee> Post([FromBody] Employee emp)
        {
            if (emp.Id <= 0)
            {
                return BadRequest("Invalid employee id");
            }

            if (employees.Any(e => e.Id == emp.Id))
            {
                return BadRequest("Employee with same id already exists");
            }

            employees.Add(emp);
            return CreatedAtAction(nameof(GetStandard), new { id = emp.Id }, emp);
        }

        [HttpPut("UpdateEmployee")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Employee> UpdateEmployee([FromBody] Employee inputEmp)
        {
            if (inputEmp.Id <= 0)
            {
                return BadRequest("Invalid employee id");
            }

            var emp = employees.FirstOrDefault(e => e.Id == inputEmp.Id);

            if (emp == null)
            {
                return BadRequest("Invalid employee id");
            }

            // Only update fields that are NOT default in input
            if (!string.IsNullOrEmpty(inputEmp.Name))
                emp.Name = inputEmp.Name;

            if (inputEmp.Salary > 0)
                emp.Salary = inputEmp.Salary;

            emp.Permanent = inputEmp.Permanent;

            if (inputEmp.Department != null)
                emp.Department = inputEmp.Department;

            if (inputEmp.Skills != null && inputEmp.Skills.Count > 0)
                emp.Skills = inputEmp.Skills;

            if (inputEmp.DateOfBirth != DateTime.MinValue)
                emp.DateOfBirth = inputEmp.DateOfBirth;

            return Ok(emp);
        }



        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid employee id");
            }

            var emp = employees.FirstOrDefault(e => e.Id == id);

            if (emp == null)
            {
                return BadRequest("Employee not found");
            }

            employees.Remove(emp);
            return Ok($"Employee with id {id} deleted successfully");
        }
    }
}
