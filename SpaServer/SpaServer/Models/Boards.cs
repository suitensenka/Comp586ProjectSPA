using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaServer.Models
{
    public partial class Boards
    {
        public Boards()
        {
            Comments = new HashSet<Comments>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("subject", TypeName = "varchar(255)")]
        public string Subject { get; set; }
        [Required]
        [Column("content", TypeName = "text")]
        public string Content { get; set; }
        [Required]
        [Column("user", TypeName = "varchar(255)")]
        public string User { get; set; }

        [ForeignKey(nameof(User))]
        [InverseProperty(nameof(Users.Boards))]
        public virtual Users UserNavigation { get; set; }
        [InverseProperty("IdBoardNavigation")]
        public virtual ICollection<Comments> Comments { get; set; }
    }
}
