using GraphicsEngine.Datatypes;
using Jitter.LinearMath;
using OpenTK;

namespace GraphicsEngine.Meshes
{
	public static class JitterDatatypes
	{
		public static Matrix4 ToMatrix4(JMatrix m, JVector position)
		{
			return new Matrix4(
				m.M11, m.M12, m.M13, 0,
				m.M21, m.M22, m.M23, 0,
				m.M31, m.M32, m.M33, 0,
				position.X, position.Y, position.Z, 1);
		}

		public static JVector ToJVector(Vector3D vector)
		{
			return new JVector(vector.x, vector.y, vector.z);
		}

		public static JVector ToJVector(Vector3 vector)
		{
			return new JVector(vector.X, vector.Y, vector.Z);
		}
	}
}