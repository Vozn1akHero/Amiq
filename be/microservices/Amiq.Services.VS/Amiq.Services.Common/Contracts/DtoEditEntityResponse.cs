﻿namespace Amiq.Services.Common.Contracts
{
    public class DtoEditEntityResponse
    {
        public bool Result { get; set; }
        public string Message { get; set; }
        public object Entity { get; set; }
    }
}
