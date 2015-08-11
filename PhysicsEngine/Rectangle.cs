using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace PhysicsEngine
{
	public class Rectangle : Entity
	{
		public Rectangle(Vector2D position, Vector2D size, Color4 color, float mass)
			: base(position, size, mass)
		{
			this.color = color;
		}

		private readonly Color4 color;

		public override void Draw()
		{
			GL.Begin(PrimitiveType.Quads);
			GL.Color4(color);
			GL.Vertex2(position.x - size.x / 2, position.y - size.y / 2);
			GL.Vertex2(position.x - size.x / 2, position.y + size.y / 2);
			GL.Vertex2(position.x + size.x / 2, position.y + size.y / 2);
			GL.Vertex2(position.x + size.x / 2, position.y - size.y / 2);
			GL.End();
		}
	}
}