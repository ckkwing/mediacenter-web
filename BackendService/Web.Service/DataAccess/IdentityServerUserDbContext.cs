﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Web.Service.DataAccess.Entity;

namespace Web.Service.DataAccess
{
    public class IdentityServerUserDbContext : IdentityDbContext<User, Role, string>
    {
        public IdentityServerUserDbContext(DbContextOptions<IdentityServerUserDbContext> options) 
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            base.OnModelCreating(builder);
        }

    }
}
