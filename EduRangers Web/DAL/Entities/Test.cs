using BinderLayer.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class Test : IEntity
    {
        public int Id { get; set; }
        public string TestName { get; set; }
        public string TestDescription { get; set; }
        public Course Course { get; set; }
        public List<Question> Questions { get; set; }
    }
}