using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaServer.Models
{
    public partial class Comments
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("content", TypeName = "text")]
        public string Content { get; set; }
        [Column("idBoard")]
        public int IdBoard { get; set; }
        [Required]
        [Column("user", TypeName = "varchar(255)")]
        public string User { get; set; }

        [ForeignKey(nameof(IdBoard))]
        [InverseProperty(nameof(Boards.Comments))]
        public virtual Boards IdBoardNavigation { get; set; }
        [ForeignKey(nameof(User))]
        [InverseProperty(nameof(Users.Comments))]
        public virtual Users UserNavigation { get; set; }
    }
}
