using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_Group3_Project_HotelManagement.Models;

namespace PRN221_Group3_Project_HotelManagement.Pages.Bookings
{
    public class DetailsModel : PageModel
    {
        private readonly PRN221_Group3_Project_HotelManagement.Models.Booking_Hotel_DBContext _context;

        public DetailsModel(PRN221_Group3_Project_HotelManagement.Models.Booking_Hotel_DBContext context)
        {
            _context = context;
        }

      public OrderDetail OrderDetail { get; set; } = default!; 
        public Order Order { get; set; } = default!;
        public RoomHotel RoomHotel { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.OrderDetails == null)
            {
                return NotFound();
            }

            var orderdetail = await _context.OrderDetails.FirstOrDefaultAsync(m => m.DetailId == id);
            var order = await _context.Orders.FirstOrDefaultAsync(m => m.OrderId == orderdetail.OrderId);
            var room = await _context.RoomHotels.FirstOrDefaultAsync(m => m.RoomId == orderdetail.RoomId);
            if (orderdetail == null || order == null || room == null)
            {
                return NotFound();
            }
            else 
            {
                OrderDetail = orderdetail;
                Order = order;
                RoomHotel= room;
            }
            return Page();
        }
    }
}
