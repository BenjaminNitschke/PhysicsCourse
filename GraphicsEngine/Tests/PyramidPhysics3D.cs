using GraphicsEngine.Datatypes;
using GraphicsEngine.Meshes;
using Jitter.Dynamics;
using Jitter.LinearMath;

namespace GraphicsEngine.Tests
{
	public class PyramidPhysics3D : GraphicsApp
	{
		public PyramidPhysics3D() : base("PyramidPhysics3D Tests") {}

		public static void Main()
		{
			new PyramidPhysics3D().Run(RenderMode.Render3D);
		}

		protected override void Init()
		{
			base.Init();
			var cubeTexture = new Texture("BoxDiffuse.jpg");
			for (int z=0; z<PyramidHeight; z++)
				for (int x=0; x<PyramidHeight-z; x++)
					new Cube(cubeTexture, new Vector3D(
						1.1f*(x-(PyramidHeight-z)/2.0f), 0, 1.2f*z+0.55f));
		}

		private const int PyramidHeight = 12;

		protected override void Update()
		{
			base.Update();
			if (form.MouseClickedHappenend)
			{
				form.MouseClickedHappenend = false;
				var screenPosition = new JVector(
					-1f + 2* (form.MouseClickPosition.x / form.ClientSize.Width),
					1, -1f + 2 * ((form.MouseClickPosition.y / form.ClientSize.Height)));
				var projectionMatrix = Common.ProjectionMatrix;
				JMatrix jMatrix = new JMatrix(
					projectionMatrix.M11, projectionMatrix.M12, projectionMatrix.M13,
					projectionMatrix.M21, projectionMatrix.M22, projectionMatrix.M23,
					projectionMatrix.M31, projectionMatrix.M32, projectionMatrix.M33);
				JMatrix invertedJMatrix;
        JMatrix.Invert(ref jMatrix, out invertedJMatrix);
				var rayDirection = JVector.Transform(screenPosition, invertedJMatrix);
				RigidBody body;
				JVector normal;
				float fraction;
				Entities.world3D.CollisionSystem.Raycast(JitterDatatypes.ToJVector(Common.CameraPosition),
					rayDirection, null, out body, out normal, out fraction);
				if (body != null && !body.IsStatic)
					body.ApplyImpulse(rayDirection * 200);
			}
    }
	}
}