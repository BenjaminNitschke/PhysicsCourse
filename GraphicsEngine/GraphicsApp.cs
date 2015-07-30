using System.Diagnostics;
using System.Windows.Forms;

namespace GraphicsEngine
{
	/// <summary>
	/// Basic application each app can derive from, updates and draws each frame until closed
	/// </summary>
	public abstract class GraphicsApp
	{
		protected GraphicsApp(string name)
		{
			this.name = name;
		}

		private readonly string name;

		public void Run(RenderMode renderMode)
		{
			Init();
			graphics.Init(renderMode);
			while (true)
			{
				Application.DoEvents();
				if (form.IsDisposed)
					break;
				graphics.Render();
				UpdateTime();
				Update();
        Entities.UpdateAll(TimeDeltaInSeconds);
				Entities.DrawAll();
				graphics.Present();
			}
			graphics.Dispose();
		}

		private void UpdateTime()
		{
			var newTime = timer.ElapsedTicks / (float)Stopwatch.Frequency;
			TimeDeltaInSeconds = newTime - currentTime;
			currentTime = newTime;
		}

		private float currentTime;
		protected virtual void Update() {}

		protected virtual void Init()
		{
			var common = new Common();
			form = new GraphicsEngineForm { Text = name };
			form.Show();
			graphics = new OpenGLGraphics(form, common);
			timer = new Stopwatch();
			timer.Start();
		}

		protected GraphicsEngineForm form;
		private OpenGLGraphics graphics;
		private Stopwatch timer;
		public float TimeDeltaInSeconds { get; private set; }
	}
}