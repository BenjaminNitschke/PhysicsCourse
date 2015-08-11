using System;

namespace PhysicsEngine
{
	/// <summary>
	/// Provides common Physics Properties and the Update method for World
	/// </summary>
	public abstract class Entity : Drawable, PhysicsUpdatable
	{
		protected Entity(Vector2D position, Vector2D size, float mass)
		{
			World.Add(this);
			this.position = position;
			this.size = size;
			this.mass = mass;
			acceleration = World.Gravity2D;
			force = mass * acceleration;
			frictionOnCollision = 0.1f;
		}

		protected Vector2D position;
		protected readonly Vector2D size;
		protected readonly float mass;
		public float frictionOnCollision;

		public void Update(float deltaTime)
		{
			velocity += (force / mass) * deltaTime; //m/s*s
			position += velocity * deltaTime; //m/s
		}

		protected Vector2D force;
		protected Vector2D acceleration;

		public void HandleGroundAndSideWallsCollision()
		{
			if (IsCollidingWithGround)
			{
				position.y = World.GroundHeight + size.y / 2;
				velocity.y = -velocity.y * (1 - frictionOnCollision);
			}
			if (IsCollidingWithLeftSide)
			{
				position.x = LeftSide + size.x / 2;
				velocity.x = -velocity.x * (1 - frictionOnCollision);
			}
			if (IsCollidingWithRightSide)
			{
				position.x = RightSide - size.x / 2;
				velocity.x = -velocity.x * (1 - frictionOnCollision);
			}
		}

		public bool IsCollidingWithGround
		{
			get
			{
				return position.y - size.y / 2 < World.GroundHeight;
			}
		}

		public bool IsCollidingWithLeftSide
		{
			get
			{
				return position.x - size.x / 2 < LeftSide;
			}
		}
		private const float LeftSide = -0.5f;

		public bool IsCollidingWithRightSide
		{
			get
			{
				return position.x + size.x / 2 > RightSide;
			}
		}
		private const float RightSide = 0.5f;

		public void HandleCollision(PhysicsUpdatable other)
		{
			var otherEntity = (Entity)other;
			if (IsCollidingWith(otherEntity))
			{
				var distanceVector = position - otherEntity.position;
				var reflection = velocity.MirrorAtNormal(distanceVector);
				var otherReflection = otherEntity.velocity.MirrorAtNormal(distanceVector);
				velocity = reflection;
				otherEntity.velocity = otherReflection;
			}
		}

		private bool IsCollidingWith(Entity other)
		{
			return position.x + size.x / 2 > other.position.x - other.size.x / 2 &&
				position.x - size.x / 2 < other.position.x + other.size.x / 2 &&
				position.y + size.y / 2 > other.position.y - other.size.y / 2 &&
				position.y - size.y / 2 < other.position.y + other.size.y / 2;
		}

		private Vector2D velocity = new Vector2D();

		public abstract void Draw();
	}
}