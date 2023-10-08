using Microsoft.EntityFrameworkCore;

namespace Messaging.Intraestructure.MySql
{
    public class MessageAppDbContext : DbContext
    {
        public DbSet<EmailAddressDTO> EmailAddresses { get; set; }
        public DbSet<MessageDTO> Messages { get; set; }
        public DbSet<EmailRecipientDTO> EmailRecipients { get; set; }

        public MessageAppDbContext(DbContextOptions<MessageAppDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string server = Environment.GetEnvironmentVariable("MYSQL_SERVER");
            string database = Environment.GetEnvironmentVariable("MYSQL_DATABASE");
            string uid = Environment.GetEnvironmentVariable("MYSQL_USER");
            string password = Environment.GetEnvironmentVariable("MYSQL_PASSWORD");

            string connectionString = $"server={server};database={database};user={uid};password={password};";

            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmailRecipientDTO>()
                .HasKey(e => new { e.MessageId, e.RecipientId });

            modelBuilder.Entity<EmailRecipientDTO>()
                .HasOne(er => er.Message)
                .WithMany()
                .HasForeignKey(er => er.MessageId);

            modelBuilder.Entity<EmailRecipientDTO>()
                .HasOne(er => er.Recipient)
                .WithMany()
                .HasForeignKey(er => er.RecipientId);

            modelBuilder.Entity<MessageDTO>()
                .HasOne(m => m.Sender)
                .WithMany()
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
