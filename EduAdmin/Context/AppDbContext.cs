using EduAdmin.Models;
using Microsoft.EntityFrameworkCore;

namespace EduAdmin.Context
{
    public class AppDbContext :DbContext 
    {

        public AppDbContext(DbContextOptions<AppDbContext> options):base(options) { }

        public DbSet<Estudiante> Estudiante { get; set; }
        public DbSet<AdmitidoComo> AdmitidoComo { get; set; }
        public DbSet<Carrera> Carrera { get; set; }


    }
}
