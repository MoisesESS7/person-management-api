using Shared.Exceptions;

namespace Application.Exceptions
{
    public class PageNotFoundException : BusinessException
    {
        public override int StatusCode => 404;
        public override string Title => "Page not found";
        public override string Type => "https://httpstatuses.com/404";

        public PageNotFoundException(string message) : base(message) {}
    }
}
