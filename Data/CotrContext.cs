using cotr.backend.Model.Tables;
using Microsoft.EntityFrameworkCore;

namespace cotr.backend.Data
{
    public class CotrContext : DbContext
    {
        public CotrContext(DbContextOptions<CotrContext> options) : base(options) { }
        public DbSet<Users> Users { get; set; }
        public DbSet<UserCredential> UserCredential { get; set; }
        public DbSet<UsersToken> UsersTokens { get; set; }
        //public DbSet<Groups> Groups { get; set; }
        //public DbSet<Roles> Roles { get; set; }
        //public DbSet<Members> Members { get; set; }
        //public DbSet<Requests> Requests { get; set; }
        //public DbSet<GroupUsersBlocked> GroupUsersBlocked { get; set; }
        //public DbSet<Notifications> Notifications { get; set; }
        //public DbSet<Languajes> Languajes { get; set; }
        //public DbSet<Exercises> Exercises { get; set; }
        //public DbSet<ExercisesTests> ExercisesTests { get; set; }
        //public DbSet<ExerciseResources> ExerciseResources { get; set; }
        //public DbSet<UserExerciseAttempts> UserExerciseAttempts { get; set; }
        //public DbSet<UserExercisesFavourite> UserExercisesFavourites { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users>()
                .HasKey(uc => uc.UserId);
            modelBuilder.Entity<UserCredential>()
                .HasKey(uc => uc.UserId);
            modelBuilder.Entity<UsersToken>()
                .HasKey(uc => uc.UserId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
