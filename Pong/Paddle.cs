using FarseerPhysics.Factories;
using GraphicsEngine;
using GraphicsEngine.Datatypes;
using GraphicsEngine.Physics2D;
using Microsoft.Xna.Framework;

namespace Pong
{
	public class Paddle : PhysicsSprite
	{
		public Paddle(Sprite sprite, Vector2D position) : base(sprite, position,
			BodyFactory.CreateCapsule(Entities.world2D, sprite.Size.Height,
				sprite.Size.Width / 2, 1.0f, position))
		{
			body.Position = position;
			Entities.Register(this);
		}

		public void IncreasePositionY(float increaseAmount)
		{
			body.Position = new Vector2(body.Position.X, body.Position.Y + increaseAmount);
		}
	}
}