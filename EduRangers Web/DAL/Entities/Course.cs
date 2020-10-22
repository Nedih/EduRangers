using BinderLayer.Interfaces;
using System.Collections.Generic;

namespace DAL.Entities
{
    public class Course : IEntity
    {
        public int Id { get; set; }
        public Professor Author { get; set; }
        public string CourseName { get; set; }
        public string CourseDescription { get; set; }
        public List<Test> Tests { get; set; }
        public List<Ability> BannedAbilities { get; set; }
    }
}