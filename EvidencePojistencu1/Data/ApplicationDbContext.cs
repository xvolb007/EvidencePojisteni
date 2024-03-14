using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EvidencePojistencu1.Models;

namespace EvidencePojistencu1.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<EvidencePojistencu1.Models.Insurance> Insurance { get; set; } = default!;
        public DbSet<EvidencePojistencu1.Models.InsuredPerson> InsuredPerson { get; set; } = default!;
    }
}
