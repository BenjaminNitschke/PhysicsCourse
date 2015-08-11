namespace PhysicsEngine
{
	public interface PhysicsUpdatable
	{
		void Update(float deltaTime);
		bool IsCollidingWithGround { get; }
		void HandleGroundCollision();
		void HandleCollision(PhysicsUpdatable other);
	}
}