using BinderLayer.Interfaces;

namespace DAL.Entities
{
    public class Attempt : IEntity
    {
        public int Id { get; set; }
        public double Mark { get; set; } 
        public bool Result { get; set; }
        public Student Student { get; set; }
        public Test Test { get; set; }
    }
}