// DTOs/EmployeeCreateDto.cs

using System.ComponentModel.DataAnnotations;

namespace employeeSample.API.Models
{
	public class EmployeeCreateDto
	{
		public int EmployeeId { get; set; }
		public string Gid { get; set; }
		public string FullName { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public bool IsActive { get; set; }
		public int? ManagerId { get; set; }
		public string? ManagerName { get; set; } 
		public string? role { get; set; }
		public int EmpCreatedBy { get; set; }
		public string? EmpCreatedByName { get; set; }
		public DateTime EmpCreatedOn { get; set; }

	}
}

