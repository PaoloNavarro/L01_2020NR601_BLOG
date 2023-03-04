using L01_2020NR601.Models;
using Microsoft.EntityFrameworkCore;

namespace WEB_API.Models
{
    public class blogContext : DbContext
    {
        public blogContext(DbContextOptions<blogContext> option):base(option)
        {

        }
        public DbSet<comentarios> comentarios {get;set;}
        public DbSet<publicaciones> publicaciones { get; set; }
        public DbSet<usuarios> usuarios { get; set; }



    }
}
