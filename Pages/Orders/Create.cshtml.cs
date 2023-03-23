using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualBasic;
using PRN221_Group3_Project_HotelManagement.Models;

namespace PRN221_Group3_Project_HotelManagement.Pages.Orders
{
    public class CreateModel : PageModel
    {
        private readonly PRN221_Group3_Project_HotelManagement.Models.Booking_Hotel_DBContext _context;

        public CreateModel(PRN221_Group3_Project_HotelManagement.Models.Booking_Hotel_DBContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            var todayDate = DateTime.Now;
            string strToday = todayDate.ToString("MM/dd/yyyy h:mm tt");
            ViewData["UserName"] = new SelectList(_context.Users, "UserId", "UserName");
            ViewData["date"] = strToday;
            return Page();
        }

        [BindProperty]
        public Order Order { get; set; } = default!;

        [BindProperty]
        public string selected { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Orders == null || Order == null)
            {
                return Page();
            }

            var todayDate = DateTime.Now;
            string strToday = todayDate.ToString("MM/dd/yyyy h:mm tt");
            Order.OrderCreated = todayDate;
            short status = short.Parse(selected);
            Order.OrderStatus = status;
            _context.Orders.Add(Order);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
