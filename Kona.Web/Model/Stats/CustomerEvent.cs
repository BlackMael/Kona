using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kona.Model
{
    public enum CustomerBehavior
    {
        
        LoggingIn,
        LoggingOut,
        AddItemToBasket,
        RemoveItemFromBasket,
        CheckoutBilling,
        CheckoutShipping,
        CheckoutFinalize,
        ViewOrder,
        ViewBasket,
        ViewCategory,
        ViewProduct,
        BoughtProduct
    }
    
    
    public class CustomerEvent
    {

        public virtual Customer Customer { get; set; }
        public virtual string IP { get; set; }
        public virtual Product Product { get; set; }
        public virtual Category Category { get; set; }
        public virtual Order Order { get; set; }
        public virtual DateTime EventDate { get; set; }
        public virtual CustomerBehavior Behavior { get; set; }
    }
}
