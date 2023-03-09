using System;
using System.Collections.Generic;

namespace PRN221_Group3_Project_HotelManagement.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int OrderId { get; set; }
        public string? OrderNote { get; set; }
        public DateTime? OrderCheckin { get; set; }
        public DateTime? OrderCheckout { get; set; }
        public DateTime? OrderCreated { get; set; }
        public short? OrderStatus { get; set; }
        public int? UserId { get; set; }

        public virtual User? User { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
