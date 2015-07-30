using GraphicsEngine.Datatypes;
using GraphicsEngine.Shapes;

namespace GraphicsEngine.Tests
{
	public class Program : GraphicsApp
	{
		public Program() : base("Physics Tests") {}

		public static void Main()
		{
			new Program().Run();
		}

		protected override void Init()
		{
			base.Init();
			var box = new Box(Vector2D.Zero, new Size(0.4f, 0.4f));
			box.velocity = new Vector2D(0.5f, 0.2f);
			box.collisionFriction = 0.6f;
			var circle = new Circle(new Vector2D(0.5f, 0), 0.2f);
			circle.velocity = new Vector2D(-0.1f, -0.3f);
		}
	}
}
