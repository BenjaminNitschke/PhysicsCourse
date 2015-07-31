using FarseerPhysics.Dynamics;
using GraphicsEngine.Datatypes;

namespace GraphicsEngine.Physics2D
{
	public abstract class PhysicsObject : Updatable
	{
		protected PhysicsObject(Vector2D position, Size size, Body body)
		{
			this.position = position;
			this.size = size;
			this.body = body;
		}

		public Vector2D position; //m
		public readonly Size size; //m
		protected Body body;

		public void Update(float timeDeltaInSeconds)
		{
			position = (Vector2D)body.Position;
		}
	}
}