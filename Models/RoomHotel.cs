using System;
using System.Collections.Generic;

namespace PRN221_Group3_Project_HotelManagement.Models
{
    public partial class RoomHotel
    {
        public RoomHotel()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int RoomId { get; set; }
        public string? RoomName { get; set; }
        public string? RoomImage { get; set; }
        public short? RoomBed { get; set; }
        public string? RoomDesc { get; set; }
        public decimal? RoomPrice { get; set; }
        public int? RoomStatus { get; set; }
        public int? TypeId { get; set; }

        public virtual TypeRoom? Type { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
