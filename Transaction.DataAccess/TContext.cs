using System;
using Microsoft.EntityFrameworkCore;
using Transaction.Domain.Models;

namespace Transaction.DataAccess
{
    public class TContext : DbContext
    {
        public TContext(DbContextOptions<TContext> options)
            : base(options)
        {

        }

        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<Domain.Models.Transaction> Transactions { get; set; }
        public virtual DbSet<Account> Accounts { get; set; }
    }
}
