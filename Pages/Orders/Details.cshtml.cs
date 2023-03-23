using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_Group3_Project_HotelManagement.Models;

namespace PRN221_Group3_Project_HotelManagement.Pages.Orders
{
    public class DetailsModel : PageModel
    {
        private readonly PRN221_Group3_Project_HotelManagement.Models.Booking_Hotel_DBContext _context;

        public DetailsModel(PRN221_Group3_Project_HotelManagement.Models.Booking_Hotel_DBContext context)
        {
            _context = context;
        }

      public Order Order { get; set; } = default!;

        [BindProperty]
        public User User { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FirstOrDefaultAsync(m => m.OrderId == id);
            var orderdetail = await _context.OrderDetails.FirstOrDefaultAsync(m => m.DetailId == id);
            var room = await _context.RoomHotels.FirstOrDefaultAsync(m => m.RoomId == orderdetail.RoomId);
            ViewData["OrderDetailLists"] = orderdetail;
            ViewData["RoomLists"] = room;
            if (order == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FirstOrDefaultAsync(m => m.UserId == order.UserId);
            if (order == null || user == null )
            {
                return NotFound();
            }
            else 
            {
                Order = order;
                User = user;
            }
            return Page();
        }
    }
}
