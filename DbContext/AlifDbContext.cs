using AlifTestTask.Models;

namespace AlifTestTask.DbContext;
using Microsoft.EntityFrameworkCore;

public class AlifDbContext : DbContext
{
    public AlifDbContext(DbContextOptions<AlifDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Wallet> Wallets { get; set; }
}