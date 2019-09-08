using System;

using MoneyTrack.Models;

namespace MoneyTrack.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel<Item>
    {
        public Item Item { get; set; }
        public ItemDetailViewModel(Item item = null)
        {
            Title = item?.Text;
            Item = item;
        }
    }
}
