using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

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
        public List<Remedio> Remedios { get; set; }
    }

    public class Remedio
    {
        private string _Medida;

        [Required]
        public string Medida { 
            get
            {
                return _Medida;
            }
            set
            {
                _Medida = value == "ml" ? "ml" : "unidade";
            }
        }

        [Required]
        public double Quantia { get; set; }

        private int _Minutos;

        [Required]
        public string Horario
        {
            get
            {
                return (_Minutos / 60).ToString("D2") + ':' + (_Minutos % 60).ToString("D2");
            }
            set
            {
                int horas = DateTime.ParseExact(value, "HH:mm", CultureInfo.InvariantCulture).Hour * 60;
                int minutos = DateTime.ParseExact(value, "HH:mm", CultureInfo.InvariantCulture).Minute;

                _Minutos = horas + minutos;
            }
        }
    }
}