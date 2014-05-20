using PlayerIOClient;

namespace CupCake.Messages
{
    public interface IRegisteredMessage
    {
        void Invoke(Message message);
    }
}