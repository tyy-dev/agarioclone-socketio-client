using TestingLib2Again.Events.Outgoing;
using TestingLib2Again.Utils;

namespace TestingLib2Again.Events.Structs {
    public class PlayerData {
        public const double MinSpeed = 6.25;
        public const double SplitCellSpeed = 20;
        public const double SpeedDecrement = 0.5;
        public const double MinDistance = 50;
        public const double PushingAwaySpeed = 1.1;
        public const double MergeTimer = 15;
        public required string id { get; set; }
        public int hue { get; set; }
        public string? name { get; set; }
        public bool admin { get; set; }
        public int? screenWidth { get; set; }
        public int? screenHeight { get; set; }
        public long? timeToMerge { get; set; }
        public long lastHeartbeat { get; set; }
        public double? massTotal { get; set; } = 0;
        public List<Cell>? cells { get; set; }
        public double? x { get; set; }
        public double? y { get; set; }
        public Vector2? target { get; set; }
        public PlayerData() { }

        public void Init(Vector2 position) {
            this.cells = [new() {
                x = position.x,
                y = position.y,
                mass = Config.defaultPlayerMass,
                speed = PlayerData.MinSpeed,
                radius = PlayerUtility.MassToRadius(Config.defaultPlayerMass)
            }];
            this.massTotal = Config.defaultPlayerMass;
            this.x = position.x;
            this.y = position.y;
            this.target = new();
        }

        public void ReceivePlayerData(PlayerData data) {
            this.x = data.x;
            this.y = data.y;
            this.hue = data.hue;
            this.massTotal = data.massTotal;
            this.cells = data.cells;
            this.id = data.id;
        }

        public async Task UpdateHeartbeat(WebSocketClient.WebSocketClient client) {
            this.lastHeartbeat = DateTime.Now.Ticks;
            await client.Emit(new HeartbeatEvent() {
                target = this.target ?? new() {
                    x = 0,
                    y = 0,
                }
            });
        }

        public string GetName() => string.IsNullOrEmpty(this.name) ? "An unnamed cell" : this.name;
    }
}
