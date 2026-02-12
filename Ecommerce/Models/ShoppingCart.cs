using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Models
{
    [Table("ShoppingCart")]
    public class ShoppingCart
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        public bool IsDeleted { get; set; }

        public ICollection<CartDetail> CartDetails { get; set; } = new List<CartDetail>();
    }
}
