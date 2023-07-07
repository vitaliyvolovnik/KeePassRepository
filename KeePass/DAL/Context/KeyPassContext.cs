using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Context
{
    public class KeyPassContext:DbContext
    {

        public KeyPassContext(DbContextOptions<KeyPassContext> dbContextOptions):
            base(dbContextOptions)
        { 
        }


        public DbSet<User> Users { get; set; }
        public DbSet<Collection> Collections { get; set; }
        public DbSet<Folder> Folders { get; set; }
        public DbSet<Note> Notes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureUser(modelBuilder.Entity<User>());
            ConfigureFolder(modelBuilder.Entity<Folder>());
            ConfigureCollection(modelBuilder.Entity<Collection>());

            base.OnModelCreating(modelBuilder);
        }

        public void ConfigureUser(EntityTypeBuilder<User> entityTypeBuilder)
        {
            entityTypeBuilder
                .HasMany(x => x.Folders)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .IsRequired();
        }
        public void ConfigureFolder(EntityTypeBuilder<Folder> entityTypeBuilder)
        {
            entityTypeBuilder
                .HasMany(x => x.Collections)
                .WithOne(x => x.Folder)
                .HasForeignKey(x => x.FolderId)
                .IsRequired();
        }
        public void ConfigureCollection(EntityTypeBuilder<Collection> entityTypeBuilder)
        {
            entityTypeBuilder
                .HasMany(x => x.Notes)
                .WithOne(x => x.Collrction)
                .HasForeignKey(x => x.CollectionId)
                .IsRequired();
        }

    }
}
