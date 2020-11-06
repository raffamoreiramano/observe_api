using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Observe.Models
{
    public class Receita
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int MID { get; set; }

        [ForeignKey("MID")]
        public Medico Medico { get; set; }

        public int PID { get; set; }

        [ForeignKey("PID")]
        public Paciente Paciente { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR(MAX)")]
        public List<string> Remedios { get; set; }
    }
}