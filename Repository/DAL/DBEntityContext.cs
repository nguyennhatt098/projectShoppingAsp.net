using Model;
using System.Data.Entity;

namespace Repository.DAL
{
    public class DBEntityContext : DbContext
    {
        public DBEntityContext() : base("name=defaultConnection")
        {
            Database.SetInitializer<DBEntityContext>(new DbInitializer());
            this.Configuration.LazyLoadingEnabled = true;
		}
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<FeedBack> FeedBacks { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Model.Action> Actions { get; set; }
        public DbSet<RoleAction> RoleActions { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<About> Abouts { get; set; }
        public DbSet<Footer> Footers { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<WishList> wishLists { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Notify> Notifies { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<ReviewProduct> ReviewProducts { get; set; }
        public DbSet<AnswerComment> Answers { get; set; }
		public DbSet<AnswerReview> AnswerReviews { get; set; }
	}
}
