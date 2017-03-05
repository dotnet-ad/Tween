namespace Tweening.Tweeners
{
	using System;

	public class Parallel : Timer
	{
		public Parallel(ITween first, ITween second) : base( Math.Max(first.Duration,second.Duration))
		{
			this.first = first;
			this.second = second;
		}

		#region Fields

		private ITween first, second;

		#endregion

		public override double Time
		{
			get => base.Time;
			set
			{
				base.Time = value;
				first.Time = Math.Min(value, first.Duration);
				second.Time = Math.Min(value, second.Duration);
			}
		}

		public override void Reset()
		{
			base.Reset();
			this.first.Reset();
			this.second.Reset();
		}

	}
}
