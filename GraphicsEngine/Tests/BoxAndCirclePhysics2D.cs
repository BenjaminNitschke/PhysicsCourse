using GraphicsEngine.Datatypes;
using GraphicsEngine.Shapes;

namespace GraphicsEngine.Tests
{
	public class BoxAndCirclePhysics2D : GraphicsApp
	{
		public BoxAndCirclePhysics2D() : base("BoxAndCirclePhysics2D Tests") {}

		public static void Main()
		{
			new BoxAndCirclePhysics2D().Run(RenderMode.Render2D);
		}

		protected override void Init()
		{
			base.Init();
			var box = new Box(Vector2D.Zero, new Size(0.4f, 0.4f));
			box.velocity = new Vector2D(0.5f, 0.2f)*10;
			var circle = new Circle(new Vector2D(0.5f, 0), 0.2f);
			circle.velocity = new Vector2D(-1f, -0.3f)*10;
		}
	}
}
