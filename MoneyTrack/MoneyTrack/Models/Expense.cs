using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using Xamarin.Forms;

namespace MoneyTrack.Models
{
    [Table("Expense")]
    public class Expense : BaseModel, IHistoryItem
    {
        public string Name { get; set; }
        [ForeignKey(typeof(Category))]
        public int CategoryId { get; set; }
        [ManyToOne]
        public Category Category { get; set; }
        public DateTime Date { get; set; }
        public decimal Value { get; set; }
        public string DisplayName { get; set; }
        [Ignore]
        public string CategoryName { get; set; }
        [Ignore]
        public Color BackgroundColor { get; set; }
    }
}
