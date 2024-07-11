namespace MeterApp.Model
{
    public static class ReadingsCalculator
    {
        private static double FirstMeter { get; set; }
        private static double SecondMeter { get; set; }
        private static readonly double _pricePerMeter = 0;//MainDB.PriceTable.RecordSource.Where(s => s.ID.Equals("PRICE PER METER")).FirstOrDefault().PriceValue;
        private static readonly double _masterMeter = 0;//MainDB.PriceTable.RecordSource.Where(s => s.ID.Equals("MASTER READING")).FirstOrDefault().PriceValue;

        private static double Formula()
        {
            double diff = Math.Abs(SecondMeter - FirstMeter);
            return (diff * _pricePerMeter) + _masterMeter;
        }

        public static double CalcReadings(double val1, double val2)
        {
            FirstMeter = (val1 > val2) ? val2 : val1;
            SecondMeter = (val1 > val2) ? val1 : val2;
            return Formula();
        }
    }
}