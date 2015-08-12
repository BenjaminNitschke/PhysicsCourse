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
			this.position = position;
			this.size = size;
		}

		protected readonly Vector3D size;
		protected Vector3D position;
		protected Quaternion orientation = Quaternion.Identity;

		public void Update(float deltaTime)
		{
			velocity += World.Gravity3D * deltaTime; //m/s*s
			position += velocity * deltaTime; //m/s

			if (position.z - size.z / 2 < 0)
			{
				position.z = size.z / 2;
				velocity.z = -velocity.z * (1- FrictionOnCollision);
			}
    }

		public void HandleCollision(Entity3D other)
		{
			if (IsCollidingWith(other))
			{
				//Handle collision
				if (position.z > other.position.z - 0.1f &&
					position.z < other.position.z + 0.1f)
				{
					// Side by side
				}
				else if (position.z > other.position.z)
				{
					// I am on top
					position.z = other.position.z + size.z;
					velocity.z = -velocity.z * (1 - FrictionOnCollision);
					other.velocity.z = 0;
				}
				else
				{
					// Other is on top
					other.position.z = position.z + size.z;
					other.velocity.z = -velocity.z * (1 - FrictionOnCollision);
					velocity.z = 0;
				}
			}
    }

		private bool IsCollidingWith(Entity3D other)
		{
			// Box check (AABB)
			return position.x + size.x / 2 > other.position.x - other.size.x / 2 &&
				position.x - size.x / 2 < other.position.x + other.size.x / 2 &&
				position.y + size.y / 2 > other.position.y - other.size.y / 2 &&
				position.y - size.y / 2 < other.position.y + other.size.y / 2 &&
				position.z + size.z / 2 > other.position.z - other.size.z / 2 &&
				position.z - size.z / 2 < other.position.z + other.size.z / 2;
			//Sphere check:
			// return (position - other.position).Length < size.Length / 2 + other.size.Length / 2;
		}

		private const float FrictionOnCollision = 0.2f;
		protected Vector3D velocity;

		public abstract void Draw();

		public void AddForce(Vector3D addForce)
		{
			velocity += addForce;
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