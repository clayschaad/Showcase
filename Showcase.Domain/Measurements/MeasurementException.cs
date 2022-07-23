namespace Showcase.Domain.Measurements
{
    public class MeasurementException : Exception
    {
        public MeasurementException(string error) : base(error) { }
    }
}
