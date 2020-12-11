using BinderLayer.Interfaces;
using System.Collections.Generic;

namespace DAL.Entities
{
    public class Ability : IEntity
    {
        public int Id { get; set; }
        public string AbilityName { get; set; }
        public string AbilityDescription { get; set; }
        public List<CourseAbility> Courses { get; set; }
        public List<UserSlot> UserSlots { get; set; }
    }
}