using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThriftStore.Data.Domain;
using ThriftStore.Data.Domain.Items;
using ThriftStore.Data.Domain.Users;

namespace ThriftStore.Data
{
    public class StoreDbContext:IdentityDbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options)
          : base(options)
        {
        }

        #region Identity Models
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        #endregion

        #region Domain 
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemImage> ItemImages { get; set; }
        #endregion



       // Take out this data before running another migration
        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);
        //    builder.Entity<SubCategory>().HasData(
        //        // Electronics
        //        new SubCategory { SubCategoryName = "Audio Systems", CategoryId = 8, CreatedBy = "System", CreatedDate = DateTime.Now },
        //        new SubCategory { SubCategoryName = "Gaming Accessories & Consoles", CategoryId = 8, CreatedBy = "System", CreatedDate = DateTime.Now },
        //        new SubCategory { SubCategoryName = "Televisions", CategoryId = 8, CreatedBy = "System", CreatedDate = DateTime.Now },
        //        new SubCategory { SubCategoryName = "Refrigerators", CategoryId = 8, CreatedBy = "System", CreatedDate = DateTime.Now },
        //        new SubCategory { SubCategoryName = "Accessories", CategoryId = 8, CreatedBy = "System", CreatedDate = DateTime.Now },

        //        // Fashion
        //        new SubCategory { SubCategoryName = "Men's wear", CategoryId = 9, CreatedBy = "System", CreatedDate = DateTime.Now },
        //        new SubCategory { SubCategoryName = "Women's wear", CategoryId = 9, CreatedBy = "System", CreatedDate = DateTime.Now },
        //        new SubCategory { SubCategoryName = "Kid's wears", CategoryId = 9, CreatedBy = "System", CreatedDate = DateTime.Now },
        //        new SubCategory { SubCategoryName = "Accessories", CategoryId = 9, CreatedBy = "System", CreatedDate = DateTime.Now },
        //        new SubCategory {  SubCategoryName = "Men's Shoes", CategoryId = 9, CreatedBy = "System", CreatedDate = DateTime.Now },
        //        new SubCategory {  SubCategoryName = "Women's Shoes", CategoryId = 9, CreatedBy = "System", CreatedDate = DateTime.Now },

        //        // Computers & Accessories
        //        new SubCategory { SubCategoryName = "Desktop & Monitors", CategoryId = 10, CreatedBy = "System", CreatedDate = DateTime.Now },
        //        new SubCategory { SubCategoryName = "Laptops", CategoryId = 10, CreatedBy = "System", CreatedDate = DateTime.Now },
        //        new SubCategory { SubCategoryName = "Printers, Scanners & Accessories", CategoryId = 10, CreatedBy = "System", CreatedDate = DateTime.Now },
        //        new SubCategory { SubCategoryName = "Mifi's and Routers", CategoryId = 10, CreatedBy = "System", CreatedDate = DateTime.Now },

        //        // Phones & Tabs
        //        new SubCategory { SubCategoryName = "Mobile Phones", CategoryId = 11, CreatedBy = "System", CreatedDate = DateTime.Now },
        //        new SubCategory { SubCategoryName = "Tablets", CategoryId = 11, CreatedBy = "System", CreatedDate = DateTime.Now },
        //        new SubCategory { SubCategoryName = "Accessories", CategoryId = 11, CreatedBy = "System", CreatedDate = DateTime.Now },

        //        // Home & Kitchen
        //        new SubCategory { SubCategoryName = "Furnitures", CategoryId = 12, CreatedBy = "System", CreatedDate = DateTime.Now },
        //        new SubCategory { SubCategoryName = "Ovens & Cookers", CategoryId = 12, CreatedBy = "System", CreatedDate = DateTime.Now },
        //        new SubCategory { SubCategoryName = "Utensils", CategoryId = 12, CreatedBy = "System", CreatedDate = DateTime.Now });
        //}





    }
}
