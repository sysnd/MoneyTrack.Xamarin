using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyTrack.Models
{
    public interface IHistoryItem
    {
        DateTime Date { get; set; }
    }
}
