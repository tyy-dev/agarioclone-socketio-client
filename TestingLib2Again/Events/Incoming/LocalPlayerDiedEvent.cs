using WebSocketClient.Events;

namespace TestingLib2Again.Events.Incoming {
    public class LocalPlayerDiedEvent : WebSocketEvent {
        public override string eventId => "RIP";
    }
}
