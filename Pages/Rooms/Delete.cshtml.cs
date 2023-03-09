using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_Group3_Project_HotelManagement.Models;

namespace PRN221_Group3_Project_HotelManagement.Pages.Rooms
{
    public class DeleteModel : PageModel
    {
        private readonly PRN221_Group3_Project_HotelManagement.Models.Booking_Hotel_DBContext _context;

        public DeleteModel(PRN221_Group3_Project_HotelManagement.Models.Booking_Hotel_DBContext context)
        {
            _context = context;
        }

        [BindProperty]
      public RoomHotel RoomHotel { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.RoomHotels == null)
            {
                return NotFound();
            }

            var roomhotel = await _context.RoomHotels.FirstOrDefaultAsync(m => m.RoomId == id);

            if (roomhotel == null)
            {
                return NotFound();
            }
            else 
            {
                RoomHotel = roomhotel;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.RoomHotels == null)
            {
                return NotFound();
            }
            var roomhotel = await _context.RoomHotels.FindAsync(id);

            if (roomhotel != null)
            {
                RoomHotel = roomhotel;
                _context.RoomHotels.Remove(RoomHotel);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
