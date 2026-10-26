using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Gushchina_UserRegistration.Model
{
    public partial class RestaurantDataEntities : DbContext
    {
        public RestaurantDataEntities()
            : base("name=RestaurantDataEntities")
        {
        }

        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Dish> Dishes { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Reservation> Reservations { get; set; }
        public virtual DbSet<UserLog> UserLogs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    }

    public partial class Client
    {
        public Client()
        {
            Orders = new HashSet<Order>();
            Reservations = new HashSet<Reservation>();
            UserLogs = new HashSet<UserLog>();
        }

        public byte ClientId { get; set; }
        public string FullName { get; set; }
        public long Phone { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public string Preferences { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
        public virtual ICollection<UserLog> UserLogs { get; set; }
    }

    public partial class Dish
    {
        public Dish()
        {
            Orders = new HashSet<Order>();
        }

        public byte DishId { get; set; }
        public string DishName { get; set; }
        public string Description { get; set; }
        public short Price { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }

    public partial class Order
    {
        public byte OrderId { get; set; }
        public byte ClientId { get; set; }
        public byte TableNumber { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; }
        public string PaymentStatus { get; set; }
        public byte? DishId { get; set; }

        public virtual Client Client { get; set; }
        public virtual Dish Dish { get; set; }
    }

    public partial class Reservation
    {
        public byte ReservationId { get; set; }
        public byte ClientId { get; set; }
        public byte TableNumber { get; set; }
        public DateTime ReservedAt { get; set; }
        public byte GuestCount { get; set; }
        public string Status { get; set; }

        public virtual Client Client { get; set; }
    }

    public partial class UserLog
    {
        public byte UserId { get; set; }
        public byte ClientId { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }

        public virtual Client Client { get; set; }
    }
}
