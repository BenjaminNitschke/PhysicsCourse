using System.Collections.Generic;
using System.Linq;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
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
			world2D = new FarseerPhysics.Dynamics.World(Gravity2D);
			var ground = BodyFactory.CreateRectangle(world2D, 100, 1f, 10, new Vector2(0, GroundHeight2D-0.05f)*Entity2D.ToPhysicsSize);
			ground.BodyType = BodyType.Static;
			var leftSide = BodyFactory.CreateRectangle(world2D, 1f, 10, 10, new Vector2(-0.55f, 0) * Entity2D.ToPhysicsSize);
			leftSide.BodyType = BodyType.Static;
			var rightSide = BodyFactory.CreateRectangle(world2D, 1f, 10, 10, new Vector2(0.55f, 0) * Entity2D.ToPhysicsSize);
			rightSide.BodyType = BodyType.Static;
		}

		internal static FarseerPhysics.Dynamics.World world2D;
    public static readonly Vector2D Gravity2D = new Vector2D(0, -9.81f); //m/s2
		public static readonly Vector3D Gravity3D = new Vector3D(0, 0, -9.81f); //m/s2
		public static float aspectRatio = 600.0f / 1024;
		public static float GroundHeight2D { get { return -aspectRatio / 2.0f; } }

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
			foreach (var entity3D in updateables.OfType<Entity3D>())
				entity3D.Update(deltaTime);
			for (var i = 0; i < updateables.Count; i++)
				for (var j = i + 1; j < updateables.Count; j++)
					if (updateables[i] is Entity3D && updateables[j] is Entity3D)
						((Entity3D)updateables[i]).HandleCollision((Entity3D)updateables[j]);
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

		public static Entity3D GetEntity3DAt(Vector2D screenWorldPosition,
			out Vector3D direction)
		{
			var ray = Raycast(screenWorldPosition);
			direction = ray;
			foreach (var entity3D in updateables.OfType<Entity3D>())
				if (entity3D.DoesRayHit(ray))
					return entity3D;
			return null;
		}

		private static Vector3D Raycast(Vector2D screenWorldPosition)
		{
			var invertedProjection = Matrix.Invert(ToXnaMatrix(World.projectionMatrix));
			var result = Microsoft.Xna.Framework.Vector3.TransformNormal(
				new Microsoft.Xna.Framework.Vector3(screenWorldPosition.x, screenWorldPosition.y, 1), 
        invertedProjection);
			return //working test ray: new Vector3D(-0.02f, 1, 0.02f);
				new Vector3D(result.X, 1, result.Y);
		}

		private static Matrix ToXnaMatrix(Matrix4 m)
		{
			return new Matrix(
				m.M11, m.M12, m.M13, m.M14,
				m.M21, m.M22, m.M23, m.M24,
				m.M31, m.M32, m.M33, m.M34,
				m.M41, m.M42, m.M43, m.M44);
		}
	}
}