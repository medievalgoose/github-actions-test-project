using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassTest.API.Models
{
    [Table("class")]
    public class Classe
    {
        [Key]
        [Column("id")]
        public string Id { get; set; }

        [Column("class_name")]
        public string ClassName { get; set; } = string.Empty;

        [Column("participant")]
        public int Participant { get; set; }
    }
}
