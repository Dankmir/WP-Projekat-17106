using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Backend.Models
{
    [Table("CartItem")]
    public class CartItem
    {
        [Key]
        [Column("ID")]
        public int ID { get; set; }
        
        [Column("Quantity")]
        [MaxLength(10)]
        public int Quantity { get; set; }

        [Column("Product")]
        public Product product { get; set; }
    }
}