using HomeAssistant.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HomeAssistant.Services;

internal class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<Relay> Relays { get; set; }
}
