using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Markup;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using PRN221_Group3_Project_HotelManagement.Models;

namespace PRN221_Group3_Project_HotelManagement.Pages.Users
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
        public User User { get; set; } = default!;
        [BindProperty]
        public string selected { get; set; }


    
        [DataType(DataType.Upload)]
        //[FileExtensions(Extensions = "png,jpg,jpeg,gif")]
        [Display(Name = "Choose a file to upload")]
        [BindProperty]
        public IFormFile FileUpload { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }
            User = user;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (FileUpload != null)
            {

                var file = Path.Combine(_hostEnvironment.WebRootPath, "images", FileUpload.FileName);
                using (var fileStream = new FileStream(file, FileMode.Create))
                {
                    await FileUpload.CopyToAsync(fileStream);

                }
                User.UserImage = "/images/" + FileUpload.FileName;
            }

            short status = short.Parse(selected);
            User.UserStatus = status;
            
            _context.Attach(User).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(User.UserId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
