using System.Collections.Generic;

namespace PhysicsEngine
{
	public static class World
	{
		public static readonly Vector2D Gravity2D = new Vector2D(0, -9.81f); //m/s2
		public static float aspectRatio;
		public static float GroundHeight { get { return -aspectRatio / 2.0f; } }

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

		public static void Update(float deltaTime)
		{
			foreach (var entity in updateables)
				entity.Update(deltaTime);
			foreach (var entity in updateables)
				if (entity.IsCollidingWithGround)
					entity.HandleGroundCollision();
			for (var i = 0; i < updateables.Count; i++)
				for (var j = i + 1; j < updateables.Count; j++)
					updateables[i].HandleCollision(updateables[j]);
		}

		public static void Draw()
		{
			foreach (var entity in drawables)
				entity.Draw();
		}
	}
}