namespace EnergyProject.Models
{
    public class MeterReading
    {
        public string Id { get; set; }
        public float ValueKWh { get; set; }
        public DateTime CreatedAt { get; set; }

        public string MeterId { get; set; }
        public Meter Meter { get; set; }
    }
}
