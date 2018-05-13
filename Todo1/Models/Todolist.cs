using System;
using System.Collections.Generic;

namespace Todo1.Models
{
    public partial class Todolist
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool? IsCompleted { get; set; }
        
    }
}
