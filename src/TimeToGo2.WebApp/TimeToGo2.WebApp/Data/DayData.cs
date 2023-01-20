public class DayData
{
    public int DayInMonth { get; set; }

    public TimeSpan? DeltaFromGateSystem { get; set; }

    public bool IsWorkDay { get; set; }

    public TimeSpan? SpentOutside { get; set; }

    public TimeOnly? TimeArrived { get; set; }

    public TimeOnly? TimeLeft { get; set; }
}