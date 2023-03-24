using System.ComponentModel.DataAnnotations;

namespace PRN221_Group3_Project_HotelManagement.Models
{
    public partial class RoomHotel
    {
        public RoomHotel()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int RoomId { get; set; }

        [Display(Prompt = "Room name")]
        [DataType(DataType.Text)]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Room name must be at least 3 characters!")]
        [Required(ErrorMessage = "Please enter room name.")]
        public string? RoomName { get; set; }
        public string? RoomImage { get; set; }

        [Display(Prompt = "Room bed")]
        [Required(ErrorMessage = "Please enter number bed.")]
        public short? RoomBed { get; set; }

        [Display(Prompt = "Room description")]
        [DataType(DataType.Text)]
        [StringLength(9999, MinimumLength = 25, ErrorMessage = "Room description must be at least 25 characters!")]
        [Required(ErrorMessage = "Please enter room description.")]
        public string? RoomDesc { get; set; }

        [Display(Prompt = "Room price")]
        [Required(ErrorMessage = "Please enter room price.")]
        public decimal? RoomPrice { get; set; }
        public int? RoomStatus { get; set; }
        public int? TypeId { get; set; }

        public virtual TypeRoom? Type { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
