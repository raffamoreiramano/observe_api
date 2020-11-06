using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Observe.Models
{
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [StringLength(128, ErrorMessage = "No máximo 128 caracteres.")]
        public string CID { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "No máximo 50 caracteres.")]
        public string Nome { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "No máximo 50 caracteres.")]
        [Column("Sobrenome")]
        public string Sobrenome { get; set; }

        [Display(Name = "Nome inteiro")]
        public string NomeInteiro
        {
            get
            {
                return Nome + " " + Sobrenome;
            }
        }
    }
}