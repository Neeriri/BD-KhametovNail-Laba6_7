using System;
using System.Windows;

namespace MarketingDB_WPF
{
    public partial class Client
    {
        public int ClientID { get; set; }
        public string FullName { get; set; } = "";
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
    }

    public partial class Campaign
    {
        public int CampaignID { get; set; }
        public string CampaignName { get; set; } = "";
        public int ClientID { get; set; }
        public decimal? Budget { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Status { get; set; }

        // Navigation property
        public virtual Client? Client { get; set; }
    }

    public partial class Employee
    {
        public int EmployeeID { get; set; }
        public string FullName { get; set; } = "";
        public string? Position { get; set; }
        public string? Email { get; set; }
        public decimal? HourlyRate { get; set; }
    }
}
