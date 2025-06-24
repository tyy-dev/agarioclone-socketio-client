using TestingLib2Again.Events.Structs;

namespace TestingLib2Again.Utils {
    public static class PlayerUtility {
        public static Vector2 GetMovementDeltaTowardsPlayer(PlayerData localPlayer, PlayerData targetPlayer) {
            if (localPlayer.cells == null || localPlayer.cells.Count == 0)
                return new() {
                    x = 0,
                    y = 0
                };

            double dx = (targetPlayer.x ?? 0) - (localPlayer.x ?? 0);
            double dy = (targetPlayer.y ?? 0) - (localPlayer.y ?? 0);

            double dist = Math.Sqrt((dx * dx) + (dy * dy));
            double deg = Math.Atan2(dy, dx);

            const double speed = 25;

            double deltaY = speed * Math.Sin(deg);
            double deltaX = speed * Math.Cos(deg);

            double slowDownRadius = PlayerData.MinDistance + localPlayer.cells[0].radius;

            if (dist < slowDownRadius) {
                double scale = dist / slowDownRadius;
                deltaY *= scale;
                deltaX *= scale;
            }

            return new() {
                x = deltaX,
                y = deltaY
            };
        }
        public static double MassToRadius(double mass) => 4 + (Math.Sqrt(mass) * 6);
    }
}
