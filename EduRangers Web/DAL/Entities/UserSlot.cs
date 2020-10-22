using BinderLayer.Interfaces;

namespace DAL.Entities
{
    public class UserSlot : IEntity
    {
        public int Id { get; set; }
        public Student Owner { get; set; }
        public Ability Ability { get; set; }
        public int count { get; set; }
    }
}