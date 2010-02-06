using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kona.Model
{



    public class Customer
    {
        public Customer()
        {
            LanguageCode = System.Globalization
                .CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
            CustomerID = Guid.NewGuid();
        }
        public virtual Guid CustomerID { get; set; }
        public virtual string UserName { get; set; }
        public virtual string First { get; set; }
        public virtual string Last { get; set; }
        public virtual string Email { get; set; }
        public virtual string LanguageCode { get; set; }
    }
}
