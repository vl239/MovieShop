using System;
using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data
{
	public class MovieShopDbContext : DbContext
	{
		public MovieShopDbContext(DbContextOptions<MovieShopDbContext> options) : base(options)
		{
		}

        // multiple DbSets

        public DbSet<Genre> Genres { get; set; }

		public DbSet<Movie> Movies { get; set; }

        public DbSet<MovieGenre> MovieGenres { get; set; }

        public DbSet<Trailer> Trailers { get; set; }

        public DbSet<Cast> Casts { get; set; }

        public DbSet<MovieCast> MovieCasts { get; set; }

        public DbSet<Crew> Crews { get; set; }

        public DbSet<MovieCrew> MovieCrews { get; set; }

        public DbSet<Purchase> Purchases { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Favorite> Favorites { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
			modelBuilder.Entity<Movie>(ConfigureMovie);
            modelBuilder.Entity<MovieGenre>(ConfigureMovieGenre);
            modelBuilder.Entity<MovieCast>(ConfigureMovieCast);
            modelBuilder.Entity<MovieCrew>(ConfigureMovieCrew);
            modelBuilder.Entity<Review>(ConfigureReview);
            modelBuilder.Entity<UserRole>(ConfigureUserRole);
        }

        private void ConfigureUserRole(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("UserRoles");
            builder.HasKey(x => new { x.UserId, x.RoleId });
        }

        private void ConfigureReview(EntityTypeBuilder<Review> builder)
        {
            builder.ToTable("Reviews");
            builder.HasKey(x => new { x.MovieId, x.UserId });
        }

        private void ConfigureMovieCrew(EntityTypeBuilder<MovieCrew> builder)
        {
            builder.ToTable("MovieCrews");
            builder.HasKey(x => new { x.MovieId, x.CrewId });
        }

        private void ConfigureMovieCast(EntityTypeBuilder<MovieCast> builder)
        {
            builder.ToTable("MovieCasts");
            builder.HasKey(x => new { x.MovieId, x.CastId });
        }

        private void ConfigureMovieGenre(EntityTypeBuilder<MovieGenre> builder)
        {
            builder.ToTable("MovieGenres");
            builder.HasKey(x => new { x.MovieId, x.GenreId });
        }

        private void ConfigureMovie(EntityTypeBuilder<Movie> builder)
        {
            // specify the Fluent API Rules
            // another way besides data annotations for your schema
            builder.ToTable("Movies");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Title).HasMaxLength(256);
            builder.Property(m => m.Overview).HasMaxLength(4096);
            builder.Property(m => m.Tagline).HasMaxLength(512);
            builder.Property(m => m.ImdbUrl).HasMaxLength(2084);
            builder.Property(m => m.TmdbUrl).HasMaxLength(2084);
            builder.Property(m => m.PosterUrl).HasMaxLength(2084);
            builder.Property(m => m.BackdropUrl).HasMaxLength(2084);
            builder.Property(m => m.OriginalLanguage).HasMaxLength(64);

            builder.Property(m => m.Price).HasColumnType("decimal(5, 2)").HasDefaultValue(9.9m);
            builder.Property(m => m.Budget).HasColumnType("decimal(18, 4)").HasDefaultValue(9.9m);
            builder.Property(m => m.Revenue).HasColumnType("decimal(18, 4)").HasDefaultValue(9.9m);

            builder.Property(m => m.CreatedDate).HasDefaultValueSql("getdate()");

        }
	}
}

