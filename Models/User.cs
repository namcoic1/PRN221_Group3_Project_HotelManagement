using System;
using System.Collections.Generic;

namespace PRN221_Group3_Project_HotelManagement.Models
{
    public partial class User
    {
        public User()
        {
            Orders = new HashSet<Order>();
        }

        public int UserId { get; set; }
        public string? UserPhone { get; set; }
        public string? UserPassword { get; set; }
        public string? UserName { get; set; }
        public string? UserImage { get; set; }
        public decimal? UserWallet { get; set; }
        public short? UserStatus { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
