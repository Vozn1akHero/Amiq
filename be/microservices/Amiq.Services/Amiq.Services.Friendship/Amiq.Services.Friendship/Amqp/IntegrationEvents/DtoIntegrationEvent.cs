namespace Amiq.Services.Friendship.Amqp.IntegrationEvents
{
    public class DtoIntegrationEvent
    {
        public string Name { get; set; }
        public IntegrationEvent Body { get; set; }
    }
}
