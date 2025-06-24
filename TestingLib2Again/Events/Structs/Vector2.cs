namespace TestingLib2Again.Events.Structs {
    public class Vector2 {
        public double x { get; set; } = 0;
        public double y { get; set; } = 0;

        public double Length() => Math.Sqrt(this.x * this.x + this.y * this.y);

        public Vector2 Normalize() {
            double length = this.Length();
            return length == 0
                ? new Vector2 { x = 0, y = 0 }
                : new Vector2 {
                    x = this.x / length,
                    y = this.y / length
                };
        }
    }
}
