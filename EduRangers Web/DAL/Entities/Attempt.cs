using BinderLayer.Interfaces;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class Attempt : IEntity
    {
        public int Id { get; set; }
        public double Mark { get; set; } 
        public bool Result { get; set; }
        public DateTime DateApplied { get; set; }
        public Student Student { get; set; }
        public Test Test { get; set; }
    }
}