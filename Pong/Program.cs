using System;
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
			new Program().Run(RenderMode.Render2D);
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
			Entities.world2D.Gravity /= 10.0f;
			Entities.Register(new Sprite(new Texture("Background.jpg"), new Size(2.0f, 1.4f)));
			ball = new Ball(new Sprite(new Texture("Ball.png"), Constants.BallSize));
			ResetBall();
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
			if (leftPaddle.position.y < ball.position.y - 2 * Constants.PaddleSpeed * timeDeltaInSeconds)
				leftPaddle.IncreasePositionY(Constants.PaddleSpeed * timeDeltaInSeconds);
			else if (leftPaddle.position.y > ball.position.y + 2 * Constants.PaddleSpeed * timeDeltaInSeconds)
				leftPaddle.IncreasePositionY(-Constants.PaddleSpeed * timeDeltaInSeconds);
			// Human player
			if (form.DownPressed && rightPaddle.position.y > -(0.55f - rightPaddle.size.Height/2))
				rightPaddle.IncreasePositionY(-Constants.PaddleSpeed * timeDeltaInSeconds);
			if (form.UpPressed && rightPaddle.position.y < 0.55f - rightPaddle.size.Height / 2)
				rightPaddle.IncreasePositionY(Constants.PaddleSpeed * timeDeltaInSeconds);
		}
		
		private void HandleBall(float timeDeltaInSeconds)
		{
			if (Math.Abs(ball.velocity.x) < 0.01f)
				ResetBall();
			if (ball.position.x < -0.9f)
			{
				rightPoints++;
				ResetBall();
			}
			else if (ball.position.x > 0.9f)
			{
				leftPoints++;
				ResetBall();
			}
		}

		private int leftPoints;
		private int rightPoints;

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