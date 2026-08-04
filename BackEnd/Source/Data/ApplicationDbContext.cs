using Microsoft.EntityFrameworkCore;
using Source.Data;
using Source.Models;
namespace Source.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Users> Users { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<Message> Messages{get;set;}
        public DbSet<MembersConversation> MembersConversations{get;set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // members conversation -> conversation 
            modelBuilder.Entity<MembersConversation>().HasOne(mc=> mc.Conversation).
            WithMany(c=>c.MembersConversations).HasForeignKey(mc=>mc.ConversationId);
            // members conversation -> user
            modelBuilder.Entity<MembersConversation>().HasOne(mc=>mc.User).
            WithMany(u=>u.MembersConversations).HasForeignKey(mc=>mc.UserId);
            // message -> conversation 
            modelBuilder.Entity<Message>().
            HasOne(m=>m.Conversation).
            WithMany(c=>c.Messages).
            HasForeignKey(m=>m.ConversationId);
            // message -> user
            modelBuilder.Entity<Message>().HasOne(m=>m.Sender).
            WithMany(u=>u.Messages).HasForeignKey(m=>m.SenderId);


        }
    }
}