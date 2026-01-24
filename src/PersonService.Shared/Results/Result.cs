namespace PersonService.Shared.Results
{
    public class Result
    {
        public bool IsSuccess { get; }
        public bool IsFailure => !IsSuccess;
        public IReadOnlyCollection<Error> Errors { get; }

        public Result(bool isSuccess, IReadOnlyCollection<Error> errors)
        {
            IsSuccess = isSuccess;
            Errors = errors;
        }

        public static Result Ok() => new(true, []);
        public static Result Fail(Error error) => new(false, [error]);
        public static Result Fail(IEnumerable<Error> errors) => new(false, [.. errors]);
    }
}
