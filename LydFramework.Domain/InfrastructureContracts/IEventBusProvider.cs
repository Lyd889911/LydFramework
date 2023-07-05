
namespace LydFramework.Domain.InfrastructureContracts
{
    public interface IEventBusProvider
    {
        public void Publish(string routingKey, object data);
    }
}
