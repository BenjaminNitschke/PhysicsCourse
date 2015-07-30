using GraphicsEngine;
using GraphicsEngine.Datatypes;
using GraphicsEngine.Physics2D;

namespace Pong
{
	public class Paddle : PhysicsSprite
	{
		public Paddle(Sprite sprite, Vector2D position) : base(sprite, position)
		{
			Entities.Register(this);
		}
	}
}