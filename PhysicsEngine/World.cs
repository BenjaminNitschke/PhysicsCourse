using System.Collections.Generic;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;

namespace PhysicsEngine
{
	public static class World
	{
		static World()
		{
			world2D = new FarseerPhysics.Dynamics.World(Gravity2D);
			var ground = BodyFactory.CreateRectangle(world2D, 100, 1f, 10, new Vector2(0, GroundHeight-0.05f)*Entity.ToPhysicsSize);
			ground.BodyType = BodyType.Static;
			var leftSide = BodyFactory.CreateRectangle(world2D, 1f, 10, 10, new Vector2(-0.55f, 0) * Entity.ToPhysicsSize);
			leftSide.BodyType = BodyType.Static;
			var rightSide = BodyFactory.CreateRectangle(world2D, 1f, 10, 10, new Vector2(0.55f, 0) * Entity.ToPhysicsSize);
			rightSide.BodyType = BodyType.Static;
		}

		internal static FarseerPhysics.Dynamics.World world2D;
    public static readonly Vector2D Gravity2D = new Vector2D(0, -9.81f); //m/s2
		public static float aspectRatio = 600.0f / 1024;
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
			world2D.Step(deltaTime);
		}

		public static void Draw()
		{
			foreach (var entity in drawables)
				entity.Draw();
		}
	}
}