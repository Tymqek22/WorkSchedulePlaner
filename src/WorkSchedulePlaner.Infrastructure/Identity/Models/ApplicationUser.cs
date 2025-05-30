using Microsoft.AspNetCore.Identity;
using WorkSchedulePlaner.Domain.Entities;

namespace WorkSchedulePlaner.Infrastructure.Identity.Models
{
	public class ApplicationUser : IdentityUser
	{
		public string Name { get; set; }
		public string LastName { get; set; }
		public DateTime BirthDate { get; set; }
		public List<WorkSchedule> Schedules { get; set; }
		public List<Employee> AssignedEmployees { get; set; }
	}
}
