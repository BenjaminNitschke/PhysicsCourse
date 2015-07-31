using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using GraphicsEngine;
using GraphicsEngine.Datatypes;
using GraphicsEngine.Physics2D;
using Microsoft.Xna.Framework;

namespace Pong
{
	public class Ball : PhysicsSprite
	{
		public Ball(Sprite sprite) : base(sprite, Vector2D.Zero,
			BodyFactory.CreateCircle(Entities.world2D, sprite.Size.Width / 2, 1.0f,
				Vector2D.Zero, BodyType.Dynamic))
		{
			Entities.Register(this);
			body.IsStatic = false;
			body.Restitution = 1;
			body.Friction = 0.01f;
		}
	}
}