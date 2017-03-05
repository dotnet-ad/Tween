namespace Tweening.Tweeners
{
	using System;

	public class Timer : ITween
	{
		public Timer(double duration)
		{
			this.Duration = duration;
			this.Reset();
		}

		#region Properties

		public virtual double Time { get; set; }

		public double Duration { get; set; }

		public float Progress 
		{
			get => this.Duration <= 0 ? 1 : Math.Max(0, Math.Min(1,(float)(this.Time / this.Duration)));
			set => this.Time = this.Duration * value;
		}

		public bool IsStarted => this.Progress >= 0;

		public bool IsFinished => this.Progress >= 1;

		#endregion

		protected virtual void OnStart() {}

		public virtual bool Update(double deltaTime)
		{
			this.Time += deltaTime;
			return this.IsFinished;
		}

		public virtual void Reset()
		{
			this.Time = 0;
		}
	}
}
