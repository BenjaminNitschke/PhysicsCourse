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
		public float collisionFriction = 0.1f; // 0=no friction, 100% bounce, 1=no bounce

		public void Update(float timeDeltaInSeconds)
		{
			velocity += acceleration * timeDeltaInSeconds;
			position += velocity * timeDeltaInSeconds;
			if (position.y - size.Height / 2 < GroundPlane.Height)
			{
				position.y = GroundPlane.Height + size.Height / 2;
				velocity *= (1.0f - collisionFriction);
				velocity.y = -velocity.y;
			}
		}
	}
}