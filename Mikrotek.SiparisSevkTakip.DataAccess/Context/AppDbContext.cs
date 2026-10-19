using Microsoft.EntityFrameworkCore;
using Mikrotek.SiparisSevkTakip.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mikrotek.SiparisSevkTakip.DataAccess.Context
{
   
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Musteri> Musteri { get; set; }
        public DbSet<Santiye> Santiye { get; set; }
        public DbSet<BetonSinifi> BetonSinifi { get; set; }
        public DbSet<Siparis> Siparis { get; set; }
        public DbSet<Sevkiyat> Sevkiyat { get; set; }
    }
}
