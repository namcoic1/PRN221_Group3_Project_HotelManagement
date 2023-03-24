using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_Group3_Project_HotelManagement.Models;

namespace PRN221_Group3_Project_HotelManagement.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private Booking_Hotel_DBContext context = new Booking_Hotel_DBContext();

        public List<TypeRoom> typeRooms { get; set; }
        public List<RoomHotel> roomHotels { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            typeRooms = context.TypeRooms.ToList();
            roomHotels = context.RoomHotels.Where(r => r.TypeId == 6).ToList();
        }
    }
}