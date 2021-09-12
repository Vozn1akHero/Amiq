using System;

namespace Amiq.Contracts.Core
{
    public class DtoDescriptionBlock
    {
        public Guid TextBlockId { get; set; }
        public string Header { get; set; }
        public string Content { get; set; }
    }
}
