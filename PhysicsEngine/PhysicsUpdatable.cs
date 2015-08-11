namespace PhysicsEngine
{
	public interface PhysicsUpdatable
	{
		void Update(float deltaTime);
		void HandleGroundAndSideWallsCollision();
		void HandleCollision(PhysicsUpdatable other);
	}
}