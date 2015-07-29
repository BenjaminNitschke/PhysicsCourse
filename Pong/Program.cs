using System;
using System.Windows.Forms;
using GraphicsEngine;
using PongMessages;

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

		private static void SetOtherPaddlePosition(float y)
		{
			if (amILeftPaddle)
				rightPaddleY = y;
			else
				leftPaddleY = y;
		}

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
			background = new Sprite(new Texture("Background.jpg"), 2.0f, 2.0f);
			ball = new Sprite(new Texture("Ball.png"), Constants.BallWidth, Constants.BallHeight);
			var paddleTexture = new Texture("Paddle.png");
			leftPaddle = new Sprite(paddleTexture, Constants.PaddleWidth, Constants.PaddleHeight);
			rightPaddle = new Sprite(paddleTexture, Constants.PaddleWidth, Constants.PaddleHeight);
		}

		private static Sprite background;
		private static Sprite ball;
		private static readonly bool amILeftPaddle = false;
		private static Sprite leftPaddle;
		private static float leftPaddleY;
		private static Sprite rightPaddle;
		private static float rightPaddleY;

		private static void HandleInput(float timeDeltaInSeconds)
		{
			// AI
			if (leftPaddleY < ballY + Constants.PaddleSpeed * timeDeltaInSeconds)
				leftPaddleY += Constants.PaddleSpeed * timeDeltaInSeconds;
			else if (leftPaddleY > ballY - Constants.PaddleSpeed * timeDeltaInSeconds)
				leftPaddleY -= Constants.PaddleSpeed * timeDeltaInSeconds;
			// Human player
			if (form.DownPressed && rightPaddleY > -0.8f)
				rightPaddleY -= Constants.PaddleSpeed * timeDeltaInSeconds;
			if (form.UpPressed && rightPaddleY < 0.8f)
				rightPaddleY += Constants.PaddleSpeed * timeDeltaInSeconds;
		}

		private static float ballX;
		private static float ballY;

		private static void HandleBall(float timeDeltaInSeconds)
		{
			ballX += ballVelocityX * timeDeltaInSeconds;
			ballY += ballVelocityY * timeDeltaInSeconds;
			if (ballY < -0.9f || ballY > 0.9f)
				ballVelocityY = -ballVelocityY;
			if (IsColliding(ballX, ballY, Constants.BallWidth, Constants.BallHeight,
				Constants.RightPaddleX, rightPaddleY, Constants.PaddleWidth, Constants.PaddleHeight))
				ballVelocityX = -Math.Abs(ballVelocityX);
			if (IsColliding(ballX, ballY, Constants.BallWidth, Constants.BallHeight,
				Constants.LeftPaddleX, leftPaddleY, Constants.PaddleWidth, Constants.PaddleHeight))
				ballVelocityX = Math.Abs(ballVelocityX);
			if (ballX < -1)
			{
				rightPoints++;
				ResetBall();
			}
			else if (ballX > 1)
			{
				leftPoints++;
				ResetBall();
			}
		}

		private static int leftPoints;
		private static int rightPoints;
		private static float ballVelocityX = Constants.DefaultBallVelocityX;
		private static float ballVelocityY = Constants.DefaultBallVelocityY;

		public static bool IsColliding(float x, float y, float width, float height, float otherX,
			float otherY, float otherWidth, float otherHeight)
		{
			var left = x - width / 2;
			var right = x + width / 2;
			var top = y - height / 2;
			var bottom = y + height / 2;
			var otherLeft = otherX - otherWidth / 2;
			var otherRight = otherX + otherWidth / 2;
			var otherTop = otherY - otherHeight / 2;
			var otherBottom = otherY + otherHeight / 2;
			return right > otherLeft && bottom > otherTop && left < otherRight && top < otherBottom;
		}

		private static void ResetBall()
		{
			title = "Pong Score: " + leftPoints + " - " + rightPoints;
			ballX = 0;
			ballY = 0;
			var random = new Random((int)DateTime.Now.Ticks);
			ballVelocityX = (random.Next(2) == 0 ? -1 : +1) * Constants.DefaultBallVelocityX;
			ballVelocityY = (random.Next(2) == 0 ? -1 : +1) * Constants.DefaultBallVelocityY;
		}

		private static void DrawSprites()
		{
			background.Draw();
			ball.Draw(ballX, ballY);
			leftPaddle.Draw(Constants.LeftPaddleX, leftPaddleY);
			rightPaddle.Draw(Constants.RightPaddleX, rightPaddleY);
		}

#if UNUSED
        public static void ToggleAPIAndRestart()
        {
            GraphicsAPI = GraphicsAPI == API.OpenGL
                ? API.OpenGL4
                : API.OpenGL;
            form.Close();
            Init();
        }
#endif
	}
}