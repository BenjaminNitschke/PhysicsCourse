using GraphicsEngine;
using GraphicsEngine.Datatypes;
using GraphicsEngine.Physics2D;

namespace Pong
{
	public class Ball : PhysicsSprite
	{
		public Ball(Sprite sprite) : base(sprite, Vector2D.Zero, null)//TODO
		{
			Entities.Register(this);
		}
	}
}