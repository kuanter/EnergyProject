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

        public Meter()
        {
            MeterReadings = new List<MeterReading>();
        }
        public Meter(string id, string serialNumber, DateTime installDate, bool isActive, string paymentAccountId)
        {
            Id = id;
            SerialNumber = serialNumber;
            InstallDate = installDate;
            IsActive = isActive;
            PaymentAccountId = paymentAccountId;
            MeterReadings = new List<MeterReading>();
        }
    }
}
