using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate.Dialect;
using NHibernate.Dialect.Function;
using NHibernate;

namespace Kona.Infrastructure {
    public class KonaDialect:MsSql2008Dialect {

        public KonaDialect() {
            RegisterFunction("SearchProducts",
                new SQLFunctionTemplate(NHibernateUtil.Class,
                    "(select sku from dbo.searchproducts(?1))"));

            RegisterFunction("contains",
              new SQLFunctionTemplate(NHibernateUtil.Boolean, "CONTAINS(?1,?2)"));

        }

    }
}
