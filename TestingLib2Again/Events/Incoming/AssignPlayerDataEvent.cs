using TestingLib2Again.Events.Structs;
using WebSocketClient.Events;

namespace TestingLib2Again.Events.Incoming {
    public class AssignPlayerDataEvent : WebSocketEvent {
        public override string eventId => "welcome";

        [EventDataIndex(0, deserializeJson = true)]
        public PlayerData? playerData { get; set; }

        [EventDataIndex(1, deserializeJson = true)]
        public MapSize? mapSize { get; set; }
    }
}
