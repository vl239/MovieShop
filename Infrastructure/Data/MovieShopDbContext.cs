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

        public DbSet<CastMember> CastMembers { get; set; }

        public DbSet<MovieCastMember> MovieCastMembers { get; set; }

        public DbSet<CrewMember> CrewMembers { get; set; }

        public DbSet<MovieCrewMember> MovieCrewMembers { get; set; }

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
            modelBuilder.Entity<MovieCastMember>(ConfigureMovieCastMember);
            modelBuilder.Entity<MovieCrewMember>(ConfigureMovieCrewMember);
            modelBuilder.Entity<Review>(ConfigureReview);
            modelBuilder.Entity<UserRole>(ConfigureUserRole);
        }

        private void ConfigureUserRole(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("UserRole");
            builder.HasKey(x => new { x.UserId, x.RoleId });
        }

        private void ConfigureReview(EntityTypeBuilder<Review> builder)
        {
            builder.ToTable("Review");
            builder.HasKey(x => new { x.MovieId, x.UserId });
        }

        private void ConfigureMovieCrewMember(EntityTypeBuilder<MovieCrewMember> builder)
        {
            builder.ToTable("MovieCrewMember");
            builder.HasKey(x => new { x.MovieId, x.CrewId });
        }

        private void ConfigureMovieCastMember(EntityTypeBuilder<MovieCastMember> builder)
        {
            builder.ToTable("MovieCastMember");
            builder.HasKey(x => new { x.MovieId, x.CastId });
        }

        private void ConfigureMovieGenre(EntityTypeBuilder<MovieGenre> builder)
        {
            builder.ToTable("MovieGenre");
            builder.HasKey(x => new { x.MovieId, x.GenreId });
        }

        private void ConfigureMovie(EntityTypeBuilder<Movie> builder)
        {
            // specify the Fluent API Rules
            // another way besides data annotations for your schema
            builder.ToTable("Movie");
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

