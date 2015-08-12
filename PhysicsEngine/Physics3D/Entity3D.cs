using FarseerPhysics.Dynamics;
using Jitter.Dynamics;
using Jitter.LinearMath;
using OpenTK;

namespace PhysicsEngine.Physics3D
{
	/// <summary>
	/// Provides common Physics Properties and the Update method for World
	/// </summary>
	public abstract class Entity3D : Drawable, PhysicsUpdatable
	{
		protected Entity3D(Vector3D position, Vector3D size, float mass)
		{
			World.Add(this);
			this.size = size;
		}

		protected readonly Vector3D size;
		protected RigidBody body;
		protected Vector3D position
		{
			get
			{
				return JitterMath.ToVector3D(body.Position);
			}
		}
		protected JMatrix orientation
		{
			get
			{
				return body.Orientation;
			}
		}
		
		public abstract void Draw();

		public void AddForce(Vector3D addForce)
		{
			body.ApplyImpulse(JitterMath.ToJVector(addForce * 10));
		}

		public bool DoesRayHit(Vector3D ray)
		{
			for (float distance = 1; distance < 20; distance++)
			{
				var checkPosition = World.cameraPosition + ray * distance;
				if (Contains(checkPosition))
					return true;
			}
			return false;
		}

		private bool Contains(Vector3D checkPosition)
		{
			// Box check AABB
			return position.x + size.x / 2 > checkPosition.x &&
				position.x - size.x / 2 < checkPosition.x &&
				position.y + size.y / 2 > checkPosition.y &&
				position.y - size.y / 2 < checkPosition.y &&
				position.z + size.z / 2 > checkPosition.z &&
				position.z - size.z / 2 < checkPosition.z;
		}
	}
}