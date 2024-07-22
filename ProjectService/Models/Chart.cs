namespace ProjectService.Models
{
    public class Chart
    {
        public string Symbol { get; set; }
        public string Timeframe { get; set; }
        public List<Indicator> Indicators { get; set; }
    }
}
