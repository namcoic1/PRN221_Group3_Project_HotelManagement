using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PRN221_Group3_Project_HotelManagement.Models;
using System.ComponentModel.DataAnnotations;

namespace PRN221_Group3_Project_HotelManagement.Pages.Rooms
{
    public class IndexModel : PageModel
    {
        private readonly PRN221_Group3_Project_HotelManagement.Models.Booking_Hotel_DBContext _context;

        public IndexModel(PRN221_Group3_Project_HotelManagement.Models.Booking_Hotel_DBContext context)
        {
            _context = context;
        }

        public IList<RoomHotel> RoomHotel { get; set; } = default!;
        [BindProperty(SupportsGet = true)]
        [Display(Prompt = "name, description, type, status...")]
        [DataType(DataType.Text)]
        public string Search { get; set; }
        public RoomHotel Room { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (HttpContext.Session.GetString("user") != null)
            {
                if (_context.RoomHotels != null)
                {
                    if (String.IsNullOrWhiteSpace(Search))
                    {
                        RoomHotel = await _context.RoomHotels
                        .Include(r => r.Type).ToListAsync();
                    }
                    else
                    {
                        int active = Search == "Active" ? 1 : 0;
                        RoomHotel = await _context.RoomHotels
                        .Include(r => r.Type)
                        .Where(r => r.RoomName.Contains(Search) || r.RoomDesc.Contains(Search) || r.Type.TypeName.Contains(Search) || r.RoomStatus == active)
                        .ToListAsync();
                    }

                    if (HttpContext.Session.GetString("operate") != null)
                    {
                        string json = HttpContext.Session.GetString("operate");
                        Room = JsonConvert.DeserializeObject<RoomHotel>(json);
                    }

                    return Page();
                }
            }
            return RedirectToPage("/AccessPage/Login");
        }
    }
}
