namespace EnergyProject.Models
{
    public class Meter
    {
        public int Id { get; set; }
        public string SerialNumber { get; set; }
        public DateTime InstallDate { get; set; }
        public bool IsActive { get; set; }

        public int PaymentAccountId { get; set; }
        public PaymentAccount PaymentAccount { get; set; }

        public ICollection<MeterReading> MeterReadings { get; set; }
    }
}
