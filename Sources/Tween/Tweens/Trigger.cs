namespace Tweening.Tweeners
{
	using System;

	public class Trigger : Timer
	{
		public Trigger(Action action) : base(0)
		{
			this.action = action;
		}

		#region Fields

		private Action action;

		#endregion

		public override double Time
		{
			get
			{
				return base.Time;
			}
			set
			{
				if (this.Time != value && value > 0)
				{
					base.Time = value;
					action();
				}
			}
		}
	}
}
