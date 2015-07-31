using GraphicsEngine.Datatypes;
using NUnit.Framework;

namespace GraphicsEngine.Tests
{
	public class Vector2DTests
	{
		[Test]
		public void AddVectors()
		{
			var a = new Vector2D(1, 2);
			var b = new Vector2D(3, 4);
			Assert.That(a + b, Is.EqualTo(new Vector2D(4, 6)));
		}

		[Test]
		public void SubtractVectors()
		{
			var a = new Vector2D(1, 2);
			var b = new Vector2D(3, 4);
			Assert.That(b - a, Is.EqualTo(new Vector2D(2, 2)));
		}

		[Test]
		public void DivideVector()
		{
			var vector = new Vector2D(2, 4);
			Assert.That(vector / 2.0f, Is.EqualTo(new Vector2D(1, 2)));
		}

		[Test]
		public void MultiplyVector()
		{
			var vector = new Vector2D(3, 5);
			Assert.That(vector * 3.0f, Is.EqualTo(new Vector2D(9, 15)));
			Assert.That(vector * new Vector2D(2, 4), Is.EqualTo(new Vector2D(6, 20)));
		}

		[Test]
		public void VectorToString()
		{
			Assert.That(new Vector2D(1.5f, 2.5f).ToString(), Is.EqualTo("1.5, 2.5"));
		}

		[Test]
		public void RotateVector()
		{
			var right = new Vector2D(1, 0);
			Assert.That(right.Rotate(0), Is.EqualTo(right));
			Assert.That(new Vector2D(0, 1).Rotate(0), Is.EqualTo(new Vector2D(0, 1)));
			Assert.That(right.Rotate(90), Is.EqualTo(new Vector2D(0, 1)));
			Assert.That(right.Rotate(180), Is.EqualTo(new Vector2D(-1, 0)));
			Assert.That(right.Rotate(270), Is.EqualTo(new Vector2D(0, -1)));
		}

		[Test]
		public void MirrorAtNormal()
		{
			Assert.That(new Vector2D(0.2f, -0.3f).MirrorAtNormal(new Vector2D(0, 1)),
				Is.EqualTo(new Vector2D(0.2f, 0.3f)));
			Assert.That(new Vector2D(0.2f, -0.3f).MirrorAtNormal(new Vector2D(1, 0)),
				Is.EqualTo(new Vector2D(-0.2f, -0.3f)));
			Assert.That(new Vector2D(-1, 0).MirrorAtNormal(new Vector2D(10, 10)),
				Is.EqualTo(new Vector2D(0, 1)));
		}
	}
}