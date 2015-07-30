using GraphicsEngine.Datatypes;
using GraphicsEngine.Shapes;

namespace GraphicsEngine.Physics2D
{
	public abstract class PhysicsObject : Updatable
	{
		protected PhysicsObject(Vector2D position, Size size)
		{
			this.position = position;
			this.size = size;
			acceleration = Entities.Gravity;
		}

		public Vector2D position; //m
		public readonly Size size; //m
		public Vector2D velocity; //m/s
		public Vector2D acceleration; //m/s2
		public float collisionFriction = 0.01f; // 0=no friction, 100% bounce, 1=no bounce

		public void Update(float timeDeltaInSeconds)
		{
			velocity += acceleration * timeDeltaInSeconds;
			position += velocity * timeDeltaInSeconds;
			if (position.y - size.Height / 2 < GroundAndSidePlanes.Bottom)
			{
				position.y = GroundAndSidePlanes.Bottom + size.Height / 2;
				velocity *= (1.0f - collisionFriction);
				velocity.y = -velocity.y;
			}
			if (position.x + size.Width / 2 > GroundAndSidePlanes.Right)
			{
				position.x = GroundAndSidePlanes.Right - size.Width / 2;
				velocity *= (1.0f - collisionFriction);
				velocity.x = -velocity.x;
			}
			if (position.x - size.Width / 2 < GroundAndSidePlanes.Left)
			{
				position.x = GroundAndSidePlanes.Left + size.Width / 2;
				velocity *= (1.0f - collisionFriction);
				velocity.x = -velocity.x;
			}
		}

		internal void CollisionHappenedWith(PhysicsObject other)
		{
			var betweenVector = position - other.position;
			var middle = position + betweenVector / 2;
			position = position * 0.9f + (middle + betweenVector.Normalize() * size.Width / 2) * 0.1f;
			velocity = velocity.MirrorAtNormal(betweenVector);
		}

		public bool IsColliding(PhysicsObject otherBody)
		{
			var left = position.x - size.Width / 2;
			var right = position.x + size.Width / 2;
			var top = position.y - size.Height / 2;
			var bottom = position.y + size.Height / 2;
			var otherLeft = otherBody.position.x - otherBody.size.Width / 2;
			var otherRight = otherBody.position.x + otherBody.size.Width / 2;
			var otherTop = otherBody.position.y - otherBody.size.Height / 2;
			var otherBottom = otherBody.position.y + otherBody.size.Height / 2;
			return right > otherLeft && bottom > otherTop && left < otherRight && top < otherBottom;
		}
	}
}