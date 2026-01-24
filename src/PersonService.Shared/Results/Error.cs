namespace PersonService.Shared.Results
{
    public sealed record Error(string Code, string Message, ErrorType Type);
}
