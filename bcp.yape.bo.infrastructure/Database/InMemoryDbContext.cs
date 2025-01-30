using Microsoft.EntityFrameworkCore;
using bcp.yape.bo.core.Entities;
using bcp.yape.bo.infrastructure.DTO;

namespace bcp.yape.bo.infrastructure.Database
{
    public class InMemoryDbContext : DbContext
    {
        public DbSet<ClientEntityDto> Clients { get; set; }

        public InMemoryDbContext(DbContextOptions<InMemoryDbContext> options) : base(options) { }
    }
}
