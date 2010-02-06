using System;
using Kona.Model;
namespace Kona.Services
{
    public interface ICustomerService
    {
        Customer GetCurrentCustomer();
        void TrackProductView(Product product);
    }
}
