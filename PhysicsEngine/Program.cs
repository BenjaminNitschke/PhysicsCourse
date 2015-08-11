using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace PhysicsEngine
{
	public class Program
	{
		private static void Main()
		{
			using (window = new GameWindow(1024, 600, GraphicsMode.Default, "PhysicsEngine"))
			{
				window.Load += Initialize;
				window.Resize += Resize;
				window.UpdateFrame += Update;
				window.RenderFrame += Draw;
				window.Run();
			}
		}

		private static void Initialize(object sender, EventArgs e)
		{
			InitializeCamera();
			InitializeLight();
			CreatePyramid();
		}

		private static void InitializeCamera()
		{
			GL.MatrixMode(MatrixMode.Modelview);
			World.cameraMatrix = Matrix4.LookAt(new Vector3(0, -12, 4), new Vector3(0, 0, 5.5f),
				Vector3.UnitZ);
			GL.LoadMatrix(ref World.cameraMatrix);
			GL.Enable(EnableCap.DepthTest);
		}

		private static void InitializeLight()
		{
			GL.ShadeModel(ShadingModel.Smooth);
			GL.Enable(EnableCap.Lighting);
			GL.Light(LightName.Light0, LightParameter.Ambient, new[] { .2f, .2f, .2f, 1.0f });
			GL.Light(LightName.Light0, LightParameter.Diffuse, new[] { 1, 1, 1, 1.0f });
			GL.Light(LightName.Light0, LightParameter.Position, new[] { 0.3f, -0.5f, 0.4f });
			GL.Enable(EnableCap.Light0);
		}

		private static void CreatePyramid()
		{
			var cubeTexture = new Texture("BoxDiffuse.jpg");
			for (int z = 0; z < PyramidHeight; z++)
				for (int x = 0; x < PyramidHeight - z; x++)
					new Cube(cubeTexture, new Vector3D(
						1.1f * (x - (PyramidHeight - z) / 2.0f), 0, 1.2f * z + 0.55f));
		}

		private const int PyramidHeight = 12;

		private static void Resize(object sender, EventArgs e)
		{
			window.VSync = VSyncMode.Off;
			GL.Viewport(0, 0, window.Width, window.Height);
			SetProjectionMatrix();
		}

		private static void SetProjectionMatrix()
		{
			GL.MatrixMode(MatrixMode.Projection);
			World.aspectRatio = window.Width / (float)window.Height;
			var matrix = Matrix4.CreatePerspectiveFieldOfView(
				Vector2D.DegreesToRadians(75), World.aspectRatio, 0.1f, 100);
			GL.LoadMatrix(ref matrix);
			GL.MatrixMode(MatrixMode.Modelview);
		}

		private static GameWindow window;

		private static void Update(object sender, FrameEventArgs e)
		{
			physicsTimeAccumulator += (float)e.Time;
			while (physicsTimeAccumulator > PhysicsTimeStep)
			{
				physicsTimeAccumulator -= PhysicsTimeStep;
				World.Update(PhysicsTimeStep);
			}
			if (window.Keyboard[Key.Escape])
				window.Exit();
			if (window.Mouse[MouseButton.Left])
			{
				if (startClickX == -1)
				{
					startClickX = window.Mouse.X;
					startClickY = window.Mouse.Y;
				}
				else
				{
					World.cameraMatrix = Matrix4.LookAt(new Vector3(
						-10.0f + 30.0f*(window.Mouse.X - startClickX)/window.Width,
						-6.0f + 30.0f * (window.Mouse.Y - startClickY)/window.Height, 4),
						new Vector3(0, 0, 5.5f),
						Vector3.UnitZ);
        }
			}
			else
			{
				startClickX = -1;
				startClickY = -1;
			}

			titleUpdateTime += (float)e.Time;
      if (titleUpdateTime > 1.0f)
			{
				titleUpdateTime -= 1.0f;
				window.Title = "PhysicsEngine Fps: " + (int)(1 / e.Time);
			}
		}

		private static int startClickX = -1;
		private static int startClickY = -1;
		private static float physicsTimeAccumulator = 0;
    private const float PhysicsTimeStep = 1 / 60.0f;
		private static float titleUpdateTime;

		private static void Draw(object sender, FrameEventArgs frameEventArgs)
		{
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
			World.Draw();
			window.SwapBuffers();
		}
	}
}