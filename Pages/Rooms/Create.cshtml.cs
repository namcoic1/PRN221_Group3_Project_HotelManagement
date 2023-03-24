using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using PRN221_Group3_Project_HotelManagement.Models;

namespace PRN221_Group3_Project_HotelManagement.Pages.Rooms
{
    public class CreateModel : PageModel
    {
        private readonly PRN221_Group3_Project_HotelManagement.Models.Booking_Hotel_DBContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public CreateModel(PRN221_Group3_Project_HotelManagement.Models.Booking_Hotel_DBContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }
        [BindProperty]
        public RoomHotel Room_Hotel { get; set; }
        List<string> _Status = new List<string>()
        {
                "Active", "Deactive"
        };
        [BindProperty]
        public string Status { get; set; }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("user") != null)
            {
                ViewData["TypeId"] = new SelectList(_context.TypeRooms, "TypeId", "TypeName");
                ViewData["StatusRoom"] = new SelectList(_Status);
                return Page();
            }
            return RedirectToPage("/AccessPage/Login");
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (Request.Form.Files.Count > 0)
            {
                var FileUpload = Request.Form.Files[0];
                var file = Path.Combine(_hostEnvironment.WebRootPath, "images/rooms", FileUpload.FileName);
                using (var fileStream = new FileStream(file, FileMode.Create))
                {
                    await FileUpload.CopyToAsync(fileStream);

                }
                Room_Hotel.RoomImage = "/images/rooms/" + FileUpload.FileName;
            }
            Room_Hotel.RoomStatus = Status.Equals("Active") ? 1 : 0;
            _context.RoomHotels.Add(Room_Hotel);
            await _context.SaveChangesAsync();

            var options = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore,
            };
            string json = JsonConvert.SerializeObject(Room_Hotel, options);
            HttpContext.Session.SetString("operate", json);

            return RedirectToPage("./Index");
        }
    }
}
