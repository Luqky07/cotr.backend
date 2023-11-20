using cotr.backend.Model.Tables;
using Microsoft.EntityFrameworkCore;

namespace cotr.backend.Data
{
    public class CotrContext : DbContext
    {
        public CotrContext(DbContextOptions<CotrContext> options) : base(options) { }
        public DbSet<Users> Users { get; set; }
        public DbSet<UserCredential> UserCredential { get; set; }
        public DbSet<Notifications> Notifications { get; set; }
        public DbSet<Groups> Groups { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Members> Members { get; set; }
        public DbSet<Requests> Requests { get; set; }
        public DbSet<GroupUsersBlocked> GroupUsersBlocked { get; set; }
        public DbSet<Languajes> Languajes { get; set; }
        public DbSet<Exercises> Exercises { get; set; }
        public DbSet<ExerciseResources> ExerciseResources { get; set; }
        public DbSet<UserExerciseAttempts> UserExerciseAttempts { get; set; }
        public DbSet<UserExercisesFavourite> UserExercisesFavourites { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>()
                .HasKey(x => x.UserId);
            modelBuilder.Entity<UserCredential>()
                .HasKey(x => x.UserId);
            modelBuilder.Entity<Notifications>()
                .HasKey(x => x.NotificationId);
            modelBuilder.Entity<Groups>()
                .HasKey(x => x.GroupId);
            modelBuilder.Entity<Roles>()
                .HasKey(x => x.RoleId);
            modelBuilder.Entity<Members>()
                .HasKey(x => x.MemberId);
            modelBuilder.Entity<Requests>()
                .HasKey(x => x.RequestId);
            modelBuilder.Entity<GroupUsersBlocked>()
                .HasKey(x => x.BlockedId);
            modelBuilder.Entity<Languajes>()
                .HasKey(x => x.LanguajeId);
            modelBuilder.Entity<Exercises>()
                .HasKey(x => x.ExerciseId);
            modelBuilder.Entity<ExerciseResources>()
                .HasKey(x => x.ResourceId);
            modelBuilder.Entity<UserExerciseAttempts>()
                .HasKey(x => x.AttemptId);
            modelBuilder.Entity<UserExercisesFavourite>()
                .HasKey(x => x.FavoriteId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
