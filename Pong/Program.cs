using System;
using System.Windows.Forms;
using GraphicsEngine;
using GraphicsEngine.Datatypes;

namespace Pong
{
	internal static class Program
	{
		[STAThread]
		private static void Main()
		{
			GraphicsAPI = API.OpenGL;
			Init();
			//graphics.Init3D();
			CreateSprites();
			while (true)
			{
				Application.DoEvents();
				if (form.IsDisposed)
					break;
				graphics.Render();
				//graphics.Draw3DCube();
				HandleInput(graphics.TimeDeltaInSeconds);
				HandleBall(graphics.TimeDeltaInSeconds);
				DrawSprites();
				graphics.Present();
				if (title != null)
					form.Text = title;
				title = null;
			}
			graphics.Dispose();
		}

		private static string title;
		public static API GraphicsAPI;

		private static void Init()
		{
			var common = new Common();
			form = new GraphicsEngineForm();
			form.Text = "Pong";
			form.Show();
			graphics = new OpenGLGraphics(form, common);
		}

		private static GraphicsEngineForm form;
		private static Graphics graphics;

		private static void CreateSprites()
		{
			background = new Sprite(new Texture("Background.jpg"), new Vector2D(2.0f, 2.0f));
			ball = new Ball(new Sprite(new Texture("Ball.png"), Constants.BallSize));
			var paddleTexture = new Texture("Paddle.png");
			leftPaddle = new Paddle(new Sprite(paddleTexture, Constants.PaddleSize));
			leftPaddle.position = new Vector2D(Constants.LeftPaddleX, 0);
			rightPaddle = new Paddle(new Sprite(paddleTexture, Constants.PaddleSize));
			rightPaddle.position = new Vector2D(Constants.RightPaddleX, 0);
		}

		private static Sprite background;
		private static Ball ball;
		private static Paddle leftPaddle;
		private static Paddle rightPaddle;

		private static void HandleInput(float timeDeltaInSeconds)
		{
			// AI
			if (leftPaddle.position.y < ball.position.y + Constants.PaddleSpeed * timeDeltaInSeconds)
				leftPaddle.position.y += Constants.PaddleSpeed * timeDeltaInSeconds;
			else if (leftPaddle.position.y > ball.position.y - Constants.PaddleSpeed * timeDeltaInSeconds)
				leftPaddle.position.y -= Constants.PaddleSpeed * timeDeltaInSeconds;
			// Human player
			if (form.DownPressed && rightPaddle.position.y > -0.8f)
				rightPaddle.position.y -= Constants.PaddleSpeed * timeDeltaInSeconds;
			if (form.UpPressed && rightPaddle.position.y < 0.8f)
				rightPaddle.position.y += Constants.PaddleSpeed * timeDeltaInSeconds;
		}
		
		private static void HandleBall(float timeDeltaInSeconds)
		{
			//TODO: tomorrow we will move this to PhysicsObject and add mass for force to change ball direction with paddle
			ball.position += ballVelocity * timeDeltaInSeconds;
			if (ball.position.y < -0.9f || ball.position.y > 0.9f)
				ballVelocity.y = -ballVelocity.y;
			if (IsColliding(ball.position, Constants.BallSize,
				rightPaddle.position, Constants.PaddleSize))
				ballVelocity.x = -Math.Abs(ballVelocity.x);
			if (IsColliding(ball.position, Constants.BallSize,
				leftPaddle.position, Constants.PaddleSize))
				ballVelocity.x = Math.Abs(ballVelocity.x);
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

		private static int leftPoints;
		private static int rightPoints;
		private static Vector2D ballVelocity = Constants.DefaultBallVelocity;

		public static bool IsColliding(Vector2D position1, Vector2D size1, Vector2D position2, Vector2D size2)
		{
			var left = position1.x - size1.x / 2;
			var right = position1.x + size1.x / 2;
			var top = position1.y - size1.y / 2;
			var bottom = position1.y + size1.y / 2;
			var otherLeft = position2.x - size2.x / 2;
			var otherRight = position2.x + size2.x / 2;
			var otherTop = position2.y - size2.y / 2;
			var otherBottom = position2.y + size2.y / 2;
			return right > otherLeft && bottom > otherTop && left < otherRight && top < otherBottom;
		}

		private static void ResetBall()
		{
			title = "Pong Score: " + leftPoints + " - " + rightPoints;
			ball.position = Vector2D.Zero;
			var random = new Random((int)DateTime.Now.Ticks);
			ballVelocity = Constants.DefaultBallVelocity *
				new Vector2D(random.Next(2) == 0 ? -1 : +1, random.Next(2) == 0 ? -1 : +1);
		}

		private static void DrawSprites()
		{
			background.Draw();
			ball.Draw();
			leftPaddle.Draw();
			rightPaddle.Draw();
		}
	}
}