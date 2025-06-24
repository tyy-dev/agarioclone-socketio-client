using TestingLib2Again.Events.Structs;

namespace TestingLib2Again.Services {
    public class PlayerService(SimpleWebSocketClient client) : BaseService(client) {
        public Dictionary<string, PlayerData> visiblePlayers { get; set; } = [];
        public PlayerData? localPlayer { get; set; }
        public PlayerData? GetPlayerById(string id) => this.visiblePlayers.TryGetValue(id, out PlayerData? player) ? player : null;
    }
}
