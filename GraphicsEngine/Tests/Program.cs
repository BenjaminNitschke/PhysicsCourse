using System.Collections.Generic;
using GraphicsEngine.Datatypes;
using GraphicsEngine.Meshes;
using Jitter.Dynamics;
using Jitter.Dynamics.Joints;
using Jitter.LinearMath;

namespace GraphicsEngine.Tests
{
	public class Program : GraphicsApp
	{
		private Cube[,] cubes;
		public Program() : base("Physics3D Tests") {}

		public static void Main()
		{
			new Program().Run(RenderMode.Render3D);
		}

		protected override void Init()
		{
			base.Init();
			var cubeTexture = new Texture("BoxDiffuse.jpg");
			cubes = new Cube[PyramidHeight, PyramidHeight];
			for (int z=0; z<PyramidHeight; z++)
				for (int x=0; x<PyramidHeight; x++)
					cubes[x, z] = new Cube(cubeTexture, new Vector3D(
						1.1f*(x-(PyramidHeight)/2.0f), 0, 1.1f*z+2.55f));
			for (int z = 0; z < PyramidHeight; z++)
				for (int x = 0; x < PyramidHeight - 1; x++)
				{
          var horizontalJoint = new PrismaticJoint(Entities.world3D,
						cubes[x, z].body, cubes[x+1, z].body, 1.1f, 1.1f);
					horizontalJoint.FixedAngleConstraint.Softness = 1;
					horizontalJoint.Activate();
					if (z < PyramidHeight - 1)
					{
						var verticalJoint = new PrismaticJoint(Entities.world3D, cubes[x, z].body,
							cubes[x, z + 1].body, 1.1f, 1.1f);
						verticalJoint.FixedAngleConstraint.Softness = 1;
						verticalJoint.Activate();
					}
				}
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