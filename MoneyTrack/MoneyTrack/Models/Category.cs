using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;

namespace MoneyTrack.Models
{
    [Table("Category")]
    public class Category :BaseModel
    {
        public string Name { get; set; }

        [OneToMany(CascadeOperations = CascadeOperation.All)]
        public List<Expense> Expenses { get; set; }
    }
}
