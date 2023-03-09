using System;
using System.Collections.Generic;

namespace PRN221_Group3_Project_HotelManagement.Models
{
    public partial class TypeRoom
    {
        public TypeRoom()
        {
            RoomHotels = new HashSet<RoomHotel>();
        }

        public int TypeId { get; set; }
        public string? TypeName { get; set; }
        public string? TypeImage { get; set; }
        public string? TypeDesc { get; set; }
        public int? TypeStatus { get; set; }

        public virtual ICollection<RoomHotel> RoomHotels { get; set; }
    }
}
