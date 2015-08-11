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
		}

		protected Vector2D position;
		protected readonly Vector2D size;
		protected readonly float mass;

		public void Update(float deltaTime)
		{
			velocity += (force / mass) * deltaTime; //m/s*s
			position += velocity * deltaTime; //m/s
		}

		protected Vector2D force;
		protected Vector2D acceleration;

		public bool IsCollidingWithGround
		{
			get
			{
				return position.y - size.y / 2 < World.GroundHeight;
			}
		}

		public void HandleGroundCollision()
		{
			position.y = World.GroundHeight + size.y / 2;
			velocity.y = -velocity.y;
		}

		public void HandleCollision(PhysicsUpdatable other)
		{
			var otherEntity = (Entity)other;
			if ((position - otherEntity.position).Length <
				size.Length / 2 + otherEntity.size.Length / 2)
			{
				var distanceVector = position - otherEntity.position;
				var normal = distanceVector.Rotate(90); // 1, 0
				velocity.y = -velocity.y;
				otherEntity.velocity.y = -otherEntity.velocity.y;
				//otherEntity.f
				//Vector2D otherForce = otherEntity.mass * otherEntity.acceleration;
				//Handle collision forces
				//velocity = new Vector2D();
				//otherEntity.velocity = new Vector2D();
			}
		}

		private Vector2D velocity = new Vector2D();
		//Mass
		//Force, Acceleration
		public abstract void Draw();
	}
}