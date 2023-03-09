using System;
using System.Collections.Generic;

namespace PRN221_Group3_Project_HotelManagement.Models
{
    public partial class OrderDetail
    {
        public int DetailId { get; set; }
        public int? RoomId { get; set; }
        public decimal? DetailPrice { get; set; }
        public short? DetailCount { get; set; }
        public int? OrderId { get; set; }

        public virtual Order? Order { get; set; }
        public virtual RoomHotel? Room { get; set; }
    }
}
