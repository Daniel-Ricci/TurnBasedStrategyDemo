using UnityEngine;

namespace GridSystem
{
    [System.Serializable]
    public struct GridPosition
    {
        public int x;
        public int z;

        public GridPosition(int x, int z)
        {
            this.x = x;
            this.z = z;
        }
        public static int operator -(GridPosition a, GridPosition b)
        {
            return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.z - b.z);
        }

        public override string ToString()
        {
            return $"x: {x}, z: {z}";
        }
    }
}
