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
    public class IndexModel : PageModel
    {
        private readonly PRN221_Group3_Project_HotelManagement.Models.Booking_Hotel_DBContext _context;

        public IndexModel(PRN221_Group3_Project_HotelManagement.Models.Booking_Hotel_DBContext context)
        {
            _context = context;
        }

        public IList<OrderDetail> OrderDetail { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.OrderDetails != null)
            {
                OrderDetail = await _context.OrderDetails
                .Include(o => o.Order.User)
                .Include(o => o.Order)
                .Include(o => o.Room).ToListAsync();
            }
        }
    }
}
