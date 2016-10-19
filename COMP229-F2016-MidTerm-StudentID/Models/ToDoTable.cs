namespace COMP229_F2016_MidTerm_300702645.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ToDoTable")]
    public partial class ToDoTable
    {
        [Key]
        public int TodoId { get; set; }

        [StringLength(255)]
        public string TodoDescription { get; set; }

        [StringLength(255)]
        public string TodoNotes { get; set; }

        public bool? Completed { get; set; }
    }
}
