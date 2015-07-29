using GraphicsEngine.Datatypes;
using OpenTK.Graphics.OpenGL;

namespace GraphicsEngine
{
	public class Sprite
	{
		public Sprite(Texture texture, Vector2D size)
		{
			this.texture = texture;
			Size = size;
			GL.Enable(EnableCap.Blend);
			GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
			GL.Enable(EnableCap.Texture2D);
		}

		private readonly Texture texture;
		public Vector2D Size { get; }

		private float[] vertices;
		private readonly float[] uvs = { 0.0f, 0.0f, 0.0f, 1.0f, 1.0f, 1.0f, 1.0f, 0.0f };
		private readonly short[] indices = { 0, 1, 2, 0, 2, 3 };

		public void Draw()
		{
			Draw(Vector2D.Zero);
		}

		public void Draw(Vector2D position)
		{
			GL.BindTexture(TextureTarget.Texture2D, texture.Handle);
			var halfSize = Size / 2;
			vertices = new[]
			{
				position.x - halfSize.x, position.y + halfSize.y, 0,
				position.x - halfSize.x, position.y - halfSize.y, 0,
				position.x + halfSize.x, position.y - halfSize.y, 0,
				position.x + halfSize.x, position.y + halfSize.y, 0
			};
			GL.EnableClientState(ArrayCap.TextureCoordArray);
			GL.TexCoordPointer(2, TexCoordPointerType.Float, 0, uvs);
			GL.EnableClientState(ArrayCap.VertexArray);
			GL.VertexPointer(3, VertexPointerType.Float, 0, vertices);
			GL.DrawElements(PrimitiveType.Triangles, indices.Length, DrawElementsType.UnsignedShort,
				indices);
		}
	}
}