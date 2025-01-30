using bcp.yape.bo.core.Entities;

namespace bcp.yape.bo.core.Ports.Driven
{
    /// <summary>
    /// Interface representing the port for interacting with the client repository.
    /// The port provides methods for interacting with the client data store.
    /// It defines operations related to client registration and querying.
    /// </summary>
    public interface IClientRepositoryPort
    {
        /// <summary>
        /// Adds a new client to the repository.
        /// This method is responsible for saving the provided client registration data in the database.
        /// </summary>
        /// <param name="client">The client registration data to be added.</param>
        /// <returns>A <see cref="Guid"/> representing the unique identifier of the added client.</returns>
        Task<Guid> AddClientAsync(ClientRegistrationData client);

        /// <summary>
        /// Checks if a phone number is already registered in the repository.
        /// This method verifies whether the provided phone number is associated with any existing client.
        /// </summary>
        /// <param name="phoneNumber">The phone number to check for registration.</param>
        /// <returns><c>true</c> if the phone number is already registered; otherwise, <c>false</c>.</returns>
        Task<bool> IsPhoneNumberRegisteredAsync(string phoneNumber);
    }
}
