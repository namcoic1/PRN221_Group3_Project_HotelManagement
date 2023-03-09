using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PRN221_Group3_Project_HotelManagement.Models
{
    public partial class Booking_Hotel_DBContext : DbContext
    {
        public Booking_Hotel_DBContext()
        {
        }

        public Booking_Hotel_DBContext(DbContextOptions<Booking_Hotel_DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderDetail> OrderDetails { get; set; } = null!;
        public virtual DbSet<RoomHotel> RoomHotels { get; set; } = null!;
        public virtual DbSet<TypeRoom> TypeRooms { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
                var connectionString = configuration.GetConnectionString("BookingHotelDB");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.OrderCheckin)
                    .HasColumnType("datetime")
                    .HasColumnName("order_checkin");

                entity.Property(e => e.OrderCheckout)
                    .HasColumnType("datetime")
                    .HasColumnName("order_checkout");

                entity.Property(e => e.OrderCreated)
                    .HasColumnType("datetime")
                    .HasColumnName("order_created");

                entity.Property(e => e.OrderNote).HasColumnName("order_note");

                entity.Property(e => e.OrderStatus).HasColumnName("order_status");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Order_User");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasKey(e => e.DetailId);

                entity.ToTable("Order_Detail");

                entity.Property(e => e.DetailId).HasColumnName("detail_id");

                entity.Property(e => e.DetailCount).HasColumnName("detail_count");

                entity.Property(e => e.DetailPrice)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("detail_price");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.RoomId).HasColumnName("room_id");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_Order_Detail_Order");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.RoomId)
                    .HasConstraintName("FK_Order_Detail_Room_Hotel");
            });

            modelBuilder.Entity<RoomHotel>(entity =>
            {
                entity.HasKey(e => e.RoomId);

                entity.ToTable("Room_Hotel");

                entity.Property(e => e.RoomId).HasColumnName("room_id");

                entity.Property(e => e.RoomBed).HasColumnName("room_bed");

                entity.Property(e => e.RoomDesc).HasColumnName("room_desc");

                entity.Property(e => e.RoomImage).HasColumnName("room_image");

                entity.Property(e => e.RoomName)
                    .HasMaxLength(150)
                    .HasColumnName("room_name");

                entity.Property(e => e.RoomPrice)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("room_price");

                entity.Property(e => e.RoomStatus).HasColumnName("room_status");

                entity.Property(e => e.TypeId).HasColumnName("type_id");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.RoomHotels)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Room_Hotel_Type_Room");
            });

            modelBuilder.Entity<TypeRoom>(entity =>
            {
                entity.HasKey(e => e.TypeId);

                entity.ToTable("Type_Room");

                entity.Property(e => e.TypeId).HasColumnName("type_id");

                entity.Property(e => e.TypeDesc)
                    .HasMaxLength(250)
                    .HasColumnName("type_desc");

                entity.Property(e => e.TypeImage).HasColumnName("type_image");

                entity.Property(e => e.TypeName)
                    .HasMaxLength(150)
                    .HasColumnName("type_name");

                entity.Property(e => e.TypeStatus).HasColumnName("type_status");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.UserImage).HasColumnName("user_image");

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .HasColumnName("user_name");

                entity.Property(e => e.UserPassword)
                    .HasMaxLength(10)
                    .HasColumnName("user_password");

                entity.Property(e => e.UserPhone)
                    .HasMaxLength(20)
                    .HasColumnName("user_phone")
                    .IsFixedLength();

                entity.Property(e => e.UserStatus).HasColumnName("user_status");

                entity.Property(e => e.UserWallet)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("user_wallet");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
