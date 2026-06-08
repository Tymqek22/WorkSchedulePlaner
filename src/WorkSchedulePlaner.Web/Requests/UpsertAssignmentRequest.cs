using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using WorkSchedulePlaner.Application.DTOs;

namespace WorkSchedulePlaner.Web.Requests
{
	public class UpsertAssignmentRequest
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Title of a shift tile is required")]
		public string? Title { get; set; }
		public string? Description { get; set; }
		public DateOnly Date { get; set; }

		[Required(ErrorMessage = "At least one employee must be assigned to shift.")]
		public List<AssignmentRequest> Shifts { get; set; }
		public SelectList? Employees { get; set; }
		public int ScheduleId { get; set; }
	}
}
