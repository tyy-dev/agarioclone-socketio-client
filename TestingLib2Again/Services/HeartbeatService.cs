using TestingLib2Again.Events.Structs;

namespace TestingLib2Again.Services {
    /// <summary>
    /// Service responsible for sending heartbeat pings (in the form of movement data) to the server.
    /// </summary>
    /// <param name="client"></param>
    public class HeartBeatService(SimpleWebSocketClient client) : BaseService(client) {
        /// <summary>
        /// Token source used to cancel the running heartbeat task.
        /// </summary>
        private CancellationTokenSource? heartbeatCancellation { get; set; }

        /// <summary>
        /// Starts the heartbeat loop, sending heartbeat pings in the form of movement data at the specified interval. (Default is every frame)
        /// </summary>
        /// <param name="intervalMs">Interval in milliseconds between heartbeats. Default is 16ms. (every frame)</param>
        public void Start(int intervalMs = 16) {
            this.heartbeatCancellation = new CancellationTokenSource();

            Task.Run(async () => {
                while (!this.heartbeatCancellation.Token.IsCancellationRequested) {
                    await this.client.playerService.localPlayer!.UpdateHeartbeat(this.client.webSocketClient);
                    await Task.Delay(intervalMs);
                }
            }, this.heartbeatCancellation.Token);
        }

        /// <summary>
        /// Stops the heartbeat loop.
        /// </summary>
        public void Stop() => this.heartbeatCancellation?.Cancel();
    }
}
