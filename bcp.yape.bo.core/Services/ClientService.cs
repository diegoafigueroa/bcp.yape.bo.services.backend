using bcp.yape.bo.core.Entities;
using bcp.yape.bo.core.Ports.Driven;
using bcp.yape.bo.core.Ports.Driving;

public class ClientService : IClientService
{
    private readonly IClientRepositoryPort _clientRepositoryPort;
    private readonly IPeopleServicePort _peopleServicePort;

    /// <summary>
    /// Initializes a new instance of the <see cref="ClientService"/> class.
    /// </summary>
    /// <param name="clientRepositoryPort">The repository port used for interacting with client data in the database.</param>
    /// <param name="peopleServicePort">The service port used to interact with external systems to retrieve people's information based on phone number.</param>
    public ClientService(IClientRepositoryPort clientRepositoryPort, IPeopleServicePort peopleServicePort)
    {
        _clientRepositoryPort = clientRepositoryPort;
        _peopleServicePort = peopleServicePort;
    }

    /// <summary>
    /// Registers a new client after validating the provided phone number, document type, and document number.
    /// The method checks if the provided document information matches an existing record via the SOAP service, and if validation is successful, the client is registered in the database.
    /// </summary>
    /// <param name="clientRegistrationData">The data provided by the client during registration, including personal information such as phone number, document type, and document number.</param>
    /// <returns>A <see cref="ValidationResult"/> indicating whether the validation was successful, along with a message describing the outcome.</returns>
    /// <exception cref="ValidationException">Thrown when the document type or number doesn't match the records or when there are multiple records for the same phone number.</exception>
    public async Task<ValidationResult> AddClientAsync(ClientRegistrationData clientRegistrationData)
    {
        // CHECK IF THE PHONE NUMBER IS ALREADY REGISTERED
        bool isPhoneNumberRegistered = await _clientRepositoryPort.IsPhoneNumberRegisteredAsync(clientRegistrationData.CellPhoneNumber);

        // IF THE PHONE NUMBER IS ALREADY REGISTERED, RETURN AN ERROR
        if (isPhoneNumberRegistered)
            return new ValidationResult(false, "El número de celular ya está registrado.");

        // CONSUME THE SOAP SERVICE TO FETCH PEOPLE DATA BY PHONE NUMBER.
        var people = await _peopleServicePort.GetPeopleByPhoneNumberAsync(clientRegistrationData.CellPhoneNumber);

        // CHECK IF THE PERSON EXISTS AND IF THE DOCUMENT TYPE AND DOCUMENT NUMBER MATCH
        var filterPeople = people.Where(person =>
            person.DocumentType == clientRegistrationData.DocumentType &&
            person.DocumentNumber == clientRegistrationData.DocumentNumber)
            .ToList();

        // IF NO VALID PERSON WAS FOUND, RETURN A VALIDATION ERROR
        if (filterPeople.Count == 0)
            return new ValidationResult(false, "La persona no esta registrada (número de documento y tipo de documento)");

        // IF THERE ARE MULTIPLE RECORDS WITH THE SAME PHONE NUMBER, RETURN AN ERROR
        if (filterPeople.Count > 1)
            return new ValidationResult(false, "Se encontraron múltiples registros con el mismo número de teléfono.");

        // IF VALIDATION IS SUCCESSFUL, PROCEED TO SAVE THE CLIENT
        var clientGuid = await _clientRepositoryPort.AddClientAsync(clientRegistrationData);

        // RETURN SUCCESS STATUS
        return new ValidationResult(true, "", clientGuid);
    }
}