﻿namespace Amiq.Services.Notification.Contracts.Utils
{
    public class DtoCreateEntityResponse
    {
        public bool Result { get; set; }
        public string Message { get; set; }
        public object Entity { get; set; }
    }
}
