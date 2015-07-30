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
			var updatable = anything as Updatable;
			if (updatable != null)
				UpdatableList.Add(updatable);
			var drawable = anything as Drawable;
			if (drawable != null)
				DrawableList.Add(drawable);
		}

		private static readonly List<Updatable> UpdatableList = new List<Updatable>();
		private static readonly List<Drawable> DrawableList = new List<Drawable>();

		public static void UpdateAll(float timeDeltaInSeconds)
		{
			timeAccumulator += timeDeltaInSeconds;
			while (timeAccumulator > PhysicsUpdateTimeStep)
			{
				timeAccumulator -= PhysicsUpdateTimeStep;
        foreach (var updatable in UpdatableList)
					updatable.Update(PhysicsUpdateTimeStep);
			}
		}

		private static float timeAccumulator;
		private const float PhysicsUpdateTimeStep = 1 / 60.0f;

		public static void DrawAll()
		{
			foreach (var drawable in DrawableList)
				drawable.Draw();
		}
	}
}