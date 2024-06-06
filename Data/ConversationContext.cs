using Microsoft.EntityFrameworkCore;
using OpenAI_UIR.Models;

namespace OpenAI_UIR.Data
{
    public class ConversationContextDb : DbContext
    {
       

        public ConversationContextDb(DbContextOptions<ConversationContextDb> options) : base(options)
        {
        }

        public DbSet<Conversation> Conversation { get; set; }
        public DbSet<Question> Question { get; set; }
        public DbSet<Response> Response { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Question>()
                .HasOne(u => u.Response)
                .WithOne(up => up.Question)
                .HasForeignKey<Response>(up => up.Id_Question);

            modelBuilder.Entity<Question>()
                .HasOne(o => o.Conversation)
                .WithMany(c => c.Questions)
                .HasForeignKey(o => o.Id_Conversation);


            base.OnModelCreating(modelBuilder);
        }

    }
}
