﻿namespace MoneyTrack.Models
{
    public enum MenuItemType
    {      
        Categories,
        Incomes,
        Expenses,
        History
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
