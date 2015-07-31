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
			for (int z=0; z<PyramidHeight; z++)
				for (int x=0; x<PyramidHeight-z; x++)
					new Cube(cubeTexture, new Vector3D(
						1.2f*(x-(PyramidHeight-z)/2.0f), 0, 1.5f*z+0.55f));
		}

		private const int PyramidHeight = 8;
	}
}