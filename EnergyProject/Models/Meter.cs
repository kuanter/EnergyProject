namespace EnergyProject.Models
{
    public class Meter
    {
        public string Id { get; set; }
        public string SerialNumber { get; set; }
        public DateTime InstallDate { get; set; }
        public bool IsActive { get; set; }
        public string PaymentAccountId { get; set; }
        public PaymentAccount PaymentAccount { get; set; }

        public ICollection<MeterReading> MeterReadings { get; set; }
    }
}
