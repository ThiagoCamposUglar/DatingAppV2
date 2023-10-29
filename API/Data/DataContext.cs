using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<AppUser> Users { get; set; }
        public DbSet<UserLike> Likes { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<UserLike>()
                    .HasKey(x => new { x.SourceUserId, x.TargetUserId });

            modelBuilder.Entity<UserLike>()
                    .HasOne(x => x.SourceUser)
                    .WithMany(x => x.LikedUsers)
                    .HasForeignKey(x => x.SourceUserId)
                    .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserLike>()
                    .HasOne(x => x.TargetUser)
                    .WithMany(x => x.LikedByUsers)
                    .HasForeignKey(x => x.TargetUserId)
                    .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Message>()
                    .HasOne(x => x.Recipient)
                    .WithMany(x => x.MessagesReceived)
                    .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<Message>()
                    .HasOne(x => x.Sender)
                    .WithMany(x => x.MessagesSent)
                    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}