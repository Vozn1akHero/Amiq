namespace Amiq.Services.User.Amqp.IntegrationEvents
{
    public class DtoIntegrationEvent
    {
        public string Name { get; set; }
        public IntegrationEvent Body { get; set; }
    }
}
