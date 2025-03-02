namespace united_movers_api.Models
{
    public class RiderFinancialDetails
    {
        public int EmployeeID { get; set; }
        public string BankAccountNumber { get; set; }
        public string BankName { get; set; }
        public string BankIFSCCode { get; set; }
        public DateTime? InsuranceEndDate { get; set; }
        public string InsurancePolicyNumber { get; set; }
        public DateTime? InsuranceStartDate { get; set; }
        public string InsurerName { get; set; }
        public string UANNumber { get; set; }
    }
}
