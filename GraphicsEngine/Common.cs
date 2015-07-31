using System;
using System.Drawing;
using GraphicsEngine.Datatypes;
using OpenTK;

namespace GraphicsEngine
{
	public class Common
	{
		public Common()
		{
			BackgroundColor = Color.Black;
		}

		public Color BackgroundColor;
		public const float PI = (float)Math.PI;
		public const float PI2 = (float)Math.PI * 2;
		public const float PIHalf = (float)Math.PI / 2;
		public const float PIThird = (float)Math.PI / 3;
		public const float FieldOfView = PI / 2;
		public const float NearPlane = 0.1f;
		public const float FarPlane = 100.0f;
		public static readonly Vector3D CameraPosition = new Vector3D(0, -6, 4);
		public static readonly Vector3D LightPosition = new Vector3D(0.3f, 0.2f, 1);
		public static Matrix4 ViewMatrix;
		public static Matrix4 ProjectionMatrix;
	}
}