using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FilghtTicketApplication.Models;

namespace FilghtTicketApplication.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<FilghtTicketApplication.Models.Flight> Flight { get; set; }
        public DbSet<FilghtTicketApplication.Models.Seat> Seat { get; set; }
        public DbSet<FilghtTicketApplication.Models.Ticket> Ticket { get; set; }
    }
}