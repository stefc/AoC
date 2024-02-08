using System.Collections.Generic;

namespace advent.of.code.y2019.day12
{
    public class ConnectionComparer : IEqualityComparer<(Point3D start, Point3D end)>
    {
        public bool Equals(
            (Point3D start, Point3D end) a, (Point3D start, Point3D end) b)
        {
            return
            (a.start.Equals(b.start) && a.end.Equals(b.end)) ||
            (a.start.Equals(b.end) && a.end.Equals(b.start));
        }

        public int GetHashCode((Point3D start, Point3D end) line)
        {
            return line.start.GetHashCode() ^ line.end.GetHashCode();
        }

        public static ConnectionComparer Default = new ConnectionComparer();
    }
}