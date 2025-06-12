using WorkSchedulePlaner.Application.Common.Errors;

namespace WorkSchedulePlaner.Application.Common.Results
{
	public class Result
	{
		public bool IsSuccess { get; }
		public bool IsFailure => !IsSuccess;
		public Error? Error { get; }

		public Result(bool isSuccess, Error? error)
		{
			IsSuccess = isSuccess;
			Error = error;
		}

		public static Result Success() => new(true,null);
		public static Result Failure(Error error) => new(false,error);
	}
}
