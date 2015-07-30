using GraphicsEngine;
using GraphicsEngine.Datatypes;
using GraphicsEngine.Physics2D;

namespace Pong
{
	public class Ball : PhysicsSprite
	{
		public Ball(Sprite sprite) : base(sprite, Vector2D.Zero)
		{
			Entities.Register(this);
		}
	}
}