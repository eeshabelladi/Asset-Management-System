using employeeSample.API.Models;
using employeeSample.API.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace employeeSample.API.Controllers
{
	[ApiController]
	//[Authorize]
	public class EmployeeAPIController : ControllerBase
	{
		private readonly AssetManagementContext _dbContext;

		public EmployeeAPIController(AssetManagementContext context)
		{
			_dbContext = context;
		}

		[HttpGet]
        [Authorize(Roles = "Admin")]
		[Route("Employees")]
		public async Task<IActionResult> GetEmployees()
		{
            try
            {
                var employees = await _dbContext.Employeemasters
        .Select(item => new EmployeeCreateDto
        {
            EmployeeId = item.EmployeeId,
            Gid = item.Gid,
            FullName = item.FullName,
            Email = item.Email,
            IsActive = item.IsActive,
            ManagerName = item.Manager != null ? item.Manager.FullName : "No Manager",
            EmpCreatedByName = item.EmpCreatedByNavigation != null ? item.EmpCreatedByNavigation.FullName : "Unknown"
        })
        .ToListAsync();

                if (employees.Count > 0)
                {
                    return Ok(employees);
                }
                else
                {
                    return Ok("No employees in the database");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        [Authorize]
		[Route("EmployeeByGid/{gid}")]
        public async Task<IActionResult> GetEmployeeByGID(string gid)
        {
            try
            {
                var employee = await _dbContext.Employeemasters
                    .Where(e => e.Gid == gid)
                    .Select(item => new EmployeeCreateDto
                    {
                        EmployeeId = item.EmployeeId,
                        Gid = item.Gid,
                        FullName = item.FullName,
                        Email = item.Email,
                        Password = item.Password,
                        IsActive = item.IsActive,
                        ManagerId = item.ManagerId,
                        EmpCreatedBy = item.EmpCreatedBy,
                        ManagerName = item.Manager != null ? item.Manager.FullName : "No Manager",
                        EmpCreatedByName = item.EmpCreatedByNavigation != null ? item.EmpCreatedByNavigation.FullName : "Unknown"
                    })
                    .FirstOrDefaultAsync();

                if (employee != null)
                {
                    return Ok(employee);
                }
                else
                {
                    return NotFound("Employee not found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }




        [HttpGet]
        [Authorize]
        [Route("Employees/{id:int}")]
        public async Task<IActionResult> GetEmployeeByID(int id)
        {
            try
            {
                var employee = await _dbContext.Employeemasters
                    .Where(e => e.EmployeeId == id)
                    .Select(item => new EmployeeCreateDto
                    {
                        EmployeeId = item.EmployeeId,
                        Gid = item.Gid,
                        FullName = item.FullName,
                        Email = item.Email,
                        Password = item.Password,
                        IsActive = item.IsActive,
                        ManagerName = item.Manager != null ? item.Manager.FullName : "No Manager",
                        EmpCreatedByName = item.EmpCreatedByNavigation != null ? item.EmpCreatedByNavigation.FullName : "Unknown"
                    })
                    .FirstOrDefaultAsync();

                if (employee != null)
                {
                    return Ok(employee);
                }
                else
                {
                    return NotFound("Employee not found");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        [Authorize]
        [Route("Employees/ByManager/{managerId:int}")]
        public async Task<IActionResult> GetEmployeesByManagerID(int managerId)
        {
            try
            {
                var employees = await _dbContext.Employeemasters
                    .Where(e => e.ManagerId == managerId)
                    .Select(item => new EmployeeCreateDto
                    {
                        EmployeeId = item.EmployeeId,
                        Gid = item.Gid,
                        FullName = item.FullName,
                        Email = item.Email,
                        Password = item.Password,
                        IsActive = item.IsActive,
                        ManagerName = item.Manager != null ? item.Manager.FullName : "No Manager",
                        EmpCreatedByName = item.EmpCreatedByNavigation != null ? item.EmpCreatedByNavigation.FullName : "Unknown"
                    })
                    .ToListAsync();

                if (employees != null && employees.Any())
                {
                    return Ok(employees);
                }
                else
                {
                    return NotFound("No employees found for the given manager ID");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("Employees")]
		public async Task<IActionResult> CreateEmployee([FromBody] EmployeeCreateDto employeeDto)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}

				var newEmployee = new Employeemaster
				{
					EmployeeId = employeeDto.EmployeeId,
					Gid = employeeDto.Gid,
					FullName = employeeDto.FullName,
					Email = employeeDto.Email,
					Password = employeeDto.Password,
					IsActive = employeeDto.IsActive,
					ManagerId = employeeDto.ManagerId,
					EmpCreatedBy = employeeDto.EmpCreatedBy,
					EmpCreatedOn = DateTime.UtcNow,
				};

				_dbContext.Employeemasters.Add(newEmployee);
				await _dbContext.SaveChangesAsync();

				return Ok();
			}
			catch (Exception ex)
			{
				return StatusCode(500, "Internal server error");
			}
		}

		[HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route("Employees/{id:int}")]
		public async Task<IActionResult> DeleteEmployee(int id)
		{
			try
			{
				var employee = await _dbContext.Employeemasters.FindAsync(id);
				if (employee == null)
				{
					return NotFound("Employee not found");
				}

				_dbContext.Employeemasters.Remove(employee);
				await _dbContext.SaveChangesAsync();

				return Ok();
			}
			catch (Exception ex)
			{
				return StatusCode(500, "Internal server error: " + ex.Message);
			}
		}

		[HttpPut]
        [Authorize(Roles = "Admin")]
        [Route("Employees/{id:int}")]
		public async Task<IActionResult> UpdateEmployee(int id, [FromBody] EmployeeCreateDto employeeDto)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return BadRequest(ModelState);
				}

				var employee = await _dbContext.Employeemasters.FindAsync(id);
				if (employee == null)
				{
					return NotFound("Employee not found");
				}

				employee.Gid = employeeDto.Gid;
				employee.FullName = employeeDto.FullName;
				employee.Email = employeeDto.Email;
				employee.Password = employeeDto.Password;
				employee.IsActive = employeeDto.IsActive;
				employee.ManagerId = employeeDto.ManagerId;
				employee.EmpCreatedBy = employeeDto.EmpCreatedBy;
				employee.EmpCreatedOn = DateTime.UtcNow;

				_dbContext.Employeemasters.Update(employee);
				await _dbContext.SaveChangesAsync(); 

				return Ok();
			}
			catch (Exception ex)
			{
				return StatusCode(500, "Internal server error: " + ex.Message);
			}
		}

        [HttpGet]
        [Authorize]
        [Route("Employees/Login")]
        public async Task<IActionResult> Login()
        {
            try
            {
				var name = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                var role = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
				var gid = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
                var employee = await _dbContext.Employeemasters
                    .Where(e => e.Gid == gid)
                    .Select(item => new EmployeeCreateDto
                    {
                        EmployeeId = item.EmployeeId,
                        Gid = item.Gid,
                        FullName = item.FullName,
                        Email = item.Email,
                        IsActive = item.IsActive,
                        ManagerId = item.ManagerId,
                        role = role
                    })
                    .FirstOrDefaultAsync();

                if (employee == null)
                {
                    return Unauthorized("Invalid GID or Password.");
                }

                return Ok(employee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }
    }
}

