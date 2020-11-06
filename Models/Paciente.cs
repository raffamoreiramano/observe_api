using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Observe.Models
{
    public class Paciente
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        public int UID { get; set; }

        [ForeignKey("UID")]
        public Usuario Usuario { get; set; }

        [Required]
        public DateTime Nascimento { get; set; }

        [Column(TypeName = "NVARCHAR(255)")]
        public List<string> Doencas { get; set; }

        [Column(TypeName = "NVARCHAR(255)")]
        public List<string> Alergias { get; set; }

        [Column(TypeName = "NVARCHAR(255)")]
        public List<string> Remedios { get; set; }
    }
}