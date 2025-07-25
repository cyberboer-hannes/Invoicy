﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Invoicy.Infrastructure.Persistence
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer  ("Server=(localdb)\\MSSQLLocalDB;Database=InvoicyDb;Trusted_Connection=True;MultipleActiveResultSets=true");
            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
