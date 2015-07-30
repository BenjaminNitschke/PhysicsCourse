using GraphicsEngine.Datatypes;
using GraphicsEngine.Meshes;
using GraphicsEngine.Shapes;

namespace GraphicsEngine.Tests
{
	public class Program : GraphicsApp
	{
		public Program() : base("Physics3D Tests") {}

		public static void Main()
		{
			new Program().Run(RenderMode.Render3D);
		}

		protected override void Init()
		{
			base.Init();
			var cubeTexture = new Texture("BoxDiffuse.jpg");
			new Cube(cubeTexture, new Vector3D(0.5f, 0, 0.5f));
			new Cube(cubeTexture, new Vector3D(-0.5f, 0, 0.5f));
			new Cube(cubeTexture, new Vector3D(0, 0, 1.5f));
		}
	}
}