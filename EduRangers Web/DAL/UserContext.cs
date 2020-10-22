using DAL.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UserContext : IdentityDbContext<User>
    {
        public UserContext() : base("IdentityDb1") { }
        public virtual DbSet<ClientProfile> ClientProfiles { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Professor> Professors { get; set; }
        public DbSet<Ability> Abilities { get; set; }
        public DbSet<Attempt> Attempts { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<UserSlot> UserSlots { get; set; }
    }
}
