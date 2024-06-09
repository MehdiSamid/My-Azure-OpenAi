using Microsoft.EntityFrameworkCore;
using OpenAI_UIR.Models;

namespace OpenAI_UIR.Data
{
    public class ConversationContextDb : DbContext
    {


        //public ConversationContextDb(DbContextOptions<ConversationContextDb> options) : base(options)
        //{
        //}

        //public DbSet<Conversation> Conversation { get; set; }
        //public DbSet<Question> Question { get; set; }
        //public DbSet<Response> Response { get; set; }
        //public DbSet<User> Users { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    //    modelBuilder.Entity<Question>()
        //    //        .HasOne(u => u.Response)
        //    //        .WithOne(up => up.Question)
        //    //        .HasForeignKey<Response>(up => up.Id_Question);

        //    //    modelBuilder.Entity<Question>()
        //    //        .HasOne(o => o.Conversation)
        //    //        .WithMany(c => c.Questions)
        //    //        .HasForeignKey(o => o.Id_Conversation);

        //    modelBuilder.Entity<Conversation>()
        //     .HasOne(c => c.User)
        //     .WithMany(u => u.Conversations)
        //     .HasForeignKey(c => c.UserId)
        //     .OnDelete(DeleteBehavior.Cascade);

        //    base.OnModelCreating(modelBuilder);

        //    base.OnModelCreating(modelBuilder);
        //}

       

        public ConversationContextDb(DbContextOptions<ConversationContextDb> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Response> Responses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Conversation>()
                .HasOne(c => c.User)
                .WithMany(u => u.Conversations)
                .HasForeignKey(c => c.UserId);

            modelBuilder.Entity<Question>()
                .HasOne(q => q.Conversation)
                .WithMany(c => c.Questions)
                .HasForeignKey(q => q.ConversationId);

            modelBuilder.Entity<Response>()
                .HasOne(r => r.Question)
                .WithMany(q => q.Responses)
                .HasForeignKey(r => r.QuestionId);

            base.OnModelCreating(modelBuilder);
        }

    }
}
