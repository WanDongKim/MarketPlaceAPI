using System;
using System.Collections.Generic;

namespace MarketPlaceServiceAPI.Models
{
    public partial class Discount
    {


        public int DiscountId { get; set; }
        public float? OfferAsPercent { get; set; }

    }
}
