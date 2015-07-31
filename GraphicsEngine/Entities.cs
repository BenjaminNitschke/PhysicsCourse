using System.Collections.Generic;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using GraphicsEngine.Meshes;
using GraphicsEngine.Physics2D;
using GraphicsEngine.Shapes;
using Jitter.Collision;
using Jitter.Collision.Shapes;
using Jitter.Dynamics;
using Jitter.LinearMath;
using Microsoft.Xna.Framework;

namespace GraphicsEngine
{
	public static class Entities
	{
		static Entities()
		{
			InitPhysics2D();
			InitPhysics3D();
		}

		private static void InitPhysics2D()
		{
			world2D = new World(new Vector2(0, -G));
			var groundPosition = new Vector2(0, GroundAndSidePlanes.Bottom);
			var ground = BodyFactory.CreateRectangle(world2D, 10, 0.1f, 1f, groundPosition);
			ground.IsStatic = true;
			ground.Restitution = 1;
			ground.Friction = 0.01f;
			var leftWall = BodyFactory.CreateRectangle(world2D, 0.1f, 4.0f, 1f,
				new Vector2(GroundAndSidePlanes.Left, 0));
			leftWall.IsStatic = true;
			leftWall.Restitution = 1;
			leftWall.Friction = 0.01f;
			var rightWall = BodyFactory.CreateRectangle(world2D, 0.1f, 4.0f, 1f,
				new Vector2(GroundAndSidePlanes.Right, 0));
			rightWall.IsStatic = true;
			rightWall.Restitution = 1;
			rightWall.Friction = 0.01f;
			var roof = BodyFactory.CreateRectangle(world2D, 10, 0.1f, 1f, -groundPosition);
			roof.IsStatic = true;
			roof.Restitution = 1;
			roof.Friction = 0.01f;
		}

		public static World world2D;
		private const float G = 9.81f;

		private static void InitPhysics3D()
		{
			world3D = new Jitter.World(new CollisionSystemBrute());
			world3D.Gravity = new JVector(0, 0, -G);
			var groundPlaneBody = new RigidBody(new BoxShape(100, 100, 0.1f));
			groundPlaneBody.IsStatic = true;
      world3D.AddBody(groundPlaneBody);
		}

		public static Jitter.World world3D;

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
		
		public static void Unregister(object anything)
		{
			var physicsObject = anything as PhysicsObject;
			if (physicsObject != null)
				PhysicsObjects.Remove(physicsObject);
			var drawable = anything as Drawable;
			if (drawable != null)
				Drawables.Remove(drawable);
		}

		public static void UpdateAll(float timeDeltaInSeconds)
		{
			timeAccumulator += timeDeltaInSeconds;
			while (timeAccumulator > PhysicsUpdateFixedTimeStep)
			{
				timeAccumulator -= PhysicsUpdateFixedTimeStep;
				world2D.Step(PhysicsUpdateFixedTimeStep);
				world3D.Step(PhysicsUpdateFixedTimeStep * 1.5f, true);
				foreach (var updatable in PhysicsObjects)
					updatable.Update(PhysicsUpdateFixedTimeStep);
				//CollisionCheck();
			}
		}

		private static float timeAccumulator;
		private const float PhysicsUpdateFixedTimeStep = 1 / 60.0f;

		/*
        private static void CollisionCheck()
        {
            for (int i = 0; i < PhysicsObjects.Count; i++)
                for (int j = i; j < PhysicsObjects.Count; j++)
                    if (i != j && IsColliding(i, j))
                        OnCollision(i, j);
        }

        private static bool IsColliding(int index1, int index2)
        {
            var object1 = PhysicsObjects[index1];
            var object2 = PhysicsObjects[index2];
            return object1.IsColliding(object2);
        }

        private static void OnCollision(int index1, int index2)
        {
            PhysicsObjects[index1].CollisionHappenedWith(PhysicsObjects[index2]);
            PhysicsObjects[index2].CollisionHappenedWith(PhysicsObjects[index1]);
        }
        */

		public static void DrawAll()
		{
			foreach (var drawable in Drawables)
				drawable.Draw();
		}
	}
}