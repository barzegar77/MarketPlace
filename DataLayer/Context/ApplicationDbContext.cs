using DataLayer.Models.Identity;
using DataLayer.Models.Store;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<UserInfo> UserInfos { get; set; }

		#region store

		public DbSet<Seller> Sellers { get; set; }

		#endregion
	}
}
