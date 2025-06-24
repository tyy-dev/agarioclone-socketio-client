using WebSocketClient;

namespace TestingLib2Again {
    internal class Program {
        private static async Task Main(string[] args) {
            WebSocketMessageHandler handler = new();
            App app = new(handler);
            _ = app.client.ConnectAsync();
           
            await Task.Delay(-1);
        }
    }
}
