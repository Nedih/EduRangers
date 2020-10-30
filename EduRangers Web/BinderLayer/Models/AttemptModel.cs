using BinderLayer.DTO;

namespace BinderLayer.Models
{
    public class AttemptModel
    {
        public int Id { get; set; }
        public double Mark { get; set; } 
        public bool Result { get; set; }
        public StudentDTO Student { get; set; }
        public TestModel Test { get; set; }
    }
}