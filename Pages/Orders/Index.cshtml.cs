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
    public class IndexModel : PageModel
    {
        private readonly PRN221_Group3_Project_HotelManagement.Models.Booking_Hotel_DBContext _context;

        public IndexModel(PRN221_Group3_Project_HotelManagement.Models.Booking_Hotel_DBContext context)
        {
            _context = context;
        }

        public IList<Order> Order { get;set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public async Task OnGetAsync()
        {
            if (_context.Orders != null)
            {
                Order = await _context.Orders
                .Include(o => o.User).ToListAsync();
            }
            IQueryable<Order> orderIQ = from s in _context.Orders
                                      select s;
            if (!String.IsNullOrEmpty(SearchString))
            {
                orderIQ = orderIQ.Where(orderIQ => orderIQ.User.UserName.Contains(SearchString));
            }
            Order = orderIQ.ToList();
        }
    }
}
