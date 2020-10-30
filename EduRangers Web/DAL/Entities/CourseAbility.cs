using BinderLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class CourseAbility : IEntity
    {
        public int Id { get; set; }
        public virtual Course Course { get; set; }
        public virtual Ability Ability { get; set; }
    }
}
