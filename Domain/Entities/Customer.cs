namespace Domain.Entities
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string CustomerCode { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerType { get; set; } = string.Empty;
        public string? GSTIN { get; set; }
        public string? PAN { get; set; }
        public string? GSTRegistrationType { get; set; }
        public bool IsGSTRegistered { get; set; }
        public string? PlaceOfSupplyState { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

        public string CreatedBy { get; set; } = "System";
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
