using System;
using FarseerPhysics.Collision.Shapes;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;

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
			this.size = size;
			body = BodyFactory.CreateRectangle(World.world2D,
				size.x * ToPhysicsSize, size.y * ToPhysicsSize, 1,
				position * ToPhysicsSize);
			body.BodyType = BodyType.Dynamic;
			body.Mass = mass;
			body.Friction = 0.2f;
			body.Restitution = 0.98f;
			body.ApplyLinearImpulse(new Vector2(0.1f, 0.5f));
		}

		internal const float ToPhysicsSize = 10.0f;
		protected Vector2D position
		{
			get
			{
				return (Vector2D)body.Position / ToPhysicsSize;
			}
		}
		protected float rotation
		{
			get
			{
				return body.Rotation * 180 / (float)Math.PI;
			}
		}
		protected readonly Vector2D size;
		private readonly Body body;
		
		public abstract void Draw();
	}
}