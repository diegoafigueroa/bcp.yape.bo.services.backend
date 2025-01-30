namespace bcp.yape.bo.core.Entities
{
    /// <summary>
    /// Represents a person with basic information.
    /// </summary>
    public class Person
    {
        /// <summary>
        /// The person's first name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The person's last name.
        /// </summary>
        public string LastName { get; }

        /// <summary>
        /// The person's cellphone number.
        /// </summary>
        public string CellPhoneNumber { get; }

        /// <summary>
        /// The type of identity document.
        /// </summary>
        public DocumentTypeEnum DocumentType { get; }

        /// <summary>
        /// The identity document number.
        /// </summary>
        public string DocumentNumber { get; }

        /// <summary>
        /// Constructor to initialize the person's properties.
        /// </summary>
        /// <param name="name">The person's first name.</param>
        /// <param name="lastName">The person's last name.</param>
        /// <param name="cellPhoneNumber">The person's cellphone number.</param>
        /// <param name="documentType">The type of identity document.</param>
        /// <param name="documentNumber">The identity document number.</param>
        public Person(string name, string lastName, string cellPhoneNumber, DocumentTypeEnum documentType, string documentNumber)
        {
            Name = name;
            LastName = lastName;
            CellPhoneNumber = cellPhoneNumber;
            DocumentType = documentType;
            DocumentNumber = documentNumber;
        }
    }
}
