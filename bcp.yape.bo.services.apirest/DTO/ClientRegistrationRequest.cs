using bcp.yape.bo.core.Entities;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace bcp.yape.bo.services.apirest.DTO
{
    /// <summary>
    /// Represents the required data to register a Yape client in the system.
    /// This class is used to receive the input data in an HTTP request for registering a new client.
    /// </summary>
    public class ClientRegistrationRequest
    {
        /// <summary>
        /// Gets or sets the client's first name.
        /// The name must have a minimum length of 2 characters and a maximum length of 100 characters.
        /// </summary>
        [Required(ErrorMessage = "El nombre es requerido")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Por favor, ingresa un nombre valido")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the client's last name.
        /// The last name must have a minimum length of 2 characters and a maximum length of 100 characters.
        /// </summary>
        [Required(ErrorMessage = "El apellido es requerido")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Por favor, ingresa un apellido valido")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the client's cell phone number.
        /// The number must be exactly 8 digits long and must start with a 6 or 7.
        /// </summary>
        [Required(ErrorMessage = "Es necesario que ingreses un nro. de celular")]
        [StringLength(8, MinimumLength = 8, ErrorMessage = "La longitud del celular no es valida")]
        [RegularExpression(@"^([67]\d{7})$", ErrorMessage = "Por favor, ingresa un número de de celular valido")]
        public string CellPhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the client's document type.
        /// </summary>
        [Required(ErrorMessage = "El tipo de documento es requerido")]
        [EnumDataType(typeof(DocumentTypeEnum), ErrorMessage = "El tipo de documento debe ser uno de los valores válidos")]
        public DocumentTypeEnum DocumentType { get; set; }

        /// <summary>
        /// Gets or sets the client's document number.
        /// The number must be a numeric value between 5 and 11 digits long.
        /// </summary>
        [Required(ErrorMessage = "El Nro. de documento es requerido")]
        [StringLength(11, MinimumLength = 5, ErrorMessage = "Por favor, ingresa un Nro. de documento valido")]
        [RegularExpression(@"^\d+$", ErrorMessage = "El Nro. de documento no es valido")]
        public string DocumentNumber { get; set; }

        /// <summary>
        /// Gets or sets the reason why the client is requesting the service.
        /// The reason must have a minimum length of 5 characters and a maximum length of 100 characters.
        /// </summary>
        [Required(ErrorMessage = "El motivo de uso es requerido")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Por favor, ingresa un motivo de uso valido")]
        public string ReasonOfUse { get; set; }


        //// OPTIONAL FIELDS SUGGESTED ... 
        //// OPTIONAL FIELDS SUGGESTED ... 

        /// <summary>
        /// Gets or sets the client's email address (optional).
        /// </summary>
        // TODO: This field is mandatory in the app and in the YouTube videos
        // TODO: Add email format validation if necessary
        public string? Email { get; set; }

        /// <summary>
        /// Gets or sets the origin of the registration request (optional).
        /// It indicates the source from which the registration is made (e.g., Web, App, etc.).
        /// </summary>
        // TODO: Add validations for the source if necessary
        public string? RegistrationSource { get; set; }

        //// OPTIONAL FIELDS SUGGESTED ... 
        //// OPTIONAL FIELDS SUGGESTED ... 

        /// <summary>
        /// Converts this registration request into a `ClientRegistrationData` object (domain object),
        /// which is used to perform the registration process in the system.
        /// </summary>
        /// <param name="clientIpAddress">The IP address from where the registration is being made.</param>
        /// <returns>A `ClientRegistrationData` object with the registration details.</returns>
        public ClientRegistrationData ToClientRegistrationData(string? clientIpAddress)
        {
            var registrationDateUtc = DateTime.UtcNow;

            // NAMED ARGUMENTS USED FOR CLARITY. DUE TO THE MANY PARAMETERS IN THE CONSTRUCTOR
            return new ClientRegistrationData(
                name: Name,
                lastName: LastName,
                cellPhoneNumber: CellPhoneNumber,
                documentType: DocumentType,
                documentNumber: DocumentNumber,
                reasonOfUse: ReasonOfUse,
                email: Email,
                registrationSource: RegistrationSource,
                clientIpAddress: clientIpAddress,
                registrationDateUtc: registrationDateUtc
            );
        }
    }
}
