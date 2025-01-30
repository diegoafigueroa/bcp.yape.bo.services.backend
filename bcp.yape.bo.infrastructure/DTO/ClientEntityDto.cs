namespace bcp.yape.bo.infrastructure.DTO
{
    public class ClientEntityDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string CellPhoneNumber { get; set; }
        public string DocumentType { get; set; }
        public string DocumentNumber { get; set; }
        public string ReasonOfUse { get; set; }
        public string? Email { get; set; }
        public string? RegistrationSource { get; set; }
        public string? ClientIpAddress { get; set; }
        public DateTime RegistrationDateUtc { get; set; }
    }
}
