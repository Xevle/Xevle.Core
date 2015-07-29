using System;

namespace Xevle.Core
{
	/// <summary>
	/// Stop timer class to stop times.
	/// </summary>
	public class StopTimer
	{
		#region Private variables
		/// <summary>
		/// The start time.
		/// </summary>
		Int64 startTime;

		/// <summary>
		/// The stop time.
		/// </summary>
		Int64 stopTime;

		/// <summary>
		/// The state of the timer.
		/// </summary>
		StopTimerState timerState;
		#endregion

		#region Constructor
		/// <summary>
		/// Initializes a new instance of the <see cref="Xevle.Core.StopTimer"/> class.
		/// </summary>
		public StopTimer()
		{
			startTime = 0;
			stopTime = 0;
			
			timerState = StopTimerState.Stop;
		}
		#endregion

		#region Properties
		/// <summary>
		/// Returns the correct stop timer difference.
		/// </summary>
		/// <value>The difference.</value>
		Int64 Difference
		{
			get
			{
				switch (timerState)
				{
					case StopTimerState.Start:
						{
							return DateTime.Now.Ticks - startTime;
						}
					case StopTimerState.Stop:
						{
							return stopTime - startTime;
						}
					default:
						{
							return 0;
						}
				}
			}
		}

		/// <summary>
		/// Returns the stopwatch time in micro seconds.
		/// </summary>
		/// <value>The micro seconds.</value>
		public double MicroSeconds
		{
			get
			{
				return Difference / (10.0);
			}
		}

		/// <summary>
		/// Returns the stopwatch time in micro seconds.
		/// </summary>
		/// <value>The milli seconds.</value>
		public double MilliSeconds
		{
			get
			{
				return Difference / (10000.0);
			}
		}

		/// <summary>
		/// Returns the stopwatch time in seconds.
		/// </summary>
		/// <value>The seconds.</value>
		public double Seconds
		{
			get
			{
				return Difference / (10000000.0);
			}
		}

		/// <summary>
		/// Returns the stopwatch time in minutes.
		/// </summary>
		/// <value>The minutes.</value>
		public double Minutes
		{
			get
			{
				return Difference / (10000000.0 * 60);
			}
		}

		/// <summary>
		/// Returns the stopwatch time in hours.
		/// </summary>
		/// <value>The hours.</value>
		public double Hours
		{
			get
			{
				return Difference / (10000000.0 * 60 * 60);
			}
		}

		/// <summary>
		/// Returns the stopwatch time in days.
		/// </summary>
		/// <value>The days.</value>
		public double Days
		{
			get
			{
				return (10000000.0 * 60 * 60 * 24);
			}
		}

		/// <summary>
		/// Returns the stopwatch time in weeks.
		/// </summary>
		/// <value>The weeks.</value>
		public double Weeks
		{
			get
			{
				return Difference / (10000000.0 * 60 * 60 * 24 * 7);
			}
		}

		/// <summary>
		/// Returns the stopwatch time in months.
		/// </summary>
		/// <value>The months.</value>
		public double Months
		{
			get
			{
				return Difference / (10000000.0 * 60 * 60 * 24 * 30.44);
			}
		}

		/// <summary>
		/// Returns the stopwatch time in years.
		/// </summary>
		/// <value>The years.</value>
		public double Years
		{
			get
			{
				return Difference / (10000000.0 * 60 * 60 * 24 * 365.25);
			}
		}
		#endregion

		#region Methods
		/// <summary>
		/// Start the stop watch.
		/// Previos state would be cleared.
		/// </summary>
		public void Start()
		{
			if (timerState == StopTimerState.Start)
			{
				throw new Exception("Timer is already started!");
			}

			startTime = DateTime.Now.Ticks;
			timerState = StopTimerState.Start;
		}

		/// <summary>
		/// Stop this stop watch.
		/// </summary>
		public void Stop()
		{
			stopTime = DateTime.Now.Ticks;
			timerState = StopTimerState.Stop;
		}
		#endregion
	}
}