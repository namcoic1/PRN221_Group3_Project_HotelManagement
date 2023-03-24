using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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
        [BindProperty]
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

                var options = new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                    NullValueHandling = NullValueHandling.Ignore,
                };
                string json = JsonConvert.SerializeObject(roomhotel, options);
                HttpContext.Session.SetString("operate", json);
            }

            return RedirectToPage("./Index");
        }
    }
}
