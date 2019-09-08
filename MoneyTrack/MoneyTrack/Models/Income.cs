using SQLite;
using System;
using Xamarin.Forms;

namespace MoneyTrack.Models
{
    [Table("Income")]
    public class Income : BaseModel, IHistoryItem
    {
        public string Name { get; set; }
        public decimal Value { get; set; }
        public DateTime Date { get; set; }
        public string DisplayName { get; set; }
        [Ignore]
        public Color BackgroundColor { get; set; }
    }
}
