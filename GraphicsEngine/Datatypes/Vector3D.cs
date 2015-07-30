using System.Runtime.InteropServices;

namespace GraphicsEngine.Datatypes
{
	[StructLayout(LayoutKind.Explicit)]
	public struct Vector3D
	{
		public Vector3D(float x, float y, float z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		[FieldOffset(0)]
		public float x;
		[FieldOffset(4)]
		public float y;
		[FieldOffset(8)]
		public float z;

		public static readonly Vector3D Zero = new Vector3D(0, 0, 0);

		public static Vector3D operator +(Vector3D first, Vector3D second)
		{
			return new Vector3D(first.x + second.x, first.y + second.y, first.z + second.z);
		}

		public static Vector3D operator /(Vector3D vector, float divident)
		{
			return new Vector3D(vector.x / divident, vector.y / divident, vector.z / divident);
		}
	}
}