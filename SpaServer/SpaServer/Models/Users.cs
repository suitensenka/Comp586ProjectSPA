using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaServer.Models
{
    public partial class Users
    {
        public Users()
        {
            Boards = new HashSet<Boards>();
            Comments = new HashSet<Comments>();
        }

        [Key]
        [Column("user", TypeName = "varchar(255)")]
        public string User { get; set; }
        [Required]
        [Column("username", TypeName = "varchar(45)")]
        public string Username { get; set; }
        [Required]
        [Column("role", TypeName = "varchar(45)")]
        public string Role { get; set; }

        [InverseProperty("UserNavigation")]
        public virtual ICollection<Boards> Boards { get; set; }
        [InverseProperty("UserNavigation")]
        public virtual ICollection<Comments> Comments { get; set; }
    }
}
