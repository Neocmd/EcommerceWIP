using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Models
{
    [Table("Order")]
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        public DateTime CreateDate { get; set; } = DateTime.UtcNow;

        [Required]
        public int OrderStatusId { get; set; }

        public bool IsDeleted { get; set; }

        public OrderStatus OrderStatus { get; set; } = null!;
        public List<OrderDetail> OrderDetail { get; set; } = new();
    }
}
