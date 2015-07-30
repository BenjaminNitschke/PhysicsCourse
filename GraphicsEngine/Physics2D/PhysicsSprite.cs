using GraphicsEngine.Datatypes;

namespace GraphicsEngine.Physics2D
{
	public abstract class PhysicsSprite : PhysicsObject, Drawable
	{
		protected PhysicsSprite(Sprite sprite, Vector2D position) : base(position, sprite.Size)
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