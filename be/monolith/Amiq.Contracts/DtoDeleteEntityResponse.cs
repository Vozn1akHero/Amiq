﻿namespace Amiq.Contracts
{
    public class DtoDeleteEntityResponse
    {
        public bool Result { get; set; }
        public string Message { get; set; }
        public object Entity { get; set; }
    }
}
