using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Backend.Models
{
    public class Restaurant
    {
        [Key]
        [Column("ID")]
        public int ID { get; set; }
        
        [Column("Name")]
        [MaxLength(32)]
        public string Name { get; set; }
        
        [Column("Address")]
        [MaxLength(255)]
        public string Address { get; set; }

        [Column("PhoneNumber")]
        [MaxLength(15)]
        public string PhoneNumber { get; set; }

        [Column("Products")]
        public virtual List<Product> Products { get; set; }
    }
}