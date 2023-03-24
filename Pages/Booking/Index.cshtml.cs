using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PRN221_Group3_Project_HotelManagement.Models;

namespace PRN221_Group3_Project_HotelManagement.Pages.Booking
{
    public class IndexModel : PageModel
    {
        private readonly Booking_Hotel_DBContext _context;
        public PaginatedList<RoomHotel> rooms { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? SelectBed { get; set; } = 0;

        [BindProperty(SupportsGet = true)]
        public int? PriceRange { get; set; } = 0;

        [BindProperty(SupportsGet = true)]
        public string? SortBy { get; set; }

        [BindProperty(SupportsGet = true)]
        public int TypeId { get; set; } = 0;

        [BindProperty(SupportsGet = true)]
        public int PageSize { get; set; } = 6;
        public decimal? MaxPrice { get; set; }
        public int PageIndex { get; set; } = 1;
        public int TotalPage { get; set; }
        public int TotalRoom { get; set; }
        public IndexModel(Booking_Hotel_DBContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync(int? pageIndex)
        {
            MaxPrice = _context.RoomHotels.OrderByDescending(room => room.RoomPrice).First().RoomPrice;

            IQueryable<RoomHotel> roomsIQ = from s in _context.RoomHotels
                                            select s;

            if (TypeId != 0)
            {
                roomsIQ = roomsIQ.Where(room => (room.TypeId == TypeId));
            }

            if (!String.IsNullOrEmpty(SearchString))
            {
                roomsIQ = roomsIQ.Where(room => room.RoomName.Contains(SearchString)
                                || room.RoomDesc.Contains(SearchString) || room.RoomPrice.ToString().Contains(SearchString)
                                || room.RoomBed.ToString().Contains(SearchString));
            }

            if (SelectBed != 0)
            {
                roomsIQ = roomsIQ.Where(room => (room.RoomBed == SelectBed));
            }

            if (PriceRange != 0)
            {
                roomsIQ = roomsIQ.Where(room => (room.RoomPrice <= PriceRange));
            }

            if (!String.IsNullOrEmpty(SortBy))
            {
                if (SortBy.Equals("NameAsc"))
                {
                    roomsIQ = roomsIQ.OrderBy(room => room.RoomName);
                }
                else if (SortBy.Equals("NameDesc"))
                {
                    roomsIQ = roomsIQ.OrderByDescending(room => room.RoomName);
                }
                else if (SortBy.Equals("BedAsc"))
                {
                    roomsIQ = roomsIQ.OrderBy(room => room.RoomBed);
                }
                else if (SortBy.Equals("BedDesc"))
                {
                    roomsIQ = roomsIQ.OrderByDescending(room => room.RoomBed);
                }
                else if (SortBy.Equals("PriceAsc"))
                {
                    roomsIQ = roomsIQ.OrderBy(room => room.RoomPrice);
                }
                else//PriceDesc
                {
                    roomsIQ = roomsIQ.OrderByDescending(room => room.RoomPrice);
                }
            }

            roomsIQ = roomsIQ.Where(room => room.RoomStatus == 1);

            TotalPage = (int)Math.Ceiling(roomsIQ.Count() / (double)PageSize); ;
            TotalRoom = roomsIQ.Count();
            PageIndex = pageIndex ?? 1;

            rooms = await PaginatedList<RoomHotel>.CreateAsync(
                roomsIQ.AsNoTracking(), PageIndex, PageSize);
        }
    }
}
