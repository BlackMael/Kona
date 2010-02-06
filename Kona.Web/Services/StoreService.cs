using System;
using System.Linq;
using Kona.Helpers;
using Kona.Model;
using Kona.ViewModels;
using NHibernate;
using NHibernate.Linq;
using NHibernate.Criterion;
using NHibernate.LambdaExtensions;
using NHibernate.Transform;
using Kona.Services;

namespace Kona.App.Services {
    public interface IStoreService{
        ProductListViewModel GetHomeModel();
        DetailsViewModel GetDetails(string sku);
        ProductListViewModel Search(string query);
    }

    public class StoreService : IStoreService{

        ISession _session;
        ICustomerService _customerService;
        public StoreService(ISession session, 
            ICustomerService customerService) {
            _session = session;
            _customerService = customerService;
        }

        public ProductListViewModel Search(string query) {
            var model = new ProductListViewModel();
            model.FeaturedProducts = NHibernate.Search.Search.CreateFullTextSession(_session)
                .CreateFullTextQuery<Product>("Name:Boots").List<Product>();
            return model;          

        }

//        IStoreRepository _repo;
//        public StoreService(IStoreRepository repo){
//            _repo = repo;
//        }

//        public ProductListViewModel GetCategoryModel(int categoryID) {
//            var result = new ProductListViewModel();

//            //add the featured product
//            result.FeaturedProducts = _repo.GetProducts(categoryID);

//            //categories
//            result.Categories = _repo.GetCategories();
//            result.SelectedCategory = _repo.GetCategories().SingleOrDefault(x => x.ID == categoryID);

//            //organize them
//            result.Categories.ToList().ForEach(x => x.SubCategories = result.Categories.Where(y => y.ParentID == x.ID).ToList());


//            return result;


//        }
        public ProductListViewModel GetHomeModel() {

            var result = new ProductListViewModel();
            
            //categories
            result.Categories = _session
                .CreateCriteria<Category>()
                .Future<Category>();

            
            var featuredProduct = _session
                .CreateCriteria<Product>()
                .SetFetchMode<Product>(x=>x.Descriptors,FetchMode.Join)
                .CreateCriteria<Product>(x=>x.Categories)
                .Add<Category>(x=>x.ID==33) 
                .SetResultTransformer(Transformers.DistinctRootEntity)
                .Future<Product>();

            result.FeaturedProduct = featuredProduct.First();
            result.FeaturedProducts = featuredProduct
                .Skip(1)
                .Take(3)
                .ToList();
            
            return result;

        }

        public DetailsViewModel GetDetails(string sku)
        {
            var result = new DetailsViewModel();
            var customer = _customerService.GetCurrentCustomer();

            //categories
            result.Categories = _session
                .CreateCriteria<Category>()
                .Future<Category>();
            

            //selected product
            result.SelectedProduct = _session.Get<Product>(sku);
            
            //get all the orders 
            var orderIDsContainingCurrentSku=DetachedCriteria.For<OrderItem>()
                        .Add<OrderItem>(x=>x.Product.SKU==sku)
                        .SetProjection(Projections.Property("Order.id"));

            var skusOfProductsAppearingInOrdersContainingCurrentSku = 
                DetachedCriteria.For<OrderItem>()
                .SetProjection(Projections.GroupProperty("Product.id"))
                .AddOrder(NHibernate.Criterion.Order.Desc(Projections.Count("Order.id")))
                .Add<OrderItem>(x=>x.Product.SKU!=sku)
                .Add(Subqueries.PropertyIn("Order.id", orderIDsContainingCurrentSku))
                .SetMaxResults(15);
                
            result.Recommended = _session.CreateCriteria<Product>()
                .SetFetchMode<Product>(x => x.Descriptors, FetchMode.Join)
                .Add(Subqueries.PropertyIn("id", skusOfProductsAppearingInOrdersContainingCurrentSku))
                .SetResultTransformer(Transformers.DistinctRootEntity)
                .List<Product>();


            //add favorites and recents
            result.Recent = _session.GetNamedQuery("RecentProductsByCustomer")
                .SetGuid("ID", customer.CustomerID).List<Product>();

            result.Favorite = _session.GetNamedQuery("FavoriteCategoriesByCustomer")
                .SetGuid("ID", customer.CustomerID).List<Category>();


            return result;

        }
    }
}
