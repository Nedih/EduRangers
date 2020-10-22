using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EduRangersWeb.Models
{
    public class ApplicationContext : IdentityDbContext
    {
        public ApplicationContext() : base("EduRangaers_TestDB"){}
        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Professor> Professors { get; set; }
        public DbSet<Ability> Abilities { get; set; }
        public DbSet<Attempt> Attempts { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<UserSlot> UserSlots { get; set; }
        public static ApplicationContext Create()
        {
          return new ApplicationContext();
        }
    }
}