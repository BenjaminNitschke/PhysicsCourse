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
			var leftTop = position + new Vector2D(-size.x / 2, -size.y / 2).Rotate(rotation);
			GL.Vertex2(leftTop.x, leftTop.y);
			var leftBottom = position + new Vector2D(-size.x / 2, size.y / 2).Rotate(rotation);
			GL.Vertex2(leftBottom.x , leftBottom.y);
			var rightBottom = position + new Vector2D(size.x / 2, size.y / 2).Rotate(rotation);
			GL.Vertex2(rightBottom.x, rightBottom.y);
			var rightTop = position + new Vector2D(size.x / 2, -size.y / 2).Rotate(rotation);
			GL.Vertex2(rightTop.x, rightTop.y);
			GL.End();
		}
	}
}