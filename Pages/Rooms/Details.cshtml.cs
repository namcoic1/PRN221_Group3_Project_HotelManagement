using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_Group3_Project_HotelManagement.Models;

namespace PRN221_Group3_Project_HotelManagement.Pages.Rooms
{
    public class DetailsModel : PageModel
    {
        private readonly PRN221_Group3_Project_HotelManagement.Models.Booking_Hotel_DBContext _context;

        public DetailsModel(PRN221_Group3_Project_HotelManagement.Models.Booking_Hotel_DBContext context)
        {
            _context = context;
        }

        public RoomHotel RoomHotel { get; set; }
        public TypeRoom TypeRoom { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (HttpContext.Session.GetString("user") != null)
            {
                if (id == null || _context.RoomHotels == null)
                {
                    return NotFound();
                }

                var roomhotel = await _context.RoomHotels.FirstOrDefaultAsync(m => m.RoomId == id);
                var typeroom = await _context.TypeRooms.FirstOrDefaultAsync(t => t.TypeId == roomhotel.TypeId);
                if (roomhotel == null)
                {
                    return NotFound();
                }
                else
                {
                    RoomHotel = roomhotel;
                    TypeRoom = typeroom;
                }
                return Page();
            }
            return RedirectToPage("/AccessPage/Login");
        }
    }
}
