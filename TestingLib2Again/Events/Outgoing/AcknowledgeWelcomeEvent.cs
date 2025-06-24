using TestingLib2Again.Events.Structs;
using WebSocketClient.Events;

namespace TestingLib2Again.Events.Outgoing {
    public class AcknowledgeWelcomeEvent : WebSocketEvent {
        public override string eventId => "gotit";

        [EventDataIndex(0)]
        public PlayerData? playerData { get; set; }
    }
}
