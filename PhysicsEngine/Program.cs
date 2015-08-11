using System;
using System.Collections.Generic;
using System.Drawing;
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
			using (window = new GameWindow(640, 360, GraphicsMode.Default, "PhysicsEngine"))
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
			new Rectangle(new Vector2D(0, 0), new Vector2D(0.2f, 0.2f), Color4.Yellow, 40);
			for (float x = -0.5f; x <= -0.1f; x += 0.1f)
				new Rectangle(new Vector2D(x, 0.3f), new Vector2D(0.05f, 0.05f), Color4.Red, 2.5f);
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
			World.Update((float)e.Time);
			if (window.Keyboard[Key.Escape])
				window.Exit();
			titleUpdateTime += (float)e.Time;
      if (titleUpdateTime > 1.0f)
			{
				titleUpdateTime -= 1.0f;
				window.Title = "PhysicsEngine Fps: " + (int)(1 / e.Time);
			}
		}

		private static float titleUpdateTime;

		private static void Draw(object sender, FrameEventArgs frameEventArgs)
		{
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
			World.Draw();
			window.SwapBuffers();
		}
	}
}