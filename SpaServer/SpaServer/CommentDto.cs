using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaServer
{
    public class CommentDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int IdBoard { get; set; }
        public string BoardName { get; set; }
        public string Owner { get; set; }
    }
}
