﻿using System;
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

		public override bool Equals(object obj)
		{
			var other = (Vector2D)obj;
			return Equals(other);
		}

		public bool Equals(Vector2D other)
		{
			return x > other.x - Epsilon && x < other.x + Epsilon &&
				y > other.y - Epsilon && y < other.y + Epsilon;
		}

		private const float Epsilon = 0.0001f;

		public override int GetHashCode()
		{
			unchecked
			{
				// ReSharper disable NonReadonlyMemberInGetHashCode
				return (x.GetHashCode() * 397) ^ y.GetHashCode();
			}
		}

		public override string ToString()
		{
			return x.ToString(CultureInfo.InvariantCulture) + ", " +
						y.ToString(CultureInfo.InvariantCulture);
		}

		public Vector2D Rotate(float angle)
		{
			float sin = (float)Math.Sin(angle * DegreeToRadians);
			float cos = (float)Math.Cos(angle * DegreeToRadians);
			return new Vector2D(cos * x - sin * y, sin * x + cos * y);
		}

		private const float DegreeToRadians = (float)Math.PI / 180.0f;
	}
}