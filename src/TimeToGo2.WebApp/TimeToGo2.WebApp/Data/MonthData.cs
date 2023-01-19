public class MonthData
{
    public MonthData(int year, int month)
    {
        Year = year;
        Month = month;
    }

    public int Year { get; }

    public int Month { get; }
    public List<DayData> Days { get; set; }
}