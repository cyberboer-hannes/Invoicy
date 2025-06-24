using Microsoft.EntityFrameworkCore;
using Invoicy.Domain.Invoices;
using Invoicy.Domain.Customers;

namespace Invoicy.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<InvoiceHeader> Invoices => Set<InvoiceHeader>();
    public DbSet<InvoiceDetail> InvoiceDetails => Set<InvoiceDetail>();
    public DbSet<Address> Addresses => Set<Address>();

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>()
            .HasOne(c => c.Address)
            .WithMany()
            .HasForeignKey(c => c.AddressId)
            .IsRequired();

        // Precision for InvoiceDetail fields
        modelBuilder.Entity<InvoiceDetail>(entity =>
        {
            entity.Property(e => e.Quantity).HasPrecision(18, 2);
            entity.Property(e => e.UnitPrice).HasPrecision(18, 2);
        });

        // Relationship between InvoiceHeader and InvoiceDetail
        modelBuilder.Entity<InvoiceDetail>()
            .HasOne(d => d.InvoiceHeader)
            .WithMany(h => h.InvoiceDetails)
            .HasForeignKey(d => d.InvoiceHeaderId)
            .IsRequired();
    }
}
