using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Kona.Model;
using NHibernate;

namespace Kona.Services
{
    public class CustomerService : Kona.Services.ICustomerService
    {
        HttpContextBase _ctx;
        ISession _session;
        public CustomerService(HttpContextBase ctx, ISession session){
            _ctx=ctx;
            _session = session;
        }
        public Customer GetCurrentCustomer()
        {
            
            //is the user logged in?
            if (_ctx.User.Identity.IsAuthenticated)
            {
                return _session.Load<Customer>(new Guid(_ctx.User.Identity.Name));
            }

            //do they have cookie?
            if (_ctx.Request.Cookies["shopper"] != null)
            {

                return _session.Load<Customer>(new Guid(_ctx.Request.Cookies["shopper"].Value));

            }

            //make a new Customer and set a cookie
            var newCustomer = new Customer();
            _session.Save(newCustomer);

            //issue cookie
            _ctx.Response.Cookies["shopper"].Value = newCustomer.CustomerID.ToString();
            _ctx.Response.Cookies["shopper"].HttpOnly = true;

            return newCustomer;

        }

        public void TrackProductView(Product product)
        {
            
            CustomerEvent ev = new CustomerEvent();
            ev.Customer = GetCurrentCustomer();
            ev.Product = product;
            ev.EventDate = DateTime.Now;
            ev.IP = _ctx.Request.UserHostAddress;
            ev.Behavior = CustomerBehavior.ViewProduct;
            ev.Category = product.Categories.FirstOrDefault();
            _session.Save(ev);
        
        }

    }
}
