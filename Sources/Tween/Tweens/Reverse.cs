namespace Tweening.Tweeners
{
	public class Reverse : Timer
	{
		public Reverse(ITween timer) : base(timer.Duration)
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
				this.timer.Time = this.Duration - this.Time;
			}
		}

		public override void Reset()
		{
			base.Reset();
			this.timer.Reset();
		}
	}
}
