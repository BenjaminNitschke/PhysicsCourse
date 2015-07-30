using System;

namespace GraphicsEngine
{
	public interface Graphics : IDisposable
	{
		void Init(RenderMode renderMode);
		void Render();
		void Present();
	}
}