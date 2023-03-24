using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using PRN221_Group3_Project_HotelManagement.Models;
using System.ComponentModel.DataAnnotations;

namespace PRN221_Group3_Project_HotelManagement.Pages.Booking
{
    public class CheckoutModel : PageModel
    {
        private readonly Booking_Hotel_DBContext _context;

        [DataType(DataType.Date)]
        [BindProperty(SupportsGet = true)] 
        public DateTime Checkin { get; set; } = DateTime.Now;
        [DataType(DataType.Date)]
        [BindProperty(SupportsGet = true)]
        public DateTime Checkout { get; set; } = DateTime.Now;
        public RoomHotel RoomHotel { get; set; }
        public List<String> TimeLine { get; set; } = new List<String>();

        public CheckoutModel(Booking_Hotel_DBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync(int? rid)
        {
            if (HttpContext.Session.GetString("user") == null)
            {
                return RedirectToPage("/AccessPage/Login");
            }

            User user = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("user"));

            if (rid == null || _context.RoomHotels == null)
            {
                return NotFound();
            }

            var roomhotel = _context.RoomHotels.FirstOrDefault(m => m.RoomId == rid);

            if (roomhotel == null) return NotFound();
            else RoomHotel = roomhotel;

            var orderRoom = _context.Orders
                .Join(_context.OrderDetails, o => o.OrderId, od => od.OrderId, (o, od) => new
                {
                    RoomId = od.RoomId,
                    Check_in = o.OrderCheckin,
                    Check_out = o.OrderCheckout,
                    Status = o.OrderStatus,
                }).Where(od => od.RoomId == rid && od.Check_out.Value.Date >= DateTime.Now.Date && od.Status == 1)
                .ToList();

            if (orderRoom != null) 
                foreach (var item in orderRoom)
                    TimeLine.Add(item.Check_in.Value.ToString("dd/MM/yyyy") + "-" + item.Check_out.Value.ToString("dd/MM/yyyy"));

            ViewData["User"] = user;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? rid)
        {
            var roomhotel = _context.RoomHotels.FirstOrDefault(m => m.RoomId == rid);
            if (roomhotel == null) return NotFound();
            else RoomHotel = roomhotel;

            var orderRoom = _context.Orders
                .Join(_context.OrderDetails, o => o.OrderId, od => od.OrderId, (o, od) => new
                {
                    RoomId = od.RoomId,
                    Check_in = o.OrderCheckin,
                    Check_out = o.OrderCheckout,
                    Status = o.OrderStatus,
                }).Where(od => od.RoomId == rid && od.Check_out.Value.Date >= DateTime.Now.Date && od.Status == 1)
                .ToList();

            if (orderRoom != null)
                foreach (var item in orderRoom)
                    TimeLine.Add(item.Check_in.Value.ToString("dd/MM/yyyy") + "-" + item.Check_out.Value.ToString("dd/MM/yyyy"));

            User user = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("user"));
            ViewData["User"] = user;

            DateTime now = DateTime.Now.Date;
            var note = Request.Form["note"];
            var totalPrice = Request.Form["totalPrice"].ToString().Substring(1);

            //check date
            if (Checkin < now)
            {
                ViewData["Error"] = "Check-in cannot be a past day.";
                return Page();
            }
            if(Checkout < now)
            {
                ViewData["Error"] = "Check-out cannot be a past day.";
                return Page();
            }
            if (Checkin > Checkout)
            {
                ViewData["Error"] = "Check-in must be before the day of Check-out.";
                return Page();
            }

            foreach (var item in orderRoom)
            {
                if (item.Status == 1)
                {
                    DateTime checkInOrder = item.Check_in.Value.Date;
                    DateTime checkOutOrder = item.Check_out.Value.Date;

                    if (checkInOrder <= Checkin && Checkin <= checkOutOrder)
                    {
                        ViewData["Error"] = "On the day of check-in, this room is already in use.";
                        return Page();
                    }
                    if (checkInOrder <= Checkout && Checkout <= checkOutOrder)
                    {
                        ViewData["Error"] = "On the day of check-out, this room is already in use.";
                        return Page();
                    }
                    if (Checkin <= checkInOrder && checkInOrder <= Checkout)
                    {
                        ViewData["Error"] = "From check-in to check-out, the room is already in use.";
                        return Page();
                    }
                }
            }

            if(user.UserWallet < decimal.Parse(totalPrice))
            {
                ViewData["Error"] = "Not enough money.";
                return Page();
            }

            _context.Orders.Add(new Order
            {
                OrderNote = note,
                OrderCheckin = Checkin,
                OrderCheckout = Checkout,
                OrderCreated = DateTime.Now,
                OrderStatus = 1,
                UserId = user.UserId
            });
            _context.SaveChanges();

            Order last_order = _context.Orders.OrderBy(o => o.OrderId).Last();
            _context.OrderDetails.Add(new OrderDetail
            {
                OrderId = last_order.OrderId,
                DetailCount = 1,
                DetailPrice = decimal.Parse(totalPrice),
                RoomId = RoomHotel.RoomId,
            });

            user.UserWallet = user.UserWallet - decimal.Parse(totalPrice);
            _context.SaveChanges();

            ViewData["Success"] = "Successfully";
            return Page();
        }
    }
}
