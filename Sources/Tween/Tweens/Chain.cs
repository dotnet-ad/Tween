namespace Tweening.Tweeners
{
	public class Chain : Timer
	{
		public Chain(ITween first, ITween second) : base( first.Duration + second.Duration)
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

				if (this.Time <= first.Duration)
				{
					first.Time = this.Time;
					second.Time = 0;
				}
				else
				{
					first.Time = first.Duration;
					second.Time = this.Time - first.Duration;
				}
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
