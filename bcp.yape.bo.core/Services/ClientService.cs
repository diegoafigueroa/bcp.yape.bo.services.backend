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
        // TODO: VALIDATE THIS SCENARIO. CREATE A PERSON (CRM !?, EXTERNAL INTEGRATION WITH ANOTHER PORT ?) OR ERROR ?
        if (filterPeople.Count == 0)
            return new ValidationResult(false, "La persona no esta registrada (número de documento y tipo de documento)");

        // IF THERE ARE MULTIPLE RECORDS WITH THE SAME PHONE NUMBER, RETURN AN ERROR
        // IF THERE ARE MULTIPLE RECORDS, RETURN A VALIDATION ERROR
        // TODO: VALIDATE THIS SCENARIOS. IS THIS POSSIBLE ?
        if (filterPeople.Count > 1)
            return new ValidationResult(false, "Se encontraron múltiples registros para la persona.");

        // TODO: ADDITIONAL SCENARIOS TO ASK
        // 1. COUNTRY OR YAPE BLACK-LIST (RISK PROFILE, FRAUD, INTERPOL ? ETC) ?
        // 2. CORPORATIVE CELL PHONE NUMBER ?
        // 3. AGE ? IN FACT, THIS IS A CURRENT VALIDATION ON THE PAGE
        // https://www.yape.com.bo/centro_de_ayuda/crear-tu-cuenta-yape.html#porque-no-puedo-crear-mi-cuenta-en-yape
        // 4. NUMBER OF ALLOWED YAPE ACCOUNTS (6)
        // https://www.yape.com.bo/centro_de_ayuda/crear-tu-cuenta-yape.html#porque-no-puedo-crear-mi-cuenta-en-yape
        // 5. IT'S NOT A BOLIVIAN IDENTITY CARD ? 
        // https://www.yape.com.bo/centro_de_ayuda/crear-tu-cuenta-yape.html#porque-no-puedo-crear-mi-cuenta-en-yape
        // 6. UPDATE THE CLIENT' DATA TO CRM !?
        // 7. VIRTUAL SIM CARDS ?
        // 8. LAWS !? 
        // 9. VALIDATE THE IDENTITY CARD ON GOVERNMENT ? THE IDENTITY CARD IS VALID ?
        // 10. SUSPICIOUS NAMES OR LAST NAMES: JOHN DOE, LA CHILINDRINA, ETC

        // IF VALIDATION IS SUCCESSFUL, PROCEED TO SAVE THE CLIENT
        // TODO. ADDITIONAL SCENARIOS TO ASK
        // 1. SAVE A HISTORICAL CREDIT RANK RECORD !?
        // 2. CATEGORIZE THE CLIENT: VIP, ETC ?
        var clientGuid = await _clientRepositoryPort.AddClientAsync(clientRegistrationData);

        // RETURN SUCCESS STATUS
        return new ValidationResult(true, "", clientGuid);
    }
}