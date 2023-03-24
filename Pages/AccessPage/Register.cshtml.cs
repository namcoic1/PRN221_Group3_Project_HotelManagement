using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PRN221_Group3_Project_HotelManagement.Models;
using System.ComponentModel.DataAnnotations;

namespace PRN221_Group3_Project_HotelManagement.Pages.AccessPage
{
    public class RegisterModel : PageModel
    {
        private Booking_Hotel_DBContext context = new Booking_Hotel_DBContext();

        [BindProperty]
        public User UserAccount { get; set; }

        [BindProperty]
        [Display(Prompt = "Your confirm password")]
        [DataType(DataType.Password)]
        [StringLength(30, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters!")]
        [Required(ErrorMessage = "Please enter confirm password.")]
        public string CfPassword { get; set; }

        public string Message { get; set; }

        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            User _u = context.Users.FirstOrDefault(u => u.UserPhone == UserAccount.UserPhone);
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (_u != null || UserAccount.UserPassword != CfPassword)
            {
                Message = "Failure";
            }
            else
            {
                _u = new User()
                {
                    UserPhone = UserAccount.UserPhone,
                    UserPassword = UserAccount.UserPassword,
                    UserName = UserAccount.UserName,
                    UserWallet = 9999,
                    UserStatus = 1
                };
                context.Users.Add(_u);
                await context.SaveChangesAsync();
                Message = "Success";
            }
            return Page();
        }
    }
}
