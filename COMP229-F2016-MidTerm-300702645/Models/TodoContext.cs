namespace COMP229_F2016_MidTerm_300702645.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class TodoContext : DbContext
    {
        public TodoContext()
            : base("name=TodoConnection")
        {
        }

        public virtual DbSet<ToDoTable> ToDoTables { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToDoTable>()
                .Property(e => e.TodoDescription)
                .IsUnicode(false);

            modelBuilder.Entity<ToDoTable>()
                .Property(e => e.TodoNotes)
                .IsUnicode(false);
        }
    }
}
