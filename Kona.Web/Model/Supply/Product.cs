using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Iesi.Collections.Generic;
using Kona.Model.Supply.Inventory;
using NHibernate.Search.Attributes;

namespace Kona.Model {
    


    public enum DeliveryMethod
    {
        Shipped = 1,
        Download = 2
    }

    [Indexed]
    public class Product {
        [DocumentId]
        public virtual string SKU { get; set; }
        [Field]
        public virtual string Name { get; set; }
        public virtual decimal Price { get; set; }
        public virtual decimal DiscountPercent { get; set; }
        public virtual string Manufacturer { get; set; }
        public virtual DeliveryMethod Delivery { get; set; }
        public virtual decimal WeightInPounds { get; set; }
        public virtual bool IsTaxable { get; set; }
        public virtual bool AllowBackOrder { get; set; }
        public virtual bool AllowPreOrder { get; set; }
        public virtual string EstimatedDelivery { get; set; }
        public virtual string DefaultImage { get; set; }
        public virtual DateTime DateAvailable { get; set; }
        public virtual int AmountOnHand { get; set; }
        public virtual InventoryState Inventory { get; set; }
        public virtual decimal DiscountedPrice
        {
            get { return Price * (1.0M - DiscountPercent); } 
        }

        //attributes and how they map
        //physical index structure
        //related entities in index
        //building index for first time


        [NHibernate.Search.Attributes.IndexedEmbedded]
        public virtual ICollection<Descriptor> Descriptors { get; set; }
        public virtual ICollection<Image> Images { get; set; }
        public virtual ISet<Category> Categories { get; set; }
        
        #region Object overrides
        public override bool Equals(object obj) {
            if (obj is Product) {
                Product compareTo = (Product)obj;
                return compareTo.SKU == this.SKU;
            } else {
                return base.Equals(obj);
            }
        }

        public override string ToString() {
            return this.Name;
        }
        public override int GetHashCode() {
            return this.SKU.GetHashCode();
        }
        #endregion
    }
}
