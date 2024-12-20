namespace TimeManagement.Data
{
    public class Task
    {
        public int? ProjectId { get; set; }
        public int? ClientId { get; set; }
        public string? TaskName { get; set; }
        public string? BillingType { get; set; }
        public DateTime? Date { get; set; }
        public decimal? TimeWorked { get; set; }
        public decimal? BreakTime { get; set; }
        public string? Description { get; set; }
    }
}
