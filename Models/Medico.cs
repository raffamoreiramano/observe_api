using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Observe.Models
{
    public class Medico
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int UID { get; set; }

        [ForeignKey("UID")]
        public Usuario Usuario { get; set; }

        [Required]
        [StringLength(13, ErrorMessage = "No máximo 13 caracteres.")]
        public string CRM { get; set; }
    }
}