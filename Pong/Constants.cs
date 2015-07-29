using GraphicsEngine.Datatypes;

namespace Pong
{
	public static class Constants
	{
		public static readonly Vector2D BallSize = new Vector2D(0.1f, 0.17f); //m
		public static readonly Vector2D DefaultBallVelocity = new Vector2D(1.0f, 1.0f); //m/s
		public static readonly Vector2D PaddleSize = new Vector2D(0.05f, 0.4f); //m
		public const float PaddleSpeed = 1.2f; //m/s
		public const float LeftPaddleX = -0.9f; //m
		public const float RightPaddleX = 0.9f; //m
	}
}