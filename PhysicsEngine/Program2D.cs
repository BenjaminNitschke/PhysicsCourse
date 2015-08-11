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
	public class Program2D
	{
		private static void Main()
		{
			using (window = new GameWindow(1024, 600, GraphicsMode.Default, "PhysicsEngine"))
			{
				CreateRectangles();
				window.Resize += Resize;
				window.UpdateFrame += Update;
				window.RenderFrame += Draw;
				window.Run();
			}
		}

		private static void CreateRectangles()
		{
			for (float y = 0.3f; y >= 0.0f; y -= 0.1f)
				for (float x = -0.475f; x <= 0.475f; x += 0.03f)
					new Rectangle(new Vector2D(x, y), new Vector2D(0.025f, 0.025f), Color4.White, 0.1f);
			new Rectangle(new Vector2D(0, 0.0f), new Vector2D(0.075f, 0.075f), Color4.Orange, 0.3f);
		}

		private static void Resize(object sender, EventArgs e)
		{
			window.VSync = VSyncMode.Off;
			GL.Viewport(0, 0, window.Width, window.Height);
			GL.MatrixMode(MatrixMode.Projection);
			World.aspectRatio = (float)window.Height / window.Width;
			var matrix = Matrix4.CreateOrthographic(1, World.aspectRatio, 0, 1);
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
			titleUpdateTime += (float)e.Time;
      if (titleUpdateTime > 1.0f)
			{
				titleUpdateTime -= 1.0f;
				window.Title = "PhysicsEngine Fps: " + (int)(1 / e.Time);
			}
		}

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