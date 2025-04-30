using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkSchedulePlaner.Domain.Entities
{
	public class EmployeeShift
	{
		public int EmployeeId { get; set; }
		public Employee Employee { get; set; }

		public int ShiftTileId { get; set; }
		public ShiftTile ShiftTile { get; set; }

		public TimeSpan StartTime { get; set; }
		public TimeSpan EndTime { get; set; }
	}
}
