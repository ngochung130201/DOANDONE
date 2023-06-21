using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Graph.Models.ExternalConnectors;
using MySqlConnector;
using Share.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.Data
{
    public class DoAnBanHangContext : DbContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectString;
        public DoAnBanHangContext(DbContextOptions<DoAnBanHangContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
            _connectString = _configuration.GetConnectionString("WebApiDoAn");
        }

        public DbSet<About> Abouts { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
   
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails {get;set;}
        public DbSet<Product> Products { get;set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostCategory> PostCategories { get; set; }
        public DbSet<PostComment> PostComments { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Slider>   Sliders { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<ProductComment> ProductComments { get; set; }
     
        public DbSet<UserDentity> UserDentity { get; set; }
        public IDbConnection CreateConnection() => new MySqlConnection(_connectString);
    }
}
