namespace Showcase.Measurement.Domain
{
    public class MeasurementException : Exception
    {
        public MeasurementException(string error) : base(error) { }
    }
}
