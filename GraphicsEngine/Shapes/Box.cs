using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using GraphicsEngine.Datatypes;
using GraphicsEngine.Physics2D;
using OpenTK.Graphics.OpenGL;

namespace GraphicsEngine.Shapes
{
	public class Box : PhysicsObject, Drawable
	{
		public Box(Vector2D position, Size size)
			: base(position, size,
				BodyFactory.CreateRectangle(Entities.world2D, size.Width, size.Height, 1.0f, position,
					BodyType.Dynamic))
		{
			Entities.Register(this);
			body.IsStatic = false;
			body.Restitution = 1;
			body.Friction = 0.01f;
		}

		private float[] vertices;
		private readonly short[] indices = { 0, 1, 2, 0, 2, 3 };

		public void Draw()
		{
			GL.Disable(EnableCap.Texture2D);
			var halfWidth = size.Width / 2;
			var halfHeight = size.Height / 2;
			vertices = new[]
			{
				position.x - halfWidth, position.y + halfHeight, 0, position.x - halfWidth,
				position.y - halfHeight, 0, position.x + halfWidth, position.y - halfHeight, 0,
				position.x + halfWidth, position.y + halfHeight, 0
			};
			GL.EnableClientState(ArrayCap.VertexArray);
			GL.VertexPointer(3, VertexPointerType.Float, 0, vertices);
			GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedShort,
				indices);
		}
	}
}