using Microsoft.EntityFrameworkCore;

namespace BookingSystem.Infrastructure.Persistance;

public class BookingSystemDbContext : DbContext
{
    public BookingSystemDbContext(DbContextOptions<BookingSystemDbContext> options) 
        : base(options)
    {

    }

    //public DbSet<>
}