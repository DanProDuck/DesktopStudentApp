using DesktopStudentApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace DesktopStudentApp.Data
{
    public class StudentAppDb : DbContext
    {
        public virtual Microsoft.EntityFrameworkCore.DbSet<Student>? Students { get; set; }

        public StudentAppDb(DbContextOptions<StudentAppDb> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
        }
    }
}
