using System.Collections.Generic;
using GraphicsEngine.Datatypes;
using GraphicsEngine.Physics2D;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using FarseerPhysics.Factories;
using GraphicsEngine.Shapes;

namespace GraphicsEngine
{
	public static class Entities
	{
        static Entities()
        {
           world = new World(Gravity);
           Vector2 groundPosition = new Vector2(0, GroundAndSidePlanes.Bottom);
           var ground = BodyFactory.CreateRectangle(world, 10, 0.1f, 1f, groundPosition);
           ground.IsStatic = true;
           ground.Restitution = 1;
           ground.Friction = 0.01f;
           var leftWall = BodyFactory.CreateRectangle(world, 0.1f, 4.0f, 1f,
               new Vector2(GroundAndSidePlanes.Left, 0));
           leftWall.IsStatic = true;
           leftWall.Restitution = 1;
           leftWall.Friction = 0.01f;
           var rightWall = BodyFactory.CreateRectangle(world, 0.1f, 4.0f, 1f,
               new Vector2(GroundAndSidePlanes.Right, 0));
           rightWall.IsStatic = true;
           rightWall.Restitution = 1;
           rightWall.Friction = 0.01f;
        }

        public static World world;
        public static Vector2 Gravity = new Vector2(0, -9.81f);

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
                world.Step(PhysicsUpdateTimeStep);
                foreach (var updatable in PhysicsObjects)
                    updatable.Update(PhysicsUpdateTimeStep);
                //CollisionCheck();
            }
        }

        private static float timeAccumulator;
        private const float PhysicsUpdateTimeStep = 1 / 60.0f;

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