using FarseerPhysics.Dynamics;
using GraphicsEngine.Datatypes;
using Microsoft.Xna.Framework;

namespace GraphicsEngine.Physics2D
{
	public abstract class PhysicsObject : Updatable
	{
		protected PhysicsObject(Vector2D position, Size size, Body body)
		{
			this.size = size;
			this.body = body;
		}

		public Vector2D position
		{
			get { return (Vector2D)body.Position; }
			set { body.Position = value; }
		}
		public readonly Size size; //m
		protected Body body;


		public Vector2D velocity
		{
			get { return (Vector2D)body.LinearVelocity; }
			set
			{
				body.LinearVelocity = Vector2.Zero;
				body.AngularVelocity = 0;
				body.Inertia = 0;
				body.ApplyLinearImpulse(value * 0.01f);
			}
		}

		public void Update(float timeDeltaInSeconds) {}
	}
}