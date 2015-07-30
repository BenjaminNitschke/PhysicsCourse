using System.Diagnostics;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Platform;

namespace GraphicsEngine
{
	public class OpenGLGraphics : Graphics
	{
		public OpenGLGraphics(Form form, Common common)
		{
			this.form = form;
			this.common = common;
			windowInfo = Utilities.CreateWindowsWindowInfo(form.Handle);
			context = new GraphicsContext(GraphicsMode.Default, windowInfo);
			context.MakeCurrent(windowInfo);
			context.LoadAll();
		}

		public void Init(RenderMode renderMode)
		{
			if (renderMode == RenderMode.Render2D)
			{
				GL.MatrixMode(MatrixMode.Projection);
				var aspectRatio = (float)form.ClientSize.Width / form.ClientSize.Height;
				GL.LoadIdentity();
				GL.Scale(1, aspectRatio, 1);
				GL.MatrixMode(MatrixMode.Modelview);
			}
			else
			{
				GL.Enable(EnableCap.DepthTest);
				GL.ShadeModel(ShadingModel.Smooth);
				GL.Enable(EnableCap.Lighting);
				GL.Light(LightName.Light0, LightParameter.Ambient, new[] { .2f, .2f, .2f, 1.0f });
				GL.Light(LightName.Light0, LightParameter.Diffuse, new[] { 1, 1, 1, 1.0f });
				GL.Light(LightName.Light0, LightParameter.Position,
					new[] { Common.LightPosition.x, Common.LightPosition.y, Common.LightPosition.z });
				GL.Enable(EnableCap.Light0);
				GL.MatrixMode(MatrixMode.Projection);
				var prespective = Matrix4.CreatePerspectiveFieldOfView(Common.FieldOfView,
					form.ClientSize.Width / (float)form.ClientSize.Height, Common.NearPlane, Common.FarPlane);
				GL.LoadMatrix(ref prespective);
				GL.MatrixMode(MatrixMode.Modelview);
				Common.ViewMatrix = Matrix4.LookAt(Common.CameraPosition.x, Common.CameraPosition.y,
					Common.CameraPosition.z, 0, 0, 0, 0, 0, 1);
				GL.LoadMatrix(ref Common.ViewMatrix);
			}
		}

		private readonly Form form;
		private readonly Common common;

		public void Render()
		{
			GL.Viewport(0, 0, form.ClientSize.Width, form.ClientSize.Height);
			var color = common.BackgroundColor;
			GL.ClearColor(color.R / 255.0f, color.G / 255.0f, color.B / 255.0f, 1);
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
		}

		public void Present()
		{
			context.MakeCurrent(windowInfo);
			context.SwapBuffers();
		}

		public GraphicsContext context;
		private readonly IWindowInfo windowInfo;

		public void Dispose()
		{
			context.Dispose();
		}
	}
}