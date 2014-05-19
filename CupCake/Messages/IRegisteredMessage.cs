using PlayerIOClient;

namespace CupCake.Messages
{
    public interface IRegisteredMessage
    {
        void Invoke(object sender, Message message);
    }
}