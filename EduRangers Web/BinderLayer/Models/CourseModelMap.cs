using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinderLayer.Models
{
    public class CourseModelMap
    {
        public int Id { get; set; }
        public string AuthorEmail { get; set; }
        public string CourseName { get; set; }
        public string CourseDescription { get; set; }
    }
}
