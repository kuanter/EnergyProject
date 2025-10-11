namespace EnergyProject.Models
{
    public class PowerStatus
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public string Reason { get; set; }
        public DateTime UpdatedAt { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
