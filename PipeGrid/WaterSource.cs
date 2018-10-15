using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PipeGridNamespace {
    public enum WaterSourceDirection {
        Up, Down, Left, Right
    }
        
    public class WaterSource : IEquatable<WaterSource> {
        public WaterSource(int x, int y, float volume, WaterSourceDirection direction) {
            X = x;
            Y = y;
            Volume = volume;
            Direction = direction;
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public float Volume { get; set; }

        public WaterSourceDirection Direction { get; set; }

        public bool Equals(WaterSource other) {
            if (other == null) return false;
            return this.Id.Equals(other.Id);
        }
    }
}
