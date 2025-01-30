using bcp.yape.bo.core.Entities;

namespace bcp.yape.bo.core.Ports.Driving
{
    /// <summary>
    /// Interface representing the service for managing client-related operations.
    /// The service provides methods for adding new clients and performing client-related tasks.
    /// </summary>
    public interface IClientService
    {
        /// <summary>
        /// Registers a new client by validating the provided registration data.
        /// The method performs validation, checks the provided document details, 
        /// and saves the client data to the system if the validation is successful.
        /// </summary>
        /// <param name="client">The client registration data.</param>
        /// <returns>A <see cref="ValidationResult"/> indicating whether the client registration was successful or not, 
        /// along with an appropriate message.</returns>
        Task<ValidationResult> AddClientAsync(ClientRegistrationData client);
    }
}
