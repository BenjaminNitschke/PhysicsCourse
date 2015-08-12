using System;
using OpenTK;

namespace PhysicsEngine
{
	public struct Vector3D
	{
		public Vector3D(float x, float y, float z) : this()
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		public float x;
		public float y;
		public float z;

		public static Vector3D operator -(Vector3D a, Vector3D b)
		{
			return new Vector3D(a.x - b.x, a.y - b.y, a.z - b.z);
		}

		public static Vector3D operator +(Vector3D a, Vector3D b)
		{
			return new Vector3D(a.x + b.x, a.y + b.y, a.z + b.z);
		}

		public static Vector3D operator *(Vector3D vector, float scalar)
		{
			return new Vector3D(vector.x * scalar, vector.y * scalar, vector.z * scalar);
		}

		public static Vector3D operator /(Vector3D vector, float divident)
		{
			return new Vector3D(vector.x / divident, vector.y / divident, vector.z / divident);
		}

		public static implicit operator Vector3(Vector3D vector)
		{
			return new Vector3(vector.x, vector.y, vector.z);
		}

		public float Length
		{
			get
			{
				return (float)Math.Sqrt(x * x + y * y + z * z);
			}
		}
	}
}