namespace Tweening
{
	using System;
	using Tweeners;

	public static class Tweens
	{
		#region Creation

		public static ITween Tween(object source, double duration, dynamic toValues, dynamic fromValues = null, Func<float, float> ease = null)
		{
			return new Tween(source, duration, toValues, fromValues, ease);
		}

		public static ITween Delay(double duration) => new Timer(duration);

		#endregion

		#region Extensions

		public static ITween Loop(this ITween timer, int times) => new Loop(timer, times);

		public static ITween Reverse(this ITween timer) => new Reverse(timer);

		public static ITween Then(this ITween first, ITween second) => new Chain(first, second);

		public static ITween Then(this ITween timer, Action action) => timer.Then(new Trigger(action));

		public static ITween ThenWait(this ITween timer, double duration) => timer.Then(Delay(duration));

		public static ITween And(this ITween first, ITween second) => new Parallel(first, second);

		public static ITween ThenReverse(this ITween timer)
		{
			var reverse = timer.Reverse();
			return timer.Then(reverse);
		}

		#endregion
	}
}
