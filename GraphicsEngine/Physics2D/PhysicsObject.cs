using FarseerPhysics.Dynamics;
using GraphicsEngine.Datatypes;
using GraphicsEngine.Shapes;
using Microsoft.Xna.Framework;
using System;

namespace GraphicsEngine.Physics2D
{
	public abstract class PhysicsObject : Updatable
	{
		protected PhysicsObject(Vector2D position, Size size, Body body)
		{
			this.position = position;
			this.size = size;
            this.body = body;
			//acceleration = -Entities.Gravity;
		}

		public Vector2D position; //m
		public readonly Size size; //m
        protected Body body;
        /*
		public Vector2D velocity; //m/s
		public Vector2 acceleration; //m/s2
		public float collisionFriction = 0.01f; // 0=no friction, 100% bounce, 1=no bounce
         */

		public void Update(float timeDeltaInSeconds)
		{
            position = (Vector2D)body.Position;
            //Rotation
            /*
			velocity += (Vector2D)acceleration * timeDeltaInSeconds;
			position += velocity * timeDeltaInSeconds;
            if (position.y - size.Height / 2 < GroundAndSidePlanes.Bottom)
			{
                position.y = GroundAndSidePlanes.Bottom + size.Height / 2;
				velocity *= (1.0f - collisionFriction);
				velocity.y = -velocity.y;
            }
            if (position.x + size.Width / 2 > GroundAndSidePlanes.Right)
            {
                position.x = GroundAndSidePlanes.Right - size.Width / 2;
                velocity *= (1.0f - collisionFriction);
                velocity.x = -velocity.x;
            }
            if (position.x - size.Width / 2 < GroundAndSidePlanes.Left)
            {
                position.x = GroundAndSidePlanes.Left + size.Width / 2;
                velocity *= (1.0f - collisionFriction);
                velocity.x = -velocity.x;
            }
             */
		}
        /*
        internal void CollisionHappenedWith(PhysicsObject other)
        {
            var betweenVector = position - other.position;
            var middle = position + betweenVector / 2;
            position = position * 0.9f + (middle + betweenVector.Normalize() * size.Width / 2) * 0.1f;
            velocity = velocity.MirrorAtNormal(betweenVector);
        }

        public bool IsColliding(PhysicsObject other)
        {
            var left = position.x - size.Width / 2;
            var right = position.x + size.Width / 2;
            var top = position.y - size.Height / 2;
            var bottom = position.y + size.Height / 2;
            var otherLeft = other.position.x - other.size.Width / 2;
            var otherRight = other.position.x + other.size.Width / 2;
            var otherTop = other.position.y - other.size.Height / 2;
            var otherBottom = other.position.y + other.size.Height / 2;
            return right > otherLeft && bottom > otherTop && left < otherRight && top < otherBottom;
        }
         */
    }
}