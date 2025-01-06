using LibraryAppWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace LibraryAppWebAPI.Data
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options)
    : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.LogTo(msg => Debug.WriteLine(msg));
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<LibraryUser> LibraryUsers { get; set; }
    }
}
