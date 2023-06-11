using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using SpellSmarty.Infrastructure.DataModels;

namespace SpellSmarty.Infrastructure.Data
{
    public partial class SpellSmartyContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public SpellSmartyContext()
        {
        }

        public SpellSmartyContext(DbContextOptions<SpellSmartyContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<Feedback> Feedbacks { get; set; } = null!;
        public virtual DbSet<Genre> Genres { get; set; } = null!;
        public virtual DbSet<Level> Levels { get; set; } = null!;
        public virtual DbSet<Plan> Plans { get; set; } = null!;
        public virtual DbSet<Video> Videos { get; set; } = null!;
        public virtual DbSet<VideoGenre> VideoGenres { get; set; } = null!;
        public virtual DbSet<VideoStat> VideoStats { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = _configuration.GetConnectionString("SpellSmarty");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasColumnName("email");

                entity.Property(e => e.EmailVerify).HasColumnName("email_verify");

                entity.Property(e => e.EndDate)
                    .HasColumnType("datetime")
                    .HasColumnName("end_date");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .HasColumnName("password");

                entity.Property(e => e.Planid)
                    .HasColumnName("planid")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.SubribeDate)
                    .HasColumnType("datetime")
                    .HasColumnName("subribe_date");

                entity.Property(e => e.Username)
                    .HasMaxLength(255)
                    .HasColumnName("username");

                entity.HasOne(d => d.Plan)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.Planid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Accounts__planid__267ABA7A");
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.Property(e => e.FeedbackId).HasColumnName("feedback_id");

                entity.Property(e => e.AccountId).HasColumnName("account_id");

                entity.Property(e => e.Content)
                    .HasMaxLength(255)
                    .HasColumnName("content");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasColumnName("date");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .HasColumnName("title");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Feedbacks__accou__403A8C7D");
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.Property(e => e.GenreId).HasColumnName("genre_id");

                entity.Property(e => e.GenreName)
                    .HasMaxLength(255)
                    .HasColumnName("genre_name");
            });

            modelBuilder.Entity<Level>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Plan>(entity =>
            {
                entity.Property(e => e.Planid).HasColumnName("planid");

                entity.Property(e => e.PlanName)
                    .HasMaxLength(255)
                    .HasColumnName("plan_name");
            });

            modelBuilder.Entity<Video>(entity =>
            {
                entity.Property(e => e.Videoid).HasColumnName("videoid");

                entity.Property(e => e.AddedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("added_date");

                entity.Property(e => e.ChannelName)
                    .HasMaxLength(255)
                    .HasColumnName("channel_name");

                entity.Property(e => e.LearntCount).HasColumnName("learnt_count");

                entity.Property(e => e.Level).HasColumnName("level");

                entity.Property(e => e.Premium).HasColumnName("premium");

                entity.Property(e => e.Rating).HasColumnName("rating");

                entity.Property(e => e.SrcId)
                    .HasMaxLength(255)
                    .HasColumnName("src_id");

                entity.Property(e => e.Subtitle).HasColumnName("subtitle");

                entity.Property(e => e.ThumbnailLink)
                    .HasMaxLength(255)
                    .HasColumnName("thumbnail_link");

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .HasColumnName("title");

                entity.Property(e => e.VideoDescription)
                    .HasMaxLength(255)
                    .HasColumnName("video_description");

                entity.HasOne(d => d.LevelNavigation)
                    .WithMany(p => p.Videos)
                    .HasForeignKey(d => d.Level)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Videos__level__6EF57B66");
            });

            modelBuilder.Entity<VideoGenre>(entity =>
            {
                entity.ToTable("VideoGenre");

                entity.Property(e => e.VideoGenreId).HasColumnName("video_genre_id");

                entity.Property(e => e.GenreId).HasColumnName("genre_id");

                entity.Property(e => e.VideoId).HasColumnName("video_id");

                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.VideoGenres)
                    .HasForeignKey(d => d.GenreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__VideoGenr__genre__6D0D32F4");

                entity.HasOne(d => d.Video)
                    .WithMany(p => p.VideoGenres)
                    .HasForeignKey(d => d.VideoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__VideoGenr__video__3B75D760");
            });

            modelBuilder.Entity<VideoStat>(entity =>
            {
                entity.HasKey(e => e.StatId)
                    .HasName("PK__VideoSta__B8A525606C1C7598");

                entity.ToTable("VideoStat");

                entity.Property(e => e.StatId).HasColumnName("stat_id");

                entity.Property(e => e.AccountId).HasColumnName("account_id");

                entity.Property(e => e.Progress).HasColumnName("progress");

                entity.Property(e => e.VideoId).HasColumnName("video_id");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.VideoStats)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__VideoStat__accou__35BCFE0A");

                entity.HasOne(d => d.Video)
                    .WithMany(p => p.VideoStats)
                    .HasForeignKey(d => d.VideoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__VideoStat__video__36B12243");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
