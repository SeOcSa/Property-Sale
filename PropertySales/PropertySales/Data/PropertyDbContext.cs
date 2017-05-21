using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
   public class PropertyDbContext : IdentityDbContext
    {
        public PropertyDbContext(DbContextOptions option) : base(option)
        {

        }
        public DbSet<PropertyDetails> PropertyInfos { get; set; }
        public DbSet<SaleDetails> SaleInfo { get; set; }
    }
}
