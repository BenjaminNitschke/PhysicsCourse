using System;
using System.Windows.Forms;
using GraphicsEngine;
using GraphicsEngine.Datatypes;

namespace Pong
{
	internal class Program : GraphicsApp
	{
		public Program() : base("Pong") {}

		[STAThread]
		private static void Main()
		{
			new Program().Run();
		}

		protected override void Update()
		{
			HandleInput(TimeDeltaInSeconds);
			HandleBall(TimeDeltaInSeconds);
			if (title != null)
				form.Text = title;
			title = null;
		}

		private static string title;
		
		protected override void Init()
		{
			base.Init();
			Entities.Register(new Sprite(new Texture("Background.jpg"), new Size(2.0f, 1.4f)));
			ball = new Ball(new Sprite(new Texture("Ball.png"), Constants.BallSize));
			ball.velocity = Constants.DefaultBallVelocity;
			var paddleTexture = new Texture("Paddle.png");
			leftPaddle = new Paddle(new Sprite(paddleTexture, Constants.PaddleSize),
				new Vector2D(Constants.LeftPaddleX, 0));
			rightPaddle = new Paddle(new Sprite(paddleTexture, Constants.PaddleSize),
				new Vector2D(Constants.RightPaddleX, 0));
		}
		
		private Ball ball;
		private Paddle leftPaddle;
		private Paddle rightPaddle;

		private void HandleInput(float timeDeltaInSeconds)
		{
			// AI
			if (leftPaddle.position.y < ball.position.y + Constants.PaddleSpeed * timeDeltaInSeconds)
				leftPaddle.position.y += Constants.PaddleSpeed * timeDeltaInSeconds;
			else if (leftPaddle.position.y > ball.position.y - Constants.PaddleSpeed * timeDeltaInSeconds)
				leftPaddle.position.y -= Constants.PaddleSpeed * timeDeltaInSeconds;
			// Human player
			if (form.DownPressed && rightPaddle.position.y > -(0.55f - rightPaddle.size.Height/2))
				rightPaddle.position.y -= Constants.PaddleSpeed * timeDeltaInSeconds;
			if (form.UpPressed && rightPaddle.position.y < 0.55f - rightPaddle.size.Height / 2)
				rightPaddle.position.y += Constants.PaddleSpeed * timeDeltaInSeconds;
		}
		
		private void HandleBall(float timeDeltaInSeconds)
		{
			if (ball.position.y < -0.55f || ball.position.y > 0.55f)
				ball.velocity.y = -ball.velocity.y;
			if (IsColliding(ball.position, Constants.BallSize,
				rightPaddle.position, Constants.PaddleSize))
				ball.velocity.x = -Math.Abs(ball.velocity.x);
			if (IsColliding(ball.position, Constants.BallSize,
				leftPaddle.position, Constants.PaddleSize))
				ball.velocity.x = Math.Abs(ball.velocity.x);
			if (ball.position.x < -1)
			{
				rightPoints++;
				ResetBall();
			}
			else if (ball.position.x > 1)
			{
				leftPoints++;
				ResetBall();
			}
		}

		private int leftPoints;
		private int rightPoints;

		public static bool IsColliding(Vector2D position1, Size size1, Vector2D position2, Size size2)
		{
			var left = position1.x - size1.Width / 2;
			var right = position1.x + size1.Width / 2;
			var top = position1.y - size1.Height / 2;
			var bottom = position1.y + size1.Height / 2;
			var otherLeft = position2.x - size2.Width / 2;
			var otherRight = position2.x + size2.Width / 2;
			var otherTop = position2.y - size2.Height / 2;
			var otherBottom = position2.y + size2.Height / 2;
			return right > otherLeft && bottom > otherTop && left < otherRight && top < otherBottom;
		}

		private void ResetBall()
		{
			title = "Pong Score: " + leftPoints + " - " + rightPoints;
			ball.position = Vector2D.Zero;
			var random = new Random((int)DateTime.Now.Ticks);
			ball.velocity = Constants.DefaultBallVelocity *
				new Vector2D(random.Next(2) == 0 ? -1 : +1, random.Next(2) == 0 ? -1 : +1);
		}
	}
}