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
        public MeterReading(string id, float valueKWh, DateTime createdAt, string meterId)
        {
            Id = id;
            ValueKWh = valueKWh;
            CreatedAt = createdAt;
            MeterId = meterId;
        }
    }
}
