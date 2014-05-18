using PlayerIOClient;

namespace CupCake.EE.Messages
{
    public interface IRegisteredMessage
    {
        void Invoke(object sender, Message message);
    }
}