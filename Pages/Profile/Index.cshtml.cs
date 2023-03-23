using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using PRN221_Group3_Project_HotelManagement.Models;
using System.ComponentModel.DataAnnotations;

namespace PRN221_Group3_Project_HotelManagement.Pages.Profile
{
    public class IndexModel : PageModel
    {
        private IWebHostEnvironment webHostEnvironment;
        private Booking_Hotel_DBContext context = new Booking_Hotel_DBContext();

        public IndexModel(IWebHostEnvironment environment)
        {
            webHostEnvironment = environment;
        }

        public String img_profile { get; set; }

        [BindProperty]
        [Display(Prompt = "Enter your phone number")]
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Please enter phone number.")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4,5})$", ErrorMessage = "Phone number must include 10 or 11 digits!")]
        public String user_phone { get; set; }

        [BindProperty]
        [Display(Prompt = "Enter your username")]
        [DataType(DataType.Text)]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Name must be at least 2 characters!")]
        [Required(ErrorMessage = "Please enter name.")]
        public String user_name { get; set; }

        public decimal? user_wallet { get; set; }

        [BindProperty]
        public int user_id { get; set; }

        [BindProperty]
        [Display(Prompt = "Enter your current password")]
        [DataType(DataType.Password)]
        [StringLength(30, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters!")]
        [Required(ErrorMessage = "Please enter password.")]
        public string current_password { get; set; }

        [BindProperty]
        [Display(Prompt = "Enter your new password")]
        [DataType(DataType.Password)]
        [StringLength(30, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters!")]
        [Required(ErrorMessage = "Please enter password.")]
        public string new_password { get; set; }

        [BindProperty]
        [Display(Prompt = "Confirm your new password")]
        [DataType(DataType.Password)]
        [StringLength(30, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters!")]
        [Required(ErrorMessage = "Please enter password.")]
        [Compare("new_password", ErrorMessage = "New and confirm password doesn't match")]
        public string confirm_pass { get; set; }

        public void OnGet()
        {
            string json = HttpContext.Session.GetString("user");
            User user = JsonConvert.DeserializeObject<User>(json);
            user_id = user.UserId;
            img_profile = user.UserImage;
            user_phone = user.UserPhone.Trim();
            user_name = user.UserName;
            user_wallet = user.UserWallet;
        }


        [HttpPost]
        public async Task<IActionResult> OnPostUploadImageAsync()
        {
            var user_img = Request.Form.Files[0];
            var file_path = "";
            var rel_path = "";
            if (user_img != null)
            {
                try
                {
                    file_path = Path.Combine(webHostEnvironment.WebRootPath, "images/users", user_img.FileName);
                    using (var file_stream = new FileStream(file_path, FileMode.Create))
                    {
                        await user_img.CopyToAsync(file_stream);
                    }
                    rel_path = "/images/users/" + user_img.FileName;
                    User user = context.Users.First();
                    user.UserImage = rel_path;
                    context.SaveChanges();

                }
                catch (Exception exc)
                {
                    return new JsonResult("error");
                }
            }

            return new JsonResult(rel_path);
        }

        [HttpPost]
        public void OnPostUpdateProfileAsync()
        {
            User user = context.Users.FirstOrDefault(x => x.UserId == user_id);
            user.UserName = user_name;
            user.UserPhone = user_phone;
            context.SaveChanges();
        }

        [HttpPost]
        public async Task<IActionResult> OnPostChangePasswordAsync()
        {
            User user = context.Users.FirstOrDefault(x => x.UserId == user_id);
            if (user.UserPassword.Equals(current_password))
            {
                if (new_password.Equals(confirm_pass))
                {
                    user.UserPassword = new_password;
                    context.SaveChanges();
                    return new JsonResult(0);
                }
                else
                {
                    return new JsonResult(1);
                }
            }
            else
            {
                return new JsonResult(2);
            }

        }
    }
}
