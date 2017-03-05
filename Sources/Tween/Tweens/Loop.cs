namespace Tweening.Tweeners
{
	public class Loop : Timer
	{
		public Loop(ITween timer, int times = -1) : base((times < 0) ? double.MaxValue : times * timer.Duration)
		{
			this.timer = timer;
		}

		#region Fields

		private ITween timer;

		#endregion

		public override double Time
		{
			get => base.Time;
			set
			{
				base.Time = value;
				var relative = value % this.timer.Duration;
				this.timer.Time = relative;
			}
		}
	}
}
