using TestingLib2Again.Events.Structs;
using WebSocketClient.Events;

namespace TestingLib2Again.Events.Outgoing {
    public class HeartbeatEvent : WebSocketEvent {
        public override string eventId => "0";

        [EventDataIndex(0)]
        public Vector2? target { get; set; }
    }
}
