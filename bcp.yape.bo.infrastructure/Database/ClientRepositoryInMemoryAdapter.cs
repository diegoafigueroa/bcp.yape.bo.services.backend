using bcp.yape.bo.core.Entities;
using bcp.yape.bo.core.Ports.Driven;
using Microsoft.EntityFrameworkCore;
using bcp.yape.bo.infrastructure.DTO;

namespace bcp.yape.bo.infrastructure.Database
{
    public class ClientRepositoryInMemoryAdapter : IClientRepositoryPort
    {
        private readonly InMemoryDbContext _dbContext;

        public ClientRepositoryInMemoryAdapter(InMemoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Adds a new client to the in-memory database.
        /// </summary>
        /// <param name="client">The client registration data to be added.</param>
        /// <returns>The GUID of the newly added client.</returns>
        public async Task<Guid> AddClientAsync(ClientRegistrationData client)
        {
            // CONVERT THE INCOMING ClientRegistrationData TO A ClientEntityDto FOR STORAGE IN THE DATABASE
            var clientEntity = new ClientEntityDto
            {
                Id = Guid.NewGuid(),
                Name = client.Name,
                LastName = client.LastName,
                CellPhoneNumber = client.CellPhoneNumber,
                DocumentType = client.DocumentType.ToString(),
                DocumentNumber = client.DocumentNumber,
                ReasonOfUse = client.ReasonOfUse,
                Email = client.Email,
                RegistrationSource = client.RegistrationSource,
                ClientIpAddress = client.ClientIpAddress,
                RegistrationDateUtc = client.RegistrationDateUtc
            };

            // ADD THE CLIENT ENTITY TO THE IN-MEMORY DATABASE
            _dbContext.Clients.Add(clientEntity);

            // SAVE CHANGES TO THE IN-MEMORY DATABASE
            await _dbContext.SaveChangesAsync();

            // RETURN THE CLIENT'S GUID (WHICH WAS GENERATED IN THE DATABASE)
            return clientEntity.Id;
        }

        public async Task<bool> IsPhoneNumberRegisteredAsync(string phoneNumber)
        {
            // CHECK IF THERE IS ANY CLIENT IN THE DATABASE WITH THE PROVIDED PHONE NUMBER
            var client = await _dbContext.Clients
                                         .FirstOrDefaultAsync(c => c.CellPhoneNumber == phoneNumber);

            // RETURN TRUE IF A CLIENT WITH THE GIVEN PHONE NUMBER IS FOUND, OTHERWISE FALSE
            return client != null;
        }
    }
}
