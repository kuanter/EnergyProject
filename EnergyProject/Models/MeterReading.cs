namespace EnergyProject.Models
{
    public class MeterReading
    {
        public int Id { get; set; }
        public float ValueKWh { get; set; }
        public DateTime CreatedAt { get; set; }

        public int MeterId { get; set; }
        public Meter Meter { get; set; }
    }
}
