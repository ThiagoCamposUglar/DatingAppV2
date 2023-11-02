using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class DataContext : IdentityDbContext<AppUser,
                                                AppRole,
                                                int,
                                                IdentityUserClaim<int>, 
                                                AppUserRole, 
                                                IdentityUserLogin<int>, 
                                                IdentityRoleClaim<int>, 
                                                IdentityUserToken<int>>
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<UserLike> Likes { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppUser>()
                    .HasMany(au => au.UserRoles)
                    .WithOne(u => u.User)
                    .HasForeignKey(au => au.UserId)
                    .IsRequired();

            modelBuilder.Entity<AppRole>()
                    .HasMany(ar => ar.UserRoles)
                    .WithOne(r => r.Role)
                    .HasForeignKey(ar => ar.RoleId)
                    .IsRequired();
            
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