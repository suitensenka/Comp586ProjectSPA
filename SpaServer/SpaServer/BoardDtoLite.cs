using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpaServer
{
    public class BoardDtoLite
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public string Owner { get; set; }

    }

    public class BoardDtoDetail
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public string Owner { get; set; }
        public List<CommentDto> Comments { get; set; }
    }
}
