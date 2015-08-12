using System;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;

namespace PhysicsEngine
{
	/// <summary>
	/// Provides common Physics Properties and the Update method for World
	/// </summary>
	public abstract class Entity2D : Drawable, PhysicsUpdatable
	{
		protected Entity2D(Vector2D position, Vector2D size, float mass)
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

		public bool Contains(Vector2D checkPosition)
		{
			// Box check (AABB)
			return position.x + size.x / 2 > checkPosition.x &&
				position.x - size.x / 2 < checkPosition.x &&
				position.y + size.y / 2 > checkPosition.y &&
				position.y - size.y / 2 < checkPosition.y;
		}

		public void AddForce(Vector2D addForce)
		{
			body.ApplyLinearImpulse(addForce);
		}
	}
}