using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinderLayer.Models
{
    public class QuestionModelMap
    {
        public int Id { get; set; }
        public int TestId { get; set; }
        public string QuestionText { get; set; }
        public List<AnswerModel> Answers { get; set; }
    }
}
