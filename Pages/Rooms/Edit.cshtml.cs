using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PRN221_Group3_Project_HotelManagement.Models;

namespace PRN221_Group3_Project_HotelManagement.Pages.Rooms
{
    public class EditModel : PageModel
    {
        private readonly PRN221_Group3_Project_HotelManagement.Models.Booking_Hotel_DBContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public EditModel(PRN221_Group3_Project_HotelManagement.Models.Booking_Hotel_DBContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        [BindProperty]
        public RoomHotel Room_Hotel { get; set; }
        [BindProperty]
        public string Status { get; set; }

        List<string> _Status = new List<string>()
        {
                "Active", "Deactive"
        };
        public string Message { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (HttpContext.Session.GetString("user") != null)
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
                Room_Hotel = roomhotel;

                ViewData["ImageRoom"] = Room_Hotel.RoomImage;
                ViewData["TypeId"] = new SelectList(_context.TypeRooms, "TypeId", "TypeName");
                ViewData["StatusRoom"] = new SelectList(_Status);
                return Page();
            }
            return RedirectToPage("/AccessPage/Login");
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            RoomHotel _RoomHotel = _context.RoomHotels.FirstOrDefault(r => r.RoomId == Room_Hotel.RoomId);
            if (Request.Form.Files.Count > 0)
            {
                var FileUpload = Request.Form.Files[0];
                var file = Path.Combine(_hostEnvironment.WebRootPath, "images/rooms", FileUpload.FileName);
                using (var fileStream = new FileStream(file, FileMode.Create))
                {
                    await FileUpload.CopyToAsync(fileStream);

                }
                Room_Hotel.RoomImage = "/images/rooms/" + FileUpload.FileName;
                _RoomHotel.RoomImage = Room_Hotel.RoomImage;
                //_context.Attach(Room_Hotel).State = EntityState.Modified;
            }
            _RoomHotel.RoomName = Room_Hotel.RoomName;
            _RoomHotel.RoomBed = Room_Hotel.RoomBed;
            _RoomHotel.RoomDesc = Room_Hotel.RoomDesc;
            _RoomHotel.RoomStatus = Status.Equals("Active") ? 1 : 0;
            _RoomHotel.RoomPrice = Room_Hotel.RoomPrice;
            _RoomHotel.TypeId = Room_Hotel.TypeId;
            try
            {
                await _context.SaveChangesAsync();
                Message = "Success";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomHotelExists(Room_Hotel.RoomId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            ViewData["ImageRoom"] = _RoomHotel.RoomImage;
            ViewData["TypeId"] = new SelectList(_context.TypeRooms, "TypeId", "TypeName");
            ViewData["StatusRoom"] = new SelectList(_Status);
            return Page();
        }

        private bool RoomHotelExists(int id)
        {
            return _context.RoomHotels.Any(e => e.RoomId == id);
        }
    }
}
