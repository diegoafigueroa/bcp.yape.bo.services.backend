using bcp.yape.bo.core.Entities;

namespace bcp.yape.bo.core.Ports.Driven
{
    /// <summary>
    /// Interface representing the port for interacting with the external people service.
    /// The port provides methods for retrieving people data based on phone number.
    /// </summary>
    public interface IPeopleServicePort
    {
        /// <summary>
        /// Retrieves a list of people based on their phone number.
        /// This method is responsible for fetching data from the external service (e.g., SOAP service).
        /// </summary>
        /// <param name="phoneNumber">The phone number used to search for people.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation. The result is an <see cref="IEnumerable{Person}"/> that contains the list of people matching the provided phone number.</returns>
        Task<IEnumerable<Person>> GetPeopleByPhoneNumberAsync(string phoneNumber);
    }
}
