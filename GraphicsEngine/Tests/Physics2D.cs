using GraphicsEngine.Datatypes;
using GraphicsEngine.Shapes;

namespace GraphicsEngine.Tests
{
	public class Physics2D : GraphicsApp
	{
		public Physics2D() : base("Physics2D Tests") {}

		public static void Main()
		{
			new Physics2D().Run(RenderMode.Render2D);
		}

		protected override void Init()
		{
			base.Init();
      for (var i = 0; i < 50; i++) //warning gets really slow with 1000
				new Circle(new Vector2D(-1+i/20.0f, 0.4f+i*0.004f), 0.07f);
			/* simple box and circle collision test
			var box = new Box(Vector2D.Zero, new Size(0.4f, 0.4f));
			box.velocity = new Vector2D(0.5f, 0.2f);
			//box.collisionFriction = 0.6f;
			var circle = new Circle(new Vector2D(0.5f, 0), 0.2f);
			circle.velocity = new Vector2D(-0.1f, -0.3f);
      */
		}
	}
}
