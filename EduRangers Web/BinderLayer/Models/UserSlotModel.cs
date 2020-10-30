using BinderLayer.DTO;

namespace BinderLayer.Models
{
    public class UserSlotModel
    {
        public int Id { get; set; }
        public StudentDTO Owner { get; set; }
        public AbilityModel Ability { get; set; }
        public int Count { get; set; }
    }
}