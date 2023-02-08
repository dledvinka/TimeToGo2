namespace TimeToGo2.WebApp.ViewModels
{
    using TimeToGo2.WebApp.Configuration;

    public class DayViewModel
    {
        private readonly IJobConstraints _jobConstraints;
        private TimeSpan? deltaFromGateSystem;
        private bool isWorkDay;
        private TimeSpan? spentOutside;
        private TimeSpan? timeArrived;
        private TimeSpan? timeLeft;

        public TimeSpan DailyDelta { get; private set; }

        public int DayInMonth { get; private set; }

        public DayOfWeek DayOfWeek { get; private set; }

        public TimeSpan? DeltaFromGateSystem
        {
            get => deltaFromGateSystem;
            set
            {
                deltaFromGateSystem = value;

                if (value != null)
                {
                    TimeArrived = null;
                    TimeLeft = null;
                    SpentOutside = null;
                    Recalculate();
                }
            }
        }

        public bool IsValid
        {
            get
            {
                var manuallyEntered = TimeArrived.HasValue && TimeLeft.HasValue && (SpentOutside.HasValue || SpentOutside == TimeSpan.Zero) &&
                    !DeltaFromGateSystem.HasValue;
                var dataFromGateSystem = !TimeArrived.HasValue && !TimeLeft.HasValue && !SpentOutside.HasValue && DeltaFromGateSystem.HasValue;

                return manuallyEntered || dataFromGateSystem;
            }
        }

        public bool IsWorkDay
        {
            get => isWorkDay;
            set
            {
                isWorkDay = value;

                if (isWorkDay == false)
                {
                    TimeArrived = null;
                    TimeLeft = null;
                    SpentOutside = null;
                    DeltaFromGateSystem = null;
                    DailyDelta = TimeSpan.Zero;
                    Recalculate();
                }
            }
        }

        public TimeSpan? SpentOutside
        {
            get => spentOutside;
            set
            {
                spentOutside = value;

                if (value != null)
                {
                    DeltaFromGateSystem = null;
                    Recalculate();
                }
            }
        }

        public TimeSpan? TimeArrived
        {
	        get => timeArrived;
	        set
	        {
		        timeArrived = value;

		        if (value != null)
		        {
			        DeltaFromGateSystem = null;
			        Recalculate();
		        }
	        }
        }

		public TimeSpan? TimeLeft
		{
			get => timeLeft;
			set
			{
				timeLeft = value;

				if (value != null)
				{
					DeltaFromGateSystem = null;
					Recalculate();
				}
			}
		}

		public bool IsEditing { get; set; }

        public DayViewModel(IJobConstraints jobConstraints) => _jobConstraints = jobConstraints;

        private TimeSpan GetCalculatedPauseDuration()
        {
            var pauseDuration = (!SpentOutside.HasValue || SpentOutside == TimeSpan.Zero) ? _jobConstraints.MinimalLunchPauseDuration : SpentOutside.Value;
            var timeLeft = TimeLeft ?? (TimeArrived.Value + TimeSpan.FromHours(8));
            var workedDuration = timeLeft - TimeArrived.Value;
            var workedOverDailyLunchPa =
                TimeSpan.FromMinutes(Math.Max((workedDuration - _jobConstraints.LunchPauseAppliesAfterAmountOfWorkedHours).TotalMinutes, 0));
            var calculatedPauseDuration = TimeSpan.FromMinutes(SpentOutside.HasValue
                                                                   ? Math.Max(workedOverDailyLunchPa.TotalMinutes, pauseDuration.TotalMinutes)
                                                                   : Math.Min(workedOverDailyLunchPa.TotalMinutes, pauseDuration.TotalMinutes));

            return calculatedPauseDuration;
        }

        private void Recalculate()
        {
            DailyDelta = GetDailyDelta();
        }

        private TimeSpan GetDailyDelta()
        {
            if (!IsValid)
                return TimeSpan.Zero;

            if (DeltaFromGateSystem.HasValue)
                return DeltaFromGateSystem.Value;

            var atWorkDuration = TimeLeft.Value - TimeArrived.Value;
            var beenOutsideDuration = SpentOutside ?? TimeSpan.Zero;
            
            var atWorkOverObligatoryLunchPause = atWorkDuration - _jobConstraints.LunchPauseAppliesAfterAmountOfWorkedHours;
            var atWorkOverObligatoryLunchPauseInMinutes = atWorkOverObligatoryLunchPause.TotalMinutes;
            var normalizedLunchPauseDurationInMinutes = Math.Min(Math.Max(0, atWorkOverObligatoryLunchPauseInMinutes), _jobConstraints.MinimalLunchPauseDuration.TotalMinutes);
            
            if (beenOutsideDuration.TotalMinutes < normalizedLunchPauseDurationInMinutes)
                beenOutsideDuration = TimeSpan.FromMinutes(normalizedLunchPauseDurationInMinutes);

            return atWorkDuration - beenOutsideDuration - _jobConstraints.WorkHoursPerDay;
        }

        public void SetData(int year, int month, DayData dayData)
        {
            Id = dayData.Id;
            TimeArrived = dayData.TimeArrived?.ToTimeSpan();
            TimeLeft = dayData.TimeLeft?.ToTimeSpan();
            SpentOutside = dayData.SpentOutside;
            DeltaFromGateSystem = dayData.DeltaFromGateSystem;
            IsWorkDay = dayData.IsWorkDay;
            DayInMonth = dayData.DayInMonth;
            DayOfWeek = new DateTime(year, month, DayInMonth).DayOfWeek;
        }

        public int Id { get; private set; }

        public DayData GetData() => new DayData();
    }
}