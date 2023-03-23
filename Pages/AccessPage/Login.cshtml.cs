using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using PRN221_Group3_Project_HotelManagement.Models;
using System.ComponentModel.DataAnnotations;

namespace PRN221_Group3_Project_HotelManagement.Pages.AccessPage
{
    public class LoginModel : PageModel
    {
        private Booking_Hotel_DBContext context = new Booking_Hotel_DBContext();

        [BindProperty]
        [Display(Prompt = "Your phone number")]
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Please enter phone number.")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4,5})$", ErrorMessage = "Phone number must include 10 or 11 digits!")]
        public string UserPhone { get; set; }

        [BindProperty]
        [Display(Prompt = "Your password")]
        [DataType(DataType.Password)]
        [StringLength(30, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters!")]
        [Required(ErrorMessage = "Please enter password.")]
        public string UserPassword { get; set; }

        [BindProperty]
        public bool Remember { get; set; }
        public string Message { get; set; }

        public void OnGet()
        {
            User user = null;
            if (Request.Cookies["remember"] != null && Request.Cookies["check"] != null)
            {
                string json = Request.Cookies["remember"];
                user = JsonConvert.DeserializeObject<User>(json);
                UserPhone = user.UserPhone.Trim();
                UserPassword = user.UserPassword;
                Remember = true;
            }
            else
            {
                Remember = false;
            }
        }
        public async Task<IActionResult> OnPostAsync()
        {
            User _u = context.Users.FirstOrDefault(u => u.UserPhone == UserPhone && u.UserPassword == UserPassword);
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (_u != null)
            {
                var options = new JsonSerializerSettings
                {
                    Formatting = Formatting.Indented,
                    NullValueHandling = NullValueHandling.Ignore,
                };
                string json = JsonConvert.SerializeObject(_u, options);

                if (Remember)
                {
                    CookieOptions option = new CookieOptions();
                    option.Expires = DateTime.Now.AddDays(60);
                    Response.Cookies.Append("remember", json, option);
                    Response.Cookies.Append("check", true.ToString(), option);
                }
                else
                {
                    if (Request.Cookies["remember"] != null) Response.Cookies.Delete("remember");
                    if (Request.Cookies["check"] != null) Response.Cookies.Delete("check");
                }

                HttpContext.Session.SetString("user", json);
                Message = "Success";
            }
            else
            {
                Message = "Failure";
            }
            return Page();
        }
        public async Task<IActionResult> OnGetLogoutAsync()
        {
            HttpContext.Session.Remove("user");
            return RedirectToPage("../Index");
        }
    }
}
