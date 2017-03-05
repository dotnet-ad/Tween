namespace Tweening.Tests
{
	using System;
	using NUnit.Framework;

	public class StubClass
	{
		public double Double { get; set; }

		public float Float { get; set; }

		public int Integer { get; set; }

		public byte Byte { get; set; }
	}

	public struct StubStruct
	{
		public double Double { get; set; }

		public float Float { get; set; }

		public int Integer { get; set; }

		public byte Byte { get; set; }
	}

	[TestFixture()]
	public class TweenTest
	{
		[Test()]
		public void WithClassSourceAndAnonymous()
		{
			var source = new StubClass()
			{
				Integer = 10,
				Byte = 100,
				Double = 10.0,
				Float = 10.0f,
			};

			var tween = new Tween(source,1, new { Integer = 100, Byte = (byte)200, Double = 100.0, Float = 100.0f });
			tween.Progress = 0.5f;
			Assert.AreEqual(55, source.Integer);
			Assert.AreEqual(150, source.Byte);
			Assert.AreEqual(55.0, source.Double);
			Assert.AreEqual(55.0f, source.Float);

			tween = new Tween(source,1, new { Integer = 100, Byte = (byte)200, Double = 100.0, Float = 100.0f }, new { Integer = 0, Byte = 0, Double = 0, Float = 0 });
			tween.Progress = 0.5f;
			Assert.AreEqual(50, source.Integer);
			Assert.AreEqual(100, source.Byte);
			Assert.AreEqual(50.0, source.Double);
			Assert.AreEqual(50.0f, source.Float);
		}

		[Test()]
		public void WithStructSourceAndAnonymous()
		{
			var source = new StubStruct()
			{
				Integer = 10,
				Byte = 100,
				Double = 10.0,
				Float = 10.0f,
			};

			var tween = new Tween(source, 1, new { Integer = 100, Byte = (byte)200, Double = 100.0, Float = 100.0f });
			tween.Progress = 0.5f;
			Assert.AreEqual(55, source.Integer);
			Assert.AreEqual(150, source.Byte);
			Assert.AreEqual(55.0, source.Double);
			Assert.AreEqual(55.0f, source.Float);

			tween = new Tween(source, 1, new { Integer = 100, Byte = (byte)200, Double = 100.0, Float = 100.0f }, new { Integer = 0, Byte = 0, Double = 0, Float = 0 });
			tween.Progress = 0.5f;
			Assert.AreEqual(50, source.Integer);
			Assert.AreEqual(100, source.Byte);
			Assert.AreEqual(50.0, source.Double);
			Assert.AreEqual(50.0f, source.Float);
		}
	}
}
