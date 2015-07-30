namespace GraphicsEngine.Datatypes
{
	public struct Size
	{
		public Size(float width, float height) : this()
		{
			Width = width;
			Height = height;
		}

		public float Width { get; private set; }
		public float Height { get; private set; }
	}
}
