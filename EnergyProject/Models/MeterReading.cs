namespace EnergyProject.Models
{
    public class MeterReading
    {
        public string Id { get; set; }
        public float ValueKWh { get; set; }
        public DateTime CreatedAt { get; set; }

        public string MeterId { get; set; }
        public Meter Meter { get; set; }

        public MeterReading() { }
        public MeterReading(float valueKWh, string meterId)
        {
            Id = Guid.NewGuid().ToString();
            CreatedAt = DateTime.Now;
            ValueKWh = valueKWh;
            MeterId = meterId;
        }
    }
}
