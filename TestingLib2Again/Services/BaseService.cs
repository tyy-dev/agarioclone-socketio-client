namespace TestingLib2Again.Services {
    public abstract class BaseService(SimpleWebSocketClient client) {
        protected SimpleWebSocketClient client { get; } = client;
    }
}
