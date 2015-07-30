using GraphicsEngine.Datatypes;

namespace Pong
{
	public static class Constants
	{
		public static readonly Size BallSize = new Size(0.1f, 0.1f); //m
		public static readonly Vector2D DefaultBallVelocity = new Vector2D(1.0f, 1.0f); //m/s
		public static readonly Size PaddleSize = new Size(0.05f, 0.25f); //m
		public const float PaddleSpeed = 1.2f; //m/s
		public const float LeftPaddleX = -0.9f; //m
		public const float RightPaddleX = 0.9f; //m
	}
}