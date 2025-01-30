using bcp.yape.bo.core.Ports.Driven;
using System.ServiceModel;
using Microsoft.Extensions.Configuration;
using bcp.yape.bo.core.Entities;
using bcp.yape.bo.core.Excepcions;

namespace bcp.yape.bo.infrastructure.Adapters
{
    /// <summary>
    /// Adapter class that bridges the gap between the WCF service and the application's port interface for people-related operations.
    /// </summary>
    public class PeopleServiceWcfAdapter : IPeopleServicePort
    {
        private readonly string _serviceUrl;

        /// <summary>
        /// Initializes a new instance of the <see cref="PeopleServiceWcfAdapter"/> class.
        /// </summary>
        /// <param name="configuration">The configuration that provides the WCF service URL.</param>
        public PeopleServiceWcfAdapter(IConfiguration configuration)
        {
            _serviceUrl = configuration["WcfSettings:ServiceUrl"];
        }

        /// <summary>
        /// Asynchronously retrieves a list of people by their phone number by calling the WCF service.
        /// </summary>
        /// <param name="phoneNumber">The phone number to search for.</param>
        /// <returns>A task representing the asynchronous operation, containing a list of <see cref="Person"/> objects.</returns>
        public async Task<IEnumerable<Person>> GetPeopleByPhoneNumberAsync(string phoneNumber)
        {
            try
            {
                // A NEW INSTANCE OF THE PERSONREQUEST IS CREATED TO SEND THE PHONE NUMBER TO THE EXTERNAL WCF SERVICE.
                var peopleRequest = new ExternalPeopleService.PersonRequest();
                peopleRequest.CellPhoneNumber = phoneNumber;

                // SET UP THE BINDING AND ENDPOINT FOR THE WCF SERVICE.
                var binding = new BasicHttpBinding();
                var endpoint = new EndpointAddress(_serviceUrl);

                // USE THE CLIENT TO CALL THE WCF SERVICE
                using var client = new ExternalPeopleService.PeopleServiceClient(binding, endpoint);

                // ASYNCHRONOUSLY CALL THE GETPEOPLEBYPHONENUMBERASYNC METHOD OF THE SERVICE CLIENT.
                var peopleResponse = await client.GetPeopleByPhoneNumberAsync(peopleRequest);

                // CONVERT THE RESPONSE FROM THE WCF SERVICE TO THE APPLICATION'S PERSON MODEL
                var people = new List<Person>();
                foreach (var personResponse in peopleResponse)
                {
                    var person = new Person(
                        name: personResponse.Name,
                        lastName: personResponse.LastName,
                        cellPhoneNumber: personResponse.CellPhoneNumber,
                        documentType: ConvertToDocumentTypeEnum(personResponse.DocumentType),
                        documentNumber: personResponse.DocumentNumber
                    );
                    people.Add(person);
                }

                return people;
            }
            catch (Exception ex)
            {
                throw new ServiceException("Ocurrio un error obteniendo la lista de personas desde el servicio WCF", ex, "PeopleServiceWcfAdapter");
            }
        }

        /// <summary>
        /// Converts the DocumentTypeEnum from the WCF service to the business domain DocumentTypeEnum.
        /// </summary>
        /// <param name="wcfDocumentType">The DocumentTypeEnum from the WCF service.</param>
        /// <returns>The corresponding DocumentTypeEnum in the business domain.</returns>
        private static DocumentTypeEnum ConvertToDocumentTypeEnum(ExternalPeopleService.DocumentTypeEnum wcfDocumentType)
        {
            // PERFORM THE CONVERSION FROM THE WCF DocumentTypeEnum TO THE BUSINESS DOMAIN
            return wcfDocumentType switch
            {
                ExternalPeopleService.DocumentTypeEnum.IdentityCard => DocumentTypeEnum.IdentityCard,
                ExternalPeopleService.DocumentTypeEnum.Passport => DocumentTypeEnum.Passport,
                _ => throw new ArgumentException("Invalid document type", nameof(wcfDocumentType))
            };
        }
    }
}
