namespace Tweening
{
	using System;

	public interface ITween
	{
		double Time { get; set; }

		double Duration { get; }

		float Progress { get; set; }

		bool IsStarted { get; }

		bool IsFinished { get; }

		bool Update(double deltaTime);

		void Reset();
	}
}
