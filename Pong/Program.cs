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
			Entities.Register(new Sprite(new Texture("Background.jpg"), new Size(2.0f, 1.4f)));
			ball = new Ball(new Sprite(new Texture("Ball.png"), Constants.BallSize));
			//TODO: ball.velocity = Constants.DefaultBallVelocity;
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
			/*TODO:
			if (ball.position.y < -0.55f || ball.position.y > 0.55f)
				ball.velocity.y = -ball.velocity.y;
			if (ball.IsColliding(rightPaddle))
				ball.velocity.x = -Math.Abs(ball.velocity.x);
			if (ball.IsColliding(leftPaddle))
				ball.velocity.x = Math.Abs(ball.velocity.x);
			*/
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

		private void ResetBall()
		{
			title = "Pong Score: " + leftPoints + " - " + rightPoints;
			ball.position = Vector2D.Zero;
			var random = new Random((int)DateTime.Now.Ticks);
			//TODO: ball.velocity = Constants.DefaultBallVelocity *
			//	new Vector2D(random.Next(2) == 0 ? -1 : +1, random.Next(2) == 0 ? -1 : +1);
		}
	}
}