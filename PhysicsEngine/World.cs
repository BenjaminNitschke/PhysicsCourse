using System.Collections.Generic;
using System.Linq;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Jitter.Collision;
using Jitter.Collision.Shapes;
using Jitter.Dynamics;
using Jitter.LinearMath;
using Microsoft.Xna.Framework;
using OpenTK;
using PhysicsEngine.Physics3D;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using Vector3 = OpenTK.Vector3;

namespace PhysicsEngine
{
	public static class World
	{
		static World()
		{
			InitializePhysicsWorld2D();
			InitializePhysicsWorld3D();
		}

		private static void InitializePhysicsWorld2D()
		{
			world2D = new FarseerPhysics.Dynamics.World(Gravity2D);
			var ground = BodyFactory.CreateRectangle(world2D, 100, 1f, 10,
				new Vector2(0, GroundHeight2D - 0.05f) * Entity2D.ToPhysicsSize);
			ground.BodyType = BodyType.Static;
			var leftSide = BodyFactory.CreateRectangle(world2D, 1f, 10, 10,
				new Vector2(-0.55f, 0) * Entity2D.ToPhysicsSize);
			leftSide.BodyType = BodyType.Static;
			var rightSide = BodyFactory.CreateRectangle(world2D, 1f, 10, 10,
				new Vector2(0.55f, 0) * Entity2D.ToPhysicsSize);
			rightSide.BodyType = BodyType.Static;
		}

		internal static FarseerPhysics.Dynamics.World world2D;
    public static readonly Vector2D Gravity2D = new Vector2D(0, -9.81f); //m/s2
		public static float aspectRatio = 600.0f / 1024;
		public static float GroundHeight2D
		{ get { return -aspectRatio / 2.0f; } }

		private static void InitializePhysicsWorld3D()
		{
			world3D = new Jitter.World(new CollisionSystemBrute());
			world3D.Gravity = new JVector(0, 0, Gravity3D.z);
			var groundPlane = new RigidBody(new BoxShape(100, 100, 0.1f));
			groundPlane.IsStatic = true;
			world3D.AddBody(groundPlane);
		}

		public static Jitter.World world3D;
		public static readonly Vector3D Gravity3D = new Vector3D(0, 0, -9.81f); //m/s2

		public static void Add(object anyDrawableOrUpdateable)
		{
			var drawableObject = anyDrawableOrUpdateable as Drawable;
			if (drawableObject != null)
				drawables.Add(drawableObject);
			var updatableObject = anyDrawableOrUpdateable as PhysicsUpdatable;
			if (updatableObject != null)
				updateables.Add(updatableObject);
		}

		public static readonly List<Drawable> drawables = new List<Drawable>();
		public static readonly List<PhysicsUpdatable> updateables = new List<PhysicsUpdatable>();
		public static Vector3D cameraPosition;
		public static Vector3D cameraTarget;
		public static Matrix4 cameraMatrix;
		public static Matrix4 projectionMatrix;

		public static void Update(float deltaTime)
		{
			world2D.Step(deltaTime);
			world3D.Step(deltaTime * 1.5f, true);
		}

		public static void Draw()
		{
			foreach (var entity in drawables)
				entity.Draw();
		}

		public static Entity2D GetEntity2DAt(Vector2D screenWorldPosition)
		{
			foreach (var entity2D in updateables.OfType<Entity2D>())
				if (entity2D.Contains(screenWorldPosition))
					return entity2D;
			return null;
		}

		public static RigidBody GetEntity3DAt(Vector2D screenWorldPosition,
			out JVector direction)
		{
			var screenPosition = new JVector(screenWorldPosition.x, 1, screenWorldPosition.y);
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
			world3D.CollisionSystem.Raycast(JitterMath.ToJVector(cameraPosition),
				rayDirection, null, out body, out normal, out fraction);
			if (body != null && !body.IsStatic)
			{
				direction = rayDirection;
				return body;
			}
			direction = new JVector();
			return null;
		}
	}
}