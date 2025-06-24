using TestingLib2Again.Events.Structs;
using WebSocketClient.Events;

namespace TestingLib2Again.Events.Incoming {
    public class ReceiveGameData : WebSocketEvent {
        public override string eventId => "serverTellPlayerMove";

        [EventDataIndex(0, deserializeJson = true)]
        public PlayerData? localPlayerData { get; set; }

        [EventDataIndex(1, deserializeJson = true)]
        public List<PlayerData>? visiblePlayers { get; set; }
    }
}
