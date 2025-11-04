using Shared.Enums;

namespace Application.Common.Commands.Documents
{
    public class RgCommand : DocumentCommand
    {
        public InssuingAuthority IssuingAuthority { get; private set; }

        public RgCommand(string number, DateTime birthDate, InssuingAuthority issuingAuthority) : base(number, birthDate)
        {
            IssuingAuthority = issuingAuthority;
        }
    }
}
