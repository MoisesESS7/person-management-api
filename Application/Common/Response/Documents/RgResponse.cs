using Shared.Enums;

namespace Application.Common.Response.Documents
{
    public class RgResponse : DocumentResponse
    {
        public InssuingAuthority IssuingAuthority { get; private set; }

        public RgResponse(string number, DateTime birthDate, InssuingAuthority issuingAuthority) : base(number, birthDate)
        {
            IssuingAuthority = issuingAuthority;
        }
    }
}
