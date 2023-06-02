﻿using GistPage.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GistPage.Data
{
    public class ApplicationDbContext:IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }
        public DbSet<ApplicationUser>? ApplicationUsers { get; set; }   
        public DbSet<Post>? Posts { get; set; }
        public DbSet<Page> Pages { get; set; }
    }
}
