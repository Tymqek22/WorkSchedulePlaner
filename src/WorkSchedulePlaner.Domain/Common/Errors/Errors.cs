namespace WorkSchedulePlaner.Domain.Common.Errors
{
	public static class Errors
	{
		public static class User
		{
			public static readonly Error DoesntExist =
				new("User.DoesntExist","User with this email doesn't exist.");

			public static readonly Error UserAlreadyAssignedToEmployee =
				new("User.AlreadyAssignedToEmployee","User with this email is already assigned to one " +
					"employee in this schedule.");
		}

		public static class Employee
		{
			public static readonly Error NotFound =
				new("Employee.NotFound","Employee not found.");

			public static readonly Error ArgumentsInWrongFormat =
				new("Employee.ArgumentsInWrongFormat","First or last name are not appropriate.");
		}

		public static class Schedule
		{
			public static readonly Error TitleRequired =
				new("Schedule.TitleRequired","Schedule title is required.");

			public static readonly Error NotFound =
				new("Schedule.NotFound","Schedule not found.");

			public static readonly Error EmployeeAlreadyExist =
				new("Schedule.EmployeeAlreadyExist","Employee is already assigned to schedule.");

			public static readonly Error EmployeeNotFound =
				new("Schedule.EmployeeNotFound","Employee is not a member of this schedule.");

			public static readonly Error EmailAlreadyAssigned =
				new("Schedule.EmailAlreadyAssigned","Employee with that email exists in schedule.");
		}

		public static class ShiftTile
		{
			public static readonly Error NoEmployeesAssigned =
				new("ShiftTile.NoEmployeesAssigned","At least one employee must be assigned to shift.");

			public static readonly Error EmployeeDuplicated =
				new("ShiftTile.EmployeeDuplicated","Employee cannot be assigned twice in a shift.");

			public static readonly Error TooManyEmployeeAssignments =
				new("ShiftTile.TooManyEmployeeAssignments","Employee cannot be assigned to more than one " +
					"shift during a day.");

			public static readonly Error NotFound =
				new("ShiftTile.NotFound","Shift tile not found.");
		}
	}
}
