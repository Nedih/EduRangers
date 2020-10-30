using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinderLayer.Models
{
    public class CourseAbilityModel
    {
        public int Id { get; set; }
        public CourseModel Course { get; set; }
        public AbilityModel Ability { get; set; }
    }
}
