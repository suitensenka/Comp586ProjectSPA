using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpaServer.Models;

namespace SpaServer
{
    public class UserDto
    {
        public string User { get; set; }
        public string Username { get; set; }
        public List<BoardDtoLite> Boards { get; set; }
        public List<CommentDto> Comments { get; set; }
    }
}
