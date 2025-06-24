
using SocketIOClient;
using SocketIOClient.Transport;
using TestingLib2Again.Events.Incoming;
using TestingLib2Again.Events.Outgoing;
using TestingLib2Again.Events.Structs;
using TestingLib2Again.Services;
using TestingLib2Again.Utils;
using WebSocketClient;
using WebSocketClient.Events;

namespace TestingLib2Again {
    public class SimpleWebSocketClient {
        public readonly WebSocketClient.WebSocketClient webSocketClient;

        /// <summary>
        /// Service responsible for sending heartbeat pings (in the form of movement data) to the server.
        /// </summary>
        public HeartBeatService heartBeatService {
            get; init;
        }

        /// <summary>
        /// Service for managing and storing the players
        /// </summary>
        public PlayerService playerService {
            get; init;
        }

        /// <summary>
        /// Handler responsible for processing incoming WebSocket messages
        /// </summary>
        public WebSocketMessageHandler messageHandler {
            get;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleWebSocketClient"/> class.
        /// </summary>
        /// <param name="url">The WebSocket server URL to connect to.</param>
        /// <param name="messageHandler">Optional custom message handler; defaults to a new instance if null.</param>
        public SimpleWebSocketClient(string url, WebSocketMessageHandler? messageHandler = null) {
            this.messageHandler = messageHandler ?? new WebSocketMessageHandler();

            this.webSocketClient = new WebSocketClient.WebSocketClient(
                url: url,
                messageHandler: this.messageHandler,
                isSocketIO: true,
                socketIOOptions: new SocketIOOptions {
                    EIO = SocketIO.Core.EngineIO.V4,
                    Transport = TransportProtocol.WebSocket,
                    Query = [new("type", "player")]
                }
            );

            this.playerService = new PlayerService(this);
            this.heartBeatService = new HeartBeatService(this);

            this.RegisterDefaultEventHandlers();
        }
        private void RegisterDefaultEventHandlers() {
            this.messageHandler.On<WebSocketEventConnected>(async _ => {
                Console.WriteLine("[Connected]");
                await this.webSocketClient.Emit("respawn");
            });
            this.messageHandler.On<AssignPlayerDataEvent>(async welcomeEvt => {
                Console.WriteLine("[Welcome]");

                PlayerData playerData = welcomeEvt.playerData!;
                playerData.name = "Bot";
                playerData.screenHeight = 99999;
                playerData.screenWidth = 99999;
                playerData.Init(new());

                this.playerService.localPlayer = playerData;
                this.playerService.visiblePlayers = new() {
                    { playerData.id, playerData }
                };

                await this.webSocketClient.Emit(new AcknowledgeWelcomeEvent() {
                    playerData = playerData!,
                });

                this.heartBeatService.Start();
            });

            //sockets[socketID].emit('serverTellPlayerMove', playerData, map.players.data, map.food.data, map.massFood.data, map.viruses.data);
            this.messageHandler.On<ReceiveGameData>(gameData => {
                this.playerService.localPlayer!.ReceivePlayerData(gameData.localPlayerData!);
                this.playerService.visiblePlayers = gameData.visiblePlayers!.ToDictionary(player => player.id);

                PlayerData? targetPlayer = this.playerService.visiblePlayers.Values.FirstOrDefault();

                if (targetPlayer != null) {
                    this.playerService.localPlayer.target =
                        PlayerUtility.GetMovementDeltaTowardsPlayer(this.playerService.localPlayer, targetPlayer);

                    Console.WriteLine($"[Target Set] x={this.playerService.localPlayer.target.x}, y={this.playerService.localPlayer.target.y}");
                }

            });

            this.messageHandler.On<LocalPlayerDiedEvent>(async _ => await this.webSocketClient.Emit("respawn"));
            this.messageHandler.On<WebSocketEventCloseConnection>(evt =>
                Console.WriteLine($"[Connection Closed] Reason={evt.status}, Description={evt.description}"));

            this.messageHandler.On(evt => {
                if (evt.innerEventEventId == "serverTellPlayerMove")
                    return;
                Console.WriteLine($"Event={evt.innerEventEventId}, Data={string.Join(", ", evt.rawData)}");
            });
        }

        /// <summary>
        /// Asynchronously connects to the WebSocket server.
        /// </summary>
        public async Task ConnectAsync() => await this.webSocketClient.ConnectAsync();
    }
}
