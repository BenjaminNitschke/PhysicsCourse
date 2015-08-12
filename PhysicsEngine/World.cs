using System.Collections.Generic;
using System.Linq;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using OpenTK;
using PhysicsEngine.Physics3D;
using Vector2 = Microsoft.Xna.Framework.Vector2;

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
		public static Matrix4 cameraMatrix;

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
			//TODO: use ray
			direction = ray;
      return null;
		}

		private static Vector3D Raycast(Vector2D screenWorldPosition)
		{
			return new Vector3D(0, 1, 0);
		}
	}
}