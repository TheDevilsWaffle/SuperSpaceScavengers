namespace Events
{
    
}
namespace InputEvents
{
    public class SubscriptionData
    {
        public virtual void Send(InputEventInfo _inputEventInfo) { }
        public virtual void Send(InputEventInfo _inputEventInfo, int _channel) { }

        internal static bool ChannelIsInvalid(int _channel, int _channelSubscriptionsCount)
        {
            if (_channel < 0 || _channel > _channelSubscriptionsCount - 1)
            {
                UnityEngine.Debug.Log("Channel '" + _channel.ToString() + "' is not a valid channel.");
                return true;
            }
            return false;
        }
    }
}
