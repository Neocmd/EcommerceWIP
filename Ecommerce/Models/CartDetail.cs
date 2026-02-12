using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Models
{
    [Table("CartDetail")]
    public class CartDetail
    {
        public int Id { get; set; }

        [Required]
        public int ShoppingCartId { get; set; }

        [Required]
        public int BookId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }

        public Book Book { get; set; } = null!;
        public ShoppingCart ShoppingCart { get; set; } = null!;
    }
}
