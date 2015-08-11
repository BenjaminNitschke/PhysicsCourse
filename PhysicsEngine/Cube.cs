using OpenTK.Graphics.OpenGL;

namespace PhysicsEngine
{
	public class Cube : Drawable
	{
		public Cube(Texture texture, Vector3D position)
		{
			this.texture = texture;
			this.position = position;
			World.Add(this);
			// Create 3D Cube from Vertices and Indices
			vertices = new Vector3D[NumberOfVertices] // 6*4 = 24 edge points
			{
				new Vector3D(1, 1, 1), new Vector3D(-1, 1, 1),
				new Vector3D(-1, -1, 1), new Vector3D(1, -1, 1), // v0,v1,v2,v3 (front)
				new Vector3D(1, 1, 1), new Vector3D(1, -1, 1),
				new Vector3D(1, -1, -1), new Vector3D(1, 1, -1), // v0,v3,v4,v5 (right)
				new Vector3D(1, 1, 1), new Vector3D(1, 1, -1),
				new Vector3D(-1, 1, -1), new Vector3D(-1, 1, 1), // v0,v5,v6,v1 (top)
				new Vector3D(-1, 1, 1), new Vector3D(-1, 1, -1),
				new Vector3D(-1, -1, -1), new Vector3D(-1, -1, 1), // v1,v6,v7,v2 (left)
				new Vector3D(-1, -1, -1), new Vector3D(1, -1, -1),
				new Vector3D(1, -1, 1), new Vector3D(-1, -1, 1), // v7,v4,v3,v2 (bottom)
				new Vector3D(1, -1, -1), new Vector3D(-1, -1, -1),
				new Vector3D(-1, 1, -1), new Vector3D(1, 1, -1) // v4,v7,v6,v5 (back)
			};
			for (int i = 0; i < vertices.Length; i++)
				vertices[i] = vertices[i] / 2;
			normals = new Vector3D[NumberOfVertices];
			SetNormals(0, new Vector3D(0, 0, 1));
			SetNormals(1, new Vector3D(1, 0, 0));
			SetNormals(2, new Vector3D(0, 1, 0));
			SetNormals(3, new Vector3D(-1, 0, 0));
			SetNormals(4, new Vector3D(0, -1, 0));
			SetNormals(5, new Vector3D(0, 0, -1));
			uvs = new Vector2D[NumberOfVertices]
			{
				new Vector2D(0, 0), new Vector2D(1, 0), new Vector2D(1, 1), new Vector2D(0, 1),
				new Vector2D(0, 0), new Vector2D(1, 0), new Vector2D(1, 1), new Vector2D(0, 1),
				new Vector2D(0, 0), new Vector2D(1, 0), new Vector2D(1, 1), new Vector2D(0, 1),
				new Vector2D(0, 0), new Vector2D(1, 0), new Vector2D(1, 1), new Vector2D(0, 1),
				new Vector2D(0, 0), new Vector2D(1, 0), new Vector2D(1, 1), new Vector2D(0, 1),
				new Vector2D(0, 0), new Vector2D(1, 0), new Vector2D(1, 1), new Vector2D(0, 1)
			};
			indices = new short[NumberOfIndices]
			{ // Front face
				0, 2, 1, 2, 0, 3,
				// Right face
				4, 6, 5, 6, 4, 7,
				// Top face
				8, 10, 9, 10, 8, 11,
				// Left face
				12, 14, 13, 14, 12, 15,
				// Bottom face
				16, 18, 17, 18, 16, 19,
				// Back face
				20, 22, 21, 22, 20, 23
			};
		}

		private readonly Texture texture;
		private readonly Vector3D position;

		private void SetNormals(int face, Vector3D normal)
		{
			for (int i = face * 4; i < face * 4 + 4; i++)
				normals[i] = normal;
		}
		
		private Vector3D[] vertices;
		private Vector3D[] normals;
		private const int NumberOfFaces = 6;
		private const int NumberOfVertices = NumberOfFaces * 4;
		private const int NumberOfIndices = NumberOfFaces * 2 * 3;
		private short[] indices;
		private Vector2D[] uvs;

		public void Draw()
		{
			//Matrix4 renderMatrix = JitterDatatypes.ToMatrix4(body.Orientation, body.Position);
			//var modelView = renderMatrix * Common.ViewMatrix;
			//GL.LoadMatrix(ref modelView);
			GL.LoadMatrix(ref World.cameraMatrix);
			GL.Translate(position.x, position.y, position.z);
			GL.Enable(EnableCap.Texture2D);
			GL.BindTexture(TextureTarget.Texture2D, texture.Handle);
			GL.EnableClientState(ArrayCap.NormalArray);
			GL.NormalPointer(NormalPointerType.Float, 0, normals);
			GL.EnableClientState(ArrayCap.TextureCoordArray);
			GL.TexCoordPointer(2, TexCoordPointerType.Float, 0, uvs);
			GL.EnableClientState(ArrayCap.VertexArray);
			GL.VertexPointer(3, VertexPointerType.Float, 0, vertices);
			GL.DrawElements(PrimitiveType.Triangles, NumberOfIndices,
				DrawElementsType.UnsignedShort, indices);
		}
	}
}