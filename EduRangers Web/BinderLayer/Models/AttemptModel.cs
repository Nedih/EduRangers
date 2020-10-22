namespace BinderLayer.Models
{
    public class AttemptModel
    {
        public int Id { get; set; }
        public double Mark { get; set; } 
        public bool Result { get; set; }
       // public StudentModel Student { get; set; }
        public TestModel Test { get; set; }
    }
}