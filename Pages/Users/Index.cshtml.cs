using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_Group3_Project_HotelManagement.Models;

namespace PRN221_Group3_Project_HotelManagement.Pages.Users
{
    public class IndexModel : PageModel
    {
        private readonly PRN221_Group3_Project_HotelManagement.Models.Booking_Hotel_DBContext _context;

        public IndexModel(PRN221_Group3_Project_HotelManagement.Models.Booking_Hotel_DBContext context)
        {
            _context = context;
        }

        public IList<User> User { get;set; } = default!;


        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public async Task OnGetAsync()
        {
            IQueryable<User> userIQ = from s in _context.Users
                                            select s;
            if (!String.IsNullOrEmpty(SearchString))
            {
                userIQ = userIQ.Where(userIQ => userIQ.UserName.Contains(SearchString));
            }
            User = userIQ.ToList();
        }
    }
}
