using CupCake.Core;
using CupCake.Messages.Blocks;

namespace CupCake.Messages
{
    public class Point3D : Point
    {

        public Point3D(Layer layer,  int x, int y) : base(x, y)
        {
            this.Layer = layer;
        }

        public Point3D()
        {
        }

        public Layer Layer { get; set; }

        public static bool operator ==(Point3D left, Point3D right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Point3D left, Point3D right)
        {
            return !Equals(left, right);
        }

        protected bool Equals(Point3D other)
        {
            return this.X == other.X && this.Y == other.Y && this.Layer == other.Layer;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return this.Equals((Point)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (base.GetHashCode() * 397) ^ (int)this.Layer;
            }
        }
    }
}