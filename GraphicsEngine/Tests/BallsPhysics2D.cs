using GraphicsEngine.Datatypes;
using GraphicsEngine.Shapes;

namespace GraphicsEngine.Tests
{
	public class BallsPhysics2D : GraphicsApp
	{
		public BallsPhysics2D() : base("BallsPhysics2D Tests") {}

		public static void Main()
		{
			new BallsPhysics2D().Run(RenderMode.Render2D);
		}

		protected override void Init()
		{
			base.Init();
      for (var i = 0; i < 40; i++) //warning gets really slow with 1000
				new Circle(new Vector2D(-1+i/20.0f, 0.4f+i*0.004f), 0.07f);
		}
	}
}
