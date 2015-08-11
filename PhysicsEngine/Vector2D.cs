using System;
using System.Globalization;
using OpenTK;

namespace PhysicsEngine
{
	public struct Vector2D
	{
		public Vector2D(float x, float y) : this()
		{
			this.x = x;
			this.y = y;
		}

		public float x;
		public float y;

		public static Vector2D operator +(Vector2D a, Vector2D b)
		{
			return new Vector2D(a.x + b.x, a.y + b.y);
		}

		public static Vector2D operator -(Vector2D a, Vector2D b)
		{
			return new Vector2D(a.x - b.x, a.y - b.y);
		}

		public static Vector2D operator *(Vector2D vector, float scalar)
		{
			return new Vector2D(vector.x * scalar, vector.y * scalar);
		}

		public static Vector2D operator *(float scalar, Vector2D vector)
		{
			return new Vector2D(vector.x * scalar, vector.y * scalar);
		}

		public static Vector2D operator /(Vector2D vector, float divident)
		{
			return new Vector2D(vector.x / divident, vector.y / divident);
		}

		public override bool Equals(object obj)
		{
			return Equals((Vector2D)obj);
		}
		
		public bool Equals(Vector2D other)
		{
			return x > other.x - Epsilon && x < other.x + Epsilon &&
				y > other.y - Epsilon && y < other.y + Epsilon;
		}

		private const float Epsilon = 0.000001f;

		public override int GetHashCode()
		{
			unchecked
			{
				return (x.GetHashCode() * 397) ^ y.GetHashCode();
			}
		}

		public static bool operator ==(Vector2D a, Vector2D b)
		{
			return a.Equals(b);
		}

		public static bool operator !=(Vector2D a, Vector2D b)
		{
			return !a.Equals(b);
		}

		public Vector2D Rotate(float degrees)
		{
			float sin = (float)Math.Sin(DegreesToRadians(degrees));
			float cos = (float)Math.Cos(DegreesToRadians(degrees));
			return new Vector2D(x * cos - y * sin, y * cos + x * sin);
		}

		private static float DegreesToRadians(float degrees)
		{
			return degrees * (float)Math.PI / 180.0f;
		}

		public float Length { get { return (float)Math.Sqrt(x*x+y*y); } }
		
		public Vector2D MirrorAtNormal(Vector2D normal)
		{
			normal = Normalize(normal);
			return this - 2 * DotProduct(this, normal) * normal;
		}

		public static float DotProduct(Vector2D a, Vector2D b)
		{
			return a.x * b.x + a.y * b.y;
		}

		public static Vector2D Normalize(Vector2D vector)
		{
			return vector / vector.Length;
		}

		public override string ToString()
		{
			return x.ToString(CultureInfo.InvariantCulture) + ", " +
						y.ToString(CultureInfo.InvariantCulture);
		}
	}
}