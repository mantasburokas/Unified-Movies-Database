using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Movies.Contexts;

namespace Movies.Migrations
{
    [DbContext(typeof(MoviesDbContext))]
    partial class MoviesDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1");

            modelBuilder.Entity("Movies.Models.Pocos.Genre", b =>
                {
                    b.Property<int>("GenreId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 64);

                    b.HasKey("GenreId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("Movies.Models.Pocos.Movie", b =>
                {
                    b.Property<string>("Title")
                        .HasAnnotation("MaxLength", 128);

                    b.Property<string>("Released");

                    b.Property<string>("Awards")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 128);

                    b.Property<string>("Director")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 64);

                    b.Property<string>("ImdbRating")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 4);

                    b.Property<string>("ImdbVotes")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 128);

                    b.Property<string>("Metascore")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 4);

                    b.Property<string>("Plot")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 512);

                    b.Property<string>("Runtime")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 32);

                    b.Property<string>("TomatoMeter")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 4);

                    b.HasKey("Title", "Released");

                    b.HasIndex("ImdbRating");

                    b.HasIndex("ImdbVotes");

                    b.HasIndex("Metascore");

                    b.HasIndex("TomatoMeter");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("Movies.Models.Pocos.MovieGenre", b =>
                {
                    b.Property<string>("Title");

                    b.Property<string>("Released");

                    b.Property<int>("GenreId");

                    b.HasKey("Title", "Released", "GenreId");

                    b.HasIndex("GenreId");

                    b.HasIndex("Title", "Released");

                    b.ToTable("MovieGenre");
                });

            modelBuilder.Entity("Movies.Models.Pocos.MovieGenre", b =>
                {
                    b.HasOne("Movies.Models.Pocos.Genre", "Genre")
                        .WithMany("MovieGenres")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Movies.Models.Pocos.Movie", "Movie")
                        .WithMany("MovieGenres")
                        .HasForeignKey("Title", "Released")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
