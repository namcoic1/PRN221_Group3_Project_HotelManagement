using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_Group3_Project_HotelManagement.Models;
using System.ComponentModel.DataAnnotations;

namespace PRN221_Group3_Project_HotelManagement.Pages.Booking
{
    public class RoomDetailModel : PageModel
    {
        private readonly Booking_Hotel_DBContext _context;

        public RoomHotel RoomHotel { get; set; }
        public string TypeName { get; set; }

        public List<RoomHotel> roomRmd { get; set; }

        public RoomDetailModel(Booking_Hotel_DBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int? rid)
        {
            if (rid == null || _context.RoomHotels == null)
            {
                return NotFound();
            }
            var roomhotel = _context.RoomHotels.FirstOrDefault(m => m.RoomId == rid);

            if (roomhotel == null) return NotFound();
            else RoomHotel = roomhotel;

            TypeName = _context.TypeRooms.FirstOrDefault(t => t.TypeId == roomhotel.TypeId).TypeName;
            
            roomRmd = _context.RoomHotels.Where(x => x.RoomBed == roomhotel.RoomBed && x.RoomId != roomhotel.RoomId).Take(6).ToList();

            return Page();
        }
    }
}
