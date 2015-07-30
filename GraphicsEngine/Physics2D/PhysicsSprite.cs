using FarseerPhysics.Dynamics;
using GraphicsEngine.Datatypes;

namespace GraphicsEngine.Physics2D
{
	public abstract class PhysicsSprite : PhysicsObject, Drawable
	{
		protected PhysicsSprite(Sprite sprite, Vector2D position, Body body)
            : base(position, sprite.Size, body)
		{
			this.sprite = sprite;
		}

		private readonly Sprite sprite;

		public void Draw()
		{
			sprite.Draw(position);
		}
	}
}