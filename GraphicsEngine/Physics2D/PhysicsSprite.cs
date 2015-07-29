namespace GraphicsEngine.Physics2D
{
	public abstract class PhysicsSprite : PhysicsObject
	{
		protected PhysicsSprite(Sprite sprite)
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