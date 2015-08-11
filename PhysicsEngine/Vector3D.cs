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

		public static Vector3D operator /(Vector3D vector, float divident)
		{
			return new Vector3D(vector.x / divident, vector.y / divident, vector.z / divident);
		}
	}
}