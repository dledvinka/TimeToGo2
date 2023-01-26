using TimeToGo2.WebApp.Data;

public class MonthData : Entity
{
    public int Year { get; set; }
    public int Month { get; set; }
    public List<DayData> Days { get; set; }
}