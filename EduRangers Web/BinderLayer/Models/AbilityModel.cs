using System.Collections.Generic;

namespace BinderLayer.Models
{
    public class AbilityModel
    {
        public int Id { get; set; }
        public string AbilityDescription { get; set; }
        public UserSlotModel UserSlot { get; set; }
    }
}