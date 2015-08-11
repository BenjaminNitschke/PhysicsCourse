using System;
using NUnit.Framework;

namespace PhysicsEngine.Tests
{
	public class Vector2DTests
	{
		[Test]
		public void Add()
		{
			Vector2D a = new Vector2D(1, 2);
			Vector2D b = new Vector2D(3, 1);
			Assert.That(a + b, Is.EqualTo(new Vector2D(4, 3)));
			Assert.That(a + new Vector2D(2, 2), Is.EqualTo(new Vector2D(3, 4)));
		}

		[Test]
		public void Subtract()
		{
			Vector2D a = new Vector2D(1, 2);
			Vector2D b = new Vector2D(3, 1);
			Assert.That(a - b, Is.EqualTo(new Vector2D(-2, 1)));
		}

		[Test]
		public void MultiplyWithScalar()
		{
			Vector2D a = new Vector2D(1, 2);
			Assert.That(a * 2.5f, Is.EqualTo(new Vector2D(2.5f, 5)));
			Assert.That(2.5f * a, Is.EqualTo(new Vector2D(2.5f, 5)));
		}

		[Test]
		public void DivideWithScalar()
		{
			Vector2D a = new Vector2D(1, 2);
			Assert.That(a / 2, Is.EqualTo(new Vector2D(0.5f, 1)));
		}

		[Test]
		public void Length()
		{
			Assert.That(new Vector2D(3, 4).Length, Is.EqualTo(5));
		}

		[Test]
		public void Rotate()
		{
			Assert.That(new Vector2D(1, 0).Rotate(90), Is.EqualTo(new Vector2D(0, 1)));
			Assert.That(new Vector2D(0, 1).Rotate(90), Is.EqualTo(new Vector2D(-1, 0)));
			Assert.That(new Vector2D(-1, 0).Rotate(90), Is.EqualTo(new Vector2D(0, -1)));
		}

		[Test]
		public void Vector2DToString()
		{
			Assert.That(new Vector2D(3, 4).ToString(), Is.EqualTo("3, 4"));
		}
	}
}