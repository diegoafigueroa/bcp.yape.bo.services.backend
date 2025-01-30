namespace bcp.yape.bo.core.Entities
{
    /// <summary>
    /// Represents the client registration data in the Yape system.
    /// Contains all the details provided during the client's registration as well as metadata 
    /// related to the registration source and the date of registration.
    /// </summary>
    public class ClientRegistrationData
    {
        /// <summary>
        /// The full name of the client.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The full last name of the client.
        /// </summary>
        public string LastName { get; }

        /// <summary>
        /// The client's cellphone number in a specific format.
        /// </summary>
        public string CellPhoneNumber { get; }

        /// <summary>
        /// The type of the client's identity document.
        /// </summary>
        public DocumentTypeEnum DocumentType { get; }

        /// <summary>
        /// The number of the client's identity document.
        /// </summary>
        public string DocumentNumber { get; }

        /// <summary>
        /// The client's reason for using Yape.
        /// </summary>
        public string ReasonOfUse { get; }

        /// <summary>
        /// The client's email address, if provided. Optional.
        /// </summary>
        public string? Email { get; }

        /// <summary>
        /// The source of the client's registration, indicating where the registration request came from 
        /// (e.g., mobile app, web, etc.). Optional.
        /// </summary>
        public string? RegistrationSource { get; }

        /// <summary>
        /// The client's IP address during registration. Optional and useful for auditing or security purposes.
        /// </summary>
        public string? ClientIpAddress { get; }

        /// <summary>
        /// The date and time the client was registered in UTC format.
        /// Useful for tracking the registration event in the system.
        /// </summary>
        public DateTime RegistrationDateUtc { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientRegistrationData"/> class with the client's details 
        /// and system metadata.
        /// </summary>
        /// <param name="name">The client's name.</param>
        /// <param name="lastName">The client's last name.</param>
        /// <param name="cellPhoneNumber">The client's cellphone number.</param>
        /// <param name="documentType">The client's document type.</param>
        /// <param name="documentNumber">The client's document number.</param>
        /// <param name="reasonOfUse">The client's reason for using Yape.</param>
        /// <param name="email">The client's email address (optional).</param>
        /// <param name="registrationSource">The source of the registration request (optional).</param>
        /// <param name="clientIpAddress">The client's IP address during registration (optional).</param>
        /// <param name="registrationDateUtc">The date and time the client was registered in UTC format.</param>
        public ClientRegistrationData(
            string name,
            string lastName,
            string cellPhoneNumber,
            DocumentTypeEnum documentType,
            string documentNumber,
            string reasonOfUse,
            string? email,
            string? registrationSource,
            string? clientIpAddress,
            DateTime registrationDateUtc)
        {
            Name = name;
            LastName = lastName;
            CellPhoneNumber = cellPhoneNumber;
            DocumentType = documentType;
            DocumentNumber = documentNumber;
            ReasonOfUse = reasonOfUse;
            Email = email;
            RegistrationSource = registrationSource;
            ClientIpAddress = clientIpAddress;
            RegistrationDateUtc = registrationDateUtc;
        }
    }
}
