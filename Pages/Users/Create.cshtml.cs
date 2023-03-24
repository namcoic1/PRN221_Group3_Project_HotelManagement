using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;
using PRN221_Group3_Project_HotelManagement.Models;

namespace PRN221_Group3_Project_HotelManagement.Pages.Users
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

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public User User { get; set; }
        [BindProperty]
        public string selected { get; set; }

        [Required(ErrorMessage = "Must choose at least one file")]
        [DataType(DataType.Upload)]
        //[FileExtensions(Extensions = "png,jpg,jpeg,gif")]
        [Display(Name = "Choose a file to upload")]
        [BindProperty]
        public IFormFile FileUpload { get; set; }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }
            if (FileUpload != null)
            {

                var file = Path.Combine(_hostEnvironment.WebRootPath, "images/users", FileUpload.FileName);
                using (var fileStream = new FileStream(file, FileMode.Create))
                {
                    await FileUpload.CopyToAsync(fileStream);

                }
                User.UserImage = "/images/users/" + FileUpload.FileName;
            }

            short status = short.Parse(selected);
            User.UserStatus = status;
            _context.Users.Add(User);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
