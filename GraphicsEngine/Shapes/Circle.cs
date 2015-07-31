using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using GraphicsEngine.Datatypes;
using GraphicsEngine.Physics2D;
using OpenTK.Graphics.OpenGL;

namespace GraphicsEngine.Shapes
{
	public class Circle : PhysicsObject, Drawable
	{
		public Circle(Vector2D position, float radius)
			: base(position, new Size(radius * 2, radius * 2),
            BodyFactory.CreateCircle(Entities.world2D, radius, 1.0f, position, BodyType.Dynamic))
		{
			this.radius = radius;
			Entities.Register(this);
			body.IsStatic = false;
			body.Restitution = 1;
			body.Friction = 0.01f;
		}

		private readonly float radius;
		private Vector2D[] vertices;
		private short[] indices;

		public void Draw()
		{
			GL.Disable(EnableCap.Texture2D);
			vertices = new Vector2D[1+NumberOfPoints];
			vertices[0] = position;
			for (int i = 1; i < vertices.Length; i++)
				vertices[i] = position + new Vector2D(radius, 0).Rotate(360 * i / (float)NumberOfPoints);
			GL.EnableClientState(ArrayCap.VertexArray);
			GL.VertexPointer(2, VertexPointerType.Float, 0, vertices);
			indices = new short[NumberOfPoints * 3];
			for (int i = 0; i < NumberOfPoints; i++)
			{
				indices[i * 3 + 0] = 0;
				indices[i * 3 + 1] = (short)(1 + i);
				indices[i * 3 + 2] = (short)(1 + (1 + i) % NumberOfPoints);
			}
			GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedShort,
				indices);
		}

		private const int NumberOfPoints = 36;
	}
}