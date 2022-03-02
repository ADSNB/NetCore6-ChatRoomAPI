namespace Domain.Models.RobotService
{
    public class CSVTemplateModel
    {
        public string Symbol { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Open { get; set; }
        public string High { get; set; }
        public string Low { get; set; }
        public string Close { get; set; }
        public string Volume { get; set; }

        public static CSVTemplateModel FromCsv(string csvLine)
        {
            string[] values = csvLine.Split(',');
            CSVTemplateModel dailyValues = new CSVTemplateModel();
            dailyValues.Symbol = Convert.ToString(values[0]);
            dailyValues.Date = Convert.ToString(values[1]);
            dailyValues.Time = Convert.ToString(values[2]);
            dailyValues.Open = Convert.ToString(values[3]);
            dailyValues.High = Convert.ToString(values[4]);
            dailyValues.Low = Convert.ToString(values[5]);
            dailyValues.Close = Convert.ToString(values[6]);
            dailyValues.Volume = Convert.ToString(values[7]);
            return dailyValues;
        }
    }
}