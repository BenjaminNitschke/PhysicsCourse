using Jitter.LinearMath;
using OpenTK;

namespace PhysicsEngine.Physics3D
{
	public static class JitterMath
	{
		public static JVector ToJVector(Vector3D position)
		{
			return new JVector(position.x, position.y, position.z);
		}

		public static Vector3D ToVector3D(JVector position)
		{
			return new Vector3D(position.X, position.Y, position.Z);
		}

		public static Matrix4 ToMatrix4(JMatrix orientation, JVector position)
		{
			return new Matrix4(
				orientation.M11, orientation.M12, orientation.M13, 0,
				orientation.M21, orientation.M22, orientation.M23, 0,
				orientation.M31, orientation.M32, orientation.M33, 0,
				position.X, position.Y, position.Z, 1);
		}
	}
}