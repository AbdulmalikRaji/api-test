using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestApiCRUDemo.EmployeeData;
using RestApiCRUDemo.Modules;
using System;
using Microsoft.AspNetCore.Authorization;

namespace RestApiCRUDemo.Controllers
{
    
    [ApiController]

    public class employeescontroller : ControllerBase
    {
        private IEmployeeData _employeeData;
        public employeescontroller(IEmployeeData employeeData)
        {
            _employeeData = employeeData;
        }

        [HttpGet]
        [Authorize]
        [Route("api/[controller]")]
        public IActionResult GetEmployees()
        {
            return Ok(_employeeData.GetEmployees());
        }

        [HttpGet]
        [Authorize]
        [Route("api/[controller]/{id}")]
        public IActionResult GetEmployees(Guid id)
        {
            var employee = _employeeData.GetEmployee(id);
            if (employee != null)
            {
                return Ok(employee);
            }
            return NotFound($"employee with Id: {id} was not found");
        }

        [HttpPost]
        [Authorize]
        [Route("api/[controller]")]
        public IActionResult GetEmployees(Employee employee )
        {
            _employeeData.AddEmployee(employee);
            return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + employee.Id,employee.Name,employee.Address,employee.Phone_number );
        }

        private IActionResult Created(string v, string name, string address, string phone_number)
        {
            return Ok();
        }

        [HttpDelete]
        [Authorize]
        [Route("api/[controller]/{id}")]
        public IActionResult DeleteEmployee(Guid id)
        {
            var employee = _employeeData.GetEmployee(id);
            if(employee != null)
            {
                _employeeData.DeleteEmployee(employee);
                return Ok();
            }
            return NotFound($"employee with Id: {id} was not found");
        }

        [HttpPatch]
        [Route("api/[controller]/{id}")]
        public IActionResult EditEmployee(Guid id, Employee employee)
        {
            var existingEmployee = _employeeData.GetEmployee(id);
            if (existingEmployee != null)
            {
                employee.Id= existingEmployee.Id;
                _ = _employeeData.EditEmployee(employee);
            }
            return Ok(employee);
        }
    }
}
