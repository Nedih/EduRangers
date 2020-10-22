using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class ClientProfile
    {
        [Key]
        [ForeignKey("User")]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public virtual User User { get; set; }
        //public virtual List<Comment> Comments { get; set; }
       /* public ClientProfile()
        {
            List<Comment> Comments = new List<Comment>();
            this.Comments = Comments;
        }*/
    }
}
