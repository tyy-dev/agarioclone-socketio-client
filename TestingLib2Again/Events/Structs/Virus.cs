using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingLib2Again.Events.Structs {
    public class Virus {
        public required string id { get; set; }
        public double x { get; set; }
        public double y { get; set; }
        public double mass { get; set; }
        public double radius { get; set; }
        public string fill { get; set; }
        public string stroke { get; set; }
        public int strokeWidth { get; set; }
        public Virus() { }
    }
}
