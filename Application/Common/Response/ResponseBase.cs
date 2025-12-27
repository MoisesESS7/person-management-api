using Application.Common.Response.Auditables;

namespace Application.Common.Response
{
    public class ResponseBase
    {
        public string Id { get; private set; }
        public AuditableEntityResponse Auditable { get; private set; }

        public ResponseBase(string id, AuditableEntityResponse auditable)
        {
            Id = id;
            Auditable = auditable;
        }
    }
}
