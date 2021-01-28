using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Backend.Models
{
    [Table("Products")]
    public class Product
    {
        [Key]
        [Column("ID")]
        public int ID { get; set; }
        
        [Column("Name")]
        [MaxLength(32)]
        public string Name { get; set; }
        
        [Column("Description")]
        [MaxLength(255)]
        public string Description { get; set; }
        
        [Column("Price")]
        public double Price { get; set; }

        [JsonIgnore]
        [Column("Restaurant")]
        public Restaurant restaurant { get; set; }
    }
}
