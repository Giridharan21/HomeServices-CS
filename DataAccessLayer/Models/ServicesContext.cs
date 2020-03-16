namespace DataAccessLayer
{
    //using ServiceStack.DataAnnotations;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using System.Data.Entity;
    using System.Linq;

    public class ServicesContext : DbContext
    {
        // Your context has been configured to use a 'ServicesContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'DataAccessLayer.ServicesContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'ServicesContext' 
        // connection string in the application configuration file.
        public ServicesContext()
            : base("ServicesContext") {
        }
        protected override void OnModelCreating(DbModelBuilder model) {
            model.Entity<Payment>().Property(g => g.Amount).HasPrecision(10, 2);
            model.Entity<Review>().Property(g => g.Stars).HasPrecision(2, 1);
            model.Entity<BankAccountDetails>().Property(g => g.Balance).HasPrecision(10, 2);
            model.Entity<ServiceProviderDetails>().Property(g => g.Charge).HasPrecision(7, 2);
        }
       


        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

         public virtual DbSet<User>Users  { get; set; }
         public virtual DbSet<CustomerDetails>Customers  { get; set; }
         public virtual DbSet<ServiceProviderDetails>ServiceProviders  { get; set; }
         public virtual DbSet<Order> Orders  { get; set; }
         public virtual DbSet<Review> Reviews  { get; set; }
         public virtual DbSet<Payment> Payments  { get; set; }
         public virtual DbSet<BankAccountDetails> BankAccounts  { get; set; }
         public virtual DbSet<Services> Services  { get; set; }
         public virtual DbSet<ServicesAssigned> ServicesAssigned { get; set; }
    }

    public class BankAccountDetails
    {
        [Key]
        public int Id { get; set; }
        public string BankName { get; set; }
        [Index(IsUnique =true)]
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
    }
    public class Services
    {
        public int Id { get; set; }
        [Required]
        [Index(IsUnique =true)]
        public string Service { get; set; }
    }
    public class ServicesAssigned
    {
        public int Id { get; set; }
        [ForeignKey("SP")]
        [Required]
        public int ServiceProviderFK { get; set; }
        [Required]
        [ForeignKey("Service")]
        public int ServicesFK { get; set; }

        public ServiceProviderDetails SP { get; set; }
        public Services Service { get; set; }
    }
    public class User
    {
        [Key]
        public int Id { get; set; }
        [StringLength(16, MinimumLength = 4)]
        [Required]
        [Index(IsUnique = true)]
        public string Username { get; set; }
        [Required]
        [StringLength(16, MinimumLength = 8)]
        public string Password { get; set; }
        [Required]
        [StringLength(16, MinimumLength = 4)]
        public string Type { get; set; }

    }
    public class CustomerDetails
    {
        public int Id { get; set; }
        [ForeignKey("User")]
        [Required]
        public int UserId { get; set; }
        [StringLength(10, MinimumLength = 10)]
        [Index(IsUnique =true)]
        public string Contact { get; set; }
        [StringLength(16, MinimumLength = 4)]
        public string Location { get; set; }
        [ForeignKey("BankAccount")]
        [Index(IsUnique =true)]
        public int BankFK { get; set; }

        public BankAccountDetails BankAccount { get; set; }
        public User User { get; set; }
    }
    public class ServiceProviderDetails
    {
        public int Id { get; set; }
        [ForeignKey("User")]
        [Required]
        public int UserId { get; set; }
        [Required]
        [StringLength(10, MinimumLength = 10)]
        [Index(IsUnique = true)]
        public string Contact { get; set; }
        [ForeignKey("BankAccount")]
        [Index(IsUnique = true)]
        public int BankFK { get; set; }
        public decimal Charge { get; set; }

        public BankAccountDetails BankAccount { get; set; }
        public User User { get; set; }
    }
    public class Order
    {
        public int Id { get; set; }
        [ForeignKey("UserFrom")]
        public int FromFK { get; set; }
        [ForeignKey("UserTo")]
        public int ToFK { get; set; }
        [Required]
        [StringLength(10,MinimumLength =4)]
        public string Status { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public DateTime ScheduleDate { get; set; }

        public User UserFrom { get; set; } 
        public User UserTo { get; set; } 
    }

    public class Payment
    {
        public int Id { get; set; }
        [ForeignKey("Orders")]
        public int OrderIdFK { get; set; }
        //[Column(TypeName ="decimal(10,2)")]
        public decimal Amount { get; set; }
        [Required]
        [StringLength(10,MinimumLength =4)]
        public string Status { get; set; }
        [Required]
        public DateTime Date { get; set; }

        public Order Orders { get; set; }
    }

    public class Review
    {
        public int Id { get; set; }
        [ForeignKey("Orders")]
        public int OrderIdFK { get; set; }
        //[Column(TypeName ="decimal(2,1)")]
        public decimal? Stars { get; set; }
        [StringLength(256)]
        public string Comment { get; set; }

        public Order Orders { get; set; }
    }
}