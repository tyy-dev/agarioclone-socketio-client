using WebSocketClient;

namespace TestingLib2Again {
    public class App(WebSocketMessageHandler? messageHandler = null) {
        public SimpleWebSocketClient client { get; private set; } = new SimpleWebSocketClient("ws://localhost:3000", messageHandler);
    }
}
