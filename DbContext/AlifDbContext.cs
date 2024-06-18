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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().ToTable("users")
            .HasOne(e => e.Wallet)
            .WithOne(e => e.User)
            .HasForeignKey<Wallet>(e => e.UserId)
            .IsRequired();
        
        modelBuilder.Entity<Wallet>().ToTable("wallets")
            .HasMany(e => e.Transactions)
            .WithOne(e=>e.Wallet)
            .HasForeignKey(e => e.WalletId)
            .IsRequired();

        modelBuilder.Entity<Transaction>().ToTable("transactions");
    }
}