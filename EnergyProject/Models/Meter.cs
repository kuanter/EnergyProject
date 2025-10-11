namespace EnergyProject.Models
{
    public class Meter
    {
        public int Id { get; set; }
        public string SerialNumber { get; set; }
        public DateTime InstallDate { get; set; }
        public bool IsActive { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<MeterReading> Readings { get; set; }
    }
}
