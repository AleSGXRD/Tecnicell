﻿using Tecnicell.Server.Models.Entity;

namespace Tecnicell.Server.Models.ViewModel.Screen
{
    public class ScreenHistoryViewModel
    {
        public string ScreenCode { get; set; } = null!;

        public string UserCode { get; set; } = null!;

        public DateTime Date { get; set; }

        public string? ActionHistory { get; set; }

        public string? ToBranch { get; set; }

        public string? Description { get; set; }

        public int? Quantity { get; set; }

        public string? SaleCode { get; set; }

        public string? SupplierCode { get; set; }

        public virtual SupplierViewModel? SupplierNavigation {  get; set; }

        public virtual SaleViewModel? SaleCodeNavigation { get; set; }

        public virtual BranchViewModel? ToBranchNavigation { get; set; }

        public virtual UserInfoViewModel? UserCodeNavigation { get; set; } = null!;
    }
}
