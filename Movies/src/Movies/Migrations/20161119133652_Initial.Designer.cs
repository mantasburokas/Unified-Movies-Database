using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Movies.Contexts;

namespace Movies.Migrations
{
    [DbContext(typeof(MoviesDbContext))]
    [Migration("20161119133652_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1");

            modelBuilder.Entity("Movies.Models.Genre", b =>
                {
                    b.Property<int>("GenreId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("GenreId");

                    b.ToTable("Genres");
                });

            modelBuilder.Entity("Movies.Models.Movie", b =>
                {
                    b.Property<string>("Title");

                    b.Property<string>("Released");

                    b.Property<string>("ImdbRating")
                        .IsRequired();

                    b.Property<string>("Metascore")
                        .IsRequired();

                    b.Property<string>("TomatoMeter")
                        .IsRequired();

                    b.HasKey("Title", "Released");

                    b.HasIndex("ImdbRating");

                    b.HasIndex("Metascore");

                    b.HasIndex("Title");

                    b.HasIndex("TomatoMeter");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("Movies.Models.MovieGenre", b =>
                {
                    b.Property<string>("Title");

                    b.Property<string>("Released");

                    b.Property<int>("GenreId");

                    b.HasKey("Title", "Released", "GenreId");

                    b.HasIndex("GenreId");

                    b.HasIndex("Title", "Released");

                    b.ToTable("MovieGenre");
                });

            modelBuilder.Entity("Movies.Models.MovieGenre", b =>
                {
                    b.HasOne("Movies.Models.Genre", "Genre")
                        .WithMany("MovieGenres")
                        .HasForeignKey("GenreId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Movies.Models.Movie", "Movie")
                        .WithMany("MovieGenres")
                        .HasForeignKey("Title", "Released")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
