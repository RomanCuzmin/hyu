using BankKapital.Models;
using Microsoft.EntityFrameworkCore;

namespace BankCapital.Services
{
    //контекест баззы данных
    public class DataBaseContext : DbContext
    {
        public DbSet<BankAccount> BankAccounts => Set<BankAccount>();
        public DbSet<User> Users => Set<User>();
        public DbSet<BankCard> BankCards => Set<BankCard>();
        public DbSet<TransactionBank> Transactions => Set<TransactionBank>();
        public DataBaseContext() => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=BankCapital/DataBase/CapitalBank.db");
        }

        public override void Dispose()
        {
            base.Dispose();
            
        }
    }
}