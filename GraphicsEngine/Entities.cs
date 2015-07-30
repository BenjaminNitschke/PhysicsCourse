using System.Collections.Generic;
using GraphicsEngine.Datatypes;
using GraphicsEngine.Physics2D;

namespace GraphicsEngine
{
	public static class Entities
	{
		public static Vector2D Gravity = new Vector2D(0, -9.81f);

		public static void Register(object anything)
		{
			var physicsObject = anything as PhysicsObject;
			if (physicsObject != null)
				PhysicsObjects.Add(physicsObject);
			var drawable = anything as Drawable;
			if (drawable != null)
				Drawables.Add(drawable);
		}

		private static readonly List<PhysicsObject> PhysicsObjects = new List<PhysicsObject>();
		private static readonly List<Drawable> Drawables = new List<Drawable>();

		public static void UpdateAll(float timeDeltaInSeconds)
		{
			timeAccumulator += timeDeltaInSeconds;
			while (timeAccumulator > PhysicsUpdateTimeStep)
			{
				timeAccumulator -= PhysicsUpdateTimeStep;
				foreach (var updatable in PhysicsObjects)
					updatable.Update(PhysicsUpdateTimeStep);
				CollisionCheck();
			}
		}

		private static void CollisionCheck()
		{
			for (var i = 0; i < PhysicsObjects.Count; i++)
				for (var j = i; j < PhysicsObjects.Count; j++)
					if (i != j && IsColliding(i, j))
						OnCollision(i, j);
		}

		private static bool IsColliding(int index1, int index2)
		{
			var body1 = PhysicsObjects[index1];
			var body2 = PhysicsObjects[index2];
			return body1.IsColliding(body2);
		}

		private static void OnCollision(int index1, int index2)
		{
			PhysicsObjects[index1].CollisionHappenedWith(PhysicsObjects[index2]);
			PhysicsObjects[index2].CollisionHappenedWith(PhysicsObjects[index1]);
		}

		private static float timeAccumulator;
		private const float PhysicsUpdateTimeStep = 1 / 60.0f;

		public static void DrawAll()
		{
			foreach (var drawable in Drawables)
				drawable.Draw();
		}
	}
}