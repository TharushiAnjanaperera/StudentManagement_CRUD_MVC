using Microsoft.EntityFrameworkCore;
using StudentManagement.web.Models.Entities;

namespace StudentManagement.web.Data

{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options): base(options) 
        {
            
        }

        public DbSet<Student> Students { get; set; }
    }
}
