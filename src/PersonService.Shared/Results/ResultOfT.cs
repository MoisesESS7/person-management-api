namespace PersonService.Shared.Results
{
    public sealed class ResultOfT<T> : Result
    {
        public T? Value { get; }

        private ResultOfT(bool isSuccess, T? value, IReadOnlyCollection<Error> errors) : base(isSuccess, errors)
        {
            Value = value;
        }

        public static ResultOfT<T> Ok(T value) => new(true, value, []);
        public static new ResultOfT<T> Fail(Error error) => new(false, default, [error]);
        public static new ResultOfT<T> Fail(IEnumerable<Error> errors) => new(false, default, [.. errors]);
    }
}
