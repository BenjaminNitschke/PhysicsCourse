using System.Globalization;
using System.Runtime.InteropServices;

namespace GraphicsEngine.Datatypes
{
	[StructLayout(LayoutKind.Explicit)]
	public struct Vector2D
	{
		public Vector2D(float x, float y)
		{
			this.x = x;
			this.y = y;
		}

		[FieldOffset(0)]
		public float x;
		[FieldOffset(4)]
		public float y;

		public static readonly Vector2D Zero = new Vector2D(0, 0);

		public static Vector2D operator +(Vector2D first, Vector2D second)
		{
			return new Vector2D(first.x + second.x, first.y + second.y);
		}

		public static Vector2D operator /(Vector2D vector, float divident)
		{
			return new Vector2D(vector.x / divident, vector.y / divident);
		}

		public static Vector2D operator *(Vector2D vector, float scalar)
		{
			return new Vector2D(vector.x * scalar, vector.y * scalar);
		}

		public static Vector2D operator *(Vector2D first, Vector2D second)
		{
			return new Vector2D(first.x * second.x, first.y * second.y);
		}

		public override string ToString()
		{
			return x.ToString(CultureInfo.InvariantCulture) + ", " +
						y.ToString(CultureInfo.InvariantCulture);
		}
	}
}
