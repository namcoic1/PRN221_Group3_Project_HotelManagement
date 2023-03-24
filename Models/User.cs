using System.ComponentModel.DataAnnotations;

namespace PRN221_Group3_Project_HotelManagement.Models
{
    public partial class User
    {
        public User()
        {
            Orders = new HashSet<Order>();
        }

        public int UserId { get; set; }

        [Display(Prompt = "Your phone number")]
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Please enter phone number.")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4,5})$", ErrorMessage = "Phone number must include 10 or 11 digits!")]
        public string? UserPhone { get; set; }

        [Display(Prompt = "Your password")]
        [DataType(DataType.Password)]
        [StringLength(30, MinimumLength = 8, ErrorMessage = "Password must be at least 8 characters!")]
        [Required(ErrorMessage = "Please enter password.")]
        public string? UserPassword { get; set; }

        [Display(Prompt = "Your name")]
        [DataType(DataType.Text)]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Name must be at least 2 characters!")]
        [Required(ErrorMessage = "Please enter name.")]
        public string? UserName { get; set; }
        public string? UserImage { get; set; }
        public decimal? UserWallet { get; set; }
        public short? UserStatus { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
