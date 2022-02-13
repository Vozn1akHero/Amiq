using System;

namespace Amiq.ApiGateways.WebApp.Contracts.User
{
    public class DtoUserDescriptionBlock
    {
        public Guid TextBlockId { get; set; }
        public string Header { get; set; }
        public string Content { get; set; }
    }
}
