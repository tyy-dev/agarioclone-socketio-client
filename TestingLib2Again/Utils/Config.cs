namespace TestingLib2Again.Utils {
    public static class Config {
        public static string host { get; set; } = "localhost";
        public static int port { get; set; } = 3000;
        public static int foodMass { get; set; } = 1;
        public static int fireFood { get; set; } = 20;
        public static int limitSplit { get; set; } = 16;
        public static double defaultPlayerMass { get; set; } = 10;
        public static VirusConfig virus { get; } = new() {
            fill = "#33ff33",
            stroke = "#19D119",
            strokeWidth = 20,
            defaultMass = new VirusDefaultMassConfig {
                from = 100,
                to = 150
            },
            splitMass = 180,
            uniformDisposition = false
        };
        public static int gameWidth { get; set; } = 2000;
        public static int gameHeight { get; set; } = 2000;
        public static string adminPass { get; set; } = "DEFAULT";
        public static int gameMass { get; set; } = 20000;
        public static int maxFood { get; set; } = 1000;
        public static int maxVirus { get; set; } = 50;
        public static double slowBase { get; set; } = 4.5;
        //public static bool logChat = false;
        public static int networkUpdateFactor { get; set; } = 40;
        public static int maxHeartbeatInterval { get; set; } = 5000;
        public static bool foodUniformDisposition { get; set; } = true;
        //public static string newPlayerInitialPosition { get; set; } = "farthes";
        public static int massLossRate { get; set; } = 1;
        public static int minMassLoss { get; set; } = 50;
    }
    public class VirusConfig {
        public string fill { get; set; }
        public string stroke { get; set; }
        public int strokeWidth { get; set; }
        public VirusDefaultMassConfig defaultMass { get; set; }
        public int splitMass { get; set; }
        public bool uniformDisposition { get; set; }
    }
    public class VirusDefaultMassConfig {
        public int from { get; set; } = 100;
        public int to { get; set; } = 150;
    }
}
