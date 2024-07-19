using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelWebApi.Domain.Entities
{
    [Table("Task")]
    public class Tasks
    {
        [Key]
        public Guid Id { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Title { get; set; } = null!;

        [Column(TypeName = "varchar(max)")]
        public string Description { get; set; } = null!;

        public Guid UserId { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime CreatedAt { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? UpdatedAt { get; set; }
        
        public virtual User UserNavigation { get; set; } = new User();
    }
}
